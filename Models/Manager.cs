using System;
using WorkingNetworkLib.Repository;

namespace WorkingNetworkLib.Models
{
    public partial class Manager: Worker
    {
        
        private readonly int prime = 20000;
        public Manager(string name):base(name)
        {
            Salary = 200000;
        }
    
        
        public override double CalcPay(DateTime startTime, DateTime endTime)
        {
            if (GetAllHours(startTime, endTime) <= 160)
            {
                return (double)(GetAllHours(startTime, endTime)) / 160 * Salary;
            }
            else
            {
                return (double)(GetAllHours(startTime, endTime)) / 160 * Salary + GetAllHours(startTime, endTime) / 160 * prime;
            }
        }

        public void SetWorkingHours(int hours, string date, string name)
        {
            WorkerRepository.SetFileName(this);
            WorkerRepository.LoadWorkersToString();
            bool isNewDate = false;
            string replaceString = "";
            int number = 0;
            for (int i = 0; i < WorkerRepository.ListWorkers.Count; i++)
            {
                string[] managerInfo = WorkerRepository.ListWorkers[i].Split(new char[] { ',' });
                if ((managerInfo[1] == WorkerName || managerInfo[1] == name) && date == managerInfo[0])
                {
                    managerInfo[2] = (int.Parse(managerInfo[2]) + hours).ToString();
                    replaceString = ArrayToString(managerInfo);
                    number = i;
                    isNewDate = false;
                    break;
                }
                else
                {
                    isNewDate = true;
                }
            }
            if (isNewDate)
            {
                WorkerRepository.ListWorkers.Add($"{DateTime.Today.ToShortDateString()},{WorkerName},{hours},{NewTask ?? " "}");
            }
            else
            {
                WorkerRepository.ListWorkers[number] = replaceString;
            }
            WorkerRepository.WriteWorkersToString();
        }
        private string ArrayToString(string [] arr)
        {
            string result = "";
            for (int i = 0; i < arr.Length; i++)
            {
                result += arr[i];
                if (i != 3)
                {
                    result += ",";
                }
            }
            return result;
        }

        public static Manager GetCurrentManager(string name)
        {
            if (!WorkerRepository.IsNewWorker(name))
            {
                Manager manager = new Manager(name);
                manager.Load("Список отработанных часов руководителей.txt");
                foreach (string line in manager.workers)
                {
                    string[] employeeInfo = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (employeeInfo[1] == name)
                    {
                        manager.DatesAndHours.Add(DateTime.Parse(employeeInfo[0]), int.Parse(employeeInfo[2]));
                        manager.allTasks.Add(employeeInfo[3]);
                    }
                }
                return manager;
            }
            else
            {
                throw new Exception("Данного сотрудника нет в списках!");
            }
        }

        
    }
}
