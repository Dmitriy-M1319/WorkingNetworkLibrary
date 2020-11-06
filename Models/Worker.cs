using System;
using System.Collections.Generic;
using System.IO;

namespace WorkingNetworkLib.Models
{
    public class Worker
    {
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public  string WorkerName { get; set;}
        public Dictionary<DateTime,int> DatesAndHours { get; set; }
        /// <summary>
        /// Добавленные сегодня часы
        /// </summary>
        public DateTime WorkingToday{ get;set;}
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
        /// Расчет заработной платы работника
        /// </summary>
        /// <returns></returns>
        public virtual double CalcPay(DateTime startTime,DateTime dateTime) => throw new Exception();
        /// <summary>
        /// Вывод информации о работнике
        /// </summary>
        /// <returns></returns>
        public  virtual string PrintInfo(DateTime startTime, DateTime endTime) => throw new Exception();
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
        public int GetAllHours(DateTime startTime, DateTime endTime)
        {
            int hours = 0;
            foreach (var pair in DatesAndHours)
            {
                if (pair.Key >= startTime && pair.Key <= endTime)
                {
                    hours += pair.Value;
                }
            }
            return hours;
        }
        protected List<string> workers = new List<string>();
        //загружает данные в виде списка строк
        protected void Load(string filename)
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    workers.Add(line);
                }
                sr.Close();
            }
        }
        //Записывает данные в файл
        protected void Write(string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename, false))
            {
                foreach (string item in workers)
                {
                    sw.WriteLine(item);
                }
            }
        }
        
        protected bool IsNewWorker(string filename, string name)
        {
            Load(filename);
            foreach (string worker in workers)
            {
                string[] info = worker.Split(new char[] { ',' });
                if (info[1]==name)
                {
                    return false;
                }
            }
            return true;
        }
        
    }
}