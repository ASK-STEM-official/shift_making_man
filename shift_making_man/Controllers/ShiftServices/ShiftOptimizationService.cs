using System;
using System.Collections.Generic;
using shift_making_man.Models;

namespace shift_making_man.Controllers.ShiftServices
{
    public class ShiftOptimizationService
    {
        private Random _random = new Random();

        public List<Shift> SimulatedAnnealingOptimize(List<Shift> shifts)
        {
            Console.WriteLine("シフトの最適化を開始します...");

            var optimizedShifts = new List<Shift>(shifts);
            var temperature = 1000.0;
            var coolingRate = 0.995;

            while (temperature > 1)
            {
                var newShifts = new List<Shift>(optimizedShifts);
                SwapShifts(newShifts);

                var currentCost = CalculateTotalCost(optimizedShifts);
                var newCost = CalculateTotalCost(newShifts);

                if (AcceptanceProbability(currentCost, newCost, temperature) > _random.NextDouble())
                {
                    optimizedShifts = new List<Shift>(newShifts);
                }

                temperature *= coolingRate;
            }

            Console.WriteLine("シフトの最適化が完了しました。");
            return optimizedShifts;
        }

        private void SwapShifts(List<Shift> shifts)
        {
            if (shifts.Count < 2)
            {
                return; // リストに要素が2つ以上なければ交換しない
            }

            var index1 = _random.Next(shifts.Count);
            var index2 = _random.Next(shifts.Count);

            while (index1 == index2)
            {
                index2 = _random.Next(shifts.Count); // 同じインデックスを避けるため再生成
            }

            var temp = shifts[index1];
            shifts[index1] = shifts[index2];
            shifts[index2] = temp;
        }

        private double CalculateTotalCost(List<Shift> shifts)
        {
            // 仮のコスト計算例を具体化
            double totalCost = 0.0;
            foreach (var shift in shifts)
            {
                // コスト計算の例:
                // 1. スタッフのシフト時間の総和が長いほどコストが低い
                // 2. シフトの重複があるとコストが高くなる
                // 3. 開始時間が遅いほどコストが低い

                double shiftDuration = (shift.EndTime - shift.StartTime).TotalHours;
                double shiftStartCost = shift.StartTime.TotalHours;

                totalCost += shiftDuration + shiftStartCost;

                // シフトの重複チェック (重複がある場合コストを増加)
                foreach (var otherShift in shifts)
                {
                    if (shift.ShiftID != otherShift.ShiftID &&
                        shift.ShiftDate == otherShift.ShiftDate &&
                        shift.StartTime < otherShift.EndTime &&
                        shift.EndTime > otherShift.StartTime)
                    {
                        totalCost += 10.0; // 重複ペナルティ
                    }
                }
            }
            return totalCost;
        }

        private double AcceptanceProbability(double currentCost, double newCost, double temperature)
        {
            if (newCost < currentCost)
                return 1.0;
            return Math.Exp((currentCost - newCost) / temperature);
        }
    }
}
