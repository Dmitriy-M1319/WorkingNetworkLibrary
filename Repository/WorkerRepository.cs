using System.Collections.Generic;
using System.IO;
using WorkingNetworkLib.Models;

namespace WorkingNetworkLib.Repository
{
    public static class WorkerRepository<T> where T:Worker
    {
        public static List<string> ListWorkers { get; set; }
        private readonly static List<string> _repository = new List<string>();
        private static string fileName = "";
        public static void SetFileName()
        {
            T value = null;
            if (value is Employee)
            {
                fileName = "Список отработанных часов сотрудников.txt";
            }
            else if (value is Manager)
            {
                fileName = "Список отработанных часов руководителей.txt";
            }
            else
            {
                fileName = "Список отработанных часов внештатных сотрудников.txt";
            }
        }
       
        public static void LoadWorkersToString()
        {
            SetFileName();
            List<string> lines = new List<string>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                sr.Close();
            }
            ListWorkers = lines;
        }
        
        public static List<Employee>GetAllEmployees()
        {
            List<Employee> lst = new List<Employee>();
            LoadWorkers("сотрудник");
            foreach (string line in _repository)
            {
                lst.Add(Employee.GetCurrentEmployee(line));
            }
            return lst;
        }
        public static List<Manager>GetAllManagers()
        {
            List<Manager> lst = new List<Manager>();
            LoadWorkers("руководитель");
            foreach (string line in _repository)
            {
                lst.Add(Manager.GetCurrentManager(line));
            }
            return lst;
        }
        public static List<Freelancer> GetAllFreelances()
        {
            List<Freelancer> lst = new List<Freelancer>();
            LoadWorkers("фрилансер");
            foreach (string line in _repository)
            {
                lst.Add(Freelancer.GetCurrentFreelancer(line));
            }
            return lst;
        }
        private static void LoadWorkers(string position)
        {
            SetFileName();
            using StreamReader sr = new StreamReader("Список сотрудников.txt");
            string line;
            while ((line = sr.ReadLine())!=null)
            {
                string[] worker = line.Split(new char[] { ',' });
                if (worker[1] == position)
                {
                    _repository.Add(worker[0]);
                }
            }

        }
            
        
        public static void WriteWorkersToString()
        {
            SetFileName();
            using StreamWriter sw = new StreamWriter(fileName);
            foreach (string item in ListWorkers)
            {
                sw.WriteLine(item);
            }
        }
        

    }
}
