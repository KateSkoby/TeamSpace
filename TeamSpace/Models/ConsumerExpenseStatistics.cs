namespace TeamSpace.Models
{
    /// Результат расчёта статистики для выбранного вида расходов
    public class ConsumerExpenseStatistics
    {
        public int PeriodCount { get; set; }

        public decimal MaximumPercentageChange { get; set; }

        public decimal MinimumPercentageChange { get; set; }
    }
}
