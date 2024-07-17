using System;
using System.Collections.Generic;

namespace shift_making_man.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string EmploymentType { get; set; }
        public int? StoreId { get; set; }
        public Dictionary<DateTime, List<Shift>> Shifts { get; set; } = new Dictionary<DateTime, List<Shift>>();

        public bool IsAvailable(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            if (Shifts.ContainsKey(date))
            {
                foreach (var shift in Shifts[date])
                {
                    if ((shift.StartTime < endTime && shift.EndTime > startTime))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public double GetDailyWorkHours(DateTime date)
        {
            if (!Shifts.ContainsKey(date)) return 0.0;
            double totalHours = 0.0;
            foreach (var shift in Shifts[date])
            {
                totalHours += (shift.EndTime - shift.StartTime).TotalHours;
            }
            return totalHours;
        }

        public double GetWeeklyWorkHours(DateTime startDate)
        {
            double totalHours = 0.0;
            for (int i = 0; i < 7; i++)
            {
                totalHours += GetDailyWorkHours(startDate.AddDays(i));
            }
            return totalHours;
        }
    }
}
