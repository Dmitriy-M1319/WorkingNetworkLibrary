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

        public override void SetWorkingHours(int hours, string date)
        {
            
            WorkerRepository.SetFileName(this);
            WorkerRepository.LoadWorkersToString();
            bool isNewDate = false;
            string replaceString = "";
            int number = 0;
            for (int i = 0; i < WorkerRepository.ListWorkers.Count; i++)
            {
                string[] managerInfo = WorkerRepository.ListWorkers[i].Split(new char[] { ',' });
                if ((managerInfo[1] == WorkerName) && date == managerInfo[0])
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
        public  void SetWorkingHours(int hours, string date, string name, string task)
        {
            string[] info = WorkerRepository.FindWorker(name);
            switch (info[1])
            {
                case "руководитель":
                    Manager manager = Manager.GetCurrentManager(name);
                    manager.NewTask = task;
                    manager.SetWorkingHours(hours, date);
                    break;
                case "сотрудник":
                    Employee employee = Employee.GetCurrentEmployee(name);
                    employee.NewTask = task;
                    employee.SetWorkingHours(hours, date);
                    break;
                case "фрилансер":
                    Freelancer freelancer = Freelancer.GetCurrentFreelancer(name);
                    freelancer.NewTask = task;
                    freelancer.SetWorkingHours(hours, date);
                    break;
            }
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
