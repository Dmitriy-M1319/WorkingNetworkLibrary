using System;
using System.Collections.Generic;

namespace WorkingNetworkLib.Models
{
    public abstract class Worker
    {
        
        public abstract string WorkerName { get; set;}
        public abstract List<DateTime> WorkingDates{ get;set; }
        public abstract DateTime WorkingToday{ get;set;}
        
        public abstract double CalcPay();
        public abstract string PrintInfo();
        public abstract void SetWorkingHours(int hours, string date);
    }
}