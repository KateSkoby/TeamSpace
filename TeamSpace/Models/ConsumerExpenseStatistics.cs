namespace TeamSpace.Models
{
    /// <summary>
    /// Результат расчёта статистики для выбранного вида расходов.
    /// </summary>
    public class ConsumerExpenseStatistics
    {
        public int PeriodCount { get; set; }

        public decimal MaximumPercentageChange { get; set; }

        public decimal MinimumPercentageChange { get; set; }
    }
}
