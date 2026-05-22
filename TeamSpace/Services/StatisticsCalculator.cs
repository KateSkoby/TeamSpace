using System.Collections.Generic;
using System.Linq;
using TeamSpace.Models;

namespace TeamSpace.Services
{
    /// <summary>
    /// Общий сервис статистики для двух форм.
    /// </summary>
    public static class StatisticsCalculator
    {
        /// <summary>
        /// Метод формы подруги. Сигнатура и назначение сохранены.
        /// </summary>
        public static (
            string MaxDecreaseRegion,
            double MaxDecrease,
            string MinDecreaseRegion,
            double MinDecrease)
            FindMaxMinDecrease(List<RoadData> data)
        {
            if (data == null || data.Count == 0)
                return ("Нет данных", 0, "Нет данных", 0);

            IEnumerable<string> regions = data
                .Select(item => item.Region)
                .Distinct();

            List<(string Region, double Decrease)> decreases =
                new List<(string Region, double Decrease)>();

            foreach (string region in regions)
            {
                List<RoadData> regionData = data
                    .Where(item => item.Region == region)
                    .OrderBy(item => item.Year)
                    .ToList();

                if (regionData.Count < 2)
                    continue;

                double decrease =
                    regionData.First().BadRoadsPercent -
                    regionData.Last().BadRoadsPercent;

                if (decrease > 0)
                    decreases.Add((region, decrease));
            }

            if (decreases.Count == 0)
                return ("Нет данных", 0, "Нет данных", 0);

            var maximum = decreases
                .OrderByDescending(item => item.Decrease)
                .First();

            var minimum = decreases
                .OrderBy(item => item.Decrease)
                .First();

            return (
                maximum.Region,
                maximum.Decrease,
                minimum.Region,
                minimum.Decrease
            );
        }

        public static List<RoadData> GetDataByRegion(
            List<RoadData> allData,
            string region)
        {
            if (allData == null)
                return new List<RoadData>();

            return allData
                .Where(item => item.Region == region)
                .OrderBy(item => item.Year)
                .ToList();
        }

        public static List<string> GetAllRegions(List<RoadData> data)
        {
            if (data == null)
                return new List<string>();

            return data
                .Select(item => item.Region)
                .Distinct()
                .OrderBy(region => region)
                .ToList();
        }

        /// <summary>
        /// Новый метод для карточек статистики ConsumerExpensesForm.
        /// Возвращает максимальное и минимальное изменение выбранного
        /// показателя относительно предыдущего года в процентах.
        /// </summary>
        public static ConsumerExpenseStatistics CalculateExpenseStatistics(
            List<ConsumerExpenseData> data,
            string indicatorName)
        {
            ConsumerExpenseStatistics result =
                new ConsumerExpenseStatistics();

            if (data == null || data.Count == 0)
                return result;

            List<ConsumerExpenseData> orderedData = data
                .OrderBy(item => item.Year)
                .ToList();

            result.PeriodCount = orderedData.Count;

            if (orderedData.Count < 2)
                return result;

            List<decimal> percentageChanges = new List<decimal>();

            for (int index = 1; index < orderedData.Count; index++)
            {
                decimal previousValue =
                    orderedData[index - 1].GetIndicatorValue(indicatorName);

                decimal currentValue =
                    orderedData[index].GetIndicatorValue(indicatorName);

                if (previousValue == 0)
                    continue;

                decimal change =
                    (currentValue - previousValue) /
                    previousValue * 100M;

                percentageChanges.Add(change);
            }

            if (percentageChanges.Count == 0)
                return result;

            result.MaximumPercentageChange = percentageChanges.Max();
            result.MinimumPercentageChange = percentageChanges.Min();

            return result;
        }
    }
}
