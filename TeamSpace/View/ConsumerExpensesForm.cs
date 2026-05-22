using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TeamSpace.View
{
    public partial class ConsumerExpensesForm : Form
    {
        private readonly Color mutedTextColor = Color.FromArgb(110, 118, 138);

        private const int ActionButtonWidth = 174;
        private const int ActionButtonHeight = 38;
        private const int DefaultMovingAverageWindow = 3;

        private SmoothMenuButton openCsvButton;
        private SmoothMenuButton buildGraphButton;
        private SmoothMenuButton exportPngButton;

        private Label lblIndicator;
        private ComboBox cmbIndicator;

        private Label lblAverageWindow;
        private NumericUpDown numAverageWindow;

        private DataTable expensesTable;
        private ExpensesChartControl expensesChart;
        private bool chartWasBuilt;

        public ConsumerExpensesForm()
        {
            InitializeComponent();

            DoubleBuffered = true;

            ConfigureFormSize();
            ConfigureLeftTablePanel();
            ConfigureRightLayout();
            ConfigureStatisticsCards();
            ConfigureSingleChart();
            ConfigureSettingsPanel();
            ReplaceDesignerButtons();

            headerPanel.SizeChanged += delegate { AlignCustomControls(); };
            settingsPanel.SizeChanged += delegate { AlignCustomControls(); };

            Load += delegate { AlignCustomControls(); };
            Shown += delegate { AlignCustomControls(); };

            AlignCustomControls();
        }

        // ------------------------------------------------------------
        // Размер формы
        // ------------------------------------------------------------

        private void ConfigureFormSize()
        {
            ClientSize = new Size(1320, 860);
            MinimumSize = new Size(1320, 860);
        }

        // ------------------------------------------------------------
        // Левая панель с таблицей
        // ------------------------------------------------------------

        private void ConfigureLeftTablePanel()
        {
            gridHeaderPanel.Height = 48;

            lblGridTitle.Location = new Point(4, 3);
            lblGridTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);

            dgvExpenses.Dock = DockStyle.Fill;
            dgvExpenses.AutoGenerateColumns = true;
            dgvExpenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvExpenses.ScrollBars = ScrollBars.Both;
            dgvExpenses.ReadOnly = true;
            dgvExpenses.AllowUserToAddRows = false;
            dgvExpenses.AllowUserToDeleteRows = false;
            dgvExpenses.AllowUserToResizeRows = false;
            dgvExpenses.RowHeadersVisible = false;
            dgvExpenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExpenses.MultiSelect = false;
            dgvExpenses.BackgroundColor = Color.White;
            dgvExpenses.BorderStyle = BorderStyle.None;

            dgvExpenses.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvExpenses.DefaultCellStyle.Font =
                new Font("Segoe UI", 9.5F, FontStyle.Regular);

            dgvExpenses.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(226, 236, 255);

            dgvExpenses.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(30, 33, 46);
        }

        // ------------------------------------------------------------
        // Правая часть формы
        // ------------------------------------------------------------

        private void ConfigureRightLayout()
        {
            rightLayout.SuspendLayout();

            rightLayout.RowStyles.Clear();
            rightLayout.RowCount = 3;

            rightLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 122F)
            );

            rightLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 142F)
            );

            rightLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F)
            );

            statsPanel.Padding = new Padding(14, 8, 14, 8);
            settingsPanel.Padding = new Padding(18, 8, 18, 8);

            rightLayout.ResumeLayout(true);
        }

        // ------------------------------------------------------------
        // Карточки статистики
        // ------------------------------------------------------------

        private void ConfigureStatisticsCards()
        {
            statsLayout.SuspendLayout();

            cardAvg.Visible = false;

            statsLayout.Controls.Clear();
            statsLayout.ColumnStyles.Clear();
            statsLayout.RowStyles.Clear();

            statsLayout.ColumnCount = 3;
            statsLayout.RowCount = 1;

            statsLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 33.33F)
            );

            statsLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 33.33F)
            );

            statsLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 33.34F)
            );

            statsLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F)
            );

            ConfigureCompactCard(cardMax, lblMaxTitle, lblMaxValue);
            ConfigureCompactCard(cardMin, lblMinTitle, lblMinValue);
            ConfigureCompactCard(cardYears, lblYearsTitle, lblYearsValue);

            lblMaxValue.Text = "0 %";
            lblMinValue.Text = "0 %";
            lblYearsValue.Text = "0";

            statsLayout.Controls.Add(cardMax, 0, 0);
            statsLayout.Controls.Add(cardMin, 1, 0);
            statsLayout.Controls.Add(cardYears, 2, 0);

            statsLayout.ResumeLayout(true);
        }

        private void ConfigureCompactCard(
            Panel card,
            Label title,
            Label value)
        {
            card.Margin = new Padding(7, 5, 7, 5);

            title.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            title.Location = new Point(14, 10);

            value.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            value.Location = new Point(14, 38);
        }

        // ------------------------------------------------------------
        // Один график фактических данных и прогноза
        // ------------------------------------------------------------

        private void ConfigureSingleChart()
        {
            chartLayout.SuspendLayout();

            forecastChartCard.Visible = false;

            chartLayout.Controls.Clear();
            chartLayout.ColumnStyles.Clear();
            chartLayout.RowStyles.Clear();

            chartLayout.ColumnCount = 1;
            chartLayout.RowCount = 1;

            chartLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F)
            );

            chartLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F)
            );

            lblHistoryChartTitle.Text = "График расходов и прогноз";
            lblHistoryChartTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);

            historyChartCard.Padding = new Padding(18, 12, 18, 18);

            pnlHistoryChart.Controls.Clear();
            pnlHistoryChart.BackColor = Color.White;

            expensesChart = new ExpensesChartControl
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            pnlHistoryChart.Controls.Add(expensesChart);
            chartLayout.Controls.Add(historyChartCard, 0, 0);

            chartLayout.ResumeLayout(true);
        }

        // ------------------------------------------------------------
        // Настройки анализа
        // ------------------------------------------------------------

        private void ConfigureSettingsPanel()
        {
            lblMovingAverage.Visible = false;
            lblMovingAverage.Enabled = false;

            cmbMovingAverage.Visible = false;
            cmbMovingAverage.Enabled = false;
            cmbMovingAverage.TabStop = false;

            lblSettingsTitle.Location = new Point(18, 4);
            lblSettingsTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);

            // Количество будущих лет для прогноза.
            lblForecastYears.Text = "Прогноз N лет";
            lblForecastYears.AutoSize = true;
            lblForecastYears.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            lblForecastYears.ForeColor = mutedTextColor;

            numForecastYears.Minimum = 1;
            numForecastYears.Maximum = 15;
            numForecastYears.Value = 3;
            numForecastYears.Enabled = false;
            numForecastYears.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            numForecastYears.Size = new Size(92, 30);
            numForecastYears.ValueChanged += numForecastYears_ValueChanged;

            // Анализируемый показатель.
            lblIndicator = new Label
            {
                Text = "Показатель:",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = mutedTextColor,
                BackColor = Color.Transparent
            };

            cmbIndicator = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Size = new Size(154, 31),
                FlatStyle = FlatStyle.Standard,
                Enabled = false
            };

            cmbIndicator.Items.AddRange(new object[]
            {
                "Общие расходы",
                "Продукты питания",
                "Одежда и обувь",
                "Жилищные услуги",
                "Транспорт",
                "Здравоохранение",
                "Образование"
            });

            cmbIndicator.SelectedIndex = 0;
            cmbIndicator.SelectedIndexChanged += cmbIndicator_SelectedIndexChanged;

            // Период скользящей средней.
            lblAverageWindow = new Label
            {
                Text = "Период (n)",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = mutedTextColor,
                BackColor = Color.Transparent
            };

            numAverageWindow = new NumericUpDown
            {
                Minimum = 2,
                Maximum = 2,
                Value = 2,
                Enabled = false,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Size = new Size(74, 30)
            };

            numAverageWindow.ValueChanged += numAverageWindow_ValueChanged;

            settingsPanel.Controls.Add(lblIndicator);
            settingsPanel.Controls.Add(cmbIndicator);
            settingsPanel.Controls.Add(lblAverageWindow);
            settingsPanel.Controls.Add(numAverageWindow);

            lblIndicator.BringToFront();
            cmbIndicator.BringToFront();
            lblAverageWindow.BringToFront();
            numAverageWindow.BringToFront();
            lblForecastYears.BringToFront();
            numForecastYears.BringToFront();

            SetAnalysisInputsEnabled(false);
        }

        // ------------------------------------------------------------
        // Сглаженные кнопки
        // ------------------------------------------------------------

        private void ReplaceDesignerButtons()
        {
            btnLoadFile.Visible = false;
            btnLoadFile.Enabled = false;

            btnBuildForecast.Visible = false;
            btnBuildForecast.Enabled = false;

            openCsvButton = CreateMenuButton(
                "Открыть CSV",
                178,
                42,
                btnLoadFile_Click
            );

            buildGraphButton = CreateMenuButton(
                "Построить график",
                ActionButtonWidth,
                ActionButtonHeight,
                btnBuildForecast_Click
            );

            exportPngButton = CreateMenuButton(
                "Экспорт в PNG",
                ActionButtonWidth,
                ActionButtonHeight,
                btnExportPng_Click
            );

            headerPanel.Controls.Add(openCsvButton);
            settingsPanel.Controls.Add(buildGraphButton);
            settingsPanel.Controls.Add(exportPngButton);

            openCsvButton.BringToFront();
            buildGraphButton.BringToFront();
            exportPngButton.BringToFront();
        }

        private SmoothMenuButton CreateMenuButton(
            string text,
            int width,
            int height,
            EventHandler clickHandler)
        {
            SmoothMenuButton button = new SmoothMenuButton
            {
                Text = text,
                Size = new Size(width, height),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            button.Click += clickHandler;

            return button;
        }

        // ------------------------------------------------------------
        // Расстановка элементов
        // ------------------------------------------------------------

        private void AlignCustomControls()
        {
            AlignHeaderButton();
            AlignSettingsControls();
        }

        private void AlignHeaderButton()
        {
            if (openCsvButton == null)
                return;

            int x = headerPanel.ClientSize.Width
                    - headerPanel.Padding.Right
                    - openCsvButton.Width;

            openCsvButton.Location = new Point(
                Math.Max(headerPanel.Padding.Left, x),
                34
            );
        }

        private void AlignSettingsControls()
        {
            if (settingsPanel == null)
                return;

            int indicatorX = 24;
            int periodX = 194;
            int forecastX = 292;

            int labelY = 47;
            int controlY = 72;

            int buttonX = settingsPanel.ClientSize.Width
                          - settingsPanel.Padding.Right
                          - ActionButtonWidth;

            buttonX = Math.Max(430, buttonX);

            if (lblIndicator != null)
                lblIndicator.Location = new Point(indicatorX, labelY);

            if (cmbIndicator != null)
            {
                cmbIndicator.Size = new Size(154, 31);
                cmbIndicator.Location = new Point(indicatorX, controlY);
            }

            if (lblAverageWindow != null)
                lblAverageWindow.Location = new Point(periodX, labelY);

            if (numAverageWindow != null)
            {
                numAverageWindow.Size = new Size(74, 30);
                numAverageWindow.Location = new Point(periodX, controlY);
            }

            lblForecastYears.Location = new Point(forecastX, labelY);
            numForecastYears.Size = new Size(92, 30);
            numForecastYears.Location = new Point(forecastX, controlY);

            if (buildGraphButton != null)
                buildGraphButton.Location = new Point(buttonX, 25);

            if (exportPngButton != null)
                exportPngButton.Location = new Point(buttonX, 72);
        }

        // Метод оставлен, так как он привязан в Designer.
        private void MenuBlueButton_Paint(object sender, PaintEventArgs e)
        {
        }

        // ------------------------------------------------------------
        // Доступность полей анализа
        // ------------------------------------------------------------

        private void SetAnalysisInputsEnabled(bool enabled)
        {
            if (cmbIndicator != null)
                cmbIndicator.Enabled = enabled;

            if (numAverageWindow != null)
                numAverageWindow.Enabled = enabled;

            if (numForecastYears != null)
                numForecastYears.Enabled = enabled;
        }

        private void ResetLoadedData()
        {
            chartWasBuilt = false;
            expensesTable = null;

            SetAnalysisInputsEnabled(false);

            dgvExpenses.DataSource = null;

            lblMaxValue.Text = "0 %";
            lblMinValue.Text = "0 %";
            lblYearsValue.Text = "0";

            if (numAverageWindow != null)
            {
                numAverageWindow.Minimum = 2;
                numAverageWindow.Maximum = 2;
                numAverageWindow.Value = 2;
            }

            if (expensesChart != null)
                expensesChart.ClearData();
        }

        // ------------------------------------------------------------
        // Загрузка CSV-файла
        // ------------------------------------------------------------

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = FindConsumerExpensesCsvPath();

                if (!File.Exists(filePath))
                {
                    MessageBox.Show(
                        "Файл consumer_expenses.csv не найден.\n\n" +
                        "Добавь файл в папку Data проекта:\n" +
                        "Data\\consumer_expenses.csv",
                        "Файл не найден",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                ResetLoadedData();
                LoadExpensesFromCsv(filePath);

                MessageBox.Show(
                    "Данные успешно загружены.\n" +
                    "Доступный период скользящей средней: от 2 до " +
                    expensesTable.Rows.Count + ".\n\n" +
                    "Выбери параметры и нажми «Построить график».",
                    "Открытие CSV",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                SetAnalysisInputsEnabled(false);

                MessageBox.Show(
                    "Ошибка при загрузке CSV-файла:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private string FindConsumerExpensesCsvPath()
        {
            string outputDirectoryPath = Path.Combine(
                Application.StartupPath,
                "Data",
                "consumer_expenses.csv"
            );

            if (File.Exists(outputDirectoryPath))
                return outputDirectoryPath;

            DirectoryInfo currentDirectory =
                new DirectoryInfo(Application.StartupPath);

            while (currentDirectory != null)
            {
                string projectDirectoryPath = Path.Combine(
                    currentDirectory.FullName,
                    "Data",
                    "consumer_expenses.csv"
                );

                if (File.Exists(projectDirectoryPath))
                    return projectDirectoryPath;

                currentDirectory = currentDirectory.Parent;
            }

            return outputDirectoryPath;
        }

        private void LoadExpensesFromCsv(string filePath)
        {
            string[] lines = File
                .ReadAllLines(filePath, Encoding.UTF8)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToArray();

            if (lines.Length < 2)
            {
                throw new InvalidDataException(
                    "CSV-файл не содержит строк с данными."
                );
            }

            char separator = DetectSeparator(lines[0]);

            string cleanHeaderLine = lines[0].TrimStart('\uFEFF');

            string[] headers = ParseCsvLine(cleanHeaderLine, separator)
                .Select(header => header.Trim())
                .ToArray();

            string[] requiredColumns =
            {
                "Год",
                "Общие расходы",
                "Продукты питания",
                "Одежда и обувь",
                "Жилищные услуги",
                "Транспорт",
                "Здравоохранение",
                "Образование"
            };

            foreach (string requiredColumn in requiredColumns)
            {
                if (!headers.Contains(requiredColumn))
                {
                    throw new InvalidDataException(
                        "В CSV-файле отсутствует обязательный столбец: " +
                        requiredColumn
                    );
                }
            }

            Dictionary<string, int> columnIndexes =
                headers
                    .Select((header, index) => new
                    {
                        Header = header,
                        Index = index
                    })
                    .ToDictionary(item => item.Header, item => item.Index);

            DataTable loadedTable = CreateExpensesTable();

            for (int lineIndex = 1; lineIndex < lines.Length; lineIndex++)
            {
                string[] cells = ParseCsvLine(lines[lineIndex], separator)
                    .Select(cell => cell.Trim())
                    .ToArray();

                if (cells.Length < headers.Length)
                {
                    throw new InvalidDataException(
                        "Недостаточно значений в строке " +
                        (lineIndex + 1) + "."
                    );
                }

                DataRow row = loadedTable.NewRow();

                row["Год"] = ParseInteger(
                    cells[columnIndexes["Год"]],
                    lineIndex + 1,
                    "Год"
                );

                row["Общие расходы"] = ParseNumber(
                    cells[columnIndexes["Общие расходы"]],
                    lineIndex + 1,
                    "Общие расходы"
                );

                row["Продукты питания"] = ParseNumber(
                    cells[columnIndexes["Продукты питания"]],
                    lineIndex + 1,
                    "Продукты питания"
                );

                row["Одежда и обувь"] = ParseNumber(
                    cells[columnIndexes["Одежда и обувь"]],
                    lineIndex + 1,
                    "Одежда и обувь"
                );

                row["Жилищные услуги"] = ParseNumber(
                    cells[columnIndexes["Жилищные услуги"]],
                    lineIndex + 1,
                    "Жилищные услуги"
                );

                row["Транспорт"] = ParseNumber(
                    cells[columnIndexes["Транспорт"]],
                    lineIndex + 1,
                    "Транспорт"
                );

                row["Здравоохранение"] = ParseNumber(
                    cells[columnIndexes["Здравоохранение"]],
                    lineIndex + 1,
                    "Здравоохранение"
                );

                row["Образование"] = ParseNumber(
                    cells[columnIndexes["Образование"]],
                    lineIndex + 1,
                    "Образование"
                );

                loadedTable.Rows.Add(row);
            }

            DataView sortedView = loadedTable.DefaultView;
            sortedView.Sort = "Год ASC";

            expensesTable = sortedView.ToTable();

            ShowExpensesInGrid();
            UpdateStatistics();
            ConfigureAverageWindowRange();
        }

        private void ConfigureAverageWindowRange()
        {
            if (numAverageWindow == null)
                return;

            int periodsCount = expensesTable != null
                ? expensesTable.Rows.Count
                : 0;

            if (periodsCount < 2)
            {
                SetAnalysisInputsEnabled(false);

                numAverageWindow.Minimum = 2;
                numAverageWindow.Maximum = 2;
                numAverageWindow.Value = 2;

                return;
            }

            numAverageWindow.Minimum = 2;
            numAverageWindow.Maximum = periodsCount;

            int defaultValue = Math.Min(
                DefaultMovingAverageWindow,
                periodsCount
            );

            numAverageWindow.Value = defaultValue;

            SetAnalysisInputsEnabled(true);
        }

        private DataTable CreateExpensesTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("Год", typeof(int));
            table.Columns.Add("Общие расходы", typeof(decimal));
            table.Columns.Add("Продукты питания", typeof(decimal));
            table.Columns.Add("Одежда и обувь", typeof(decimal));
            table.Columns.Add("Жилищные услуги", typeof(decimal));
            table.Columns.Add("Транспорт", typeof(decimal));
            table.Columns.Add("Здравоохранение", typeof(decimal));
            table.Columns.Add("Образование", typeof(decimal));

            return table;
        }

        private char DetectSeparator(string headerLine)
        {
            if (headerLine.Contains(";"))
                return ';';

            if (headerLine.Contains(","))
                return ',';

            throw new InvalidDataException(
                "Не удалось определить разделитель CSV-файла."
            );
        }

        private List<string> ParseCsvLine(string line, char separator)
        {
            List<string> cells = new List<string>();
            StringBuilder currentCell = new StringBuilder();

            bool insideQuotes = false;

            for (int index = 0; index < line.Length; index++)
            {
                char currentCharacter = line[index];

                if (currentCharacter == '"')
                {
                    if (insideQuotes &&
                        index + 1 < line.Length &&
                        line[index + 1] == '"')
                    {
                        currentCell.Append('"');
                        index++;
                    }
                    else
                    {
                        insideQuotes = !insideQuotes;
                    }
                }
                else if (currentCharacter == separator && !insideQuotes)
                {
                    cells.Add(currentCell.ToString());
                    currentCell.Clear();
                }
                else
                {
                    currentCell.Append(currentCharacter);
                }
            }

            cells.Add(currentCell.ToString());

            return cells;
        }

        private int ParseInteger(
            string text,
            int lineNumber,
            string columnName)
        {
            int value;

            if (int.TryParse(text.Trim(), out value))
                return value;

            throw new InvalidDataException(
                "Некорректное значение в строке " + lineNumber +
                ", столбец \"" + columnName + "\": " + text
            );
        }

        private decimal ParseNumber(
            string text,
            int lineNumber,
            string columnName)
        {
            decimal value;

            string preparedText = text
                .Replace("\u00A0", string.Empty)
                .Replace(" ", string.Empty)
                .Trim();

            if (decimal.TryParse(
                preparedText,
                NumberStyles.Any,
                CultureInfo.GetCultureInfo("ru-RU"),
                out value))
            {
                return value;
            }

            if (decimal.TryParse(
                preparedText,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out value))
            {
                return value;
            }

            throw new InvalidDataException(
                "Некорректное значение в строке " + lineNumber +
                ", столбец \"" + columnName + "\": " + text
            );
        }

        // ------------------------------------------------------------
        // Заполнение таблицы
        // ------------------------------------------------------------

        private void ShowExpensesInGrid()
        {
            dgvExpenses.DataSource = null;
            dgvExpenses.DataSource = expensesTable;

            dgvExpenses.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.AllCells;

            dgvExpenses.ScrollBars = ScrollBars.Both;

            foreach (DataGridViewColumn column in dgvExpenses.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

                if (column.DataPropertyName != "Год")
                {
                    column.DefaultCellStyle.Format = "N0";
                    column.DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }
                else
                {
                    column.DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleCenter;
                }
            }

            dgvExpenses.ClearSelection();
        }

        // ------------------------------------------------------------
        // Статистика
        // ------------------------------------------------------------

        private void cmbIndicator_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatistics();
            RebuildChartIfAlreadyBuilt();
        }

        private void numForecastYears_ValueChanged(object sender, EventArgs e)
        {
            RebuildChartIfAlreadyBuilt();
        }

        private void numAverageWindow_ValueChanged(object sender, EventArgs e)
        {
            RebuildChartIfAlreadyBuilt();
        }

        private void RebuildChartIfAlreadyBuilt()
        {
            if (!chartWasBuilt)
                return;

            try
            {
                BuildForecastChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при обновлении прогноза:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void UpdateStatistics()
        {
            if (expensesTable == null || expensesTable.Rows.Count == 0)
            {
                lblMaxValue.Text = "0 %";
                lblMinValue.Text = "0 %";
                lblYearsValue.Text = "0";
                return;
            }

            lblYearsValue.Text = expensesTable.Rows.Count.ToString();

            string selectedIndicator = GetSelectedIndicator();

            if (!expensesTable.Columns.Contains(selectedIndicator))
            {
                lblMaxValue.Text = "—";
                lblMinValue.Text = "—";
                return;
            }

            if (expensesTable.Rows.Count < 2)
            {
                lblMaxValue.Text = "0 %";
                lblMinValue.Text = "0 %";
                return;
            }

            List<decimal> percentageChanges = new List<decimal>();

            for (int index = 1; index < expensesTable.Rows.Count; index++)
            {
                decimal previousValue = Convert.ToDecimal(
                    expensesTable.Rows[index - 1][selectedIndicator]
                );

                decimal currentValue = Convert.ToDecimal(
                    expensesTable.Rows[index][selectedIndicator]
                );

                if (previousValue == 0)
                    continue;

                decimal change =
                    (currentValue - previousValue) / previousValue * 100M;

                percentageChanges.Add(change);
            }

            if (percentageChanges.Count == 0)
            {
                lblMaxValue.Text = "0 %";
                lblMinValue.Text = "0 %";
                return;
            }

            CultureInfo russianCulture =
                CultureInfo.GetCultureInfo("ru-RU");

            decimal maximumChange = percentageChanges.Max();
            decimal minimumChange = percentageChanges.Min();

            lblMaxValue.Text =
                maximumChange.ToString("0.##", russianCulture) + " %";

            lblMinValue.Text =
                minimumChange.ToString("0.##", russianCulture) + " %";
        }

        private string GetSelectedIndicator()
        {
            if (cmbIndicator != null && cmbIndicator.SelectedItem != null)
                return cmbIndicator.SelectedItem.ToString();

            return "Общие расходы";
        }

        private int GetSelectedMovingAverageWindow()
        {
            if (numAverageWindow != null && numAverageWindow.Enabled)
                return (int)numAverageWindow.Value;

            return DefaultMovingAverageWindow;
        }

        // ------------------------------------------------------------
        // Прогнозирование методом скользящей средней
        // ------------------------------------------------------------

        private void btnBuildForecast_Click(object sender, EventArgs e)
        {
            try
            {
                if (expensesTable == null || expensesTable.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Сначала загрузите данные кнопкой «Открыть CSV».",
                        "Нет данных",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                if (expensesTable.Rows.Count < 2)
                {
                    MessageBox.Show(
                        "Для построения прогноза требуется минимум 2 периода.",
                        "Недостаточно данных",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                BuildForecastChart();
                chartWasBuilt = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при построении графика:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BuildForecastChart()
        {
            string indicator = GetSelectedIndicator();
            int movingAverageWindow = GetSelectedMovingAverageWindow();

            if (expensesTable == null || expensesTable.Rows.Count == 0)
            {
                throw new InvalidOperationException(
                    "Нет данных для построения графика."
                );
            }

            if (!expensesTable.Columns.Contains(indicator))
            {
                throw new InvalidOperationException(
                    "Выбранный показатель отсутствует в данных."
                );
            }

            List<ExpensePoint> actualPoints = new List<ExpensePoint>();
            List<decimal> extendedValues = new List<decimal>();

            foreach (DataRow row in expensesTable.Rows)
            {
                int year = Convert.ToInt32(row["Год"]);
                decimal value = Convert.ToDecimal(row[indicator]);

                actualPoints.Add(new ExpensePoint(year, value));
                extendedValues.Add(value);
            }

            if (actualPoints.Count < movingAverageWindow)
            {
                throw new InvalidOperationException(
                    "Для прогноза с периодом " +
                    movingAverageWindow +
                    " требуется не менее " +
                    movingAverageWindow +
                    " строк исходных данных."
                );
            }

            int forecastYears = (int)numForecastYears.Value;
            int lastYear = actualPoints[actualPoints.Count - 1].Year;

            List<ExpensePoint> forecastPoints = new List<ExpensePoint>();

            for (int step = 1; step <= forecastYears; step++)
            {
                decimal predictedValue = extendedValues
                    .Skip(extendedValues.Count - movingAverageWindow)
                    .Take(movingAverageWindow)
                    .Average();

                predictedValue = Math.Round(predictedValue, 2);

                forecastPoints.Add(
                    new ExpensePoint(lastYear + step, predictedValue)
                );

                extendedValues.Add(predictedValue);
            }

            expensesChart.SetData(
                actualPoints,
                forecastPoints,
                indicator,
                movingAverageWindow
            );
        }

        // ------------------------------------------------------------
        // Экспорт графика в PNG
        // ------------------------------------------------------------

        private void btnExportPng_Click(object sender, EventArgs e)
        {
            try
            {
                if (!chartWasBuilt ||
                    expensesChart == null ||
                    !expensesChart.HasData)
                {
                    MessageBox.Show(
                        "Сначала постройте график.",
                        "Экспорт PNG",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "PNG image (*.png)|*.png";
                    saveDialog.Title = "Экспортировать график в PNG";
                    saveDialog.FileName = "consumer_expenses_forecast.png";
                    saveDialog.DefaultExt = "png";
                    saveDialog.AddExtension = true;

                    if (saveDialog.ShowDialog() != DialogResult.OK)
                        return;

                    using (Bitmap bitmap = new Bitmap(
                        expensesChart.Width,
                        expensesChart.Height))
                    {
                        expensesChart.DrawToBitmap(
                            bitmap,
                            new Rectangle(
                                0,
                                0,
                                expensesChart.Width,
                                expensesChart.Height
                            )
                        );

                        bitmap.Save(saveDialog.FileName, ImageFormat.Png);
                    }

                    MessageBox.Show(
                        "График успешно сохранён в PNG:\n" +
                        saveDialog.FileName,
                        "Экспорт завершён",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при экспорте графика в PNG:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }

    // ------------------------------------------------------------
    // Точка ряда данных
    // ------------------------------------------------------------

    public class ExpensePoint
    {
        public int Year { get; private set; }

        public decimal Value { get; private set; }

        public ExpensePoint(int year, decimal value)
        {
            Year = year;
            Value = value;
        }
    }

    // ------------------------------------------------------------
    // Пользовательский график данных и прогноза
    // ------------------------------------------------------------

    public class ExpensesChartControl : Control
    {
        private readonly Color actualLineColor =
            Color.FromArgb(26, 86, 232);

        private readonly Color forecastLineColor =
            Color.FromArgb(232, 126, 47);

        private readonly Color forecastBackgroundColor =
            Color.FromArgb(255, 246, 236);

        private readonly Color gridColor =
            Color.FromArgb(225, 232, 242);

        private readonly Color textColor =
            Color.FromArgb(76, 86, 105);

        private readonly Color borderColor =
            Color.FromArgb(211, 220, 233);

        private List<ExpensePoint> actualPoints;
        private List<ExpensePoint> forecastPoints;

        private string indicatorName;
        private int windowSize;

        public bool HasData
        {
            get
            {
                return actualPoints != null && actualPoints.Count > 0;
            }
        }

        public ExpensesChartControl()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true
            );

            BackColor = Color.White;

            actualPoints = new List<ExpensePoint>();
            forecastPoints = new List<ExpensePoint>();
            indicatorName = string.Empty;
        }

        public void SetData(
            List<ExpensePoint> actual,
            List<ExpensePoint> forecast,
            string indicator,
            int movingAverageWindow)
        {
            actualPoints = actual ?? new List<ExpensePoint>();
            forecastPoints = forecast ?? new List<ExpensePoint>();
            indicatorName = indicator ?? string.Empty;
            windowSize = movingAverageWindow;

            Invalidate();
        }

        public void ClearData()
        {
            actualPoints.Clear();
            forecastPoints.Clear();
            indicatorName = string.Empty;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graphics = e.Graphics;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;

            graphics.Clear(Color.White);

            if (!HasData)
            {
                DrawEmptyMessage(graphics);
                return;
            }

            RectangleF plotArea = new RectangleF(
                72,
                48,
                Math.Max(100, Width - 102),
                Math.Max(100, Height - 102)
            );

            List<ExpensePoint> allPoints = new List<ExpensePoint>();
            allPoints.AddRange(actualPoints);
            allPoints.AddRange(forecastPoints);

            decimal minimumValue = allPoints.Min(point => point.Value);
            decimal maximumValue = allPoints.Max(point => point.Value);

            PrepareAxisRange(ref minimumValue, ref maximumValue);

            DrawForecastBackground(graphics, plotArea, allPoints.Count);

            DrawGridAndAxes(
                graphics,
                plotArea,
                allPoints,
                minimumValue,
                maximumValue
            );

            DrawActualSeries(
                graphics,
                plotArea,
                allPoints.Count,
                minimumValue,
                maximumValue
            );

            DrawForecastSeries(
                graphics,
                plotArea,
                allPoints.Count,
                minimumValue,
                maximumValue
            );

            DrawLegend(graphics);
        }

        private void DrawEmptyMessage(Graphics graphics)
        {
            string text = "Загрузите CSV и нажмите «Построить график»";

            using (Font font = new Font("Segoe UI", 11F, FontStyle.Regular))
            {
                TextRenderer.DrawText(
                    graphics,
                    text,
                    font,
                    ClientRectangle,
                    Color.FromArgb(130, 138, 155),
                    TextFormatFlags.HorizontalCenter |
                    TextFormatFlags.VerticalCenter
                );
            }
        }

        private void PrepareAxisRange(
            ref decimal minimumValue,
            ref decimal maximumValue)
        {
            if (minimumValue == maximumValue)
            {
                decimal addition = minimumValue == 0
                    ? 1
                    : Math.Abs(minimumValue) * 0.1M;

                minimumValue -= addition;
                maximumValue += addition;

                return;
            }

            decimal range = maximumValue - minimumValue;
            decimal padding = range * 0.12M;

            minimumValue = Math.Max(0, minimumValue - padding);
            maximumValue += padding;
        }

        private void DrawForecastBackground(
            Graphics graphics,
            RectangleF plotArea,
            int allPointCount)
        {
            if (forecastPoints.Count == 0 || actualPoints.Count == 0)
                return;

            float lastActualX = GetX(
                actualPoints.Count - 1,
                allPointCount,
                plotArea
            );

            float firstForecastX = GetX(
                actualPoints.Count,
                allPointCount,
                plotArea
            );

            float separatorX = (lastActualX + firstForecastX) / 2F;

            RectangleF forecastArea = new RectangleF(
                separatorX,
                plotArea.Top,
                plotArea.Right - separatorX,
                plotArea.Height
            );

            using (SolidBrush brush = new SolidBrush(forecastBackgroundColor))
            {
                graphics.FillRectangle(brush, forecastArea);
            }

            using (Pen separatorPen = new Pen(forecastLineColor, 1F))
            {
                separatorPen.DashStyle = DashStyle.Dash;

                graphics.DrawLine(
                    separatorPen,
                    separatorX,
                    plotArea.Top,
                    separatorX,
                    plotArea.Bottom
                );
            }

            using (Font font = new Font("Segoe UI", 9F, FontStyle.Bold))
            {
                TextRenderer.DrawText(
                    graphics,
                    "ПРОГНОЗ",
                    font,
                    new Rectangle(
                        (int)separatorX + 8,
                        (int)plotArea.Top + 8,
                        90,
                        22
                    ),
                    forecastLineColor,
                    TextFormatFlags.Left |
                    TextFormatFlags.VerticalCenter
                );
            }
        }

        private void DrawGridAndAxes(
            Graphics graphics,
            RectangleF plotArea,
            List<ExpensePoint> allPoints,
            decimal minimumValue,
            decimal maximumValue)
        {
            const int horizontalSections = 5;

            using (Pen gridPen = new Pen(gridColor, 1F))
            using (Pen borderPen = new Pen(borderColor, 1F))
            using (Font axisFont = new Font("Segoe UI", 8.5F, FontStyle.Regular))
            {
                for (int index = 0; index <= horizontalSections; index++)
                {
                    float y = plotArea.Bottom -
                              plotArea.Height * index / horizontalSections;

                    graphics.DrawLine(
                        gridPen,
                        plotArea.Left,
                        y,
                        plotArea.Right,
                        y
                    );

                    decimal value = minimumValue +
                                    (maximumValue - minimumValue) *
                                    index / horizontalSections;

                    string text = value.ToString(
                        "N0",
                        CultureInfo.GetCultureInfo("ru-RU")
                    );

                    TextRenderer.DrawText(
                        graphics,
                        text,
                        axisFont,
                        new Rectangle(
                            0,
                            (int)y - 10,
                            (int)plotArea.Left - 8,
                            22
                        ),
                        textColor,
                        TextFormatFlags.Right |
                        TextFormatFlags.VerticalCenter
                    );
                }

                int labelStep = Math.Max(
                    1,
                    (int)Math.Ceiling(allPoints.Count / 10.0)
                );

                for (int index = 0; index < allPoints.Count; index++)
                {
                    bool drawLabel = index % labelStep == 0 ||
                                     index == allPoints.Count - 1;

                    if (!drawLabel)
                        continue;

                    float x = GetX(index, allPoints.Count, plotArea);

                    graphics.DrawLine(
                        gridPen,
                        x,
                        plotArea.Top,
                        x,
                        plotArea.Bottom
                    );

                    TextRenderer.DrawText(
                        graphics,
                        allPoints[index].Year.ToString(),
                        axisFont,
                        new Rectangle(
                            (int)x - 28,
                            (int)plotArea.Bottom + 7,
                            56,
                            22
                        ),
                        textColor,
                        TextFormatFlags.HorizontalCenter |
                        TextFormatFlags.VerticalCenter
                    );
                }

                graphics.DrawRectangle(
                    borderPen,
                    plotArea.X,
                    plotArea.Y,
                    plotArea.Width,
                    plotArea.Height
                );
            }
        }

        private void DrawActualSeries(
            Graphics graphics,
            RectangleF plotArea,
            int allPointCount,
            decimal minimumValue,
            decimal maximumValue)
        {
            if (actualPoints.Count == 0)
                return;

            PointF[] points = new PointF[actualPoints.Count];

            for (int index = 0; index < actualPoints.Count; index++)
            {
                points[index] = new PointF(
                    GetX(index, allPointCount, plotArea),
                    GetY(
                        actualPoints[index].Value,
                        minimumValue,
                        maximumValue,
                        plotArea
                    )
                );
            }

            using (Pen linePen = new Pen(actualLineColor, 2.6F))
            using (SolidBrush pointBrush = new SolidBrush(actualLineColor))
            {
                if (points.Length > 1)
                    graphics.DrawLines(linePen, points);

                foreach (PointF point in points)
                {
                    graphics.FillEllipse(
                        pointBrush,
                        point.X - 3.5F,
                        point.Y - 3.5F,
                        7F,
                        7F
                    );
                }
            }
        }

        private void DrawForecastSeries(
            Graphics graphics,
            RectangleF plotArea,
            int allPointCount,
            decimal minimumValue,
            decimal maximumValue)
        {
            if (forecastPoints.Count == 0 || actualPoints.Count == 0)
                return;

            PointF[] points = new PointF[forecastPoints.Count + 1];

            ExpensePoint lastActualPoint =
                actualPoints[actualPoints.Count - 1];

            points[0] = new PointF(
                GetX(actualPoints.Count - 1, allPointCount, plotArea),
                GetY(
                    lastActualPoint.Value,
                    minimumValue,
                    maximumValue,
                    plotArea
                )
            );

            for (int index = 0; index < forecastPoints.Count; index++)
            {
                points[index + 1] = new PointF(
                    GetX(actualPoints.Count + index, allPointCount, plotArea),
                    GetY(
                        forecastPoints[index].Value,
                        minimumValue,
                        maximumValue,
                        plotArea
                    )
                );
            }

            using (Pen linePen = new Pen(forecastLineColor, 2.8F))
            using (SolidBrush pointBrush = new SolidBrush(forecastLineColor))
            {
                graphics.DrawLines(linePen, points);

                for (int index = 1; index < points.Length; index++)
                {
                    graphics.FillEllipse(
                        pointBrush,
                        points[index].X - 4F,
                        points[index].Y - 4F,
                        8F,
                        8F
                    );
                }
            }
        }

        private void DrawLegend(Graphics graphics)
        {
            using (Font legendFont = new Font("Segoe UI", 9F, FontStyle.Regular))
            using (Font titleFont = new Font("Segoe UI", 9F, FontStyle.Bold))
            using (Pen actualPen = new Pen(actualLineColor, 2.5F))
            using (Pen forecastPen = new Pen(forecastLineColor, 2.5F))
            {
                TextRenderer.DrawText(
                    graphics,
                    indicatorName,
                    titleFont,
                    new Rectangle(72, 6, 185, 25),
                    Color.FromArgb(30, 33, 46),
                    TextFormatFlags.Left |
                    TextFormatFlags.VerticalCenter |
                    TextFormatFlags.EndEllipsis
                );

                int legendX = Math.Max(268, Width - 402);

                graphics.DrawLine(
                    actualPen,
                    legendX,
                    18,
                    legendX + 24,
                    18
                );

                TextRenderer.DrawText(
                    graphics,
                    "Фактические данные",
                    legendFont,
                    new Rectangle(legendX + 30, 6, 124, 25),
                    textColor,
                    TextFormatFlags.Left |
                    TextFormatFlags.VerticalCenter
                );

                graphics.DrawLine(
                    forecastPen,
                    legendX + 166,
                    18,
                    legendX + 190,
                    18
                );

                TextRenderer.DrawText(
                    graphics,
                    "Прогноз, период (n = " + windowSize + ")",
                    legendFont,
                    new Rectangle(legendX + 196, 6, 202, 25),
                    textColor,
                    TextFormatFlags.Left |
                    TextFormatFlags.VerticalCenter
                );
            }
        }

        private float GetX(
            int pointIndex,
            int pointCount,
            RectangleF plotArea)
        {
            if (pointCount <= 1)
                return plotArea.Left + plotArea.Width / 2F;

            return plotArea.Left +
                   plotArea.Width * pointIndex / (pointCount - 1);
        }

        private float GetY(
            decimal value,
            decimal minimumValue,
            decimal maximumValue,
            RectangleF plotArea)
        {
            if (maximumValue == minimumValue)
                return plotArea.Top + plotArea.Height / 2F;

            decimal position =
                (value - minimumValue) / (maximumValue - minimumValue);

            return plotArea.Bottom -
                   (float)position * plotArea.Height;
        }
    }

    // ------------------------------------------------------------
    // Сглаженная синяя кнопка
    // ------------------------------------------------------------

    public class SmoothMenuButton : Control
    {
        private readonly Color normalColor =
            Color.FromArgb(0x1A, 0x56, 0xE8);

        private readonly Color hoverColor =
            Color.FromArgb(21, 72, 196);

        private readonly Color pressedColor =
            Color.FromArgb(16, 61, 170);

        private bool hovered;
        private bool pressed;

        public SmoothMenuButton()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor,
                true
            );

            BackColor = Color.Transparent;
            ForeColor = Color.White;
            Size = new Size(132, 42);
            Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Cursor = Cursors.Hand;
            TabStop = true;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Parent == null)
            {
                base.OnPaintBackground(e);
                return;
            }

            using (SolidBrush brush = new SolidBrush(Parent.BackColor))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            Color currentColor = normalColor;

            if (pressed)
                currentColor = pressedColor;
            else if (hovered)
                currentColor = hoverColor;

            RectangleF rect = new RectangleF(
                0.5F,
                0.5F,
                Width - 1F,
                Height - 1F
            );

            using (GraphicsPath path =
                   CreateRoundedRectangle(rect, Height / 2F))
            using (SolidBrush brush = new SolidBrush(currentColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            TextRenderer.DrawText(
                e.Graphics,
                Text,
                Font,
                ClientRectangle,
                ForeColor,
                TextFormatFlags.HorizontalCenter |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis |
                TextFormatFlags.NoPadding
            );
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            hovered = true;
            Invalidate();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            hovered = false;
            pressed = false;
            Invalidate();

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pressed = true;
                Invalidate();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            pressed = false;
            Invalidate();

            base.OnMouseUp(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                pressed = true;
                Invalidate();
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                pressed = false;
                Invalidate();
                OnClick(EventArgs.Empty);
            }

            base.OnKeyUp(e);
        }

        private GraphicsPath CreateRoundedRectangle(
            RectangleF rect,
            float radius)
        {
            GraphicsPath path = new GraphicsPath();

            float diameter = radius * 2F;

            if (diameter > rect.Width)
                diameter = rect.Width;

            if (diameter > rect.Height)
                diameter = rect.Height;

            RectangleF arc = new RectangleF(
                rect.X,
                rect.Y,
                diameter,
                diameter
            );

            path.AddArc(arc, 180, 90);

            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();

            return path;
        }
    }
}