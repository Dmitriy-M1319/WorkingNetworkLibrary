using System;

namespace WorkingNetworkLib.Models
{
    public abstract class Worker
    {
        public string WorkerName { get; set;}
        public DateTime WorkingTime { get;set; }
        public int CountOfHours { get; set; }
        public abstract double CalcPay(int countOfDays);
        public abstract string PrintInfo();
        public abstract void SetWorkingHours(int hours);
    }
}