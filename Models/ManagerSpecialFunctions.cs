using WorkingNetworkLib.Repository;
using System.Collections.Generic;
using System;
using System.Text;

namespace WorkingNetworkLib.Models
{
    public partial class Manager
    {
        
        public string GetReportByAllWorkers(DateTime startTime, DateTime endTime)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"Отчет за период от {startTime} до {endTime}\n");
            builder.Append("\n");
            builder.Append("Руководители:\n");
            WorkerRepository<Manager>.LoadWorkersToString();
            foreach (string manager in WorkerRepository<Manager>.ListWorkers)
            {
                builder.Append(manager + "\n");
            }
            builder.Append("\n");
            builder.Append("Сотрудники:\n");
            WorkerRepository<Employee>.LoadWorkersToString();
            foreach (string employee in WorkerRepository<Employee>.ListWorkers)
            {
                builder.Append(employee + "\n");
            }
            builder.Append("\n");
            builder.Append("Фрилансеры:\n");
            WorkerRepository<Freelancer>.LoadWorkersToString();
            foreach (string freec in WorkerRepository<Freelancer>.ListWorkers)
            {
                builder.Append(freec + "\n");
            }
            //TODO:Сделать подсчет общего количества часов
            return builder.ToString();
        }
    }
}
