using System;
using System.Collections.Generic;

namespace WorkingNetworkLib.Models
{
    public class Worker
    {
        /// <summary>
        /// ��� ����������
        /// </summary>
        public  string WorkerName { get; set;}
        /// <summary>
        /// ������ ��� ������������ ����� �� �������
        /// </summary>
        public  List<DateTime> WorkingDates{ get;set; }
        /// <summary>
        /// ����������� ������� ����
        /// </summary>
        public  DateTime WorkingToday{ get;set;}
        /// <summary>
        /// ����������� ������� �������
        /// </summary>
        public string WorkerTask { get; set; }
        /// <summary>
        /// ���������� ����� ���������
        /// </summary>
        public int Salary { get; set; }
        /// <summary>
        /// ������ ���� ������������ �����
        /// </summary>
        protected List<string> allTasks = new List<string>();

        /// <summary>
        /// ������ ���� ������������ �����
        /// </summary>
        public List<int> CountOfHours { get; set; }

        /// <summary>
        /// ������ ���������� ����� ���������
        /// </summary>
        /// <returns></returns>
        public virtual double CalcPay() => throw new Exception();
        /// <summary>
        /// ����� ���������� � ���������
        /// </summary>
        /// <returns></returns>
        public  virtual string PrintInfo() => throw new Exception();
        /// <summary>
        /// ���������� ������������ �����
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="date"></param>
        public  virtual void SetWorkingHours(int hours, string date) => throw new Exception();
        /// <summary>
        /// ���������� ����� ���� ������������ �����
        /// </summary>
        /// <returns></returns>
        public int GetAllHours()
        {
            int hours = 0;
            foreach (int item in CountOfHours)
            {
                hours += item;
            }
            return hours;
        }

        
    }
}