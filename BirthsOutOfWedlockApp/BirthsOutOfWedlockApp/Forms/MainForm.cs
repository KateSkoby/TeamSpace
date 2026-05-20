using BirthsOutOfWedlockApp.Models;
using BirthsOutOfWedlockApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.DataVisualization.Charting;

namespace BirthsOutOfWedlockApp.Forms
{
    public partial class MainForm : Form
    {
        private List<BirthsOutOfWedlockApp.Models.DataPoint> _data;
        private DataGridView dgvData;
        private Chart chartMain;
        private TextBox txtResults;
        private string _currentFilePath = "";
        private NumericUpDown nudForecastYears;

        public MainForm()
        {
            InitializeComponent();
            this.Text = "Анализ детей, рожденных вне брака (Вариант 17)";
            this.Size = new System.Drawing.Size(1200, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.White;
            BuildUI();
        }

        private void BuildUI()
        {
            // ===== ЗАГОЛОВОК =====
            Label lblTitle = new Label
            {
                Text = "Анализ детей, рожденных вне брака (Вариант 17)",
                Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.DarkBlue,
                Location = new System.Drawing.Point(300, 15),
                Size = new System.Drawing.Size(600, 35),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitle);

            // ===== ПАНЕЛЬ С КНОПКАМИ =====
            Panel buttonPanel = new Panel
            {
                Location = new System.Drawing.Point(50, 60),
                Size = new System.Drawing.Size(1100, 50),
                BackColor = System.Drawing.Color.LightGray
            };

            // Кнопка 1: Загрузить файл
            Button btnLoad = new Button
            {
                Text = "1. Загрузить данные",
                Location = new System.Drawing.Point(10, 8),
                Size = new System.Drawing.Size(150, 35),
                BackColor = System.Drawing.Color.LightBlue,
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
            };
            btnLoad.Click += BtnLoad_Click;

            // Кнопка 2: Показать таблицу
            Button btnShowTable = new Button
            {
                Text = "2. Показать таблицу",
                Location = new System.Drawing.Point(170, 8),
                Size = new System.Drawing.Size(150, 35),
                BackColor = System.Drawing.Color.LightGreen,
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
            };
            btnShowTable.Click += BtnShowTable_Click;

            // Кнопка 3: Макс/мин % изменения
            Button btnCalc = new Button
            {
                Text = "3. Макс/мин % изменения",
                Location = new System.Drawing.Point(330, 8),
                Size = new System.Drawing.Size(170, 35),
                BackColor = System.Drawing.Color.LightYellow,
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
            };
            btnCalc.Click += BtnCalc_Click;

            // Кнопка 4: Построить график
            Button btnChart = new Button
            {
                Text = "4. Построить график",
                Location = new System.Drawing.Point(510, 8),
                Size = new System.Drawing.Size(150, 35),
                BackColor = System.Drawing.Color.LightCoral,
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
            };
            btnChart.Click += BtnChart_Click;

            // Кнопка 5: Прогноз
            Button btnForecast = new Button
            {
                Text = "5. Прогноз",
                Location = new System.Drawing.Point(670, 8),
                Size = new System.Drawing.Size(130, 35),
                BackColor = System.Drawing.Color.Orange,
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
            };
            btnForecast.Click += BtnForecast_Click;

            // Поле для N (лет прогноза)
            Label lblN = new Label
            {
                Text = "N лет:",
                Location = new System.Drawing.Point(820, 15),
                Size = new System.Drawing.Size(40, 20)
            };
            nudForecastYears = new NumericUpDown
            {
                Location = new System.Drawing.Point(865, 12),
                Size = new System.Drawing.Size(60, 25),
                Minimum = 1,
                Maximum = 10,
                Value = 3
            };

            buttonPanel.Controls.Add(btnLoad);
            buttonPanel.Controls.Add(btnShowTable);
            buttonPanel.Controls.Add(btnCalc);
            buttonPanel.Controls.Add(btnChart);
            buttonPanel.Controls.Add(btnForecast);
            buttonPanel.Controls.Add(lblN);
            buttonPanel.Controls.Add(nudForecastYears);
            this.Controls.Add(buttonPanel);

            // ===== ТАБЛИЦА =====
            dgvData = new DataGridView
            {
                Location = new System.Drawing.Point(20, 130),
                Size = new System.Drawing.Size(750, 280),
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = System.Drawing.Color.White
            };
            this.Controls.Add(dgvData);

            // ===== ГРАФИК =====
            chartMain = new Chart
            {
                Location = new System.Drawing.Point(20, 420),
                Size = new System.Drawing.Size(750, 280),
                BackColor = System.Drawing.Color.White
            };

            ChartArea chartArea = new ChartArea("MainArea");
            chartArea.AxisX.Title = "Год";
            chartArea.AxisY.Title = "Процент детей вне брака";
            chartArea.AxisX.Interval = 1;
            chartMain.ChartAreas.Add(chartArea);

            chartMain.Series.Add(new Series("Фактические данные")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = System.Drawing.Color.Blue,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8
            });

            chartMain.Series.Add(new Series("Прогноз")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                Color = System.Drawing.Color.Red,
                MarkerStyle = MarkerStyle.Diamond,
                MarkerSize = 6
            });

            this.Controls.Add(chartMain);

            // ===== ПАНЕЛЬ РЕЗУЛЬТАТОВ (СПРАВА) =====
            Panel rightPanel = new Panel
            {
                Location = new System.Drawing.Point(790, 130),
                Size = new System.Drawing.Size(380, 570),
                BackColor = System.Drawing.Color.WhiteSmoke
            };

            Label lblResults = new Label
            {
                Text = "РЕЗУЛЬТАТЫ",
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(360, 30),
                Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            txtResults = new TextBox
            {
                Location = new System.Drawing.Point(10, 50),
                Size = new System.Drawing.Size(360, 510),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Font = new System.Drawing.Font("Consolas", 9),
                BackColor = System.Drawing.Color.White
            };

            rightPanel.Controls.Add(lblResults);
            rightPanel.Controls.Add(txtResults);
            this.Controls.Add(rightPanel);
        }

        // ===== 1. ЗАГРУЗКА ФАЙЛА =====
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV файлы (*.csv)|*.csv";
                openFileDialog.Title = "Выберите файл с данными";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _currentFilePath = openFileDialog.FileName;
                        var service = new DataService();
                        _data = service.LoadFromCsv(_currentFilePath);

                        txtResults.Clear();
                        txtResults.AppendText($"Файл загружен: {Path.GetFileName(_currentFilePath)}\n");
                        txtResults.AppendText($"Записей: {_data.Count}\n");
                        txtResults.AppendText($"Годы: {_data.First().Year} - {_data.Last().Year}\n");
                        txtResults.AppendText($"\n");

                        MessageBox.Show($"Данные загружены! Найдено {_data.Count} записей.",
                            "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ===== 2. ПОКАЗАТЬ ТАБЛИЦУ =====
        private void BtnShowTable_Click(object sender, EventArgs e)
        {
            if (_data == null || _data.Count == 0)
            {
                MessageBox.Show("Сначала загрузите файл (кнопка 1)");
                return;
            }

            dgvData.DataSource = null;
            dgvData.DataSource = _data;
            dgvData.Columns["Year"].HeaderText = "Год";
            dgvData.Columns["Percent"].HeaderText = "Процент детей вне брака";
            dgvData.Columns["Percent"].DefaultCellStyle.Format = "F1";

            txtResults.AppendText($"Таблица: показано {_data.Count} записей\n");
        }

        // ===== 3. МАКС/МИН % ИЗМЕНЕНИЯ =====
        private void BtnCalc_Click(object sender, EventArgs e)
        {
            if (_data == null || _data.Count < 2)
            {
                MessageBox.Show("Загрузите данные (кнопка 1)");
                return;
            }

            List<double> changes = new List<double>();
            List<int> changeYears = new List<int>();

            for (int i = 1; i < _data.Count; i++)
            {
                double change = ((_data[i].Percent - _data[i - 1].Percent) / _data[i - 1].Percent) * 100;
                changes.Add(change);
                changeYears.Add(_data[i].Year);
            }

            double maxChange = changes.Max();
            double minChange = changes.Min();
            int maxYear = changeYears[changes.IndexOf(maxChange)];
            int minYear = changeYears[changes.IndexOf(minChange)];

            txtResults.AppendText($"\n--- ИЗМЕНЕНИЯ ЗА ГОД ---\n");
            txtResults.AppendText($"Максимальный рост: {maxChange:F2}% ({maxYear} г.)\n");
            txtResults.AppendText($"Минимальный рост: {minChange:F2}% ({minYear} г.)\n");
        }

        // ===== 4. ПОСТРОИТЬ ГРАФИК =====
        private void BtnChart_Click(object sender, EventArgs e)
        {
            if (_data == null || _data.Count == 0)
            {
                MessageBox.Show("Сначала загрузите данные (кнопка 1)");
                return;
            }

            chartMain.Series["Фактические данные"].Points.Clear();

            foreach (var point in _data)
            {
                chartMain.Series["Фактические данные"].Points.AddXY(point.Year, point.Percent);
            }

            txtResults.AppendText($"График построен ({_data.Count} точек)\n");
        }

        // ===== 5. ПРОГНОЗ =====
        private void BtnForecast_Click(object sender, EventArgs e)
        {
            if (_data == null || _data.Count == 0)
            {
                MessageBox.Show("Сначала загрузите файл (кнопка 1)");
                return;
            }

            int n = (int)nudForecastYears.Value;
            int window = 3;

            List<double> values = _data.Select(d => d.Percent).ToList();
            List<int> years = _data.Select(d => d.Year).ToList();
            List<double> forecastValues = new List<double>();
            List<int> forecastYears = new List<int>();

            for (int i = 0; i < n; i++)
            {
                double avg = values.Skip(values.Count - window).Take(window).Average();
                forecastValues.Add(avg);
                forecastYears.Add(years.Last() + i + 1);
                values.Add(avg);
            }

            // Очищаем старый прогноз и добавляем новый
            chartMain.Series["Прогноз"].Points.Clear();
            for (int i = 0; i < forecastValues.Count; i++)
            {
                chartMain.Series["Прогноз"].Points.AddXY(forecastYears[i], forecastValues[i]);
            }

            txtResults.AppendText($"\n--- ПРОГНОЗ НА {n} ЛЕТ ---\n");
            for (int i = 0; i < forecastValues.Count; i++)
            {
                txtResults.AppendText($"{forecastYears[i]}: {forecastValues[i]:F2}%\n");
            }
        }
    }
}