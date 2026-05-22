using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamSpace.Services
{
    /// <summary>
    /// Общий сервис прогнозирования методом скользящей средней.
    /// double-методы используются формой дорог, decimal-методы — формой расходов.
    /// </summary>
    public static class MovingAverageForecast
    {
        /// <summary>
        /// Метод для формы подруги. Сигнатура сохранена.
        /// </summary>
        public static List<double> CalculateForecast(
            List<double> historicalData,
            int period,
            int forecastSteps)
        {
            ValidateParameters(
                historicalData == null ? 0 : historicalData.Count,
                period,
                forecastSteps
            );

            List<double> workingData =
                new List<double>(historicalData);

            List<double> forecast = new List<double>();

            for (int step = 0; step < forecastSteps; step++)
            {
                double nextValue = workingData
                    .Skip(workingData.Count - period)
                    .Take(period)
                    .Average();

                forecast.Add(nextValue);
                workingData.Add(nextValue);
            }

            return forecast;
        }

        /// <summary>
        /// Метод для формы расходов. Decimal сохраняет точность денежных данных.
        /// </summary>
        public static List<decimal> CalculateForecast(
            List<decimal> historicalData,
            int period,
            int forecastSteps)
        {
            ValidateParameters(
                historicalData == null ? 0 : historicalData.Count,
                period,
                forecastSteps
            );

            List<decimal> workingData =
                new List<decimal>(historicalData);

            List<decimal> forecast = new List<decimal>();

            for (int step = 0; step < forecastSteps; step++)
            {
                decimal nextValue = workingData
                    .Skip(workingData.Count - period)
                    .Take(period)
                    .Average();

                forecast.Add(Math.Round(nextValue, 2));
                workingData.Add(nextValue);
            }

            return forecast;
        }

        public static List<double> CalculateMovingAverage(
            List<double> data,
            int period)
        {
            ValidateHistoricalData(
                data == null ? 0 : data.Count,
                period
            );

            List<double> result = new List<double>();

            for (int index = period - 1; index < data.Count; index++)
            {
                result.Add(
                    data.Skip(index - period + 1)
                        .Take(period)
                        .Average()
                );
            }

            return result;
        }

        public static List<decimal> CalculateMovingAverage(
            List<decimal> data,
            int period)
        {
            ValidateHistoricalData(
                data == null ? 0 : data.Count,
                period
            );

            List<decimal> result = new List<decimal>();

            for (int index = period - 1; index < data.Count; index++)
            {
                result.Add(
                    data.Skip(index - period + 1)
                        .Take(period)
                        .Average()
                );
            }

            return result;
        }

        private static void ValidateParameters(
            int dataCount,
            int period,
            int forecastSteps)
        {
            ValidateHistoricalData(dataCount, period);

            if (forecastSteps <= 0)
            {
                throw new ArgumentException(
                    "Количество шагов прогноза должно быть больше 0."
                );
            }
        }

        private static void ValidateHistoricalData(
            int dataCount,
            int period)
        {
            if (period <= 0)
                throw new ArgumentException("Период должен быть больше 0.");

            if (dataCount < period)
            {
                throw new ArgumentException(
                    "Недостаточно данных для периода " + period + "."
                );
            }
        }
    }
}
