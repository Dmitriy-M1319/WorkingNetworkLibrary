using System;
using System.Collections.Generic;
using System.Text;

namespace WorkingNetworkLib.Models
{
    public class Freelancer:Worker
    {
        public Freelancer(string name)
        {
            WorkerName = name ?? throw new ArgumentNullException("Имя руководителя не должно быть пустым");
            Salary = 1000;
        }

        //TODO:Вынести в часть класса Worker
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
            builder.Append($"Всего часов: {GetAllHours(startTime, endTime)}, зарплата за данный период ={CalcPay(startTime, endTime)}");
            return builder.ToString();
        }

        public override double CalcPay(DateTime startTime, DateTime endTime)
        {
            return GetAllHours(startTime, endTime) * Salary;
        }
        public override void SetWorkingHours(int hours, string date)
        {
            if (DateTime.Today.Day - DateTime.Parse(date).Day > 2)
            {
                throw new Exception("Нельзя прибавлять часы ранее чем за 2 дня!");
            }
            Load("Список отработанных часов внештатных сотрудников.txt");
            bool isNewDate = false;
            foreach (string item in workers)
            {
                string[] freelancerInfo = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (freelancerInfo[1] == WorkerName && date == freelancerInfo[0])
                {
                    freelancerInfo[2] = (int.Parse(freelancerInfo[2]) + hours).ToString();
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
            Write("Список отработанных часов внештатных сотрудников.txt");
        }

    }
}
