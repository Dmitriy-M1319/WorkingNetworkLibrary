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
            if (DateTime.Parse(date) > DateTime.Today)
            {
                throw new ArgumentException("Дата не может быть позже сегодняшнего дня!");
            }
            WorkerRepository.SetFileName(this);
            WorkerRepository.LoadWorkersToString();
            RefactorStringParameters.Worker = this;
            var parameters = RefactorStringParameters.FindOrCreateNewNote(date, hours);
            RefactorStringParameters.RefactorListWorkers(parameters, hours, date);
            WorkerRepository.ListWorkers.Sort();
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
