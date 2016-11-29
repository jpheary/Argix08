using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Argix {
    //
    public class SortableBindingList<T>:BindingList<T> {
        //Members
        private List<T> originalList;               //Reference to the list provided at the time of instantiation
        private ListSortDirection sortDirection;
        private PropertyDescriptor sortProperty;
        
        //Function that refereshes the contents of the base classes collection of elements
        Action<SortableBindingList<T>, List<T>>populateBaseList = (a,b) => a.resetItems(b);    

        //A cache of functions that perform the sorting for a given type, property, and sort direction
        static Dictionary<string,Func<List<T>,IEnumerable<T>>>  cachedOrderByExpressions = new Dictionary<string,Func<List<T>, IEnumerable<T>>>();

        public SortableBindingList() {
            //Constructor
            originalList = new List<T>();
        }
        public SortableBindingList(IEnumerable<T> enumerable) {
            //Constructor
            originalList = enumerable.ToList();
            populateBaseList(this,originalList);
        }
        public SortableBindingList(List<T> list) {
            //Constructor
            originalList = list;
            populateBaseList(this,originalList);
        }
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction) {
            //Look for an appropriate sort method in the cache if not found. Call createOrderByMethod to create 
            //one. Apply it to the original list. Notify any bound controls that the sort has been applied.
            sortProperty = prop;
            
            var orderByMethodName = sortDirection == ListSortDirection.Ascending ? "OrderBy" : "OrderByDescending";
            var cacheKey = typeof(T).GUID + prop.Name + orderByMethodName;
            
            if(!cachedOrderByExpressions.ContainsKey(cacheKey)) createOrderByMethod(prop,orderByMethodName,cacheKey);
            
            resetItems(cachedOrderByExpressions[cacheKey](originalList).ToList());
            ResetBindings();
            sortDirection = sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
        }
        protected override void RemoveSortCore() { resetItems(originalList); }
        protected override bool SupportsSortingCore { get { return true;  } }
        protected override ListSortDirection SortDirectionCore { get { return sortDirection; }  }
        protected override PropertyDescriptor SortPropertyCore { get { return sortProperty; } }
        protected override void OnListChanged(ListChangedEventArgs e) { originalList = base.Items.ToList(); }

        private void createOrderByMethod(PropertyDescriptor prop,string orderByMethodName,string cacheKey) {
            //Create a generic method implementation for IEnumerable<T>. Cache it.
            var sourceParameter = Expression.Parameter(typeof(List<T>),"source");
            var lambdaParameter = Expression.Parameter(typeof(T),"lambdaParameter");
            var accesedMember = typeof(T).GetProperty(prop.Name);
            var propertySelectorLambda = Expression.Lambda(Expression.MakeMemberAccess(lambdaParameter,accesedMember),lambdaParameter);
            var orderByMethod = typeof(Enumerable).GetMethods()
                                          .Where(a => a.Name == orderByMethodName && a.GetParameters().Length == 2)
                                          .Single()
                                          .MakeGenericMethod(typeof(T),prop.PropertyType);
            var orderByExpression = Expression.Lambda<Func<List<T>,IEnumerable<T>>>(Expression.Call(orderByMethod,new Expression[] { sourceParameter,propertySelectorLambda }),sourceParameter);
            cachedOrderByExpressions.Add(cacheKey,orderByExpression.Compile());
        }
        private void resetItems(List<T> items) {
            //
            base.ClearItems();
            for(int i = 0; i < items.Count; i++) base.InsertItem(i,items[i]);
        }
    }
}
