using System;
using System.Collections.Generic;
using System.Text;

namespace WorkingNetworkLib.Models
{
    public class Manager: Worker
    {
        private int prime = 20000;
        /// <summary>
        /// Конструктор для создания руководителей
        /// </summary>
        /// <param name="name"></param>
        public Manager(string name)
        {
            WorkerName = name ?? throw new ArgumentNullException("Имя руководителя не должно быть пустым");
            Salary = 200000;
        }
        /// <summary>
        /// Конструктор уже работавших руководителей
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hours"></param>
        /// <param name="tasks"></param>
        public Manager(string name, List<int> hours,List<string> tasks)
        {
            WorkerName = name ?? throw new ArgumentNullException("Имя руководителя не должно быть пустым");
            CountOfHours = hours ?? throw new ArgumentNullException("Пустое количество часов");
            allTasks = tasks ?? throw new ArgumentNullException("Пустой список задач!");
            Salary = 200000;
        }

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
        public override double CalcPay()
        {
            if (GetAllHours() <= 160)
            {
                return GetAllHours() / 160 * Salary;
            }
            else
            {
                return GetAllHours() / 160 * Salary + GetAllHours() / 160 * prime;
            }
        }

        public override void SetWorkingHours(int hours, string date)
        {
            base.SetWorkingHours(hours, date);
        }
    }
}
