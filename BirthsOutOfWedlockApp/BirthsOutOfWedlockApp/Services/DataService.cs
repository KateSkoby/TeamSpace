using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BirthsOutOfWedlockApp.Models;

namespace BirthsOutOfWedlockApp.Services
{
    public class DataService
    {
        public List<DataPoint> LoadFromCsv(string filePath)
        {
            var data = new List<DataPoint>();
            var lines = File.ReadAllLines(filePath);

            // Пропускаем заголовок (первая строка)
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');

                if (parts.Length >= 2)
                {
                    // Парсим год
                    if (!int.TryParse(parts[0], out int year))
                        continue;

                    // Берём вторую колонку
                    string percentStr = parts[1].Trim();

                    // Заменяем запятую на точку (24,9 -> 24.9)
                    percentStr = percentStr.Replace(',', '.');

                    // Парсим число с инвариантной культурой (где разделитель - точка)
                    if (double.TryParse(percentStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double percent))
                    {
                        data.Add(new DataPoint { Year = year, Percent = percent });
                    }
                    else
                    {
                        // Если не удалось распарсить, выводим отладочную информацию
                        System.Diagnostics.Debug.WriteLine($"Не удалось распарсить: '{percentStr}'");
                    }
                }
            }
            return data.OrderBy(d => d.Year).ToList();
        }
    }
}