using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WorkingNetworkLib.Models
{
    public class Employee : Worker
    {
        
        /// <summary>
        /// Конструктор для добавления сотрудника руководителем
        /// </summary>
        /// <param name="name"></param>
        public Employee(string name)
        {
            WorkerName = name ?? throw new ArgumentNullException("Имя сотрудника не может быть пустым");
            Salary = 120000; 
        }
 
        /// <summary>
        /// Получение экземпляра сотрудника по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Расчет заработной платы за период
        /// </summary>
        /// <returns></returns>
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



        /// <summary>
        /// Просмотр информации о данном сотруднике
        /// </summary>
        /// <returns></returns>
        public override string PrintInfo(DateTime startTime, DateTime endTime)
        {
            StringBuilder builder = new StringBuilder();
            
            foreach (var pair in DatesAndHours)
            {
                if (pair.Key >= startTime && pair.Key <= endTime)
                {
                    builder.Append($"Дата - {pair.Key}, отработано {pair.Value} часов");
                }
            }
            builder.Append($"Всего часов: {GetAllHours(startTime,  endTime)}, зарплата за данный период ={CalcPay(startTime, endTime)}");
            return builder.ToString();
        }
        
        public override void SetWorkingHours(int hours, string date)
        {

            Load("Список отработанных часов сотрудников.txt");
            bool isNewDate = false;
            foreach (string item in workers)
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
                workers.Add($"{date},{WorkerName},{hours},{WorkerTask}");
            }
            Write("Список отработанных часов сотрудников.txt");
            
        }
        
        
        public static List<Employee> GetAllEmployees()
        {
            List<Employee> allEmployes = new List<Employee>();
            using (StreamReader sr = new StreamReader("Список отработанных часов сотрудников.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] employeeInfo = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (allEmployes.Count == 0)
                    {
                        Employee emp = new Employee(employeeInfo[1]);
                        emp.DatesAndHours.Add(DateTime.Parse(employeeInfo[0]), int.Parse(employeeInfo[2]));
                        emp.allTasks.Add(employeeInfo[3]);
                        allEmployes.Add(emp);
                    }
                    else
                    {
                        bool isNewEmployee = false;
                        foreach (Employee emp in allEmployes)
                        {
                            if (emp.WorkerName == employeeInfo[1])
                            {
                                emp.DatesAndHours.Add(DateTime.Parse(employeeInfo[0]), int.Parse(employeeInfo[2]));
                                emp.allTasks.Add(employeeInfo[3]);
                                isNewEmployee = false;
                                break;
                            }
                            else
                            {
                                isNewEmployee = true;
                            }
                        }
                        if (isNewEmployee)
                        {
                            Employee emp = new Employee(employeeInfo[1]);
                            emp.DatesAndHours.Add(DateTime.Parse(employeeInfo[0]), int.Parse(employeeInfo[2]));
                            emp.allTasks.Add(employeeInfo[3]);
                            allEmployes.Add(emp);
                        }
                    }

                }
                sr.Close();
            }
            return allEmployes;
        }
    }
}