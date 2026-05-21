using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TeamSpace.Models;
using TeamSpace.Services;

namespace TeamSpace.View
{
    public partial class BadRoadsForm : Form
    {
        // Поле для хранения всех загруженных данных о дорогах
        private List<RoadData> _allData;

        // Поле для хранения текущего выбранного региона
        private string _currentRegion;

        // Конструктор формы
        public BadRoadsForm()
        {
            InitializeComponent();
            InitializeCustomSettings();
        }

        // Метод для начальной настройки элементов управления
        private void InitializeCustomSettings()
        {
            // Устанавливаем значения по умолчанию для параметров прогноза
            txtPeriod.Text = "3";
            txtForecastYears.Text = "3";

            // Настраиваем DataGridView: автогенерация колонок, выбор всей строки, запрет множественного выбора
            dataGridView.AutoGenerateColumns = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
        }

        // Обработчик кнопки "Загрузить данные"
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                // Формируем путь к файлу данных
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(basePath, "Data", "bad_roads.csv");

                // Если файл не найден по основному пути, пробуем альтернативный
                if (!File.Exists(filePath))
                {
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "bad_roads.csv");
                }

                // Если файл так и не найден - показываем ошибку
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Файл данных не найден!\nПроверьте путь к bad_roads.csv",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Загружаем данные из CSV файла через сервис парсинга
                _allData = FileParser.LoadRoadDataFromCsv(filePath);

                // Получаем список регионов и заполняем ComboBox
                var regions = StatisticsCalculator.GetAllRegions(_allData);
                cmbRegions.Items.Clear();
                cmbRegions.Items.AddRange(regions.ToArray());

                // Выбираем первый регион по умолчанию, если список не пуст
                if (regions.Count > 0)
                {
                    cmbRegions.SelectedIndex = 0;
                }

                // Отображаем все данные в таблице
                dataGridView.DataSource = _allData.ToList();

                // Рассчитываем и показываем статистику
                DisplayStatistics();

                // Показываем сообщение об успешной загрузке
                MessageBox.Show($"Загружено {_allData.Count} записей",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Обработка ошибок при загрузке
                MessageBox.Show($"Ошибка при загрузке данных:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик изменения выбранного региона в ComboBox
        private void cmbRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Если регион выбран и данные загружены
            if (cmbRegions.SelectedItem != null && _allData != null)
            {
                // Сохраняем выбранный регион
                _currentRegion = cmbRegions.SelectedItem.ToString();

                // Фильтруем данные по выбранному региону и обновляем таблицу
                var regionData = StatisticsCalculator.GetDataByRegion(_allData, _currentRegion);
                dataGridView.DataSource = regionData.ToList();
            }
        }

        // Обработчик кнопки "Показать график"
        private void btnShowGraph_Click(object sender, EventArgs e)
        {
            // Проверяем, что данные загружены и регион выбран
            if (_allData == null || string.IsNullOrEmpty(_currentRegion))
            {
                MessageBox.Show("Сначала загрузите данные и выберите регион",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Валидация ввода: проверяем, что период (n) - положительное число
                if (!int.TryParse(txtPeriod.Text, out int period) || period <= 0)
                {
                    MessageBox.Show("Период (n) должен быть положительным числом",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Валидация ввода: проверяем, что количество лет прогноза - положительное число
                if (!int.TryParse(txtForecastYears.Text, out int forecastYears) || forecastYears <= 0)
                {
                    MessageBox.Show("Количество лет прогноза должно быть положительным числом",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Получаем данные по выбранному региону
                var regionData = StatisticsCalculator.GetDataByRegion(_allData, _currentRegion);

                // Подготовка массивов для графика: годы и проценты плохих дорог
                double[] years = regionData.Select(d => (double)d.Year).ToArray();
                double[] percents = regionData.Select(d => d.BadRoadsPercent).ToArray();

                // Рассчитываем прогноз методом скользящей средней
                var forecastValues = MovingAverageForecast.CalculateForecast(
                    percents.ToList(), period, forecastYears);

                // Формируем массив годов для прогноза (следующие после последнего года)
                int lastYear = regionData.Last().Year;
                double[] forecastYearsArray = Enumerable.Range(lastYear + 1, forecastYears)
                    .Select(y => (double)y).ToArray();

                // Очищаем график перед построением новых данных
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

                // Настраиваем подписи осей и заголовок графика
                formsPlot.Plot.XLabel("Год");
                formsPlot.Plot.YLabel("Процент плохих дорог (%)");
                formsPlot.Plot.Title($"Динамика состояния дорог в регионе {_currentRegion}");

                // Включаем отображение легенды и настраиваем сетку
                formsPlot.Plot.Legend.IsVisible = true;
                formsPlot.Plot.Grid.MajorLineColor = ScottPlot.Color.FromColor(System.Drawing.Color.FromArgb(50, System.Drawing.Color.Black));

                // Обновляем график
                formsPlot.Refresh();

                // Автоматически масштабируем оси под данные
                formsPlot.Plot.Axes.AutoScale();
            }
            catch (Exception ex)
            {
                // Обработка ошибок при построении графика
                MessageBox.Show($"Ошибка при построении графика:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Метод для расчета и отображения статистики
        private void DisplayStatistics()
        {
            // Если данные не загружены - ничего не делаем
            if (_allData == null || _allData.Count == 0)
                return;

            // Рассчитываем статистику: регионы с макс. и мин. снижением процента плохих дорог
            var stats = StatisticsCalculator.FindMaxMinDecrease(_allData);

            // Формируем текст для отображения статистики
            string statsText = $"📊 СТАТИСТИКА ЗА 15 ЛЕТ (2010-2024):\n\n" +
                             $"✅ Максимальное снижение:\n" +
                             $"   Регион: {stats.MaxDecreaseRegion}\n" +
                             $"   Снижение: {stats.MaxDecrease:F2}%\n\n" +
                             $"📉 Минимальное снижение:\n" +
                             $"   Регион: {stats.MinDecreaseRegion}\n" +
                             $"   Снижение: {stats.MinDecrease:F2}%";

            // Обновляем текст в Label статистики
            lblStatistics.Text = statsText;
        }

        // Обработчик кнопки "Экспорт графика"
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // Открываем диалог сохранения файла
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
                    saveDialog.Title = "Экспорт графика";
                    saveDialog.FileName = $"roads_forecast_{_currentRegion ?? "data"}";

                    // Если пользователь выбрал файл для сохранения
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveDialog.FileName;

                        // Экспортируем график в файл с разрешением 1200x800
                        formsPlot.Plot.SavePng(filePath, 1200, 800);

                        // Показываем сообщение об успешном экспорте
                        MessageBox.Show($"График успешно сохранен:\n{filePath}",
                            "Экспорт", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок при экспорте
                MessageBox.Show($"Ошибка при экспорте:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Пустые обработчики событий
        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
