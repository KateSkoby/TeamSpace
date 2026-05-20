using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamSpace.Services
{
    /// Сервис для прогнозирования методом скользящей средней
    public static class MovingAverageForecast
    {
        /// Рассчитывает прогноз на N шагов вперед методом скользящей средней
        public static List<double> CalculateForecast(List<double> historicalData, int period, int forecastSteps)
        {
            if (historicalData == null || historicalData.Count < period)
                throw new ArgumentException($"Недостаточно данных для периода {period}");

            if (period <= 0)
                throw new ArgumentException("Период должен быть больше 0");

            if (forecastSteps <= 0)
                throw new ArgumentException("Количество шагов прогноза должно быть больше 0");

            // Создаем копию исторических данных для работы
            var workingData = new List<double>(historicalData);
            var forecast = new List<double>();

            // Рассчитываем прогноз на каждый шаг
            for (int step = 0; step < forecastSteps; step++)
            {
                // Берем последние 'period' значений
                var window = workingData.Skip(Math.Max(0, workingData.Count - period)).ToList();

                // Считаем среднее
                double nextValue = window.Average();

                // Добавляем в прогноз
                forecast.Add(nextValue);

                // Добавляем рассчитанное значение в рабочий список для следующего шага
                workingData.Add(nextValue);
            }

            return forecast;
        }

        /// Рассчитывает скользящую среднюю для исторических данных
        public static List<double> CalculateMovingAverage(List<double> data, int period)
        {
            var result = new List<double>();

            for (int i = period - 1; i < data.Count; i++)
            {
                var window = data.Skip(i - period + 1).Take(period);
                result.Add(window.Average());
            }

            return result;
        }
    }
}
