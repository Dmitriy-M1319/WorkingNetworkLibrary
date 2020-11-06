using System;
using WorkingNetworkLib.Repository;

namespace WorkingNetworkLib.Models
{
    public class Freelancer:Worker
    {
        public Freelancer(string name):base(name)
        {
            Salary = 1000;
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
            WorkerRepository<Freelancer>.SetFileName();
            WorkerRepository<Freelancer>.LoadWorkersToString();
            bool isNewDate = false;
            foreach (string item in WorkerRepository<Freelancer>.ListWorkers)
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
                WorkerRepository<Freelancer>.ListWorkers.Add($"{date},{WorkerName},{hours},{NewTask}");
            }
            WorkerRepository<Freelancer>.WriteWorkersToString();
        }

        public static Freelancer GetCurrentFreelancer(string name)
        {
            Freelancer freec = new Freelancer(name);
            freec.Load("Список отработанных часов внештатных сотрудников.txt");
            foreach (string line in freec.workers)
            {
                string[] freecInfo = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (freecInfo[1] == name)
                {
                    freec.DatesAndHours.Add(DateTime.Parse(freecInfo[0]), int.Parse(freecInfo[2]));
                    freec.allTasks.Add(freecInfo[3]);
                }
            }
            if (freec.DatesAndHours.Count == 0)
            {
                throw new Exception("Данного сотрудника нет в списках!");
            }
            return freec;

        }

    }
}
