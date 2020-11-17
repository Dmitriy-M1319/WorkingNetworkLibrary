using WorkingNetworkLib.Repository;

namespace WorkingNetworkLib.Models
{
    public class RefactorStringParameters
    {
        private bool _isNewDate;
        private readonly string _refactorString = "";
        private readonly int _numberOfNote;
        public static Worker Worker { get; set; }
        public RefactorStringParameters(bool isNew, string refacStr, int hours)
        {
            _isNewDate = isNew;
            _refactorString = refacStr;
            _numberOfNote = hours;
        }
         
        public static RefactorStringParameters FindOrCreateNewNote(string date, int hours)
        {
            var parameteres = new RefactorStringParameters(false, "", 0);
            for (int i = 0; i < WorkerRepository.ListWorkers.Count; i++)
            {
                string[] employeeInfo = WorkerRepository.ListWorkers[i].Split(new char[] { ',' });
                if (employeeInfo[1] == Worker.WorkerName && date == employeeInfo[0])
                {
                    SetNewHoursInString(ref employeeInfo, hours);
                    parameteres = GetNewParametres(employeeInfo, i);
                    break;
                }
                else
                {
                    parameteres._isNewDate = true;
                }
            }
            return parameteres;
        }

        private static void SetNewHoursInString(ref string[] info, int hours)
        {
            info[2] = (int.Parse(info[2]) + hours).ToString();
        }
        private static RefactorStringParameters GetNewParametres(string[] info, int id)
        {
            return new RefactorStringParameters(false, ArrayToString(info), id);
        }
        private static string ArrayToString(string[] arr)
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

        public static void RefactorListWorkers(RefactorStringParameters parameteres, int hours, string date)
        {
            if (parameteres._isNewDate == true)
            {
                WorkerRepository.ListWorkers.Add($"{date},{Worker.WorkerName},{hours},{Worker.NewTask ?? " "}");
            }
            else
            {
                WorkerRepository.ListWorkers[parameteres._numberOfNote] = parameteres._refactorString;
            }
        }
    }
}
