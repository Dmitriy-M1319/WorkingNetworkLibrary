using System.Collections.Generic;
using System.IO;
using WorkingNetworkLib.Models;

namespace WorkingNetworkLib.Repository
{
    public static class WorkerRepositoryGeneric<T> where T:Worker
    {
        public static List<string> ListWorkers { get; set; }
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
