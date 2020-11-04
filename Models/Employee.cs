using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WorkingNetworkLib.Models
{
    public class Employee : Worker
    {
        
        private List<string> employees = new List<string>();
        /// <summary>
        /// Конструктор для добавления сотрудника руководителем
        /// </summary>
        /// <param name="name"></param>
        public Employee(string name)
        { 
            WorkerName = name ?? throw new ArgumentNullException("Имя сотрудника не может быть пустым");
            Salary = 120000;
        }
        public Employee(string name, List<int> count, List<DateTime> dateTimes)
        {
            WorkerName = name ?? throw new ArgumentNullException("Имя сотрудника не может быть пустым!");
            CountOfHours = count ?? throw new ArgumentNullException("Пустое количество часов!");
            WorkingDates = dateTimes ?? throw new ArgumentNullException("Должны быть указаны даты работы!");
            Salary = 120000;
        }
        
        /// <summary>
        /// Получение экземпляра сотрудника по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Employee GetCurrentEmployee(string name)
        {

            List<int> countHours = new List<int>();
            List<DateTime> dateTimes = new List<DateTime>();
            using (StreamReader sr = new StreamReader("Список отработанных часов сотрудников.txt"))
            {
                string line;
                
                while ((line = sr.ReadLine()) != null)
                {
                    string[] employeeInfo = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (employeeInfo[1]==name)
                    {
                        countHours.Add(int.Parse(employeeInfo[2]));
                        dateTimes.Add(DateTime.Parse(employeeInfo[0]));
                    }
                }
                if (countHours.Count==0)
                {
                    throw new Exception("Данного сотрудника нет в списках!");
                }
                sr.Close();
            }
            return new Employee(name, countHours, dateTimes);
        }
        
        /// <summary>
        /// Расчет заработной платы за период
        /// </summary>
        /// <returns></returns>
        public override double CalcPay()
        {
            if (GetAllHours()<=160)
            {
                return GetAllHours() / 160 * Salary;
            }
            else
            {
                double salaryForOneHour = Salary / 160;
                return Salary + (GetAllHours() - 160) * 2 * salaryForOneHour;
            }
        }



        /// <summary>
        /// Просмотр информации о данном сотруднике
        /// </summary>
        /// <returns></returns>
        public override string PrintInfo()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < WorkingDates.Count; i++)
            {
                builder.Append($"Дата: {WorkingDates[i]} - количество отработанных часов: {CountOfHours[i]}\n");
            }
            builder.Append($"Всего часов: {GetAllHours()}, зарплата за данный период ={CalcPay()}");
            return builder.ToString();
        }
        
        public override void SetWorkingHours(int hours, string date)
        {

            LoadEmployees();
            bool isNewDate = false;
            foreach (string item in employees)
            {
                string[] employeeInfo = item.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
                if (employeeInfo[1] == this.WorkerName && date == employeeInfo[0])
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
                employees.Add($"{date},{WorkerName},{hours},{WorkerTask}");
            }
            WriteEmployees();
            
        }
        private void LoadEmployees()
        {
            using (StreamReader sr = new StreamReader("Список отработанных часов сотрудников.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    employees.Add(line);
                }
                sr.Close();
            }
        }
        private void WriteEmployees()
        {
            using (StreamWriter sw = new StreamWriter("Список отработанных часов сотрудников.txt", false))
            {
                foreach (string item in employees)
                {
                    sw.WriteLine(item);
                }
            }
        }
        
        /// <summary>
        /// Получение списка всех сотрудников
        /// </summary>
        /// <returns></returns>
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
                        emp.CountOfHours.Add(int.Parse(employeeInfo[2]));
                        emp.WorkingDates.Add(DateTime.Parse(employeeInfo[0]));
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
                                emp.CountOfHours.Add(int.Parse(employeeInfo[2]));
                                emp.WorkingDates.Add(DateTime.Parse(employeeInfo[0]));
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
                            emp.CountOfHours.Add(int.Parse(employeeInfo[2]));
                            emp.WorkingDates.Add(DateTime.Parse(employeeInfo[0]));
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