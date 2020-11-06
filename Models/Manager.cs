using System;
using System.Text;

namespace WorkingNetworkLib.Models
{
    public partial class Manager: Worker
    {
        
        private readonly int prime = 20000;
        /// <summary>
        /// Конструктор для создания руководителей
        /// </summary>
        /// <param name="name"></param>
        public Manager(string name)
        {
            WorkerName = name ?? throw new ArgumentNullException("Имя руководителя не должно быть пустым");
            Salary = 200000;
        }
    
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
            builder.Append($"Всего часов: {GetAllHours(startTime,endTime)}, зарплата за данный период ={CalcPay(startTime,endTime)}");
            return builder.ToString();
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
            Load("Список отработанных часов руководителей.txt");
            bool isNewDate = false;
            foreach (string item in workers)
            {
                string[] employeeInfo = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if ((employeeInfo[1] == WorkerName || employeeInfo[1] == name) && date == employeeInfo[0])
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
            Write("Список отработанных часов руководителей.txt");
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
