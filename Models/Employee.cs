using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace WorkingNetworkLib.Models
{
    public class Employee : Worker
    {
        public override string WorkerName { get; set; }
        public override List<DateTime> WorkingDates { get;set; }
        private string employeeTask;
        private int salary = 120000;
        private int hours;
        private List<string> allTasks = new List<string>();
        
        public List<int> CountOfHours { get; set; }
        
        public override DateTime WorkingToday { get; set; }

        /// <summary>
        /// Конструктор для добавления сотрудника руководителем
        /// </summary>
        /// <param name="name"></param>
        public Employee(string name)
        { 
            WorkerName = name ?? throw new ArgumentNullException("Имя сотрудника не может быть пустым");
        }
        public Employee(string name, List<int> count, List<DateTime> dateTimes)
        {
            WorkerName = name ?? throw new ArgumentNullException("Имя сотрудника не может быть пустым!");
            CountOfHours = count ?? throw new ArgumentNullException("Пустое количество часов!");
            WorkingDates = dateTimes ?? throw new ArgumentNullException("Должны быть указаны даты работы!");
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
                return GetAllHours() / 160 * salary;
            }
            else
            {
                double salaryForOneHour = salary / 160;
                return salary + (GetAllHours() - 160) * 2 * salaryForOneHour;
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
        private int GetAllHours()
        {
            int hours = 0;
            foreach (int item in CountOfHours)
            {
                hours += item;
            }
            return hours;
        }
        public override void SetWorkingHours(int hours, string date)
        {
            List<string> employees = new List<string>();
            using (StreamReader sr = new StreamReader("Список отработанных часов сотрудников.txt"))
            {
                string line;
                while ((line = sr.ReadLine())!=null)
                {
                    employees.Add(line);
                }
                sr.Close();
            }
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
                employees.Add($"{date},{this.WorkerName},{hours},{employeeTask}");
            }
            using (StreamWriter sw = new StreamWriter("Список отработанных часов сотрудников.txt",false))
            {
                foreach (string item in employees)
                {
                    sw.WriteLine(item);
                }
            }
            
        }

        public static void Write(Worker worker, bool append)
        {
            if (worker is Employee)
            {
                Employee emp = (Employee)worker;
                using (StreamWriter sw = new StreamWriter("Список отработанных часов сотрудников.txt",append))
                {
                    sw.WriteLine($"{emp.WorkingToday},{emp.hours},{emp.employeeTask}");
                    sw.Close();
                }
            }
        }

        public void SetEmployeeTask(string task)
        {
            employeeTask = task;
        }
        public async Task WriteAsync(Worker worker, bool append)
        {
            await Task.Run(() => Write(worker,append));
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