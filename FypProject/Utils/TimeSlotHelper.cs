using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Config;
using FypProject.Models;
using Utf8Json.Formatters;
using FypProject.Repository;


namespace FypProject.Utils
{
    public class TimeSlotHelper
    {
        public string Slot { set; get; }
        public string StartTime { set; get; }
        public int SlotKey { set; get; }

        public static List<TimeSlotHelper> returnSlot(string slot)
        {
            string startTime;
            string endTime;
            int start;
            List<TimeSlotHelper> list = new List<TimeSlotHelper>();
            if (slot == "1")
            {
                startTime = SystemData.TimeSlot.SlotOneStart;
                endTime = SystemData.TimeSlot.SlotOneEnd;
                start = HourToInt(startTime);
            }
            else if(slot=="2")
            {
                startTime = SystemData.TimeSlot.SlotTwoStart;
                endTime = SystemData.TimeSlot.SlotTwoEnd;
                start = HourToInt(startTime);
            }
            else
            {
                startTime = SystemData.TimeSlot.SlotThreeStart;
                endTime = SystemData.TimeSlot.SlotThreeEnd;
                start = HourToInt(startTime);
            }
                DateTime starts = DateTime.Parse(startTime);
                DateTime end = DateTime.Parse(endTime);
                if(starts> end)
                {
                    end = end.AddDays(1);
                }
                TimeSpan duration = end.Subtract(starts);
                int dur = CalcStartEndDiff(startTime,endTime);
                Debug.WriteLine("duration calculated=>" + dur);
                for(int i = start; i <=start+dur; i++)
                {
                if (i >= 24)
                {
                    list.Add(new TimeSlotHelper
                    {
                        Slot = new DateTime(2012, 12, 25, 0, 0, 0).ToString("hh:mm tt"),
                        SlotKey = i
                    });
                }
                else
                {
                    list.Add(new TimeSlotHelper
                    {
                        Slot = new DateTime(2012, 12, 25, i, 0, 0).ToString("hh:mm tt"),
                        SlotKey = i
                    });
                }
                   
                }
            return list;
        }

        public static List<TimeSlotHelper> ReturnSpecificTimeSlot(List<Appointment> appt, TimeSlot slot, int duration)
        {
            int start = HourToInt(slot.Start);
            int end = HourToInt(slot.End);
            DateTime slots;
            int differentHour = CalcStartEndDiff(slot.Start, slot.End);
            List<TimeSlotHelper> timeSlotHelpers = new List<TimeSlotHelper>();
            int slotPerHour = 60 / duration;
            int slotDurationCount = 0;
         
            for (int i = start; i < start + differentHour; i++)
            {
                

                for (int j = 0; j < slotPerHour; j++)
                {
                    if(j == 0)
                    {
                        slots = new DateTime(2012, 12, 25, i, 0, 0);
                        if (appt.Where(c => c.StartTime == slots.ToString("hh:mm tt")).Any())
                        {
                            continue;
                        }
                        timeSlotHelpers.Add(new TimeSlotHelper
                        {
                            StartTime = slots.ToString("hh:mm tt"),
                            Slot = slots.ToString("hh:mm tt") + "-" + slots.AddMinutes(duration).ToString("hh:mm tt"),
                            SlotKey = i
                        });
                    }
                    else
                    {
                        slotDurationCount = duration * j;
                        slots = new DateTime(2012, 12, 25, i, slotDurationCount, 0);                        
                        if(appt.Where(c=>c.StartTime == slots.ToString("hh:mm tt")).Any())
                        {
                            continue;
                        }
                        else
                        {
                            timeSlotHelpers.Add(new TimeSlotHelper
                            {
                                StartTime = slots.ToString("hh:mm tt"),
                                Slot = slots.ToString("hh:mm tt") + "-"+slots.AddMinutes(duration).ToString("hh:mm tt"),
                                SlotKey = i
                            });
                        }

                    }
                    slotDurationCount = 0;
                }
                if ((end - i) == 1)
                {
                    if(end == 24)
                    {
                        end = 0;
                    }
                    timeSlotHelpers.Add(new TimeSlotHelper
                    {
                        Slot = new DateTime(2012, 12, 25, end, 0, 0).ToString("hh:mm tt"),
                        SlotKey = i
                    });
                }
            }
            return timeSlotHelpers;
        }
  
        public static void UpdateTimeSlot(TimeSlot obj, IGenericRepository<TimeSlot> timeSlotRepository)
        {
            string startTime;
            string endTime;
               
            startTime = new DateTime(2012, 12, 25, int.Parse(obj.Start), 0, 0).ToString("hh:mm tt");
            Debug.WriteLine("end Time=>" + int.Parse(obj.End));
            if (int.Parse(obj.End) == 24)
            {
                obj.End = "0";
            }
            endTime = new DateTime(2012, 12, 25, int.Parse(obj.End), 0, 0).ToString("hh:mm tt");
            var timeSlot = new TimeSlot
            {
                Id = obj.Id,
                Slot = obj.Slot,
                Start = startTime,
                End = endTime
            };
            timeSlotRepository.Update(timeSlot);
        }

        public static bool ReturnCheckIn(string date, string startTime)
        {
            bool isToday = TimeSlotHelper.ReturnTodayDate(date);
            String now = DateTime.Now.ToString("HH:mm tt");
            DateTime timeNow = DateTime.Parse(now);
            DateTime startNow;
            if (!string.IsNullOrEmpty(startTime))
            {
                startNow = DateTime.Parse(startTime);
                bool isOverTime = ReturnIfOverTime(startNow, timeNow, isToday);
                if (isToday)
                {
                    if (isOverTime) // appt date is today and appt time already over the current time
                    {
                        return false; // cannot check in
                    }
                    else
                    {
                        if (ReturnHourDiff(startTime) <= 30) // if current time subtract appt time smaller equal 30mins
                        {
                            Debug.WriteLine("return true");
                            return true; //allow check in
                        }
                        else
                        {
                            return false; //not allow
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("appointment date is not today");
                    return false;

                }
            }
            return false;
            
        }
        public static bool ReturnIfOverTime(DateTime startTime, DateTime timeNow, bool isToday)
        {
            TimeSpan duration;
            int different = 0;
            if (isToday && (startTime < timeNow)) //appt time already past current time
            {
                duration= timeNow.Subtract(startTime);
                different = (int)Math.Ceiling(duration.TotalMinutes);
                return true;
            }
            else if(isToday && (startTime>timeNow)) //appt time havent past current time
            {
                duration = startTime.Subtract(timeNow);
                different = (int)Math.Ceiling(duration.TotalMinutes);
                return false;
            }
            return false;
        }
        public static bool ReturnIfOverTime(DateTime timeNow, bool isToday)
        {
            if (isToday)
            {
                return false;
            }
            return true;
        }

        public static bool ReturnOneDayBefore(string date)
        {
            DateTime apptDate = Convert.ToDateTime(date);
            DateTime today = DateTime.Today;
            int dayInt = int.Parse((apptDate - today).TotalDays.ToString());
            if (dayInt == 1) return true; // 1 day before
            return false;
        }

        public static bool ReturnTodayDate(string date)
        {
            DateTime apptDate = Convert.ToDateTime(date);
            DateTime today = DateTime.Today;
            if (apptDate == today) return true;
            return false;
        }

        public static bool ReturnPastTodayDate(string date)
        {
            DateTime apptDate = Convert.ToDateTime(date);
            DateTime today = DateTime.Today;
            if (today > apptDate) return true;
            return false;
        }

        public static int ReturnHourDiff(string startTime)
        {
            String now = DateTime.Now.ToString("HH:mm tt");

            DateTime timeNow = DateTime.Parse(now);
            DateTime startNow = DateTime.Parse(startTime);

            TimeSpan duration;
            if (startNow > timeNow)//means it is midnight
            {
                duration = startNow.Subtract(timeNow);
            }
            else { duration = timeNow.Subtract(startNow); }
            Debug.WriteLine("duration time of appt=>" + (int)Math.Ceiling(duration.TotalMinutes));
            return (int)Math.Ceiling(duration.TotalMinutes);
        }

        private static int CalcStartEndDiff(string startTime, string endTime)
        {
            DateTime starts = DateTime.Parse(startTime);
            DateTime end = DateTime.Parse(endTime);
            string formattedDate = DateTime.Parse(startTime).ToString("HH");

            //Debug.WriteLine("formatted hour=>" + formattedDate.TrimStart('0'));
            if (starts > end)
            {
                end = end.AddDays(1);
            }
            TimeSpan duration = end.Subtract(starts);
            return (int)Math.Ceiling(duration.TotalHours);
        }

        private static int HourToInt(string hour)
        {
            string formattedHourString = DateTime.Parse(hour).ToString("HH");
            if (formattedHourString.StartsWith("0") && formattedHourString.EndsWith("0"))
            {
                formattedHourString = "24";
            }
            int formattedHours = int.Parse(formattedHourString.TrimStart('0'));
            return formattedHours;
        }

    }
}
