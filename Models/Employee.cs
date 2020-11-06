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
            Employee employee = new Employee(name);
            employee.Load("Список отработанных часов сотрудников.txt");
            foreach (string line in employee.workers)
            {
                string[] employeeInfo = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (employeeInfo[1] == name)
                {
                    employee.DatesAndHours.Add(DateTime.Parse(employeeInfo[0]), int.Parse(employeeInfo[2]));
                    employee.allTasks.Add(employeeInfo[3]);
                }
            }
            if (employee.DatesAndHours.Count==0)
            {
                throw new Exception("Данного сотрудника нет в списках!");
            }
            return employee;
        }
        
      

        public override double CalcPay(DateTime startTime, DateTime endTime)
        {
            if (GetAllHours(startTime,endTime)<=160)
            {
                return GetAllHours(startTime,endTime) / 160 * Salary;
            }
            else
            {
                double salaryForOneHour = Salary / 160;
                return Salary + (GetAllHours(startTime,endTime) - 160) * 2 * salaryForOneHour;
            }
        }


        public override void SetWorkingHours(int hours, string date)
        {
            WorkerRepository<Employee>.SetFileName();
            WorkerRepository<Employee>.LoadWorkersToString();
            bool isNewDate = false;
            foreach (string item in WorkerRepository<Employee>.ListWorkers)
            {
                string[] employeeInfo = item.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
                if (employeeInfo[1] == WorkerName && date == employeeInfo[0])
                {
                    employeeInfo[2] = (int.Parse(employeeInfo[2]) + hours).ToString();
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
                WorkerRepository<Employee>.ListWorkers.Add($"{date},{WorkerName},{hours},{NewTask}");
            }
            WorkerRepository<Employee>.WriteWorkersToString();
        }
        
    }
}