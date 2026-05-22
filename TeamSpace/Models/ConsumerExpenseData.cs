using System;
using System.ComponentModel;

namespace TeamSpace.Models
{
    /// <summary>
    /// Одна строка таблицы потребительских расходов.
    /// </summary>
    public class ConsumerExpenseData
    {
        public const string TotalIndicator = "Общие расходы";
        public const string FoodIndicator = "Продукты питания";
        public const string ClothingIndicator = "Одежда и обувь";
        public const string HousingIndicator = "Жилищные услуги";
        public const string TransportIndicator = "Транспорт";
        public const string HealthcareIndicator = "Здравоохранение";
        public const string EducationIndicator = "Образование";

        public static readonly string[] IndicatorNames =
        {
            TotalIndicator,
            FoodIndicator,
            ClothingIndicator,
            HousingIndicator,
            TransportIndicator,
            HealthcareIndicator,
            EducationIndicator
        };

        [DisplayName("Год")]
        public int Year { get; set; }

        [DisplayName(TotalIndicator)]
        public decimal TotalExpenses { get; set; }

        [DisplayName(FoodIndicator)]
        public decimal FoodExpenses { get; set; }

        [DisplayName(ClothingIndicator)]
        public decimal ClothingAndFootwearExpenses { get; set; }

        [DisplayName(HousingIndicator)]
        public decimal HousingExpenses { get; set; }

        [DisplayName(TransportIndicator)]
        public decimal TransportExpenses { get; set; }

        [DisplayName(HealthcareIndicator)]
        public decimal HealthcareExpenses { get; set; }

        [DisplayName(EducationIndicator)]
        public decimal EducationExpenses { get; set; }

        /// <summary>
        /// Возвращает значение показателя, который выбран в ComboBox формы.
        /// </summary>
        public decimal GetIndicatorValue(string indicatorName)
        {
            switch (indicatorName)
            {
                case TotalIndicator:
                    return TotalExpenses;

                case FoodIndicator:
                    return FoodExpenses;

                case ClothingIndicator:
                    return ClothingAndFootwearExpenses;

                case HousingIndicator:
                    return HousingExpenses;

                case TransportIndicator:
                    return TransportExpenses;

                case HealthcareIndicator:
                    return HealthcareExpenses;

                case EducationIndicator:
                    return EducationExpenses;

                default:
                    throw new ArgumentException(
                        "Неизвестный показатель расходов: " + indicatorName,
                        nameof(indicatorName)
                    );
            }
        }
    }
}
