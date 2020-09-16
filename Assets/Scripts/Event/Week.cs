using System;
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
    public class Week : Iinit
    {
        private WeekTable _weekTable;
        public WeekTable GetWeekTable
        {
            get
            {
                return _weekTable;
            }
        }

        private uint AccumulateDate = 0;
        public uint GetAccumulateDate
        {
            get
            {
                return AccumulateDate;
            }
        }

        public Action OnDayEvent = delegate { };
        public Action OnSixDayEvent = delegate { };
        public Action OnMonthEvent = delegate { };
        public Action OnBubbleEvent = delegate { };

        private GameEvent evt;
        //Constructor
        public Week(GameEvent evt)
        {
            this.evt = evt;
        }

        //override
        public void Initialize()
        {
            _weekTable = evt.InitWeekTable;
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

        //Check Day Event
        bool IsDayEvent(uint day)
        {
            if (day == 10 || day == 20)
                return true;
            return false;
        }

        //Check Month Event
        uint LastMonth = 1;
        bool IsMonthEvent(uint month)
        {
            if (LastMonth != month)
            {
                LastMonth = month;
                return true;
            }
            return false;
        }

        //Print Current Weeks In Console : Debug
        void ApplyWeekText()
        {
            evt.WeekText.text = $"{_weekTable.day} - {_weekTable.month} - {_weekTable.years}";
            evt.AccumulateText.text = GetAccumulateDate.ToString();
        }

        bool SetSpecificDayEvent(uint specificDay, uint currentDay)
        {
            if (currentDay % specificDay == 0)
                return true;
            return false;
        }

        bool OnEvent()
        {
            if (IsDayEvent(_weekTable.day))
                OnDayEvent();
            if (IsMonthEvent(_weekTable.month))
                OnMonthEvent();

            if (SetSpecificDayEvent(6, _weekTable.day))
                OnSixDayEvent();

            OnBubbleEvent();
            return true;
        }

        public IEnumerator EWeekProcess()
        {
            while (true)
            {
                ++_weekTable.day;
                ++AccumulateDate;
                Calendar(ref (_weekTable.day), ref _weekTable.month, ref _weekTable.years);

                OnEvent();

                ApplyWeekText();
                yield return new WaitForSeconds(evt.GetWeekUploadTime());
            }
        }


    }
}
