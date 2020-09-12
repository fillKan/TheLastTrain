using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 
 * Usage
 * 
 *  // GetWeekTable
 *  GetWeekTable.years, GetWeekTable.month, GetWeekTable.day ..
 * 
 *  //Append Event Day
 * OnEvent += Func;
 * 
 *  //Disappend Event Day
 *  OnEvent -= Func;
 */


namespace InGame.UI.Week
{
    [System.Serializable]
    public struct WeekTable
    {
        public uint years;
        public uint month;
        public uint day;
    }
    public class Week : MonoBehaviour
    {
        [SerializeField] [Range(0.1f, 1.0f)] private float UploadTime = 1.0f;

        [SerializeField] private WeekTable StartYear;
        private WeekTable _weekTable;
        public WeekTable GetWeekTable
        {
            get
            {
                return _weekTable;
            }
        }

        public delegate void DayEvent();
        public static event DayEvent OnEvent = delegate { };

        public UnityEngine.UI.Text text;
        void OnEnable()
        {
            _weekTable = StartYear;
            StartCoroutine(WeekProcessing());
        }
        void OnDisable()
        {
            StopCoroutine(WeekProcessing());
        }

        // Check Leap Year
        bool IsLeapYear(uint Year)
        {
            if ((Year % 4 == 0) && (Year % 100 != 0) || (Year % 400 == 0))
                return true;
            return false;
        }

        // Check 31 day Months
        bool Is31Days(uint Month)
        {
            if (Month == 1 
                || Month == 3 
                || Month == 5 
                || Month == 7 
                || Month == 8 
                || Month == 10 
                || Month == 12)
                return true;
            return false;
        }

        // Check 30 day Months
        bool Is30Days(uint Month)
        {
            if (Month == 2 
                || Month == 4 
                || Month == 6 
                || Month == 9 
                || Month == 11 
                || Month == 12)
                return true;
            return false;
        }

        // Check End Of Year
        bool IsEndOfYear(uint Month)
        {
            return (Month > 12);
        }

        // Day -> Month -> Year
        void Calendar(ref uint Day, ref uint Month, ref uint Year)
        {
            if (Day <= 28)
                return;
            if (IsLeapYear(Year) && Month == 2 && Day == 29)
            {
                Month++;
                Day = 1;
            }
            else
            {
                if (Is30Days(Month) && Day == 31)
                {
                    Month++;
                    Day = 1;
                }
                else if (Is31Days(Month) && Day == 32)
                {
                    Month++;
                    Day = 1;
                }
            }
            if (IsEndOfYear(Month))
            {
                Month = 1;
                ++Year;
            }
        }

        //Check Event Day
        bool IsEventDay(ref uint day)
        {
            if (day == 10 || day == 20)
                return true;
            return false;
        }

        //Print Current Weeks In Console : Debug
        void PrintConsole()
        {
            //Debug.Log($"year : {_weekTable.years} month : {_weekTable.month} day : {_weekTable.day}");
            text.text = $"{_weekTable.day} - {_weekTable.month} - {_weekTable.years}";
        }
        public IEnumerator WeekProcessing()
        {
            while (true)
            {
                ++(_weekTable.day);
                Calendar(ref (_weekTable.day), ref _weekTable.month, ref _weekTable.years);
                if (IsEventDay(ref _weekTable.day))
                    OnEvent();
                PrintConsole();
                yield return new WaitForSeconds(UploadTime);
            }
        }


    }
}
