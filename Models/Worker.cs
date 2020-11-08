using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WorkingNetworkLib.Models
{
    public class Worker
    {
       
        public  string WorkerName { get; set;}
        public Dictionary<DateTime,int> DatesAndHours { get; set; }
        public string NewTask { get; set; }
        public int Salary { get; set; }
        protected List<string> allTasks = new List<string>();
        protected List<string> workers = new List<string>();
        public Worker(string name)
        {
            WorkerName = name ?? throw new ArgumentNullException("Имя сотрудника не может быть пустым");
        }
       
        public virtual double CalcPay(DateTime startTime,DateTime dateTime) => throw new Exception();
        
        public  virtual void SetWorkingHours(int hours, string date) => throw new Exception();
        
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


        //загружает данные в виде списка строк
        protected void Load(string filename)
        {
            using StreamReader sr = new StreamReader(filename);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                workers.Add(line);
            }
            sr.Close();
        }
       
      
        public string PrintInfo(DateTime startTime, DateTime endTime)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var pair in DatesAndHours)
            {
                if (pair.Key >= startTime && pair.Key <= endTime)
                {
                    builder.Append($"Дата - {pair.Key}, отработано {pair.Value} часов");
                }
            }
            builder.Append($"Всего часов: {GetAllHours(startTime, endTime)}, зарплата за данный период ={CalcPay(startTime, endTime)}");
            return builder.ToString();
        }
        public string GetTask(int index)
        {
            return allTasks[index];
        }
    }
}