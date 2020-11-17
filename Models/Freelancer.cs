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
            return (double)(GetAllHours(startTime, endTime)) * Salary;
        }
        
        public override void SetWorkingHours(int hours, string date)
        {
            if (DateTime.Today.Day - DateTime.Parse(date).Day > 2)
            {
                throw new Exception("Нельзя прибавлять часы ранее чем за 2 дня!");
            }
            if (DateTime.Parse(date) > DateTime.Today)
            {
                throw new ArgumentException("Дата не может быть позже сегодняшнего дня!");
            }
            WorkerRepository.SetFileName(this);
            WorkerRepository.LoadWorkersToString();
            RefactorStringParameters.Worker = this;
            var parameters = RefactorStringParameters.FindOrCreateNewNote(date, hours);
            RefactorStringParameters.RefactorListWorkers(parameters, hours, date);
            WorkerRepository.ListWorkers.Sort();
            WorkerRepository.WriteWorkersToString();
        }
       

        public static Freelancer GetCurrentFreelancer(string name)
        {
            if (!WorkerRepository.IsNewWorker(name))
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
                return freec;
            }
            else
            {
                throw new Exception("Данного сотрудника нет в списках!");
            }
            

        }

    }
}
