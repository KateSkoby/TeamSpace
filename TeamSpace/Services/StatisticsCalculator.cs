using System;
using System.Collections.Generic;
using System.Linq;
using TeamSpace.Models;

namespace TeamSpace.Services
{
    /// Сервис для расчета статистики по данным о дорогах
    public static class StatisticsCalculator
    {
        /// Находит регион с максимальным и минимальным снижением процента плохих дорог
        public static (string MaxDecreaseRegion, double MaxDecrease, string MinDecreaseRegion, double MinDecrease)
            FindMaxMinDecrease(List<RoadData> data)
        {
            // Получаем уникальные регионы
            var regions = data.Select(d => d.Region).Distinct();
            var decreases = new List<(string Region, double Decrease)>();

            foreach (var region in regions)
            {
                // Получаем данные по региону, отсортированные по годам
                var regionData = data
                    .Where(d => d.Region == region)
                    .OrderBy(d => d.Year)
                    .ToList();

                if (regionData.Count < 2)
                    continue;

                // Берем первый и последний год
                var firstYear = regionData.First();
                var lastYear = regionData.Last();

                // Считаем снижение (положительное число = дороги улучшились)
                var decrease = firstYear.BadRoadsPercent - lastYear.BadRoadsPercent;

                if (decrease > 0) // Только если есть снижение
                {
                    decreases.Add((region, decrease));
                }
            }

            if (decreases.Count == 0)
                return ("Нет данных", 0, "Нет данных", 0);

            // Находим максимум и минимум
            var maxDecrease = decreases.OrderByDescending(x => x.Decrease).First();
            var minDecrease = decreases.OrderBy(x => x.Decrease).First();

            return (maxDecrease.Region, maxDecrease.Decrease, minDecrease.Region, minDecrease.Decrease);
        }

        /// Получает данные по конкретному региону
        public static List<RoadData> GetDataByRegion(List<RoadData> allData, string region)
        {
            return allData
                .Where(d => d.Region == region)
                .OrderBy(d => d.Year)
                .ToList();
        }

        /// Получает список всех регионов
        public static List<string> GetAllRegions(List<RoadData> data)
        {
            return data.Select(d => d.Region).Distinct().OrderBy(r => r).ToList();
        }
    }
}
