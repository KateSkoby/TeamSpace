using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TeamSpace.Models;

namespace TeamSpace.Services
{
    /// <summary>
    /// Сервис чтения CSV. Содержит методы и для формы дорог,
    /// и для формы потребительских расходов.
    /// </summary>
    public static class FileParser
    {
        /// <summary>
        /// Старый метод для формы подруги. Его сигнатура сохранена.
        /// </summary>
        public static List<RoadData> LoadRoadDataFromCsv(string filePath)
        {
            string[] lines = ReadLines(filePath);
            char separator = DetectSeparator(lines[0]);

            List<RoadData> result = new List<RoadData>();

            for (int lineIndex = 1; lineIndex < lines.Length; lineIndex++)
            {
                string[] cells = ParseCsvLine(lines[lineIndex], separator)
                    .Select(cell => cell.Trim())
                    .ToArray();

                if (cells.Length < 3)
                {
                    throw new InvalidDataException(
                        "Недостаточно значений в строке " +
                        (lineIndex + 1) + " файла с данными о дорогах."
                    );
                }

                result.Add(new RoadData
                {
                    Year = ParseInteger(cells[0], lineIndex + 1, "Year"),
                    Region = cells[1],
                    BadRoadsPercent = ParseDouble(
                        cells[2],
                        lineIndex + 1,
                        "BadRoadsPercent"
                    )
                });
            }

            return result.OrderBy(item => item.Year).ToList();
        }

        /// <summary>
        /// Новый метод для ConsumerExpensesForm.
        /// </summary>
        public static List<ConsumerExpenseData> LoadConsumerExpensesFromCsv(
            string filePath)
        {
            string[] lines = ReadLines(filePath);
            char separator = DetectSeparator(lines[0]);

            string[] headers = ParseCsvLine(
                    lines[0].TrimStart('\uFEFF'),
                    separator
                )
                .Select(header => header.Trim())
                .ToArray();

            Dictionary<string, int> columnIndexes =
                GetConsumerExpenseColumnIndexes(headers);

            List<ConsumerExpenseData> result =
                new List<ConsumerExpenseData>();

            for (int lineIndex = 1; lineIndex < lines.Length; lineIndex++)
            {
                string[] cells = ParseCsvLine(lines[lineIndex], separator)
                    .Select(cell => cell.Trim())
                    .ToArray();

                if (cells.Length < headers.Length)
                {
                    throw new InvalidDataException(
                        "Недостаточно значений в строке " +
                        (lineIndex + 1) + " файла расходов."
                    );
                }

                result.Add(new ConsumerExpenseData
                {
                    Year = ParseInteger(
                        cells[columnIndexes["Год"]],
                        lineIndex + 1,
                        "Год"
                    ),
                    TotalExpenses = ParseDecimal(
                        cells[columnIndexes[ConsumerExpenseData.TotalIndicator]],
                        lineIndex + 1,
                        ConsumerExpenseData.TotalIndicator
                    ),
                    FoodExpenses = ParseDecimal(
                        cells[columnIndexes[ConsumerExpenseData.FoodIndicator]],
                        lineIndex + 1,
                        ConsumerExpenseData.FoodIndicator
                    ),
                    ClothingAndFootwearExpenses = ParseDecimal(
                        cells[columnIndexes[ConsumerExpenseData.ClothingIndicator]],
                        lineIndex + 1,
                        ConsumerExpenseData.ClothingIndicator
                    ),
                    HousingExpenses = ParseDecimal(
                        cells[columnIndexes[ConsumerExpenseData.HousingIndicator]],
                        lineIndex + 1,
                        ConsumerExpenseData.HousingIndicator
                    ),
                    TransportExpenses = ParseDecimal(
                        cells[columnIndexes[ConsumerExpenseData.TransportIndicator]],
                        lineIndex + 1,
                        ConsumerExpenseData.TransportIndicator
                    ),
                    HealthcareExpenses = ParseDecimal(
                        cells[columnIndexes[ConsumerExpenseData.HealthcareIndicator]],
                        lineIndex + 1,
                        ConsumerExpenseData.HealthcareIndicator
                    ),
                    EducationExpenses = ParseDecimal(
                        cells[columnIndexes[ConsumerExpenseData.EducationIndicator]],
                        lineIndex + 1,
                        ConsumerExpenseData.EducationIndicator
                    )
                });
            }

            return result.OrderBy(item => item.Year).ToList();
        }

        private static string[] ReadLines(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Не указан путь к CSV-файлу.");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("CSV-файл не найден.", filePath);

            string[] lines = File
                .ReadAllLines(filePath, Encoding.UTF8)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToArray();

            if (lines.Length < 2)
            {
                throw new InvalidDataException(
                    "CSV-файл не содержит строк с данными."
                );
            }

            return lines;
        }

        private static Dictionary<string, int> GetConsumerExpenseColumnIndexes(
            string[] headers)
        {
            string[] requiredColumns =
            {
                "Год",
                ConsumerExpenseData.TotalIndicator,
                ConsumerExpenseData.FoodIndicator,
                ConsumerExpenseData.ClothingIndicator,
                ConsumerExpenseData.HousingIndicator,
                ConsumerExpenseData.TransportIndicator,
                ConsumerExpenseData.HealthcareIndicator,
                ConsumerExpenseData.EducationIndicator
            };

            Dictionary<string, int> result =
                headers
                    .Select((header, index) => new { header, index })
                    .ToDictionary(item => item.header, item => item.index);

            foreach (string requiredColumn in requiredColumns)
            {
                if (!result.ContainsKey(requiredColumn))
                {
                    throw new InvalidDataException(
                        "В CSV-файле отсутствует обязательный столбец: " +
                        requiredColumn
                    );
                }
            }

            return result;
        }

        private static char DetectSeparator(string headerLine)
        {
            if (headerLine.Contains(";"))
                return ';';

            if (headerLine.Contains(","))
                return ',';

            throw new InvalidDataException(
                "Не удалось определить разделитель CSV-файла."
            );
        }

        private static List<string> ParseCsvLine(string line, char separator)
        {
            List<string> cells = new List<string>();
            StringBuilder currentCell = new StringBuilder();
            bool insideQuotes = false;

            for (int index = 0; index < line.Length; index++)
            {
                char character = line[index];

                if (character == '"')
                {
                    if (insideQuotes &&
                        index + 1 < line.Length &&
                        line[index + 1] == '"')
                    {
                        currentCell.Append('"');
                        index++;
                    }
                    else
                    {
                        insideQuotes = !insideQuotes;
                    }
                }
                else if (character == separator && !insideQuotes)
                {
                    cells.Add(currentCell.ToString());
                    currentCell.Clear();
                }
                else
                {
                    currentCell.Append(character);
                }
            }

            cells.Add(currentCell.ToString());

            return cells;
        }

        private static int ParseInteger(
            string text,
            int lineNumber,
            string columnName)
        {
            int value;

            if (int.TryParse(text.Trim(), out value))
                return value;

            throw CreateValueException(lineNumber, columnName, text);
        }

        private static decimal ParseDecimal(
            string text,
            int lineNumber,
            string columnName)
        {
            decimal value;
            string preparedText = PrepareNumber(text);

            if (decimal.TryParse(
                preparedText,
                NumberStyles.Any,
                CultureInfo.GetCultureInfo("ru-RU"),
                out value))
            {
                return value;
            }

            if (decimal.TryParse(
                preparedText,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out value))
            {
                return value;
            }

            throw CreateValueException(lineNumber, columnName, text);
        }

        private static double ParseDouble(
            string text,
            int lineNumber,
            string columnName)
        {
            double value;
            string preparedText = PrepareNumber(text);

            if (double.TryParse(
                preparedText,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out value))
            {
                return value;
            }

            if (double.TryParse(
                preparedText,
                NumberStyles.Any,
                CultureInfo.GetCultureInfo("ru-RU"),
                out value))
            {
                return value;
            }

            throw CreateValueException(lineNumber, columnName, text);
        }

        private static string PrepareNumber(string text)
        {
            return text
                .Replace("\u00A0", string.Empty)
                .Replace(" ", string.Empty)
                .Trim();
        }

        private static InvalidDataException CreateValueException(
            int lineNumber,
            string columnName,
            string text)
        {
            return new InvalidDataException(
                "Некорректное значение в строке " + lineNumber +
                ", столбец \"" + columnName + "\": " + text
            );
        }
    }
}
