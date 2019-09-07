using System;
using System.Collections.Generic;
using System.Text;

namespace EBZ.Mobile.Models
{
    public class DateModel
    {
        public List<string> DayPicker()
        {
            var dayList = new List<string>();
            dayList.Add("1");
            dayList.Add("2");
            dayList.Add("3");
            dayList.Add("4");
            dayList.Add("5");
            dayList.Add("6");
            dayList.Add("7");
            dayList.Add("8");
            dayList.Add("9");
            dayList.Add("10");
            dayList.Add("11");
            dayList.Add("12");
            dayList.Add("13");
            dayList.Add("14");
            dayList.Add("15");
            dayList.Add("16");
            dayList.Add("17");
            dayList.Add("18");
            dayList.Add("19");
            dayList.Add("20");
            dayList.Add("21");
            dayList.Add("22");
            dayList.Add("23");
            dayList.Add("24");
            dayList.Add("25");
            dayList.Add("26");
            dayList.Add("27");
            dayList.Add("28");
            dayList.Add("29");
            dayList.Add("30");
            dayList.Add("31");
            return dayList;
        }

        public List<string> MonthPicker()
        {
            var monthList = new List<string>();
            monthList.Add("Jan");
            monthList.Add("Feb");
            monthList.Add("Mar");
            monthList.Add("Apr");
            monthList.Add("May");
            monthList.Add("Jun");
            monthList.Add("Jul");
            monthList.Add("Aug");
            monthList.Add("Sep");
            monthList.Add("Oct");
            monthList.Add("Nov");
            monthList.Add("Dec");
            return monthList;
        }
    }
}
