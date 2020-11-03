
using System;
using System.Threading.Tasks;

namespace WorkingNetworkLib.Models
{
    public class Leader: Worker, IWriteInFile
    {
        private int salary = 200000;

        public override string WorkerName { get; set; }
        public override DateTime StartWorking { get; set; }
        public override DateTime EndWorking { get; set; }
        public override int AllCountOfHours { get; set; }

        public Leader(string name,string startWork)
        {
            WorkerName = name ?? throw new ArgumentNullException("Имя не должно быть пустым");
            if (string.IsNullOrWhiteSpace(startWork))
            {
                StartWorking = DateTime.Parse(startWork);
            }
            
            
            
            
        }

        public override double CalcPay(int countOfDays)
        {
            return countOfDays / 160 * salary; 
        }

        public override string PrintInfo()
        {
            throw new NotImplementedException();
        }

        public override void SetWorkingHours(int hours)
        {
            throw new NotImplementedException();
        }

        public void Write()
        {
            throw new NotImplementedException();
        }

        public async Task WriteAsync()
        {
            await Task.Run(()=>Write());
        }
    }
}