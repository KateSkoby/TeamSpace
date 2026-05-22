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
using TeamSpace.Models;
using TeamSpace.Services;

namespace TeamSpace.View
{
    /// Форма модуля анализа потребительских расходов.

    /// На форме обеспечиваются:
    /// - загрузка исходных данных из CSV-файла через сервис FileParser;
    /// - отображение загруженных записей в табличной области;
    /// - выбор анализируемого показателя и периода скользящей средней;
    /// - отображение статистических карточек;
    /// - формирование прогнозного ряда через сервис MovingAverageForecast;
    /// - визуализация фактических и прогнозных значений;
    /// - экспорт построенного графика в изображение формата PNG.

    /// Расчётная логика не размещается непосредственно в форме:
    /// статистика и прогнозирование выполняются сервисами,
    /// а данные представлены объектами модели ConsumerExpenseData.
    public partial class ConsumerExpensesForm : Form
    {
        private readonly Color mutedTextColor = Color.FromArgb(110, 118, 138);

        private const int ActionButtonWidth = 174;
        private const int ActionButtonHeight = 38;
        private const int DefaultMovingAverageWindow = 3;

        // Смещение кнопок влево относительно текущего положения.
        private const int HeaderButtonLeftOffset = 40;
        private const int SettingsButtonsLeftOffset = 30;

        private SmoothMenuButton openCsvButton;
        private SmoothMenuButton buildGraphButton;
        private SmoothMenuButton exportPngButton;

        private Label lblIndicator;
        private ComboBox cmbIndicator;

        private Label lblAverageWindow;
        private NumericUpDown numAverageWindow;

        private List<ConsumerExpenseData> expensesData;
        private ExpensesChartControl expensesChart;
        private bool chartWasBuilt;

        /// Создаёт форму и выполняет её первоначальную визуальную настройку.

        /// После стандартной инициализации компонентов выполняется
        /// конфигурирование областей формы, создаются дополнительные
        /// элементы управления и назначаются обработчики выравнивания.
        public ConsumerExpensesForm()
        {
            // Выполняется загрузка элементов управления,
            // сформированных средствами визуального конструктора формы.
            InitializeComponent();

            // Включение двойной буферизации уменьшает мерцание
            // при перерисовке панелей и изменении размеров окна.
            DoubleBuffered = true;

            // Выполняется последовательная настройка всех функциональных
            // областей формы и замена стандартных кнопок стилизованными.
            ConfigureFormSize();
            ConfigureLeftTablePanel();
            ConfigureRightLayout();
            ConfigureStatisticsCards();
            ConfigureSingleChart();
            ConfigureSettingsPanel();
            ReplaceDesignerButtons();

            // При изменении размеров контейнеров созданные программно
            // элементы заново выравниваются относительно их границ.
            headerPanel.SizeChanged += delegate { AlignCustomControls(); };
            settingsPanel.SizeChanged += delegate { AlignCustomControls(); };

            Load += delegate { AlignCustomControls(); };
            Shown += delegate { AlignCustomControls(); };

            AlignCustomControls();
        }

        // ------------------------------------------------------------
        // Размер формы
        // ------------------------------------------------------------

        /// Устанавливает первоначальный и минимально допустимый размер формы.

        /// Ограничение минимального размера требуется для сохранения
        /// корректного расположения таблицы, карточек, панели настроек
        /// и области графика.
        private void ConfigureFormSize()
        {
            ClientSize = new Size(1320, 860);
            MinimumSize = new Size(1320, 860);
        }

        // ------------------------------------------------------------
        // Левая панель с таблицей
        // ------------------------------------------------------------

        /// Настраивает левую область, предназначенную для отображения
        /// загруженных годовых данных о потребительских расходах.

        /// В методе задаются параметры заголовка таблицы,
        /// режим только для чтения, стиль выделения строк,
        /// автоматический подбор ширины столбцов и параметры прокрутки.
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

        /// Настраивает компоновку правой части формы.

        /// Правая область разбивается на три строки:
        /// карточки статистики, панель параметров анализа
        /// и оставшееся пространство для графика.
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

        /// Формирует область карточек статистики.

        /// В модуле расходов отображаются три показателя:
        /// максимальное изменение, минимальное изменение
        /// и количество доступных периодов наблюдения.
        /// Неиспользуемая карточка среднего значения скрывается.
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

        /// Применяет единое компактное оформление к карточке статистики.
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

        /// Настраивает единственную область графика, на которой
        /// совместно отображаются фактические значения и прогноз.

        /// Дополнительная карточка отдельного прогнозного графика
        /// скрывается, а в основную карточку добавляется
        /// пользовательский элемент ExpensesChartControl.
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

        /// Настраивает панель параметров анализа.

        /// На панели создаются и размещаются элементы выбора:
        /// - показателя расходов;
        /// - периода скользящей средней;
        /// - числа будущих лет прогнозирования.

        /// До успешной загрузки CSV-файла элементы анализа
        /// остаются недоступными для изменения.
        private void ConfigureSettingsPanel()
        {
            // Элементы, оставшиеся от исходного варианта формы,
            // скрываются, поскольку вместо них применяется отдельный
            // числовой выбор периода скользящей средней.
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

            // Список показателей берётся из модели данных,
            // благодаря чему названия столбцов и варианты выбора
            // поддерживаются в одном месте проекта.
            cmbIndicator.Items.AddRange(
                ConsumerExpenseData.IndicatorNames
                    .Cast<object>()
                    .ToArray()
            );

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

        /// Заменяет стандартные кнопки, созданные в Designer,
        /// на пользовательские сглаженные кнопки единого стиля.

        /// Стандартные элементы скрываются, после чего на форму
        /// добавляются кнопки загрузки CSV, построения графика
        /// и экспорта результата в PNG.
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

        /// Создаёт пользовательскую кнопку меню с заданными размерами
        /// и назначенным обработчиком нажатия.
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

        /// Выполняет общее выравнивание динамически созданных элементов.

        /// Метод вызывается после создания интерфейса и при изменении
        /// размеров контейнеров, чтобы сохранить требуемое расположение.
        private void AlignCustomControls()
        {
            AlignHeaderButton();
            AlignSettingsControls();
        }

        /// Выравнивает кнопку открытия CSV-файла
        /// относительно правой границы заголовочной панели.
        private void AlignHeaderButton()
        {
            if (openCsvButton == null)
                return;

            // Координата X вычисляется от правого края панели.
            // Дополнительное смещение уменьшает значение X,
            // поэтому кнопка перемещается левее.
            int x = headerPanel.ClientSize.Width
                    - headerPanel.Padding.Right
                    - openCsvButton.Width
                    - HeaderButtonLeftOffset;

            openCsvButton.Location = new Point(
                Math.Max(headerPanel.Padding.Left, x),
                34
            );
        }

        /// Вычисляет и назначает положения элементов панели анализа.

        /// Параметры располагаются слева, а кнопки построения графика
        /// и экспорта выравниваются по правой стороне панели.
        private void AlignSettingsControls()
        {
            if (settingsPanel == null)
                return;

            int indicatorX = 24;
            int periodX = 194;
            int forecastX = 292;

            int labelY = 47;
            int controlY = 72;

            // Координата обеих кнопок рассчитывается от правой границы панели.
            // Вычитание SettingsButtonsLeftOffset перемещает кнопки левее.
            int buttonX = settingsPanel.ClientSize.Width
                          - settingsPanel.Padding.Right
                          - ActionButtonWidth
                          - SettingsButtonsLeftOffset;

            // Минимальная граница также немного уменьшена,
            // чтобы смещение сохранялось при небольшом размере панели.
            buttonX = Math.Max(395, buttonX);

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

        /// Сохраняет совместимость с обработчиком, назначенным в Designer.

        /// Фактическая отрисовка актуальных кнопок выполняется
        /// пользовательским классом SmoothMenuButton.
        private void MenuBlueButton_Paint(object sender, PaintEventArgs e)
        {
        }

        // ------------------------------------------------------------
        // Доступность полей анализа
        // ------------------------------------------------------------

        /// Управляет доступностью элементов выбора параметров анализа.
        private void SetAnalysisInputsEnabled(bool enabled)
        {
            if (cmbIndicator != null)
                cmbIndicator.Enabled = enabled;

            if (numAverageWindow != null)
                numAverageWindow.Enabled = enabled;

            if (numForecastYears != null)
                numForecastYears.Enabled = enabled;
        }

        /// Сбрасывает ранее загруженные данные и результаты анализа.

        /// Метод подготавливает форму к чтению нового CSV-файла:
        /// очищаются модели, таблица и график, сбрасываются карточки
        /// статистики и временно блокируются параметры анализа.
        private void ResetLoadedData()
        {
            // Удаляется признак ранее построенного графика
            // и создаётся новый пустой список моделей расходов.
            chartWasBuilt = false;
            expensesData = new List<ConsumerExpenseData>();

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

        /// Обрабатывает нажатие кнопки открытия CSV-файла.

        /// Выполняются поиск файла consumer_expenses.csv,
        /// сброс предыдущего состояния, загрузка данных через FileParser,
        /// заполнение таблицы, расчёт статистики и настройка диапазона
        /// допустимого периода скользящей средней.
        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                // Определяется ожидаемое расположение CSV-файла
                // с исходными данными модуля расходов.
                string filePath = FindConsumerExpensesCsvPath();

                // При отсутствии файла расчёт и отображение
                // новых данных не выполняются.
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

                // Предыдущее состояние формы очищается перед загрузкой
                // нового набора данных.
                ResetLoadedData();

                // Чтение и преобразование CSV выполняются сервисом,
                // возвращающим список объектов модели расходов.
                expensesData = FileParser.LoadConsumerExpensesFromCsv(filePath);

                // После успешной загрузки обновляются таблица, карточки
                // статистики и доступный диапазон периода прогноза.
                ShowExpensesInGrid();
                UpdateStatistics();
                ConfigureAverageWindowRange();

                MessageBox.Show(
                    "Данные успешно загружены.\n" +
                    "Доступный период скользящей средней: от 2 до " +
                    expensesData.Count + ".\n\n" +
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

        /// Выполняет поиск CSV-файла расходов в каталоге Data.

        /// Сначала проверяется папка рядом с запущенным приложением.
        /// Если файл там отсутствует, выполняется подъём по родительским
        /// каталогам для поиска папки Data в структуре проекта.
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

        /// Настраивает допустимый диапазон периода скользящей средней
        /// на основании количества загруженных годовых записей.

        /// Минимально допускается два периода, а максимальное значение
        /// соответствует числу доступных строк исходных данных.
        private void ConfigureAverageWindowRange()
        {
            if (numAverageWindow == null)
                return;

            int periodsCount = expensesData != null
                ? expensesData.Count
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

        // ------------------------------------------------------------
        // Заполнение таблицы
        // ------------------------------------------------------------

        /// Отображает список моделей ConsumerExpenseData в таблице формы.

        /// После привязки источника задаются формат денежных столбцов,
        /// выравнивание значений и запрет ручной сортировки столбцов.
        private void ShowExpensesInGrid()
        {
            // Предыдущая привязка удаляется, после чего таблице
            // назначается актуальный список объектов модели.
            dgvExpenses.DataSource = null;
            dgvExpenses.DataSource = expensesData;

            dgvExpenses.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.AllCells;

            dgvExpenses.ScrollBars = ScrollBars.Both;

            foreach (DataGridViewColumn column in dgvExpenses.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

                if (column.DataPropertyName != nameof(ConsumerExpenseData.Year))
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

        /// Обрабатывает изменение выбранного показателя расходов.

        /// После выбора другого показателя пересчитываются карточки
        /// статистики и, при наличии построенного графика,
        /// обновляется прогнозное отображение.
        private void cmbIndicator_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatistics();
            RebuildChartIfAlreadyBuilt();
        }

        /// Обрабатывает изменение количества прогнозируемых лет.

        /// При уже построенном графике выполняется его обновление
        /// с учётом нового горизонта прогнозирования.
        private void numForecastYears_ValueChanged(object sender, EventArgs e)
        {
            RebuildChartIfAlreadyBuilt();
        }

        /// Обрабатывает изменение периода скользящей средней.

        /// При наличии ранее построенного графика инициируется
        /// повторный расчёт прогнозных значений.
        private void numAverageWindow_ValueChanged(object sender, EventArgs e)
        {
            RebuildChartIfAlreadyBuilt();
        }

        /// Перестраивает график после изменения параметров только в случае,
        /// если до этого уже было выполнено его первоначальное построение.

        /// Возможная ошибка повторного расчёта перехватывается
        /// и выводится в отдельном информационном сообщении.
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

        /// Запрашивает статистику выбранного показателя у сервиса
        /// StatisticsCalculator и выводит результат в карточки формы.

        /// Максимальное и минимальное процентные изменения
        /// форматируются с использованием русской культуры.
        private void UpdateStatistics()
        {
            // Расчёт процентной динамики не выполняется формой напрямую:
            // данные и выбранный показатель передаются сервису статистики.
            ConsumerExpenseStatistics statistics =
                StatisticsCalculator.CalculateExpenseStatistics(
                    expensesData,
                    GetSelectedIndicator()
                );

            lblYearsValue.Text = statistics.PeriodCount.ToString();

            CultureInfo russianCulture =
                CultureInfo.GetCultureInfo("ru-RU");

            lblMaxValue.Text =
                statistics.MaximumPercentageChange.ToString(
                    "0.##",
                    russianCulture
                ) + " %";

            lblMinValue.Text =
                statistics.MinimumPercentageChange.ToString(
                    "0.##",
                    russianCulture
                ) + " %";
        }

        /// Возвращает наименование показателя, выбранного для анализа.
        private string GetSelectedIndicator()
        {
            if (cmbIndicator != null && cmbIndicator.SelectedItem != null)
                return cmbIndicator.SelectedItem.ToString();

            return "Общие расходы";
        }

        /// Возвращает установленный размер окна скользящей средней.
        private int GetSelectedMovingAverageWindow()
        {
            if (numAverageWindow != null && numAverageWindow.Enabled)
                return (int)numAverageWindow.Value;

            return DefaultMovingAverageWindow;
        }

        // ------------------------------------------------------------
        // Прогнозирование методом скользящей средней
        // ------------------------------------------------------------

        /// Обрабатывает нажатие кнопки построения графика.

        /// Перед расчётом проверяется наличие не менее двух периодов
        /// исходных данных. При успешной проверке строится график
        /// и устанавливается признак наличия рассчитанного результата.
        private void btnBuildForecast_Click(object sender, EventArgs e)
        {
            try
            {
                if (expensesData == null || expensesData.Count == 0)
                {
                    MessageBox.Show(
                        "Сначала загрузите данные кнопкой «Открыть CSV».",
                        "Нет данных",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                if (expensesData.Count < 2)
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

        /// Формирует фактический и прогнозный ряды выбранного показателя.
        /// Исходные записи сортируются по году, преобразуются в точки
        /// графика, после чего значения передаются общему сервису
        /// прогнозирования методом скользящей средней.
        /// Сформированные ряды передаются элементу ExpensesChartControl.
        private void BuildForecastChart()
        {
            string indicator = GetSelectedIndicator();
            int movingAverageWindow = GetSelectedMovingAverageWindow();

            if (expensesData == null || expensesData.Count == 0)
            {
                throw new InvalidOperationException(
                    "Нет данных для построения графика."
                );
            }

            // Исходные записи располагаются в хронологическом порядке,
            // необходимом для корректного отображения временного ряда.
            List<ConsumerExpenseData> orderedData = expensesData
                .OrderBy(item => item.Year)
                .ToList();

            List<ExpensePoint> actualPoints = orderedData
                .Select(item => new ExpensePoint(
                    item.Year,
                    item.GetIndicatorValue(indicator)
                ))
                .ToList();

            List<decimal> historicalValues = actualPoints
                .Select(point => point.Value)
                .ToList();

            int forecastYears = (int)numForecastYears.Value;

            // Вычисление будущих значений делегируется общему сервису
            // прогнозирования, поддерживающему денежный тип decimal.
            List<decimal> forecastValues =
                MovingAverageForecast.CalculateForecast(
                    historicalValues,
                    movingAverageWindow,
                    forecastYears
                );

            int lastYear = actualPoints.Last().Year;

            List<ExpensePoint> forecastPoints = forecastValues
                .Select((value, index) => new ExpensePoint(
                    lastYear + index + 1,
                    Math.Round(value, 2)
                ))
                .ToList();

            // Фактический и прогнозный ряды передаются
            // специализированному элементу отрисовки графика.
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

        /// Обрабатывает экспорт построенного графика в PNG-файл.

        /// При наличии данных открывается диалог выбора места сохранения,
        /// текущее изображение элемента графика копируется в Bitmap
        /// и сохраняется в выбранный PNG-файл.
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

                    // Создаётся растровое изображение с размерами
                    // текущей области графика для дальнейшего сохранения.
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

    /// Модель одной точки временного ряда, отображаемой на графике.

    /// Экземпляр хранит год наблюдения или прогнозирования
    /// и соответствующее денежное значение выбранного показателя.
    public class ExpensePoint
    {
        /// Год, соответствующий значению точки.
        public int Year { get; private set; }

        /// Значение выбранного показателя расходов за указанный год.
        public decimal Value { get; private set; }

        /// Создаёт точку временного ряда с заданными координатами данных.
        public ExpensePoint(int year, decimal value)
        {
            // Переданные значения сохраняются в свойствах модели
            // и впоследствии используются при вычислении координат графика.
            Year = year;
            Value = value;
        }
    }

    // ------------------------------------------------------------
    // Пользовательский график данных и прогноза
    // ------------------------------------------------------------

    /// Пользовательский элемент управления для визуализации
    /// фактических и прогнозных значений потребительских расходов.

    /// Отрисовка выполняется средствами Graphics без стандартного
    /// компонента диаграмм: отображаются сетка, оси, подписи годов,
    /// легенда, фактическая линия, прогнозная линия
    /// и выделенная фоновая область прогноза.
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

        /// Возвращает признак наличия фактических точек,
        /// достаточных для отображения графика.
        public bool HasData
        {
            get
            {
                return actualPoints != null && actualPoints.Count > 0;
            }
        }

        /// Создаёт элемент графика и включает режимы качественной
        /// пользовательской отрисовки с двойной буферизацией.
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

            // При создании элемента устанавливаются пустые наборы точек,
            // поэтому до передачи данных отображается только подсказка.
            actualPoints = new List<ExpensePoint>();
            forecastPoints = new List<ExpensePoint>();
            indicatorName = string.Empty;
        }

        /// Передаёт элементу управления новые данные для отображения.
        public void SetData(
            List<ExpensePoint> actual,
            List<ExpensePoint> forecast,
            string indicator,
            int movingAverageWindow)
        {
            // Переданные ряды сохраняются во внутреннем состоянии.
            // При передаче null используются пустые безопасные коллекции.
            actualPoints = actual ?? new List<ExpensePoint>();
            forecastPoints = forecast ?? new List<ExpensePoint>();
            indicatorName = indicator ?? string.Empty;
            windowSize = movingAverageWindow;

            // Запрашивается повторная отрисовка с новыми данными.
            Invalidate();
        }

        /// Очищает отображаемые ряды и возвращает график
        /// в состояние отсутствия загруженных данных.
        public void ClearData()
        {
            actualPoints.Clear();
            forecastPoints.Clear();
            indicatorName = string.Empty;

            Invalidate();
        }

        /// Выполняет полную пользовательскую отрисовку графика.

        /// При отсутствии точек отображается информационная надпись.
        /// При наличии данных рассчитывается рабочая область графика,
        /// диапазон оси значений и последовательно рисуются
        /// фон прогноза, сетка, линии данных и легенда.
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graphics = e.Graphics;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;

            graphics.Clear(Color.White);

            // При отсутствии фактических точек вместо осей и линий
            // выводится информационное состояние пустого графика.
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

            // Фактические и прогнозные точки объединяются
            // для определения общего диапазона осей и подписей годов.
            List<ExpensePoint> allPoints = new List<ExpensePoint>();
            allPoints.AddRange(actualPoints);
            allPoints.AddRange(forecastPoints);

            // Вычисляются границы вертикального диапазона,
            // после чего добавляется визуальный запас по краям графика.
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

        /// Выводит подсказку в пустой области графика,
        /// когда исходные данные ещё не загружены и не рассчитаны.
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

        /// Расширяет диапазон вертикальной оси для визуальных отступов.

        /// При совпадении минимального и максимального значения
        /// создаётся искусственный диапазон, необходимый
        /// для корректного вычисления координат.
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

        /// Выделяет участок будущих периодов светлым фоном
        /// и пунктирной линией отделяет его от фактических данных.
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

            // Граница области прогноза располагается посередине
            // между последней фактической и первой прогнозной точками.
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

        /// Рисует горизонтальную и вертикальную сетку,
        /// подписи значений, подписи годов и границу области графика.
        private void DrawGridAndAxes(
            Graphics graphics,
            RectangleF plotArea,
            List<ExpensePoint> allPoints,
            decimal minimumValue,
            decimal maximumValue)
        {
            // Вертикальный диапазон делится на пять одинаковых секций,
            // что определяет число интервалов горизонтальной сетки.
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

                // Для большого числа периодов подписи годов прореживаются,
                // чтобы текстовые значения не перекрывали друг друга.
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

        /// Рисует линию и маркеры фактических значений
        /// выбранного показателя расходов.
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

        /// Рисует линию и маркеры прогнозного ряда.

        /// Начальная точка линии совпадает с последним фактическим
        /// значением, что обеспечивает визуальную непрерывность графика.
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

            // Последняя фактическая точка включается в прогнозную линию,
            // благодаря чему между рядами не образуется разрыв.
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

        /// Отображает название показателя и пояснения цветов линий
        /// фактического и прогнозного рядов.
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

        /// Преобразует порядковый номер точки в горизонтальную координату.
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

        /// Преобразует числовое значение точки в вертикальную координату
        /// с учётом диапазона отображаемой оси.
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

    /// Пользовательская кнопка с закруглённой формой
    /// и визуальными состояниями наведения и нажатия.

    /// Элемент применяется вместо стандартных кнопок формы,
    /// чтобы сохранить единое оформление интерфейса.
    /// Управление поддерживается как мышью, так и клавиатурой.
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

        /// Создаёт кнопку, задаёт стандартное оформление
        /// и включает оптимизированную пользовательскую отрисовку.
        public SmoothMenuButton()
        {
            // Включаются режимы собственной отрисовки, двойной
            // буферизации и корректной обработки прозрачного фона.
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

        /// Отрисовывает фон элемента с учётом цвета родительской панели.

        /// Такой способ позволяет визуально сохранить прозрачное
        /// окружение вокруг скруглённой поверхности кнопки.
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

        /// Рисует скруглённую поверхность кнопки и центрированный текст.
        /// Цвет поверхности зависит от текущего интерактивного состояния.
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            // Выбирается цвет поверхности кнопки согласно приоритету:
            // нажатое состояние имеет приоритет над наведением.
            Color currentColor = normalColor;

            if (pressed)
                currentColor = pressedColor;
            else if (hovered)
                currentColor = hoverColor;

            // Рабочая область немного уменьшается относительно границ
            // элемента, чтобы исключить обрезание сглаженного контура.
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

        /// Включает состояние наведения при попадании указателя
        /// мыши в область кнопки.
        protected override void OnMouseEnter(EventArgs e)
        {
            hovered = true;
            Invalidate();

            base.OnMouseEnter(e);
        }

        /// Сбрасывает состояния наведения и нажатия
        /// после выхода указателя мыши из области кнопки.
        protected override void OnMouseLeave(EventArgs e)
        {
            hovered = false;
            pressed = false;
            Invalidate();

            base.OnMouseLeave(e);
        }

        /// Включает состояние нажатия при нажатии левой кнопки мыши.
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pressed = true;
                Invalidate();
            }

            base.OnMouseDown(e);
        }

        /// Сбрасывает состояние нажатия после отпускания кнопки мыши.
        protected override void OnMouseUp(MouseEventArgs e)
        {
            pressed = false;
            Invalidate();

            base.OnMouseUp(e);
        }

        /// Отображает состояние нажатия при активации кнопки
        /// клавишами Enter или Space.
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                pressed = true;
                Invalidate();
            }

            base.OnKeyDown(e);
        }

        /// Завершает клавиатурное нажатие и инициирует событие Click
        /// после отпускания клавиш Enter или Space.
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

        /// Формирует графический контур прямоугольника
        /// со скруглёнными углами для отрисовки кнопки.
        private GraphicsPath CreateRoundedRectangle(
            RectangleF rect,
            float radius)
        {
            GraphicsPath path = new GraphicsPath();

            // Диаметр дуг рассчитывается из переданного радиуса
            // и при необходимости ограничивается размерами элемента.
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