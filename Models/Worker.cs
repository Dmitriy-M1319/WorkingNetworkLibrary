using System;
using System.Collections.Generic;

namespace WorkingNetworkLib.Models
{
    public class Worker
    {
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public  string WorkerName { get; set;}
        /// <summary>
        /// Список дат отработанных часов по порядку
        /// </summary>
        public  List<DateTime> WorkingDates{ get;set; }
        /// <summary>
        /// Добавленные сегодня часы
        /// </summary>
        public  DateTime WorkingToday{ get;set;}
        /// <summary>
        /// Добавленное сегодня задание
        /// </summary>
        public string WorkerTask { get; set; }
        /// <summary>
        /// Заработная плата работника
        /// </summary>
        public int Salary { get; set; }
        /// <summary>
        /// Список всех отработанных задач
        /// </summary>
        protected List<string> allTasks = new List<string>();

        /// <summary>
        /// Список всех отработанных часов
        /// </summary>
        public List<int> CountOfHours { get; set; }

        /// <summary>
        /// Расчет заработной платы работника
        /// </summary>
        /// <returns></returns>
        public virtual double CalcPay() => throw new Exception();
        /// <summary>
        /// Вывод информации о работнике
        /// </summary>
        /// <returns></returns>
        public  virtual string PrintInfo() => throw new Exception();
        /// <summary>
        /// Добавление отработанных часов
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="date"></param>
        public  virtual void SetWorkingHours(int hours, string date) => throw new Exception();
        /// <summary>
        /// Возвращает сумму всех отработанных часов
        /// </summary>
        /// <returns></returns>
        public int GetAllHours()
        {
            int hours = 0;
            foreach (int item in CountOfHours)
            {
                hours += item;
            }
            return hours;
        }

        
    }
}