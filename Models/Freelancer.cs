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
            WorkerRepository.SetFileName(this);
            WorkerRepository.LoadWorkersToString();
            bool isNewDate = false;
            string replaceString = "";
            int number = 0;
            for (int i = 0; i < WorkerRepository.ListWorkers.Count; i++)
            {
                string[] freecInfo = WorkerRepository.ListWorkers[i].Split(new char[] { ',' });
                if (freecInfo[1] == WorkerName && date == freecInfo[0])
                {
                    freecInfo[2] = (int.Parse(freecInfo[2]) + hours).ToString();
                    replaceString = ArrayToString(freecInfo);
                    number = i;
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
                WorkerRepository.ListWorkers.Add($"{DateTime.Today.ToShortDateString()},{WorkerName},{hours},{NewTask ?? " "}");
            }
            else
            {
                WorkerRepository.ListWorkers[number] = replaceString;
            }
            WorkerRepository.WriteWorkersToString();
        }

        private string ArrayToString(string[] arr)
        {
            string result = "";
            for (int i = 0; i < arr.Length; i++)
            {
                result += arr[i];
                if (i != 3)
                {
                    result += ",";
                }
            }
            return result;
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
