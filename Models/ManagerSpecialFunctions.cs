using WorkingNetworkLib.Repository;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;

namespace WorkingNetworkLib.Models
{
    public class ManagerSpecialFunctions
    {
        private static readonly List<Manager> managers = WorkerRepository.GetAllManagers();
        private static readonly List<Employee> employees = WorkerRepository.GetAllEmployees();
        private static readonly List<Freelancer> freelancers = WorkerRepository.GetAllFreelances();
        public static  string GetReportByAllWorkers(DateTime startTime, DateTime endTime)
        {
            int allHours = 0;
            double allSum = 0;
            StringBuilder builder = new StringBuilder();
            builder.Append($"Отчет за период от {startTime} до {endTime}\n");
            builder.Append("\n");
            builder.Append("Руководители:\n");
            foreach (Manager man in managers)
            {
                builder.Append($"{man.WorkerName} отработал {man.GetAllHours(startTime, endTime)} " +
                    $"и заработал {man.CalcPay(startTime, endTime)}\n");
                allHours += man.GetAllHours(startTime, endTime);
                allSum += man.CalcPay(startTime, endTime);
            }
            builder.Append("\n");
            builder.Append("Сотрудники:\n");
            foreach (var emp in employees)
            {
                builder.Append($"{emp.WorkerName} отработал {emp.GetAllHours(startTime, endTime)} " +
                    $"и заработал {emp.CalcPay(startTime, endTime)}\n");
                allHours += emp.GetAllHours(startTime, endTime);
                allSum += emp.CalcPay(startTime, endTime);
            }
            builder.Append("\n");
            builder.Append("Фрилансеры:\n");
            foreach (var freec in freelancers)
            {
                builder.Append($"{freec.WorkerName} отработал {freec.GetAllHours(startTime, endTime)} " +
                    $"и заработал {freec.CalcPay(startTime, endTime)}\n");
                allHours += freec.GetAllHours(startTime, endTime);
                allSum += freec.CalcPay(startTime, endTime);
            }
            builder.Append($"Всего отработано {allHours} часов; к выплате {allSum} рублей\n");
            return builder.ToString();
        }
        public static string GetReportByOneWorker(string name, DateTime startTime, DateTime endTime, string position)
        {
            StringBuilder builder = new StringBuilder();
            Worker worker = GetWorker(position, name);
            if (worker != null)
            {
                builder.Append($"Отчет по сотруднику {worker.WorkerName}: период с {startTime} по {endTime}\n");
                int i = 0;
                foreach (var pair in worker.DatesAndHours)
                {
                    if (pair.Key >= startTime && pair.Key <= endTime)
                    {
                        builder.Append($"{pair.Key}, {pair.Value} часов, {worker.GetTask(i)}");
                    }
                    i++;
                }
                builder.Append($"Итого {worker.GetAllHours(startTime, endTime)}, к выплате {worker.CalcPay(startTime, endTime)}");
                return builder.ToString();
            }
            else
            {
                throw new Exception("Данного сотрудника не существует!");
            }
        }

        private static  Worker GetWorker(string position, string name)
        {
            return position switch
            {
                "руководитель" => managers?.Find(m => m.WorkerName == name),
                "сотрудник" => employees?.Find(e => e.WorkerName == name),
                "фрилансер" => freelancers?.Find(f => f.WorkerName == name),
                _ => throw new Exception("Такого сотрудника нет!"),
            };
        }
        
        public static void SetNewWorker(string name, string position)
        {
            if (WorkerRepository.IsNewWorker(name))
            {
                using StreamWriter sw = new StreamWriter("Список сотрудников.txt", true);
                sw.WriteLine($"{name},{position}");
            }
            else
            {
                throw new Exception("Данный сотрудник уже существует");
            }
        }
    }
}
