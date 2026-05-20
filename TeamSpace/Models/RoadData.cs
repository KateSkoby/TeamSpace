using System;

namespace TeamSpace.Models
{
    /// Модель данных о состоянии дорог
    public class RoadData
    {
        /// Год 
        public int Year { get; set; }

        /// Регион (субъект РФ)
        public string Region { get; set; }

        /// Процент плохих дорог
        public double BadRoadsPercent { get; set; }
    }
}
