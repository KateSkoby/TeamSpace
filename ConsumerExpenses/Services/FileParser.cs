using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TeamSpace.Models;

namespace TeamSpace.Services
{
    /// Класс для чтения и парсинга CSV файлов
    public static class FileParser
    {
        /// Загружает данные о дорогах из CSV файла
        public static List<RoadData> LoadRoadDataFromCsv(string filePath)
        {
            var result = new List<RoadData>();

            // Читаем все строки из файла
            var lines = File.ReadAllLines(filePath);

            // Пропускаем заголовок (первая строка)
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                if (string.IsNullOrEmpty(line))
                    continue;

                // Разделяем по запятой
                var parts = line.Split(',');

                if (parts.Length >= 3)
                {
                    result.Add(new RoadData
                    {
                        Year = int.Parse(parts[0].Trim()),
                        Region = parts[1].Trim(),
                        BadRoadsPercent = double.Parse(parts[2].Trim(), CultureInfo.InvariantCulture)
                    });
                }
            }

            return result;
        }
    }
}
