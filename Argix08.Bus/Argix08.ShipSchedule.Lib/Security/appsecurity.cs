using System;
using System.Data;
using System.Data.SqlClient;

namespace Argix.Security {
	//
	public class AppSecurity {
		//Members
		public static string Role=ROLE_NONE;
		
		public static string ROLE_NONE = "LineHaulOperator";
        public static string ROLE_COORDINATOR = "LineHaulCoordinator";
        public static string ROLE_ADMINISTRATOR = "LineHaulAdministrator";
		
		//Interface
		static AppSecurity() { }
        private AppSecurity() { }
		public static bool UserCanUpdateAll {
			get {
				//User should have at least one role defined in the Configuration table
				bool retValue=false;
				if(AppSecurity.Role.ToLower() == ROLE_COORDINATOR.ToLower() || AppSecurity.Role.ToLower() == ROLE_ADMINISTRATOR.ToLower())
					retValue = true;
				return retValue;
			}
		}
		public static bool UserCanAddSchedule {
			get {
				//User should have at least one role defined in the Configuration table
				bool retValue = false;
				if(AppSecurity.Role.ToLower() == ROLE_ADMINISTRATOR.ToLower())
					retValue = true;
				return retValue;
			}
		}
		public static bool UserCanAccessTemplate {
			get {
				//User should have at least one role defined in the Configuration table
				bool retValue = false;
				if(AppSecurity.Role.ToLower() == ROLE_ADMINISTRATOR.ToLower()) 
					retValue = true;
				return retValue;
			}
		}
	}
}
