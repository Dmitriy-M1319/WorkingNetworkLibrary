using System;

namespace WorkingNetworkLib.Models
{
    public abstract class Worker
    {
        
        public abstract string WorkerName { get; set;}
        public abstract DateTime StartWorking{ get;set; }
        public abstract DateTime EndWorking{ get; set; }
        public abstract int AllCountOfHours { get; set; }
        public abstract double CalcPay(int countOfDays);
        public abstract string PrintInfo();
        public abstract void SetWorkingHours(int hours);
    }
}