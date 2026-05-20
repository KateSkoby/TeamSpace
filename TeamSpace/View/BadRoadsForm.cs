using ScottPlot;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TeamSpace.Models;
using TeamSpace.Services;
using ImageFormat = System.Drawing.Imaging.ImageFormat; // Явно указываем пространство имен

namespace TeamSpace.View
{
    public partial class BadRoadsForm : Form
    {
        private List<RoadData> _allData;
        private string _currentRegion;

        public BadRoadsForm()
        {
            InitializeComponent();
            InitializeCustomSettings();
        }

        private void InitializeCustomSettings()
        {
            txtPeriod.Text = "3";
            txtForecastYears.Text = "3";

            dataGridView.AutoGenerateColumns = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(basePath, "Data", "bad_roads.csv");

                if (!File.Exists(filePath))
                {
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "bad_roads.csv");
                }

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Файл данных не найден!\nПроверьте путь к bad_roads.csv",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _allData = FileParser.LoadRoadDataFromCsv(filePath);

                var regions = StatisticsCalculator.GetAllRegions(_allData);
                cmbRegions.Items.Clear();
                cmbRegions.Items.AddRange(regions.ToArray());

                if (regions.Count > 0)
                {
                    cmbRegions.SelectedIndex = 0;
                }

                dataGridView.DataSource = _allData.ToList();
                DisplayStatistics();

                MessageBox.Show($"Загружено {_allData.Count} записей",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRegions.SelectedItem != null && _allData != null)
            {
                _currentRegion = cmbRegions.SelectedItem.ToString();
                var regionData = StatisticsCalculator.GetDataByRegion(_allData, _currentRegion);
                dataGridView.DataSource = regionData.ToList();
            }
        }

        private void btnShowGraph_Click(object sender, EventArgs e)
        {
            if (_allData == null || string.IsNullOrEmpty(_currentRegion))
            {
                MessageBox.Show("Сначала загрузите данные и выберите регион",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!int.TryParse(txtPeriod.Text, out int period) || period <= 0)
                {
                    MessageBox.Show("Период (n) должен быть положительным числом",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtForecastYears.Text, out int forecastYears) || forecastYears <= 0)
                {
                    MessageBox.Show("Количество лет прогноза должно быть положительным числом",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var regionData = StatisticsCalculator.GetDataByRegion(_allData, _currentRegion);

                double[] years = regionData.Select(d => (double)d.Year).ToArray();
                double[] percents = regionData.Select(d => d.BadRoadsPercent).ToArray();

                var forecastValues = MovingAverageForecast.CalculateForecast(
                    percents.ToList(), period, forecastYears);

                int lastYear = regionData.Last().Year;
                double[] forecastYearsArray = Enumerable.Range(lastYear + 1, forecastYears)
                    .Select(y => (double)y).ToArray();

                formsPlot.Plot.Clear();

                // ScottPlot 5 - используем Add.Scatter
                var scatterHistorical = formsPlot.Plot.Add.Scatter(years, percents);
                scatterHistorical.LineWidth = 3;
                scatterHistorical.MarkerSize = 8;
                scatterHistorical.LegendText = "Фактические данные";  // ИСПРАВЛЕНО: LegendText вместо Label
                scatterHistorical.Color = ScottPlot.Color.FromColor(System.Drawing.Color.FromArgb(0, 102, 204));  // ИСПРАВЛЕНО: преобразование цвета

                var scatterForecast = formsPlot.Plot.Add.Scatter(forecastYearsArray, forecastValues.ToArray());
                scatterForecast.LineWidth = 3;
                scatterForecast.MarkerSize = 8;
                scatterForecast.LegendText = $"Прогноз (n={period})";  // ИСПРАВЛЕНО: LegendText вместо Label
                scatterForecast.Color = ScottPlot.Color.FromColor(System.Drawing.Color.FromArgb(220, 53, 69));  // ИСПРАВЛЕНО: преобразование цвета

                formsPlot.Plot.XLabel("Год");
                formsPlot.Plot.YLabel("Процент плохих дорог (%)");
                formsPlot.Plot.Title($"Динамика состояния дорог в регионе {_currentRegion}");

                // ScottPlot 5 - Legend.IsVisible вместо Legend.Visible
                formsPlot.Plot.Legend.IsVisible = true;  // ИСПРАВЛЕНО: IsVisible вместо Visible
                formsPlot.Plot.Grid.MajorLineColor = ScottPlot.Color.FromColor(System.Drawing.Color.FromArgb(50, System.Drawing.Color.Black));

                formsPlot.Refresh();

                // ScottPlot 5 - AxisAuto() заменено на AutoScale()
                formsPlot.Plot.Axes.AutoScale();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при построении графика:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayStatistics()
        {
            if (_allData == null || _allData.Count == 0)
                return;

            var stats = StatisticsCalculator.FindMaxMinDecrease(_allData);

            string statsText = $"📊 СТАТИСТИКА ЗА 15 ЛЕТ (2010-2024):\n\n" +
                             $"✅ Максимальное снижение:\n" +
                             $"   Регион: {stats.MaxDecreaseRegion}\n" +
                             $"   Снижение: {stats.MaxDecrease:F2}%\n\n" +
                             $"📉 Минимальное снижение:\n" +
                             $"   Регион: {stats.MinDecreaseRegion}\n" +
                             $"   Снижение: {stats.MinDecrease:F2}%";

            lblStatistics.Text = statsText;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
                    saveDialog.Title = "Экспорт графика";
                    saveDialog.FileName = $"roads_forecast_{_currentRegion ?? "data"}";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveDialog.FileName;

                        // ScottPlot 5 - используем SavePng вместо SaveFig
                        formsPlot.Plot.SavePng(filePath, 1200, 800);

                        MessageBox.Show($"График успешно сохранен:\n{filePath}",
                            "Экспорт", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}