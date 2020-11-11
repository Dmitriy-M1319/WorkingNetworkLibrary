using System.Collections.Generic;
using System.IO;
using WorkingNetworkLib.Models;

namespace WorkingNetworkLib.Repository
{
    public class WorkerRepository
    {
        private readonly static List<string> _repository = new List<string>();
        public static List<string> ListWorkers { get; set; }
        private static string fileName = "";

        public static void SetFileName(object o)
        {

            if (o is Employee)
            {
                fileName = "Список отработанных часов сотрудников.txt";
            }
            else if (o is Manager)
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


        public static void WriteWorkersToString()
        {
            using StreamWriter sw = new StreamWriter(fileName, false);
            foreach (string item in ListWorkers)
            {
                sw.WriteLine(item);
            }
        }
        public static List<Employee> GetAllEmployees()
        {
            List<Employee> lst = new List<Employee>();
            LoadWorkers("сотрудник");
            foreach (string line in _repository)
            {
                lst.Add(Employee.GetCurrentEmployee(line));
            }
            _repository.Clear();
            return lst;
        }
        public static List<Manager> GetAllManagers()
        {
            List<Manager> lst = new List<Manager>();
            LoadWorkers("руководитель");
            foreach (string line in _repository)
            {
                lst.Add(Manager.GetCurrentManager(line));
            }
            _repository.Clear();
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
            _repository.Clear();
            return lst;
        }
        private static void LoadWorkers(string position)
        {
            using StreamReader sr = new StreamReader("Список сотрудников.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] worker = line.Split(new char[] { ',' });
                if (worker[1] == position)
                {
                    _repository.Add(worker[0]);
                }
            }

        }
        public static bool IsNewWorker(string name)
        {
            using StreamReader sr = new StreamReader("Список сотрудников.txt");
            List<string> names = new List<string>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] info = line.Split(new char[] { ',' });
                if (name == info[0])
                {
                    return false;
                }
            }
            sr.Close();
            return true;
        }


        public static string[] FindWorker(string name)
        {
            using StreamReader sr = new StreamReader("Список сотрудников.txt");
            List<string> names = new List<string>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] info = line.Split(new char[] { ',' });
                if (name == info[0])
                {
                    return info;
                }
            }
            sr.Close();
            return null;
        }

    }
}
