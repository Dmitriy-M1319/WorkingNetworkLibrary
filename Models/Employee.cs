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
                employee.Load("Список отработанных часов сотрудников.txt");
                foreach (string line in employee.workers)
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
            WorkerRepository.SetFileName(this);
            WorkerRepository.LoadWorkersToString();
            bool isNewDate = false;
            string replaceString = "";
            int number = 0;
            for (int i = 0; i < WorkerRepository.ListWorkers.Count; i++)
            {
                string[] employeeInfo = WorkerRepository.ListWorkers[i].Split(new char[] { ',' });
                if (employeeInfo[1] == WorkerName && date == employeeInfo[0])
                {
                    employeeInfo[2] = (int.Parse(employeeInfo[2]) + hours).ToString();
                    replaceString = ArrayToString(employeeInfo);
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

        private string ArrayToString(string[] arr)
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

    }
}