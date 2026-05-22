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

        private SmoothMenuButton openCsvButton;
        private SmoothMenuButton buildGraphButton;
        private SmoothMenuButton exportPngButton;

        private Label lblIndicator;
        private ComboBox cmbIndicator;

        private DataTable expensesTable;

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
        // Основная компоновка правой части
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
        // Компактные карточки статистики
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
        // Один увеличенный график
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

            lblHistoryChartTitle.Text = "График зависимости расходов от года";
            lblHistoryChartTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);

            historyChartCard.Padding = new Padding(18, 12, 18, 18);

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

            lblForecastYears.Text = "Прогноз N лет";
            lblForecastYears.AutoSize = true;
            lblForecastYears.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            lblForecastYears.ForeColor = mutedTextColor;

            numForecastYears.Minimum = 1;
            numForecastYears.Maximum = 15;
            numForecastYears.Value = 3;
            numForecastYears.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            numForecastYears.Size = new Size(108, 30);

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
                Size = new Size(190, 31),
                FlatStyle = FlatStyle.Standard
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

            settingsPanel.Controls.Add(lblIndicator);
            settingsPanel.Controls.Add(cmbIndicator);

            lblIndicator.BringToFront();
            cmbIndicator.BringToFront();
            lblForecastYears.BringToFront();
            numForecastYears.BringToFront();
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
            int forecastX = 300;

            int labelY = 47;
            int controlY = 72;

            int buttonX = settingsPanel.ClientSize.Width
                          - settingsPanel.Padding.Right
                          - ActionButtonWidth;

            buttonX = Math.Max(470, buttonX);

            if (lblIndicator != null)
                lblIndicator.Location = new Point(indicatorX, labelY);

            if (cmbIndicator != null)
            {
                cmbIndicator.Size = new Size(190, 31);
                cmbIndicator.Location = new Point(indicatorX, controlY);
            }

            lblForecastYears.Location = new Point(forecastX, labelY);
            numForecastYears.Location = new Point(forecastX, controlY);

            if (buildGraphButton != null)
            {
                buildGraphButton.Location = new Point(buttonX, 25);
            }

            if (exportPngButton != null)
            {
                exportPngButton.Location = new Point(buttonX, 72);
            }
        }

        // Метод оставлен, так как он привязан в Designer.
        private void MenuBlueButton_Paint(object sender, PaintEventArgs e)
        {
        }

        // ------------------------------------------------------------
        // Загрузка CSV-файла из папки Data
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

                LoadExpensesFromCsv(filePath);

                MessageBox.Show(
                    "Данные успешно загружены из файла:\n" + filePath,
                    "Открытие CSV",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
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
                    .Select((header, index) => new { Header = header, Index = index })
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
        // Вывод данных в таблицу
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
        // Подсчёт статистики
        // ------------------------------------------------------------

        private void cmbIndicator_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatistics();
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

            string selectedIndicator =
                cmbIndicator != null &&
                cmbIndicator.SelectedItem != null
                    ? cmbIndicator.SelectedItem.ToString()
                    : "Общие расходы";

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

        // ------------------------------------------------------------
        // Построение графика
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

                string indicator =
                    cmbIndicator != null &&
                    cmbIndicator.SelectedItem != null
                        ? cmbIndicator.SelectedItem.ToString()
                        : "Показатель не выбран";

                int forecastYears = (int)numForecastYears.Value;

                MessageBox.Show(
                    "Построение графика.\n\n" +
                    "Показатель: " + indicator + "\n" +
                    "Прогноз: " + forecastYears + " лет",
                    "График",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                /*
                 * Здесь позже можно реализовать единый график:
                 * - исходные значения выбранного показателя;
                 * - прогноз на выбранное число лет;
                 * - отображение обеих линий в pnlHistoryChart.
                 */
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

        // ------------------------------------------------------------
        // Экспорт графика в PNG
        // ------------------------------------------------------------

        private void btnExportPng_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlHistoryChart.Width <= 0 || pnlHistoryChart.Height <= 0)
                {
                    MessageBox.Show(
                        "Область графика недоступна для экспорта.",
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
                    saveDialog.FileName = "consumer_expenses_chart.png";
                    saveDialog.DefaultExt = "png";
                    saveDialog.AddExtension = true;

                    if (saveDialog.ShowDialog() != DialogResult.OK)
                        return;

                    using (Bitmap bitmap = new Bitmap(
                        pnlHistoryChart.Width,
                        pnlHistoryChart.Height))
                    {
                        pnlHistoryChart.DrawToBitmap(
                            bitmap,
                            new Rectangle(
                                0,
                                0,
                                pnlHistoryChart.Width,
                                pnlHistoryChart.Height
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
    // Сглаженная кнопка в стиле главного меню
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