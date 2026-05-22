namespace TeamSpace.View
{
    partial class BadRoadsForm
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
            this.btnLoadData = new System.Windows.Forms.Panel();
            this.lblLoadData = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.gridCardPanel = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.gridHeaderPanel = new System.Windows.Forms.Panel();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.rightLayout = new System.Windows.Forms.TableLayoutPanel();
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.controlsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.btnShowGraph = new System.Windows.Forms.Panel();
            this.lblShowGraph = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Panel();
            this.lblExport = new System.Windows.Forms.Label();
            this.txtForecastYears = new System.Windows.Forms.NumericUpDown();
            this.lblForecastYears = new System.Windows.Forms.Label();
            this.txtPeriod = new System.Windows.Forms.NumericUpDown();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.cmbRegions = new System.Windows.Forms.ComboBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.chartCard = new System.Windows.Forms.Panel();
            this.formsPlot = new ScottPlot.WinForms.FormsPlot();
            this.lblChartTitle = new System.Windows.Forms.Label();
            this.statisticsPanel = new System.Windows.Forms.Panel();
            this.statsTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.maxDecreasePanel = new System.Windows.Forms.Panel();
            this.lblMaxPercentVal = new System.Windows.Forms.Label();
            this.lblMaxRegionVal = new System.Windows.Forms.Label();
            this.lblMaxTitle = new System.Windows.Forms.Label();
            this.minDecreasePanel = new System.Windows.Forms.Panel();
            this.lblMinPercentVal = new System.Windows.Forms.Label();
            this.lblMinRegionVal = new System.Windows.Forms.Label();
            this.lblMinTitle = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.btnLoadData.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.mainTableLayout.SuspendLayout();
            this.leftPanel.SuspendLayout();
            this.gridCardPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.gridHeaderPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.rightLayout.SuspendLayout();
            this.controlsPanel.SuspendLayout();
            this.controlsLayout.SuspendLayout();
            this.btnShowGraph.SuspendLayout();
            this.btnExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtForecastYears)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriod)).BeginInit();
            this.chartCard.SuspendLayout();
            this.statisticsPanel.SuspendLayout();
            this.statsTableLayout.SuspendLayout();
            this.maxDecreasePanel.SuspendLayout();
            this.minDecreasePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.headerPanel.Controls.Add(this.btnLoadData);
            this.headerPanel.Controls.Add(this.lblSubtitle);
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Padding = new System.Windows.Forms.Padding(28, 18, 28, 18);
            this.headerPanel.Size = new System.Drawing.Size(1520, 108);
            this.headerPanel.TabIndex = 0;
            // 
            // btnLoadData
            // 
            this.btnLoadData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(120)))), ((int)(((byte)(150)))));
            this.btnLoadData.Controls.Add(this.lblLoadData);
            this.btnLoadData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadData.Location = new System.Drawing.Point(1332, 34);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(160, 42);
            this.btnLoadData.TabIndex = 2;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            this.btnLoadData.Paint += new System.Windows.Forms.PaintEventHandler(this.RoundedButton_Paint);
            // 
            // lblLoadData
            // 
            this.lblLoadData.BackColor = System.Drawing.Color.Transparent;
            this.lblLoadData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLoadData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoadData.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLoadData.ForeColor = System.Drawing.Color.White;
            this.lblLoadData.Location = new System.Drawing.Point(0, 0);
            this.lblLoadData.Name = "lblLoadData";
            this.lblLoadData.Size = new System.Drawing.Size(160, 42);
            this.lblLoadData.TabIndex = 0;
            this.lblLoadData.Text = "📂 Загрузить";
            this.lblLoadData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(100)))), ((int)(((byte)(115)))));
            this.lblSubtitle.Location = new System.Drawing.Point(32, 70);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(590, 25);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Доля плохих дорог за 15 лет по субъектам Российской Федерации";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Georgia", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.lblTitle.Location = new System.Drawing.Point(28, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(476, 46);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Анализ плохих дорог";
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.contentPanel.Controls.Add(this.mainTableLayout);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 108);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(24);
            this.contentPanel.Size = new System.Drawing.Size(1520, 772);
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
            this.mainTableLayout.Size = new System.Drawing.Size(1472, 724);
            this.mainTableLayout.TabIndex = 0;
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.gridCardPanel);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPanel.Location = new System.Drawing.Point(3, 3);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Padding = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.leftPanel.Size = new System.Drawing.Size(612, 718);
            this.leftPanel.TabIndex = 0;
            // 
            // gridCardPanel
            // 
            this.gridCardPanel.BackColor = System.Drawing.Color.White;
            this.gridCardPanel.Controls.Add(this.dataGridView);
            this.gridCardPanel.Controls.Add(this.gridHeaderPanel);
            this.gridCardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCardPanel.Location = new System.Drawing.Point(0, 0);
            this.gridCardPanel.Name = "gridCardPanel";
            this.gridCardPanel.Padding = new System.Windows.Forms.Padding(18);
            this.gridCardPanel.Size = new System.Drawing.Size(598, 718);
            this.gridCardPanel.TabIndex = 0;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersHeight = 40;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(215)))), ((int)(((byte)(225)))));
            this.dataGridView.Location = new System.Drawing.Point(18, 78);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 34;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(562, 622);
            this.dataGridView.TabIndex = 1;
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
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.lblGridTitle.Location = new System.Drawing.Point(4, 12);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(343, 35);
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
            this.rightPanel.Size = new System.Drawing.Size(848, 718);
            this.rightPanel.TabIndex = 1;
            // 
            // rightLayout
            // 
            this.rightLayout.ColumnCount = 1;
            this.rightLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rightLayout.Controls.Add(this.controlsPanel, 0, 0);
            this.rightLayout.Controls.Add(this.chartCard, 0, 1);
            this.rightLayout.Controls.Add(this.statisticsPanel, 0, 2);
            this.rightLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightLayout.Location = new System.Drawing.Point(14, 0);
            this.rightLayout.Name = "rightLayout";
            this.rightLayout.RowCount = 3;
            this.rightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 131F));
            this.rightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.rightLayout.Size = new System.Drawing.Size(834, 718);
            this.rightLayout.TabIndex = 0;
            // 
            // controlsPanel
            // 
            this.controlsPanel.BackColor = System.Drawing.Color.White;
            this.controlsPanel.Controls.Add(this.controlsLayout);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlsPanel.Location = new System.Drawing.Point(3, 3);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Padding = new System.Windows.Forms.Padding(16);
            this.controlsPanel.Size = new System.Drawing.Size(828, 125);
            this.controlsPanel.TabIndex = 0;
            // 
            // controlsLayout
            // 
            this.controlsLayout.ColumnCount = 4;
            this.controlsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.controlsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.controlsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.controlsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.controlsLayout.Controls.Add(this.btnShowGraph, 3, 1);
            this.controlsLayout.Controls.Add(this.btnExport, 3, 0);
            this.controlsLayout.Controls.Add(this.txtForecastYears, 2, 1);
            this.controlsLayout.Controls.Add(this.lblForecastYears, 2, 0);
            this.controlsLayout.Controls.Add(this.txtPeriod, 1, 1);
            this.controlsLayout.Controls.Add(this.lblPeriod, 1, 0);
            this.controlsLayout.Controls.Add(this.cmbRegions, 0, 1);
            this.controlsLayout.Controls.Add(this.lblRegion, 0, 0);
            this.controlsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlsLayout.Location = new System.Drawing.Point(16, 16);
            this.controlsLayout.Name = "controlsLayout";
            this.controlsLayout.RowCount = 3;
            this.controlsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.controlsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.controlsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.controlsLayout.Size = new System.Drawing.Size(796, 93);
            this.controlsLayout.TabIndex = 0;
            // 
            // btnShowGraph
            // 
            this.btnShowGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowGraph.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(140)))), ((int)(((byte)(170)))));
            this.btnShowGraph.Controls.Add(this.lblShowGraph);
            this.btnShowGraph.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowGraph.Location = new System.Drawing.Point(653, 45);
            this.btnShowGraph.Name = "btnShowGraph";
            this.btnShowGraph.Size = new System.Drawing.Size(140, 36);
            this.btnShowGraph.TabIndex = 8;
            this.btnShowGraph.Click += new System.EventHandler(this.btnShowGraph_Click);
            this.btnShowGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.RoundedButton_Paint);
            // 
            // lblShowGraph
            // 
            this.lblShowGraph.BackColor = System.Drawing.Color.Transparent;
            this.lblShowGraph.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblShowGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblShowGraph.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblShowGraph.ForeColor = System.Drawing.Color.White;
            this.lblShowGraph.Location = new System.Drawing.Point(0, 0);
            this.lblShowGraph.Name = "lblShowGraph";
            this.lblShowGraph.Size = new System.Drawing.Size(140, 36);
            this.lblShowGraph.TabIndex = 0;
            this.lblShowGraph.Text = "📊 График";
            this.lblShowGraph.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblShowGraph.Click += new System.EventHandler(this.btnShowGraph_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(180)))), ((int)(((byte)(160)))));
            this.btnExport.Controls.Add(this.lblExport);
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.Location = new System.Drawing.Point(653, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(140, 36);
            this.btnExport.TabIndex = 7;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            this.btnExport.Paint += new System.Windows.Forms.PaintEventHandler(this.RoundedButton_Paint);
            // 
            // lblExport
            // 
            this.lblExport.BackColor = System.Drawing.Color.Transparent;
            this.lblExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(50)))), ((int)(((byte)(60)))));
            this.lblExport.Location = new System.Drawing.Point(0, 0);
            this.lblExport.Name = "lblExport";
            this.lblExport.Size = new System.Drawing.Size(140, 36);
            this.lblExport.TabIndex = 0;
            this.lblExport.Text = "💾 Экспорт";
            this.lblExport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtForecastYears
            // 
            this.txtForecastYears.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtForecastYears.Location = new System.Drawing.Point(480, 45);
            this.txtForecastYears.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtForecastYears.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtForecastYears.Name = "txtForecastYears";
            this.txtForecastYears.Size = new System.Drawing.Size(150, 30);
            this.txtForecastYears.TabIndex = 6;
            this.txtForecastYears.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lblForecastYears
            // 
            this.lblForecastYears.AutoSize = true;
            this.lblForecastYears.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblForecastYears.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(90)))), ((int)(((byte)(105)))));
            this.lblForecastYears.Location = new System.Drawing.Point(480, 0);
            this.lblForecastYears.Name = "lblForecastYears";
            this.lblForecastYears.Size = new System.Drawing.Size(116, 23);
            this.lblForecastYears.TabIndex = 5;
            this.lblForecastYears.Text = "Лет прогноза";
            // 
            // txtPeriod
            // 
            this.txtPeriod.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPeriod.Location = new System.Drawing.Point(321, 45);
            this.txtPeriod.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.Size = new System.Drawing.Size(150, 30);
            this.txtPeriod.TabIndex = 4;
            this.txtPeriod.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(90)))), ((int)(((byte)(105)))));
            this.lblPeriod.Location = new System.Drawing.Point(321, 0);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(95, 23);
            this.lblPeriod.TabIndex = 3;
            this.lblPeriod.Text = "Период (n)";
            // 
            // cmbRegions
            // 
            this.cmbRegions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegions.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRegions.FormattingEnabled = true;
            this.cmbRegions.Location = new System.Drawing.Point(3, 45);
            this.cmbRegions.Name = "cmbRegions";
            this.cmbRegions.Size = new System.Drawing.Size(310, 31);
            this.cmbRegions.TabIndex = 2;
            this.cmbRegions.SelectedIndexChanged += new System.EventHandler(this.cmbRegions_SelectedIndexChanged);
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRegion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(90)))), ((int)(((byte)(105)))));
            this.lblRegion.Location = new System.Drawing.Point(3, 0);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(66, 23);
            this.lblRegion.TabIndex = 1;
            this.lblRegion.Text = "Регион";
            // 
            // chartCard
            // 
            this.chartCard.BackColor = System.Drawing.Color.White;
            this.chartCard.Controls.Add(this.formsPlot);
            this.chartCard.Controls.Add(this.lblChartTitle);
            this.chartCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartCard.Location = new System.Drawing.Point(3, 134);
            this.chartCard.Name = "chartCard";
            this.chartCard.Padding = new System.Windows.Forms.Padding(18, 14, 18, 14);
            this.chartCard.Size = new System.Drawing.Size(828, 481);
            this.chartCard.TabIndex = 1;
            // 
            // formsPlot
            // 
            this.formsPlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.formsPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot.Location = new System.Drawing.Point(18, 24);
            this.formsPlot.Name = "formsPlot";
            this.formsPlot.Size = new System.Drawing.Size(792, 443);
            this.formsPlot.TabIndex = 1;
            // 
            // lblChartTitle
            // 
            this.lblChartTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChartTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblChartTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.lblChartTitle.Location = new System.Drawing.Point(18, 14);
            this.lblChartTitle.Name = "lblChartTitle";
            this.lblChartTitle.Size = new System.Drawing.Size(792, 10);
            this.lblChartTitle.TabIndex = 0;
            this.lblChartTitle.Visible = false;
            // 
            // statisticsPanel
            // 
            this.statisticsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.statisticsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statisticsPanel.Controls.Add(this.statsTableLayout);
            this.statisticsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statisticsPanel.Location = new System.Drawing.Point(3, 621);
            this.statisticsPanel.Name = "statisticsPanel";
            this.statisticsPanel.Size = new System.Drawing.Size(828, 94);
            this.statisticsPanel.TabIndex = 2;
            // 
            // statsTableLayout
            // 
            this.statsTableLayout.ColumnCount = 2;
            this.statsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.statsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.statsTableLayout.Controls.Add(this.maxDecreasePanel, 0, 0);
            this.statsTableLayout.Controls.Add(this.minDecreasePanel, 1, 0);
            this.statsTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statsTableLayout.Location = new System.Drawing.Point(0, 0);
            this.statsTableLayout.Name = "statsTableLayout";
            this.statsTableLayout.RowCount = 1;
            this.statsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.statsTableLayout.Size = new System.Drawing.Size(826, 92);
            this.statsTableLayout.TabIndex = 0;
            // 
            // maxDecreasePanel
            // 
            this.maxDecreasePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.maxDecreasePanel.Controls.Add(this.lblMaxPercentVal);
            this.maxDecreasePanel.Controls.Add(this.lblMaxRegionVal);
            this.maxDecreasePanel.Controls.Add(this.lblMaxTitle);
            this.maxDecreasePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maxDecreasePanel.Location = new System.Drawing.Point(8, 8);
            this.maxDecreasePanel.Margin = new System.Windows.Forms.Padding(8);
            this.maxDecreasePanel.Name = "maxDecreasePanel";
            this.maxDecreasePanel.Size = new System.Drawing.Size(397, 76);
            this.maxDecreasePanel.TabIndex = 0;
            // 
            // lblMaxPercentVal
            // 
            this.lblMaxPercentVal.AutoSize = true;
            this.lblMaxPercentVal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMaxPercentVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(100)))), ((int)(((byte)(115)))));
            this.lblMaxPercentVal.Location = new System.Drawing.Point(12, 49);
            this.lblMaxPercentVal.Name = "lblMaxPercentVal";
            this.lblMaxPercentVal.Size = new System.Drawing.Size(87, 21);
            this.lblMaxPercentVal.TabIndex = 2;
            this.lblMaxPercentVal.Text = "Снижение:";
            // 
            // lblMaxRegionVal
            // 
            this.lblMaxRegionVal.AutoSize = true;
            this.lblMaxRegionVal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMaxRegionVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(100)))), ((int)(((byte)(115)))));
            this.lblMaxRegionVal.Location = new System.Drawing.Point(12, 28);
            this.lblMaxRegionVal.Name = "lblMaxRegionVal";
            this.lblMaxRegionVal.Size = new System.Drawing.Size(63, 21);
            this.lblMaxRegionVal.TabIndex = 1;
            this.lblMaxRegionVal.Text = "Регион:";
            // 
            // lblMaxTitle
            // 
            this.lblMaxTitle.AutoSize = true;
            this.lblMaxTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMaxTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.lblMaxTitle.Location = new System.Drawing.Point(10, 5);
            this.lblMaxTitle.Name = "lblMaxTitle";
            this.lblMaxTitle.Size = new System.Drawing.Size(256, 23);
            this.lblMaxTitle.TabIndex = 0;
            this.lblMaxTitle.Text = "✅ Максимальное снижение:";
            // 
            // minDecreasePanel
            // 
            this.minDecreasePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.minDecreasePanel.Controls.Add(this.lblMinPercentVal);
            this.minDecreasePanel.Controls.Add(this.lblMinRegionVal);
            this.minDecreasePanel.Controls.Add(this.lblMinTitle);
            this.minDecreasePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minDecreasePanel.Location = new System.Drawing.Point(421, 8);
            this.minDecreasePanel.Margin = new System.Windows.Forms.Padding(8);
            this.minDecreasePanel.Name = "minDecreasePanel";
            this.minDecreasePanel.Size = new System.Drawing.Size(397, 76);
            this.minDecreasePanel.TabIndex = 1;
            // 
            // lblMinPercentVal
            // 
            this.lblMinPercentVal.AutoSize = true;
            this.lblMinPercentVal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMinPercentVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(100)))), ((int)(((byte)(115)))));
            this.lblMinPercentVal.Location = new System.Drawing.Point(12, 49);
            this.lblMinPercentVal.Name = "lblMinPercentVal";
            this.lblMinPercentVal.Size = new System.Drawing.Size(87, 21);
            this.lblMinPercentVal.TabIndex = 2;
            this.lblMinPercentVal.Text = "Снижение:";
            // 
            // lblMinRegionVal
            // 
            this.lblMinRegionVal.AutoSize = true;
            this.lblMinRegionVal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMinRegionVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(100)))), ((int)(((byte)(115)))));
            this.lblMinRegionVal.Location = new System.Drawing.Point(12, 28);
            this.lblMinRegionVal.Name = "lblMinRegionVal";
            this.lblMinRegionVal.Size = new System.Drawing.Size(63, 21);
            this.lblMinRegionVal.TabIndex = 1;
            this.lblMinRegionVal.Text = "Регион:";
            // 
            // lblMinTitle
            // 
            this.lblMinTitle.AutoSize = true;
            this.lblMinTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMinTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.lblMinTitle.Location = new System.Drawing.Point(10, 5);
            this.lblMinTitle.Name = "lblMinTitle";
            this.lblMinTitle.Size = new System.Drawing.Size(250, 23);
            this.lblMinTitle.TabIndex = 0;
            this.lblMinTitle.Text = "📉 Минимальное снижение:";
            // 
            // BadRoadsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1520, 880);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.MinimumSize = new System.Drawing.Size(1320, 820);
            this.Name = "BadRoadsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TeamSpace — Вариант 16 (Вика)";
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.btnLoadData.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.mainTableLayout.ResumeLayout(false);
            this.leftPanel.ResumeLayout(false);
            this.gridCardPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.gridHeaderPanel.ResumeLayout(false);
            this.gridHeaderPanel.PerformLayout();
            this.rightPanel.ResumeLayout(false);
            this.rightLayout.ResumeLayout(false);
            this.controlsPanel.ResumeLayout(false);
            this.controlsLayout.ResumeLayout(false);
            this.controlsLayout.PerformLayout();
            this.btnShowGraph.ResumeLayout(false);
            this.btnExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtForecastYears)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriod)).EndInit();
            this.chartCard.ResumeLayout(false);
            this.statisticsPanel.ResumeLayout(false);
            this.statsTableLayout.ResumeLayout(false);
            this.maxDecreasePanel.ResumeLayout(false);
            this.maxDecreasePanel.PerformLayout();
            this.minDecreasePanel.ResumeLayout(false);
            this.minDecreasePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel btnLoadData;
        private System.Windows.Forms.Label lblLoadData;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Panel gridCardPanel;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Panel gridHeaderPanel;
        private System.Windows.Forms.Label lblGridTitle;

        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.TableLayoutPanel rightLayout;
        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.TableLayoutPanel controlsLayout;
        private System.Windows.Forms.Panel btnShowGraph;
        private System.Windows.Forms.Label lblShowGraph;
        private System.Windows.Forms.Panel btnExport;
        private System.Windows.Forms.Label lblExport;
        private System.Windows.Forms.NumericUpDown txtForecastYears;
        private System.Windows.Forms.Label lblForecastYears;
        private System.Windows.Forms.NumericUpDown txtPeriod;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.ComboBox cmbRegions;
        private System.Windows.Forms.Label lblRegion;

        private System.Windows.Forms.Panel chartCard;
        private ScottPlot.WinForms.FormsPlot formsPlot;
        private System.Windows.Forms.Label lblChartTitle;

        private System.Windows.Forms.Panel statisticsPanel;
        private System.Windows.Forms.TableLayoutPanel statsTableLayout;
        private System.Windows.Forms.Panel maxDecreasePanel;
        private System.Windows.Forms.Label lblMaxTitle;
        private System.Windows.Forms.Label lblMaxRegionVal;
        private System.Windows.Forms.Label lblMaxPercentVal;
        private System.Windows.Forms.Panel minDecreasePanel;
        private System.Windows.Forms.Label lblMinTitle;
        private System.Windows.Forms.Label lblMinRegionVal;
        private System.Windows.Forms.Label lblMinPercentVal;
    }
}