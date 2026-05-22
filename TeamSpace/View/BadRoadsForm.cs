using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TeamSpace.Models;
using TeamSpace.Services;

namespace TeamSpace.View
{
    public partial class BadRoadsForm : Form
    {
        // Хранилище всех загруженных данных
        private List<RoadData> _allData;
        // Текущий выбранный регион
        private string _currentRegion;

        public BadRoadsForm()
        {
            InitializeComponent();
            InitializeCustomSettings();

            // Применяем скругление к кнопкам после инициализации компонентов
            MakeButtonRounded(btnLoadData, 14);
            MakeButtonRounded(btnShowGraph, 14);
            MakeButtonRounded(btnExport, 14);
        }

        // Метод для скругления углов кнопки
        private void MakeButtonRounded(Panel btn, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            btn.Region = new System.Drawing.Region(path);
        }

        // Начальная настройка элементов управления
        private void InitializeCustomSettings()
        {
            // Значения по умолчанию для параметров прогноза
            txtPeriod.Text = "3";
            txtForecastYears.Text = "3";

            // Настройка DataGridView
            dataGridView.AutoGenerateColumns = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
        }

        // Обработчик кнопки "Загрузить данные"
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                // Формируем путь к файлу
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(basePath, "Data", "bad_roads.csv");

                // Альтернативный путь, если первый не сработал
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

                // Загружаем данные через сервис парсинга
                _allData = FileParser.LoadRoadDataFromCsv(filePath);

                // Заполняем ComboBox списком регионов
                var regions = StatisticsCalculator.GetAllRegions(_allData);
                cmbRegions.Items.Clear();
                cmbRegions.Items.AddRange(regions.ToArray());

                // Выбираем первый регион по умолчанию
                if (regions.Count > 0)
                {
                    cmbRegions.SelectedIndex = 0;
                }

                // Отображаем все данные в таблице
                dataGridView.DataSource = _allData.ToList();

                // Рассчитываем и показываем статистику
                DisplayStatistics();

                MessageBox.Show($"✅ Загружено {_allData.Count} записей",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Ошибка при загрузке данных:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик выбора региона
        private void cmbRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRegions.SelectedItem != null && _allData != null)
            {
                _currentRegion = cmbRegions.SelectedItem.ToString();

                // Фильтруем таблицу по выбранному региону
                var regionData = StatisticsCalculator.GetDataByRegion(_allData, _currentRegion);
                dataGridView.DataSource = regionData.ToList();
            }
        }

        // Обработчик кнопки "Показать график"
        private void btnShowGraph_Click(object sender, EventArgs e)
        {
            // Проверка загрузки данных и выбора региона
            if (_allData == null || string.IsNullOrEmpty(_currentRegion))
            {
                MessageBox.Show("Сначала загрузите данные и выберите регион",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Валидация ввода периода (n)
                if (!int.TryParse(txtPeriod.Text, out int period) || period <= 0)
                {
                    MessageBox.Show("Период (n) должен быть положительным числом",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Валидация ввода лет прогноза
                if (!int.TryParse(txtForecastYears.Text, out int forecastYears) || forecastYears <= 0)
                {
                    MessageBox.Show("Количество лет прогноза должно быть положительным числом",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Получаем данные по региону
                var regionData = StatisticsCalculator.GetDataByRegion(_allData, _currentRegion);

                // Подготавливаем массивы для графика
                double[] years = regionData.Select(d => (double)d.Year).ToArray();
                double[] percents = regionData.Select(d => d.BadRoadsPercent).ToArray();

                // Рассчитываем прогноз скользящей средней
                var forecastValues = MovingAverageForecast.CalculateForecast(
                    percents.ToList(), period, forecastYears);

                // Формируем годы для прогноза
                int lastYear = regionData.Last().Year;
                double[] forecastYearsArray = Enumerable.Range(lastYear + 1, forecastYears)
                    .Select(y => (double)y).ToArray();

                // Очищаем график перед отрисовкой
                formsPlot.Plot.Clear();

                // Рисуем исторические данные
                var scatterHistorical = formsPlot.Plot.Add.Scatter(years, percents);
                scatterHistorical.LineWidth = 3;
                scatterHistorical.MarkerSize = 8;
                scatterHistorical.LegendText = "Фактические данные";
                scatterHistorical.Color = ScottPlot.Color.FromColor(System.Drawing.Color.FromArgb(0, 102, 204));

                // Рисуем прогноз
                var scatterForecast = formsPlot.Plot.Add.Scatter(forecastYearsArray, forecastValues.ToArray());
                scatterForecast.LineWidth = 3;
                scatterForecast.MarkerSize = 8;
                scatterForecast.LegendText = $"Прогноз (n={period})";
                scatterForecast.Color = ScottPlot.Color.FromColor(System.Drawing.Color.FromArgb(220, 53, 69));

                // Настраиваем оси и заголовок
                formsPlot.Plot.XLabel("Год");
                formsPlot.Plot.YLabel("Процент плохих дорог (%)");
                formsPlot.Plot.Title($"Динамика состояния дорог в регионе {_currentRegion}");

                // Включаем легенду и сетку
                formsPlot.Plot.Legend.IsVisible = true;
                formsPlot.Plot.Grid.MajorLineColor = ScottPlot.Color.FromColor(System.Drawing.Color.FromArgb(50, System.Drawing.Color.Black));

                // Обновляем график и масштабируем оси
                formsPlot.Refresh();
                formsPlot.Plot.Axes.AutoScale();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Ошибка при построении графика:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Отображение статистики в нижней панели
        private void DisplayStatistics()
        {
            if (_allData == null || _allData.Count == 0)
                return;

            var stats = StatisticsCalculator.FindMaxMinDecrease(_allData);

            // Заполняем левую колонку (Макс. снижение)
            lblMaxRegionVal.Text = $"Регион: {stats.MaxDecreaseRegion}";
            lblMaxPercentVal.Text = $"Снижение: {stats.MaxDecrease:F2}%";

            // Заполняем правую колонку (Мин. снижение)
            lblMinRegionVal.Text = $"Регион: {stats.MinDecreaseRegion}";
            lblMinPercentVal.Text = $"Снижение: {stats.MinDecrease:F2}%";
        }

        // Обработчик экспорта графика
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

                        // Экспортируем график в файл
                        formsPlot.Plot.SavePng(filePath, 1200, 800);

                        MessageBox.Show($"✅ График успешно сохранен:\n{filePath}",
                            "Экспорт", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Ошибка при экспорте:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Метод для отрисовки скругленных кнопок
        private void RoundedButton_Paint(object sender, PaintEventArgs e)
        {
            var button = sender as Panel;
            if (button == null) return;

            int radius = 8;
            var path = new System.Drawing.Drawing2D.GraphicsPath();

            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(button.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(button.Width - radius, button.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, button.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            button.Region = new Region(path);
        }

        // Пустые обработчики для совместимости с дизайнером
        private void lblTitle_Click(object sender, EventArgs e) { }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}