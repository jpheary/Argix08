//	File:	statistics.cs
//	Author:	J. Heary
//	Date:	02/07/05
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;

namespace Tsort.Sort {
    //
    public class Statistics {
        //Members
        private DateTime mStartTime=DateTime.Now;
        private int mUpTimeSec=0;
        private int mDownTimeSec=0;
        private int mCartonsSorted=0;
        private int mCartonsUnsorted=0;
        private int mCartonsDeleted=0;
        private DateTime mLastProcessed=DateTime.Now;
        private System.Collections.ArrayList mCartons=null;

        //Constants
        public const int DOWN_TIME_MAX=60;

        //Events
        public event EventHandler StatisticsChanged=null;

        //Interface
        public Statistics() {
            //Constructor
            this.mCartons = new System.Collections.ArrayList();
            Reset();
        }
        public DateTime StartTime { get { return this.mStartTime; } }
        public int RunningTimeMinutes { get { return MinutesBetween(this.mStartTime,DateTime.Now); } }
        public int UpTimeMinutes { get { return (this.mUpTimeSec / 60); } }
        public int DownTimeMinutes { get { return (this.mDownTimeSec / 60); } }
        public int CartonsSorted { get { return this.mCartonsSorted; } }
        public int CartonsUnsorted { get { return this.mCartonsUnsorted; } }
        public int CartonsDeleted { get { return this.mCartonsDeleted; } }
        public int CartonsTotal { get { return this.mCartonsSorted + this.mCartonsUnsorted - this.mCartonsDeleted; } }
        public int ItemsPerHour { get { return ItemsPerMinute * 60; } }
        public int ItemsPerMinute {
            get {
                int minutes = MinutesBetween(this.mStartTime,DateTime.Now);
                return minutes == 0 ? 0: (int)CartonsTotal / minutes;
            }
        }
        public int ItemsPerMinuteUptime {
            get {
                int seconds = this.mUpTimeSec;
                return seconds == 0 ? 0: (int)(60 * CartonsTotal/seconds);
            }
        }
        public int ItemsPerLastMinute { get { return this.mCartons.Count; } }

        public void AddSortedCarton() {
            //Add a carton and notify client of change
            this.mCartonsSorted++;
            if(this.StatisticsChanged != null) this.StatisticsChanged(this,new EventArgs());
        }
        public void AddUnsortedCarton() {
            //Delete a carton and notify client of change
            this.mCartonsUnsorted++;
            if(this.StatisticsChanged != null) this.StatisticsChanged(this,new EventArgs());
        }
        public void DeleteSortedCarton() {
            //Delete a carton and notify client of change
            this.mCartonsDeleted++;
            if(this.StatisticsChanged != null) this.StatisticsChanged(this,new EventArgs());
        }
        public void Reset() {
            //Reset and notify client of change
            this.mCartonsSorted = this.mCartonsUnsorted = this.mCartonsDeleted = 0;
            this.mUpTimeSec = this.mDownTimeSec = 0;
            this.mCartons.Clear();
            if(this.StatisticsChanged != null) this.StatisticsChanged(this,new EventArgs());
        }
        public int CalcDuration() {
            //Determine elapsed & down times for this sorted item
            int duration=0;
            try {
                //Determine uptime\downtime
                duration = (int)((TimeSpan)DateTime.Now.Subtract(this.mLastProcessed)).TotalSeconds;
                if(duration < Statistics.DOWN_TIME_MAX) {
                    this.mUpTimeSec += duration;
                    this.mDownTimeSec += 0;
                }
                else {
                    this.mUpTimeSec += DOWN_TIME_MAX;
                    this.mDownTimeSec += duration - DOWN_TIME_MAX;
                }

                try {
                    this.mCartons.Add(DateTime.Now);
                    DateTime dt0 = (DateTime)this.mCartons[0];
                    DateTime dtn = (DateTime)this.mCartons[this.mCartons.Count-1];
                    while((int)((TimeSpan)dtn.Subtract(dt0)).TotalSeconds > 60) {
                        this.mCartons.RemoveAt(0);
                        dt0 = (DateTime)this.mCartons[0];
                    }
                }
                catch(Exception) { }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while calculating processing duration.",ex); }
            finally { this.mLastProcessed = DateTime.Now; }
            return duration;
        }
        #region Local Services: MinutesBetween(), SecondsBetween()
        private int MinutesBetween(DateTime From,DateTime To) {
            TimeSpan t = To.Subtract(From);
            return (t.Days * 24 + t.Hours) * 60 + t.Minutes;
        }
        private int SecondsBetween(DateTime From,DateTime To) {
            TimeSpan t = To.Subtract(From);
            return ((t.Days * 24 + t.Hours) * 60 + t.Minutes) * 60 + t.Seconds;
        }
        #endregion
    }
}
