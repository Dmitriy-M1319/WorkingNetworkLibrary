using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WorkingNetworkLib.Models
{
    public class Worker
    {
       
        public  string WorkerName { get; set;}
        public Dictionary<DateTime, int> DatesAndHours { get; set; } = new Dictionary<DateTime, int>();
        public string NewTask { get; set; }
        public int Salary { get; set; }
        protected List<string> allTasks = new List<string>();
        protected List<string> workers = new List<string>();
        public Worker(string name)
        {
            WorkerName = name ?? throw new ArgumentNullException("��� ���������� �� ����� ���� ������");
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


        //��������� ������ � ���� ������ �����
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
                    builder.Append($"���� - {pair.Key}, ���������� {pair.Value} �����");
                }
            }
            builder.Append($"����� �����: {GetAllHours(startTime, endTime)}, �������� �� ������ ������ ={CalcPay(startTime, endTime)}");
            return builder.ToString();
        }
        public string GetTask(int index)
        {
            return allTasks[index];
        }
    }
}