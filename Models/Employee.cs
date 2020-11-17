using System;
using WorkingNetworkLib.Repository;

namespace WorkingNetworkLib.Models
{
    public class Employee : Worker
    {  
        public Employee(string name):base(name)
        {
            Salary = 120000; 
        }

        public static Employee GetCurrentEmployee(string name)
        {
            if (!WorkerRepository.IsNewWorker(name))
            {
                Employee employee = new Employee(name);
                WorkerRepository.SetFileName(employee);
                WorkerRepository.LoadWorkersToString();
                foreach (string line in WorkerRepository.ListWorkers)
                {
                    string[] employeeInfo = line.Split(new char[] { ',' });
                    if (employeeInfo[1] == name)
                    {
                        employee.DatesAndHours.Add(DateTime.Parse(employeeInfo[0]), int.Parse(employeeInfo[2]));
                        employee.allTasks.Add(employeeInfo[3]);
                    }
                }
                return employee;
            }
            else
            {
                throw new Exception("Данного сотрудника нет в списках!");
            }
            
        }
        
        public override double CalcPay(DateTime startTime, DateTime endTime)
        {
            if (GetAllHours(startTime,endTime)<=160)
            {
                return (double)(GetAllHours(startTime,endTime)) / 160 * Salary;
            }
            else
            {
                double salaryForOneHour = Salary / 160;
                return Salary + (GetAllHours(startTime,endTime) - 160) * 2 * salaryForOneHour;
            }
        }

        public override void SetWorkingHours(int hours, string date)
        {
            if (DateTime.Parse(date) > DateTime.Today)
            {
                throw new ArgumentException("Дата не может быть раньше сегодняшнего дня!");
            }
            WorkerRepository.SetFileName(this);
            WorkerRepository.LoadWorkersToString();
            RefactorStringParameters.Worker = this;
            var parameters = RefactorStringParameters.FindOrCreateNewNote(date, hours);
            RefactorStringParameters.RefactorListWorkers(parameters, hours, date);
            WorkerRepository.ListWorkers.Sort();
            WorkerRepository.WriteWorkersToString();
        }
    }
}