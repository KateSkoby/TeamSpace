namespace TeamSpace.View
{
    partial class ConsumerExpensesForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.btnLoadFile = new System.Windows.Forms.Panel();
            this.lblLoadFile = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.gridCardPanel = new System.Windows.Forms.Panel();
            this.dgvExpenses = new System.Windows.Forms.DataGridView();
            this.gridHeaderPanel = new System.Windows.Forms.Panel();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.rightLayout = new System.Windows.Forms.TableLayoutPanel();
            this.statsPanel = new System.Windows.Forms.Panel();
            this.statsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.cardMax = new System.Windows.Forms.Panel();
            this.lblMaxValue = new System.Windows.Forms.Label();
            this.lblMaxTitle = new System.Windows.Forms.Label();
            this.cardMin = new System.Windows.Forms.Panel();
            this.lblMinValue = new System.Windows.Forms.Label();
            this.lblMinTitle = new System.Windows.Forms.Label();
            this.cardAvg = new System.Windows.Forms.Panel();
            this.lblAvgValue = new System.Windows.Forms.Label();
            this.lblAvgTitle = new System.Windows.Forms.Label();
            this.cardYears = new System.Windows.Forms.Panel();
            this.lblYearsValue = new System.Windows.Forms.Label();
            this.lblYearsTitle = new System.Windows.Forms.Label();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.btnBuildForecast = new System.Windows.Forms.Panel();
            this.lblBuildForecast = new System.Windows.Forms.Label();
            this.numForecastYears = new System.Windows.Forms.NumericUpDown();
            this.lblForecastYears = new System.Windows.Forms.Label();
            this.cmbMovingAverage = new System.Windows.Forms.ComboBox();
            this.lblMovingAverage = new System.Windows.Forms.Label();
            this.lblSettingsTitle = new System.Windows.Forms.Label();
            this.chartsPanel = new System.Windows.Forms.Panel();
            this.chartLayout = new System.Windows.Forms.TableLayoutPanel();
            this.historyChartCard = new System.Windows.Forms.Panel();
            this.pnlHistoryChart = new System.Windows.Forms.Panel();
            this.lblHistoryChartTitle = new System.Windows.Forms.Label();
            this.forecastChartCard = new System.Windows.Forms.Panel();
            this.pnlForecastChart = new System.Windows.Forms.Panel();
            this.lblForecastChartTitle = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.headerPanel.SuspendLayout();
            this.btnLoadFile.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.mainTableLayout.SuspendLayout();
            this.leftPanel.SuspendLayout();
            this.gridCardPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenses)).BeginInit();
            this.gridHeaderPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.rightLayout.SuspendLayout();
            this.statsPanel.SuspendLayout();
            this.statsLayout.SuspendLayout();
            this.cardMax.SuspendLayout();
            this.cardMin.SuspendLayout();
            this.cardAvg.SuspendLayout();
            this.cardYears.SuspendLayout();
            this.settingsPanel.SuspendLayout();
            this.btnBuildForecast.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numForecastYears)).BeginInit();
            this.chartsPanel.SuspendLayout();
            this.chartLayout.SuspendLayout();
            this.historyChartCard.SuspendLayout();
            this.forecastChartCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.White;
            this.headerPanel.Controls.Add(this.btnLoadFile);
            this.headerPanel.Controls.Add(this.lblSubtitle);
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Padding = new System.Windows.Forms.Padding(28, 18, 28, 18);
            this.headerPanel.Size = new System.Drawing.Size(1520, 120);
            this.headerPanel.TabIndex = 0;
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadFile.BackColor = System.Drawing.Color.Transparent;
            this.btnLoadFile.Controls.Add(this.lblLoadFile);
            this.btnLoadFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadFile.Location = new System.Drawing.Point(1332, 34);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(132, 42);
            this.btnLoadFile.TabIndex = 2;
            this.btnLoadFile.Paint += new System.Windows.Forms.PaintEventHandler(this.MenuBlueButton_Paint);
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // lblLoadFile
            // 
            this.lblLoadFile.BackColor = System.Drawing.Color.Transparent;
            this.lblLoadFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLoadFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoadFile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLoadFile.ForeColor = System.Drawing.Color.White;
            this.lblLoadFile.Location = new System.Drawing.Point(0, 0);
            this.lblLoadFile.Name = "lblLoadFile";
            this.lblLoadFile.Size = new System.Drawing.Size(132, 42);
            this.lblLoadFile.TabIndex = 0;
            this.lblLoadFile.Text = "Открыть";
            this.lblLoadFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(118)))), ((int)(((byte)(138)))));
            this.lblSubtitle.Location = new System.Drawing.Point(32, 70);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(580, 25);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Потребительские расходы за 15 лет, анализ изменений и прогноз";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Georgia", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(33)))), ((int)(((byte)(46)))));
            this.lblTitle.Location = new System.Drawing.Point(28, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(645, 46);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Анализ потребительских расходов";
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(254)))));
            this.contentPanel.Controls.Add(this.mainTableLayout);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 120);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(24);
            this.contentPanel.Size = new System.Drawing.Size(1520, 760);
            this.contentPanel.TabIndex = 1;
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.ColumnCount = 2;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.mainTableLayout.Controls.Add(this.leftPanel, 0, 0);
            this.mainTableLayout.Controls.Add(this.rightPanel, 1, 0);
            this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout.Location = new System.Drawing.Point(24, 24);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 1;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.Size = new System.Drawing.Size(1472, 712);
            this.mainTableLayout.TabIndex = 0;
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.gridCardPanel);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPanel.Location = new System.Drawing.Point(3, 3);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Padding = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.leftPanel.Size = new System.Drawing.Size(612, 706);
            this.leftPanel.TabIndex = 0;
            // 
            // gridCardPanel
            // 
            this.gridCardPanel.BackColor = System.Drawing.Color.White;
            this.gridCardPanel.Controls.Add(this.dgvExpenses);
            this.gridCardPanel.Controls.Add(this.gridHeaderPanel);
            this.gridCardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCardPanel.Location = new System.Drawing.Point(0, 0);
            this.gridCardPanel.Name = "gridCardPanel";
            this.gridCardPanel.Padding = new System.Windows.Forms.Padding(18);
            this.gridCardPanel.Size = new System.Drawing.Size(598, 706);
            this.gridCardPanel.TabIndex = 0;
            // 
            // dgvExpenses
            // 
            this.dgvExpenses.AllowUserToAddRows = false;
            this.dgvExpenses.AllowUserToDeleteRows = false;
            this.dgvExpenses.AllowUserToResizeRows = false;
            this.dgvExpenses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExpenses.BackgroundColor = System.Drawing.Color.White;
            this.dgvExpenses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvExpenses.ColumnHeadersHeight = 40;
            this.dgvExpenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExpenses.EnableHeadersVisualStyles = false;
            this.dgvExpenses.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(236)))), ((int)(((byte)(248)))));
            this.dgvExpenses.Location = new System.Drawing.Point(18, 78);
            this.dgvExpenses.MultiSelect = false;
            this.dgvExpenses.Name = "dgvExpenses";
            this.dgvExpenses.ReadOnly = true;
            this.dgvExpenses.RowHeadersVisible = false;
            this.dgvExpenses.RowTemplate.Height = 34;
            this.dgvExpenses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExpenses.Size = new System.Drawing.Size(562, 610);
            this.dgvExpenses.TabIndex = 1;
            // 
            // gridHeaderPanel
            // 
            this.gridHeaderPanel.Controls.Add(this.lblGridTitle);
            this.gridHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridHeaderPanel.Location = new System.Drawing.Point(18, 18);
            this.gridHeaderPanel.Name = "gridHeaderPanel";
            this.gridHeaderPanel.Size = new System.Drawing.Size(562, 60);
            this.gridHeaderPanel.TabIndex = 0;
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(33)))), ((int)(((byte)(46)))));
            this.lblGridTitle.Location = new System.Drawing.Point(4, 12);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(307, 35);
            this.lblGridTitle.TabIndex = 0;
            this.lblGridTitle.Text = "Таблица исходных данных";
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.rightLayout);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(621, 3);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.rightPanel.Size = new System.Drawing.Size(848, 706);
            this.rightPanel.TabIndex = 1;
            // 
            // rightLayout
            // 
            this.rightLayout.ColumnCount = 1;
            this.rightLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rightLayout.Controls.Add(this.statsPanel, 0, 0);
            this.rightLayout.Controls.Add(this.settingsPanel, 0, 1);
            this.rightLayout.Controls.Add(this.chartsPanel, 0, 2);
            this.rightLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightLayout.Location = new System.Drawing.Point(14, 0);
            this.rightLayout.Name = "rightLayout";
            this.rightLayout.RowCount = 3;
            this.rightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.rightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.rightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rightLayout.Size = new System.Drawing.Size(834, 706);
            this.rightLayout.TabIndex = 0;
            // 
            // statsPanel
            // 
            this.statsPanel.BackColor = System.Drawing.Color.White;
            this.statsPanel.Controls.Add(this.statsLayout);
            this.statsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statsPanel.Location = new System.Drawing.Point(3, 3);
            this.statsPanel.Name = "statsPanel";
            this.statsPanel.Padding = new System.Windows.Forms.Padding(16);
            this.statsPanel.Size = new System.Drawing.Size(828, 174);
            this.statsPanel.TabIndex = 0;
            // 
            // statsLayout
            // 
            this.statsLayout.ColumnCount = 4;
            this.statsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.statsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.statsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.statsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.statsLayout.Controls.Add(this.cardMax, 0, 0);
            this.statsLayout.Controls.Add(this.cardMin, 1, 0);
            this.statsLayout.Controls.Add(this.cardAvg, 2, 0);
            this.statsLayout.Controls.Add(this.cardYears, 3, 0);
            this.statsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statsLayout.Location = new System.Drawing.Point(16, 16);
            this.statsLayout.Name = "statsLayout";
            this.statsLayout.RowCount = 1;
            this.statsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.statsLayout.Size = new System.Drawing.Size(796, 142);
            this.statsLayout.TabIndex = 0;
            // 
            // cardMax
            // 
            this.cardMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(254)))));
            this.cardMax.Controls.Add(this.lblMaxValue);
            this.cardMax.Controls.Add(this.lblMaxTitle);
            this.cardMax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardMax.Location = new System.Drawing.Point(8, 8);
            this.cardMax.Margin = new System.Windows.Forms.Padding(8);
            this.cardMax.Name = "cardMax";
            this.cardMax.Size = new System.Drawing.Size(183, 126);
            this.cardMax.TabIndex = 0;
            // 
            // lblMaxValue
            // 
            this.lblMaxValue.AutoSize = true;
            this.lblMaxValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblMaxValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(86)))), ((int)(((byte)(232)))));
            this.lblMaxValue.Location = new System.Drawing.Point(15, 53);
            this.lblMaxValue.Name = "lblMaxValue";
            this.lblMaxValue.Size = new System.Drawing.Size(76, 41);
            this.lblMaxValue.TabIndex = 1;
            this.lblMaxValue.Text = "0 %";
            // 
            // lblMaxTitle
            // 
            this.lblMaxTitle.AutoSize = true;
            this.lblMaxTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMaxTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(118)))), ((int)(((byte)(138)))));
            this.lblMaxTitle.Location = new System.Drawing.Point(18, 20);
            this.lblMaxTitle.Name = "lblMaxTitle";
            this.lblMaxTitle.Size = new System.Drawing.Size(144, 23);
            this.lblMaxTitle.TabIndex = 0;
            this.lblMaxTitle.Text = "Макс. изменение";
            // 
            // cardMin
            // 
            this.cardMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(254)))));
            this.cardMin.Controls.Add(this.lblMinValue);
            this.cardMin.Controls.Add(this.lblMinTitle);
            this.cardMin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardMin.Location = new System.Drawing.Point(207, 8);
            this.cardMin.Margin = new System.Windows.Forms.Padding(8);
            this.cardMin.Name = "cardMin";
            this.cardMin.Size = new System.Drawing.Size(183, 126);
            this.cardMin.TabIndex = 1;
            // 
            // lblMinValue
            // 
            this.lblMinValue.AutoSize = true;
            this.lblMinValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblMinValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(86)))), ((int)(((byte)(232)))));
            this.lblMinValue.Location = new System.Drawing.Point(15, 53);
            this.lblMinValue.Name = "lblMinValue";
            this.lblMinValue.Size = new System.Drawing.Size(76, 41);
            this.lblMinValue.TabIndex = 2;
            this.lblMinValue.Text = "0 %";
            // 
            // lblMinTitle
            // 
            this.lblMinTitle.AutoSize = true;
            this.lblMinTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMinTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(118)))), ((int)(((byte)(138)))));
            this.lblMinTitle.Location = new System.Drawing.Point(18, 20);
            this.lblMinTitle.Name = "lblMinTitle";
            this.lblMinTitle.Size = new System.Drawing.Size(141, 23);
            this.lblMinTitle.TabIndex = 1;
            this.lblMinTitle.Text = "Мин. изменение";
            // 
            // cardAvg
            // 
            this.cardAvg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(254)))));
            this.cardAvg.Controls.Add(this.lblAvgValue);
            this.cardAvg.Controls.Add(this.lblAvgTitle);
            this.cardAvg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardAvg.Location = new System.Drawing.Point(406, 8);
            this.cardAvg.Margin = new System.Windows.Forms.Padding(8);
            this.cardAvg.Name = "cardAvg";
            this.cardAvg.Size = new System.Drawing.Size(183, 126);
            this.cardAvg.TabIndex = 2;
            // 
            // lblAvgValue
            // 
            this.lblAvgValue.AutoSize = true;
            this.lblAvgValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblAvgValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(86)))), ((int)(((byte)(232)))));
            this.lblAvgValue.Location = new System.Drawing.Point(15, 53);
            this.lblAvgValue.Name = "lblAvgValue";
            this.lblAvgValue.Size = new System.Drawing.Size(76, 41);
            this.lblAvgValue.TabIndex = 2;
            this.lblAvgValue.Text = "0 %";
            // 
            // lblAvgTitle
            // 
            this.lblAvgTitle.AutoSize = true;
            this.lblAvgTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAvgTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(118)))), ((int)(((byte)(138)))));
            this.lblAvgTitle.Location = new System.Drawing.Point(18, 20);
            this.lblAvgTitle.Name = "lblAvgTitle";
            this.lblAvgTitle.Size = new System.Drawing.Size(140, 23);
            this.lblAvgTitle.TabIndex = 1;
            this.lblAvgTitle.Text = "Средний темп %";
            // 
            // cardYears
            // 
            this.cardYears.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(254)))));
            this.cardYears.Controls.Add(this.lblYearsValue);
            this.cardYears.Controls.Add(this.lblYearsTitle);
            this.cardYears.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardYears.Location = new System.Drawing.Point(605, 8);
            this.cardYears.Margin = new System.Windows.Forms.Padding(8);
            this.cardYears.Name = "cardYears";
            this.cardYears.Size = new System.Drawing.Size(183, 126);
            this.cardYears.TabIndex = 3;
            // 
            // lblYearsValue
            // 
            this.lblYearsValue.AutoSize = true;
            this.lblYearsValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblYearsValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(86)))), ((int)(((byte)(232)))));
            this.lblYearsValue.Location = new System.Drawing.Point(15, 53);
            this.lblYearsValue.Name = "lblYearsValue";
            this.lblYearsValue.Size = new System.Drawing.Size(56, 41);
            this.lblYearsValue.TabIndex = 2;
            this.lblYearsValue.Text = "15";
            // 
            // lblYearsTitle
            // 
            this.lblYearsTitle.AutoSize = true;
            this.lblYearsTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblYearsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(118)))), ((int)(((byte)(138)))));
            this.lblYearsTitle.Location = new System.Drawing.Point(18, 20);
            this.lblYearsTitle.Name = "lblYearsTitle";
            this.lblYearsTitle.Size = new System.Drawing.Size(128, 23);
            this.lblYearsTitle.TabIndex = 1;
            this.lblYearsTitle.Text = "Число периодов";
            // 
            // settingsPanel
            // 
            this.settingsPanel.BackColor = System.Drawing.Color.White;
            this.settingsPanel.Controls.Add(this.btnBuildForecast);
            this.settingsPanel.Controls.Add(this.numForecastYears);
            this.settingsPanel.Controls.Add(this.lblForecastYears);
            this.settingsPanel.Controls.Add(this.cmbMovingAverage);
            this.settingsPanel.Controls.Add(this.lblMovingAverage);
            this.settingsPanel.Controls.Add(this.lblSettingsTitle);
            this.settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPanel.Location = new System.Drawing.Point(3, 183);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Padding = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.settingsPanel.Size = new System.Drawing.Size(828, 134);
            this.settingsPanel.TabIndex = 1;
            // 
            // btnBuildForecast
            // 
            this.btnBuildForecast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuildForecast.BackColor = System.Drawing.Color.Transparent;
            this.btnBuildForecast.Controls.Add(this.lblBuildForecast);
            this.btnBuildForecast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuildForecast.Location = new System.Drawing.Point(676, 61);
            this.btnBuildForecast.Name = "btnBuildForecast";
            this.btnBuildForecast.Size = new System.Drawing.Size(132, 42);
            this.btnBuildForecast.TabIndex = 5;
            this.btnBuildForecast.Paint += new System.Windows.Forms.PaintEventHandler(this.MenuBlueButton_Paint);
            this.btnBuildForecast.Click += new System.EventHandler(this.btnBuildForecast_Click);
            // 
            // lblBuildForecast
            // 
            this.lblBuildForecast.BackColor = System.Drawing.Color.Transparent;
            this.lblBuildForecast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBuildForecast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBuildForecast.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBuildForecast.ForeColor = System.Drawing.Color.White;
            this.lblBuildForecast.Location = new System.Drawing.Point(0, 0);
            this.lblBuildForecast.Name = "lblBuildForecast";
            this.lblBuildForecast.Size = new System.Drawing.Size(132, 42);
            this.lblBuildForecast.TabIndex = 0;
            this.lblBuildForecast.Text = "Построить";
            this.lblBuildForecast.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBuildForecast.Click += new System.EventHandler(this.btnBuildForecast_Click);
            // 
            // numForecastYears
            // 
            this.numForecastYears.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numForecastYears.Location = new System.Drawing.Point(369, 67);
            this.numForecastYears.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numForecastYears.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numForecastYears.Name = "numForecastYears";
            this.numForecastYears.Size = new System.Drawing.Size(120, 30);
            this.numForecastYears.TabIndex = 4;
            this.numForecastYears.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lblForecastYears
            // 
            this.lblForecastYears.AutoSize = true;
            this.lblForecastYears.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblForecastYears.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(118)))), ((int)(((byte)(138)))));
            this.lblForecastYears.Location = new System.Drawing.Point(365, 39);
            this.lblForecastYears.Name = "lblForecastYears";
            this.lblForecastYears.Size = new System.Drawing.Size(149, 23);
            this.lblForecastYears.TabIndex = 3;
            this.lblForecastYears.Text = "Горизонт прогноза";
            // 
            // cmbMovingAverage
            // 
            this.cmbMovingAverage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMovingAverage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbMovingAverage.FormattingEnabled = true;
            this.cmbMovingAverage.Items.AddRange(new object[] {
            "Скользящая средняя (3)",
            "Скользящая средняя (4)",
            "Скользящая средняя (5)"});
            this.cmbMovingAverage.Location = new System.Drawing.Point(24, 67);
            this.cmbMovingAverage.Name = "cmbMovingAverage";
            this.cmbMovingAverage.Size = new System.Drawing.Size(303, 31);
            this.cmbMovingAverage.TabIndex = 2;
            // 
            // lblMovingAverage
            // 
            this.lblMovingAverage.AutoSize = true;
            this.lblMovingAverage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMovingAverage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(118)))), ((int)(((byte)(138)))));
            this.lblMovingAverage.Location = new System.Drawing.Point(20, 39);
            this.lblMovingAverage.Name = "lblMovingAverage";
            this.lblMovingAverage.Size = new System.Drawing.Size(146, 23);
            this.lblMovingAverage.TabIndex = 1;
            this.lblMovingAverage.Text = "Метод прогноза";
            // 
            // lblSettingsTitle
            // 
            this.lblSettingsTitle.AutoSize = true;
            this.lblSettingsTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblSettingsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(33)))), ((int)(((byte)(46)))));
            this.lblSettingsTitle.Location = new System.Drawing.Point(18, 8);
            this.lblSettingsTitle.Name = "lblSettingsTitle";
            this.lblSettingsTitle.Size = new System.Drawing.Size(216, 30);
            this.lblSettingsTitle.TabIndex = 0;
            this.lblSettingsTitle.Text = "Настройки анализа";
            // 
            // chartsPanel
            // 
            this.chartsPanel.BackColor = System.Drawing.Color.Transparent;
            this.chartsPanel.Controls.Add(this.chartLayout);
            this.chartsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartsPanel.Location = new System.Drawing.Point(3, 323);
            this.chartsPanel.Name = "chartsPanel";
            this.chartsPanel.Size = new System.Drawing.Size(828, 380);
            this.chartsPanel.TabIndex = 2;
            // 
            // chartLayout
            // 
            this.chartLayout.ColumnCount = 1;
            this.chartLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.chartLayout.Controls.Add(this.historyChartCard, 0, 0);
            this.chartLayout.Controls.Add(this.forecastChartCard, 0, 1);
            this.chartLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartLayout.Location = new System.Drawing.Point(0, 0);
            this.chartLayout.Name = "chartLayout";
            this.chartLayout.RowCount = 2;
            this.chartLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.chartLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.chartLayout.Size = new System.Drawing.Size(828, 380);
            this.chartLayout.TabIndex = 0;
            // 
            // historyChartCard
            // 
            this.historyChartCard.BackColor = System.Drawing.Color.White;
            this.historyChartCard.Controls.Add(this.pnlHistoryChart);
            this.historyChartCard.Controls.Add(this.lblHistoryChartTitle);
            this.historyChartCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyChartCard.Location = new System.Drawing.Point(3, 3);
            this.historyChartCard.Name = "historyChartCard";
            this.historyChartCard.Padding = new System.Windows.Forms.Padding(18, 14, 18, 18);
            this.historyChartCard.Size = new System.Drawing.Size(822, 184);
            this.historyChartCard.TabIndex = 0;
            // 
            // pnlHistoryChart
            // 
            this.pnlHistoryChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(254)))));
            this.pnlHistoryChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHistoryChart.Location = new System.Drawing.Point(18, 53);
            this.pnlHistoryChart.Name = "pnlHistoryChart";
            this.pnlHistoryChart.Size = new System.Drawing.Size(786, 113);
            this.pnlHistoryChart.TabIndex = 1;
            // 
            // lblHistoryChartTitle
            // 
            this.lblHistoryChartTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHistoryChartTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblHistoryChartTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(33)))), ((int)(((byte)(46)))));
            this.lblHistoryChartTitle.Location = new System.Drawing.Point(18, 14);
            this.lblHistoryChartTitle.Name = "lblHistoryChartTitle";
            this.lblHistoryChartTitle.Size = new System.Drawing.Size(786, 39);
            this.lblHistoryChartTitle.TabIndex = 0;
            this.lblHistoryChartTitle.Text = "График зависимости расходов от года";
            // 
            // forecastChartCard
            // 
            this.forecastChartCard.BackColor = System.Drawing.Color.White;
            this.forecastChartCard.Controls.Add(this.pnlForecastChart);
            this.forecastChartCard.Controls.Add(this.lblForecastChartTitle);
            this.forecastChartCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.forecastChartCard.Location = new System.Drawing.Point(3, 193);
            this.forecastChartCard.Name = "forecastChartCard";
            this.forecastChartCard.Padding = new System.Windows.Forms.Padding(18, 14, 18, 18);
            this.forecastChartCard.Size = new System.Drawing.Size(822, 184);
            this.forecastChartCard.TabIndex = 1;
            // 
            // pnlForecastChart
            // 
            this.pnlForecastChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(254)))));
            this.pnlForecastChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForecastChart.Location = new System.Drawing.Point(18, 53);
            this.pnlForecastChart.Name = "pnlForecastChart";
            this.pnlForecastChart.Size = new System.Drawing.Size(786, 113);
            this.pnlForecastChart.TabIndex = 2;
            // 
            // lblForecastChartTitle
            // 
            this.lblForecastChartTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblForecastChartTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblForecastChartTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(33)))), ((int)(((byte)(46)))));
            this.lblForecastChartTitle.Location = new System.Drawing.Point(18, 14);
            this.lblForecastChartTitle.Name = "lblForecastChartTitle";
            this.lblForecastChartTitle.Size = new System.Drawing.Size(786, 39);
            this.lblForecastChartTitle.TabIndex = 1;
            this.lblForecastChartTitle.Text = "Прогноз по скользящей средней";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "CSV files (*.csv)|*.csv|Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            this.openFileDialog1.Title = "Открыть файл с данными";
            // 
            // ConsumerExpensesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1520, 880);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.MinimumSize = new System.Drawing.Size(1320, 820);
            this.Name = "ConsumerExpensesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TeamSpace — Потребительские расходы";
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.btnLoadFile.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.mainTableLayout.ResumeLayout(false);
            this.leftPanel.ResumeLayout(false);
            this.gridCardPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenses)).EndInit();
            this.gridHeaderPanel.ResumeLayout(false);
            this.gridHeaderPanel.PerformLayout();
            this.rightPanel.ResumeLayout(false);
            this.rightLayout.ResumeLayout(false);
            this.statsPanel.ResumeLayout(false);
            this.statsLayout.ResumeLayout(false);
            this.cardMax.ResumeLayout(false);
            this.cardMax.PerformLayout();
            this.cardMin.ResumeLayout(false);
            this.cardMin.PerformLayout();
            this.cardAvg.ResumeLayout(false);
            this.cardAvg.PerformLayout();
            this.cardYears.ResumeLayout(false);
            this.cardYears.PerformLayout();
            this.settingsPanel.ResumeLayout(false);
            this.settingsPanel.PerformLayout();
            this.btnBuildForecast.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numForecastYears)).EndInit();
            this.chartsPanel.ResumeLayout(false);
            this.chartLayout.ResumeLayout(false);
            this.historyChartCard.ResumeLayout(false);
            this.forecastChartCard.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel btnLoadFile;
        private System.Windows.Forms.Label lblLoadFile;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Panel gridCardPanel;
        private System.Windows.Forms.DataGridView dgvExpenses;
        private System.Windows.Forms.Panel gridHeaderPanel;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.TableLayoutPanel rightLayout;
        private System.Windows.Forms.Panel statsPanel;
        private System.Windows.Forms.TableLayoutPanel statsLayout;
        private System.Windows.Forms.Panel cardMax;
        private System.Windows.Forms.Label lblMaxValue;
        private System.Windows.Forms.Label lblMaxTitle;
        private System.Windows.Forms.Panel cardMin;
        private System.Windows.Forms.Label lblMinValue;
        private System.Windows.Forms.Label lblMinTitle;
        private System.Windows.Forms.Panel cardAvg;
        private System.Windows.Forms.Label lblAvgValue;
        private System.Windows.Forms.Label lblAvgTitle;
        private System.Windows.Forms.Panel cardYears;
        private System.Windows.Forms.Label lblYearsValue;
        private System.Windows.Forms.Label lblYearsTitle;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.Panel btnBuildForecast;
        private System.Windows.Forms.Label lblBuildForecast;
        private System.Windows.Forms.NumericUpDown numForecastYears;
        private System.Windows.Forms.Label lblForecastYears;
        private System.Windows.Forms.ComboBox cmbMovingAverage;
        private System.Windows.Forms.Label lblMovingAverage;
        private System.Windows.Forms.Label lblSettingsTitle;
        private System.Windows.Forms.Panel chartsPanel;
        private System.Windows.Forms.TableLayoutPanel chartLayout;
        private System.Windows.Forms.Panel historyChartCard;
        private System.Windows.Forms.Panel pnlHistoryChart;
        private System.Windows.Forms.Label lblHistoryChartTitle;
        private System.Windows.Forms.Panel forecastChartCard;
        private System.Windows.Forms.Panel pnlForecastChart;
        private System.Windows.Forms.Label lblForecastChartTitle;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}