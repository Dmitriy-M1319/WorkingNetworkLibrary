using System;

namespace WorkingNetworkLib.Models
{
    public class StandardWorker : Worker
    {
        public override string WorkerName { get; set; }
        public override DateTime StartWorking { get; set; }
        public override DateTime EndWorking { get; set; }
        public override int AllCountOfHours { get; set; }

        public override double CalcPay(int countOfDays)
        {
            throw new NotImplementedException();
        }

        public override string PrintInfo()
        {
            throw new NotImplementedException();
        }

        public override void SetWorkingHours(int hours)
        {
            throw new NotImplementedException();
        }
    }
}