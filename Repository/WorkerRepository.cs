using System.Collections.Generic;
using System.IO;
using WorkingNetworkLib.Models;

namespace WorkingNetworkLib.Repository
{
    public class WorkerRepository
    {
        private readonly static List<string> _repository = new List<string>();
        public static List<Employee> GetAllEmployees()
        {
            List<Employee> lst = new List<Employee>();
            LoadWorkers("сотрудник");
            foreach (string line in _repository)
            {
                lst.Add(Employee.GetCurrentEmployee(line));
            }
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
    }
}
