using shift_making_man.Data;
using shift_making_man.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class ShiftSchedulerService
{
    private readonly IDataAccess _dataAccess;

    public ShiftSchedulerService(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public List<Employee> GetAllEmployees()
    {
        return _dataAccess.GetAllEmployees();
    }

    public List<Shift> GetAllShifts()
    {
        return _dataAccess.GetAllShifts();
    }

    public Dictionary<int, List<Shift>> GenerateMonthlySchedule(List<Employee> employees, List<Shift> shifts, DateTime startDate, int daysInMonth)
    {
        Dictionary<int, List<Shift>> schedule = InitializeEmptySchedule(daysInMonth);

        for (int day = 0; day < daysInMonth; day++)
        {
            DateTime currentDate = startDate.AddDays(day);
            GenerateDailySchedule(employees, shifts, currentDate, schedule);
        }

        OptimizeSchedule(schedule, employees, shifts, startDate, daysInMonth);

        return schedule;
    }

    private Dictionary<int, List<Shift>> InitializeEmptySchedule(int daysInMonth)
    {
        var schedule = new Dictionary<int, List<Shift>>();

        for (int i = 0; i < daysInMonth; i++)
        {
            schedule[i] = new List<Shift>();
        }

        return schedule;
    }

    private void GenerateDailySchedule(List<Employee> employees, List<Shift> shifts, DateTime date, Dictionary<int, List<Shift>> schedule)
    {
        foreach (var shift in shifts.Where(s => s.Date == date))
        {
            Employee assignedEmployee = null;

            foreach (var employee in employees)
            {
                if (employee.EmploymentType == "FullTime" &&
                    employee.IsAvailable(date, shift.StartTime.TimeOfDay, shift.EndTime.TimeOfDay) &&
                    employee.GetDailyWorkHours(date) + (shift.EndTime - shift.StartTime).TotalHours <= 8 &&
                    employee.GetWeeklyWorkHours(date) + (shift.EndTime - shift.StartTime).TotalHours <= 40 &&
                    employee.HasWeeklyRestDay(date))
                {
                    assignedEmployee = employee;
                    break;
                }
            }

            if (assignedEmployee != null)
            {
                assignedEmployee.Shifts[date].Add(shift);
                shift.EmployeeId = assignedEmployee.Id;
                schedule[date.Day - 1].Add(shift);
            }
        }
    }

    private void OptimizeSchedule(Dictionary<int, List<Shift>> schedule, List<Employee> employees, List<Shift> shifts, DateTime startDate, int daysInMonth)
    {
        var random = new Random();
        double temperature = 100.0;
        double coolingRate = 0.99;

        while (temperature > 1.0)
        {
            for (int day = 0; day < daysInMonth; day++)
            {
                foreach (var shift in schedule[day])
                {
                    var newEmployee = employees[random.Next(employees.Count)];

                    if (newEmployee.IsAvailable(shift.Date, shift.StartTime.TimeOfDay, shift.EndTime.TimeOfDay) &&
                        newEmployee.GetDailyWorkHours(shift.Date) + (shift.EndTime - shift.StartTime).TotalHours <= 8 &&
                        newEmployee.GetWeeklyWorkHours(shift.Date) + (shift.EndTime - shift.StartTime).TotalHours <= 40 &&
                        newEmployee.HasWeeklyRestDay(shift.Date))
                    {
                        var currentEmployee = employees.FirstOrDefault(e => e.Id == shift.EmployeeId);
                        if (currentEmployee != null)
                        {
                            currentEmployee.Shifts[shift.Date].Remove(shift);
                        }

                        shift.EmployeeId = newEmployee.Id;
                        newEmployee.Shifts[shift.Date].Add(shift);

                        double currentEnergy = CalculateEnergy(schedule);
                        double newEnergy = CalculateEnergy(schedule);

                        if (AcceptanceProbability(currentEnergy, newEnergy, temperature) > random.NextDouble())
                        {
                            schedule[day].Add(shift);
                        }
                        else
                        {
                            // 元に戻す
                            if (currentEmployee != null)
                            {
                                currentEmployee.Shifts[shift.Date].Add(shift);
                            }
                            shift.EmployeeId = currentEmployee?.Id ?? 0;
                            newEmployee.Shifts[shift.Date].Remove(shift);
                        }
                    }
                }
            }

            temperature *= coolingRate;
        }
    }

    private double CalculateEnergy(Dictionary<int, List<Shift>> schedule)
    {
        // エネルギー計算ロジックをここに追加
        double energy = 0.0;
        // シフトの割り当てや拘束条件違反を計算
        return energy;
    }

    private double AcceptanceProbability(double currentEnergy, double newEnergy, double temperature)
    {
        if (newEnergy < currentEnergy)
        {
            return 1.0;
        }
        return Math.Exp((currentEnergy - newEnergy) / temperature);
    }
}
