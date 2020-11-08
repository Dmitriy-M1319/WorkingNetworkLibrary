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
                return GetAllHours(startTime, endTime) / 160 * Salary;
            }
            else
            {
                return GetAllHours(startTime, endTime) / 160 * Salary + GetAllHours(startTime, endTime) / 160 * prime;
            }
        }

        public void SetWorkingHours(int hours, string date, string name)
        {
            WorkerRepositoryGeneric<Manager>.SetFileName();
            WorkerRepositoryGeneric<Manager>.LoadWorkersToString();
            bool isNewDate = false;
            foreach (string item in WorkerRepositoryGeneric<Manager>.ListWorkers)
            {
                string[] managerInfo = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if ((managerInfo[1] == WorkerName || managerInfo[1] == name) && date == managerInfo[0])
                {
                    managerInfo[2] = (int.Parse(managerInfo[2]) + hours).ToString();
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
                WorkerRepositoryGeneric<Manager>.ListWorkers.Add($"{DateTime.Today},{WorkerName},{hours},{NewTask}");
            }
            WorkerRepositoryGeneric<Manager>.LoadWorkersToString();
        }

        public static Manager GetCurrentManager(string name)
        {
            Manager manager = new Manager(name);
            manager.Load("Список отработанных часов руководителей");
            foreach (string line in manager.workers)
            {
                string[] employeeInfo = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (employeeInfo[1] == name)
                {
                    manager.DatesAndHours.Add(DateTime.Parse(employeeInfo[0]), int.Parse(employeeInfo[2]));
                    manager.allTasks.Add(employeeInfo[3]);
                }
            }
            if(manager.DatesAndHours.Count == 0)
            {
                throw new Exception("Данного сотрудника нет в списках!");
            }
            return manager;
        }

        
    }
}
