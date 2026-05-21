namespace TeamSpace.View
{
    partial class BadRoadsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.lblRegion = new System.Windows.Forms.Label();
            this.cmbRegions = new System.Windows.Forms.ComboBox();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.txtPeriod = new System.Windows.Forms.TextBox();
            this.lblForecastYears = new System.Windows.Forms.Label();
            this.txtForecastYears = new System.Windows.Forms.TextBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.btnShowGraph = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.pnlDataGrid = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.pnlChart = new System.Windows.Forms.Panel();
            this.formsPlot = new ScottPlot.WinForms.FormsPlot();
            this.pnlStatistics = new System.Windows.Forms.Panel();
            this.lblStatistics = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.pnlChart.SuspendLayout();
            this.pnlStatistics.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1465, 107);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitle.Location = new System.Drawing.Point(562, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(273, 54);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "ВАРИАНТ 16";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblSubtitle.Location = new System.Drawing.Point(446, 64);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(540, 28);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Доля плохих дорог по субъектам Российской Федерации";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlControls
            // 
            this.pnlControls.BackColor = System.Drawing.Color.White;
            this.pnlControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlControls.Controls.Add(this.lblRegion);
            this.pnlControls.Controls.Add(this.cmbRegions);
            this.pnlControls.Controls.Add(this.lblPeriod);
            this.pnlControls.Controls.Add(this.txtPeriod);
            this.pnlControls.Controls.Add(this.lblForecastYears);
            this.pnlControls.Controls.Add(this.txtForecastYears);
            this.pnlControls.Location = new System.Drawing.Point(23, 128);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1417, 85);
            this.pnlControls.TabIndex = 1;
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblRegion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblRegion.Location = new System.Drawing.Point(23, 27);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(82, 25);
            this.lblRegion.TabIndex = 0;
            this.lblRegion.Text = "Регион:";
            // 
            // cmbRegions
            // 
            this.cmbRegions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.cmbRegions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRegions.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbRegions.FormattingEnabled = true;
            this.cmbRegions.Location = new System.Drawing.Point(103, 23);
            this.cmbRegions.Name = "cmbRegions";
            this.cmbRegions.Size = new System.Drawing.Size(319, 33);
            this.cmbRegions.TabIndex = 1;
            this.cmbRegions.SelectedIndexChanged += new System.EventHandler(this.cmbRegions_SelectedIndexChanged);
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblPeriod.Location = new System.Drawing.Point(480, 27);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(121, 25);
            this.lblPeriod.TabIndex = 2;
            this.lblPeriod.Text = "Период (n):";
            // 
            // txtPeriod
            // 
            this.txtPeriod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txtPeriod.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPeriod.Location = new System.Drawing.Point(589, 23);
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.Size = new System.Drawing.Size(91, 32);
            this.txtPeriod.TabIndex = 3;
            this.txtPeriod.Text = "3";
            this.txtPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblForecastYears
            // 
            this.lblForecastYears.AutoSize = true;
            this.lblForecastYears.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblForecastYears.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblForecastYears.Location = new System.Drawing.Point(731, 27);
            this.lblForecastYears.Name = "lblForecastYears";
            this.lblForecastYears.Size = new System.Drawing.Size(142, 25);
            this.lblForecastYears.TabIndex = 4;
            this.lblForecastYears.Text = "Лет прогноза:";
            // 
            // txtForecastYears
            // 
            this.txtForecastYears.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txtForecastYears.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtForecastYears.Location = new System.Drawing.Point(863, 23);
            this.txtForecastYears.Name = "txtForecastYears";
            this.txtForecastYears.Size = new System.Drawing.Size(91, 32);
            this.txtForecastYears.TabIndex = 5;
            this.txtForecastYears.Text = "3";
            this.txtForecastYears.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.White;
            this.pnlButtons.Controls.Add(this.btnLoadData);
            this.pnlButtons.Controls.Add(this.btnShowGraph);
            this.pnlButtons.Controls.Add(this.btnExport);
            this.pnlButtons.Location = new System.Drawing.Point(23, 235);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1417, 53);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnLoadData
            // 
            this.btnLoadData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnLoadData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadData.FlatAppearance.BorderSize = 0;
            this.btnLoadData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadData.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLoadData.ForeColor = System.Drawing.Color.White;
            this.btnLoadData.Location = new System.Drawing.Point(6, 5);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(229, 43);
            this.btnLoadData.TabIndex = 0;
            this.btnLoadData.Text = "📂 Загрузить данные";
            this.btnLoadData.UseVisualStyleBackColor = false;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // btnShowGraph
            // 
            this.btnShowGraph.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnShowGraph.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowGraph.FlatAppearance.BorderSize = 0;
            this.btnShowGraph.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowGraph.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnShowGraph.ForeColor = System.Drawing.Color.White;
            this.btnShowGraph.Location = new System.Drawing.Point(252, 5);
            this.btnShowGraph.Name = "btnShowGraph";
            this.btnShowGraph.Size = new System.Drawing.Size(251, 43);
            this.btnShowGraph.TabIndex = 1;
            this.btnShowGraph.Text = "📊 Показать график";
            this.btnShowGraph.UseVisualStyleBackColor = false;
            this.btnShowGraph.Click += new System.EventHandler(this.btnShowGraph_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnExport.Location = new System.Drawing.Point(1176, 5);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(229, 43);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "💾 Экспорт графика";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // pnlDataGrid
            // 
            this.pnlDataGrid.BackColor = System.Drawing.Color.White;
            this.pnlDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDataGrid.Controls.Add(this.dataGridView);
            this.pnlDataGrid.Location = new System.Drawing.Point(23, 309);
            this.pnlDataGrid.Name = "pnlDataGrid";
            this.pnlDataGrid.Size = new System.Drawing.Size(685, 448);
            this.pnlDataGrid.TabIndex = 3;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.ColumnHeadersHeight = 35;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 5;
            this.dataGridView.RowTemplate.Height = 28;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(683, 446);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // pnlChart
            // 
            this.pnlChart.BackColor = System.Drawing.Color.White;
            this.pnlChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlChart.Controls.Add(this.formsPlot);
            this.pnlChart.Location = new System.Drawing.Point(731, 309);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(708, 448);
            this.pnlChart.TabIndex = 4;
            // 
            // formsPlot
            // 
            this.formsPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot.Location = new System.Drawing.Point(0, 0);
            this.formsPlot.Name = "formsPlot";
            this.formsPlot.Size = new System.Drawing.Size(706, 446);
            this.formsPlot.TabIndex = 0;
            // 
            // pnlStatistics
            // 
            this.pnlStatistics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlStatistics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatistics.Controls.Add(this.lblStatistics);
            this.pnlStatistics.Location = new System.Drawing.Point(23, 779);
            this.pnlStatistics.Name = "pnlStatistics";
            this.pnlStatistics.AutoScroll = true;
            this.pnlStatistics.Padding = new System.Windows.Forms.Padding(20);
            this.pnlStatistics.Size = new System.Drawing.Size(1415, 259);
            this.pnlStatistics.TabIndex = 5;
            // 
            // lblStatistics
            // 
            // 
            // lblStatistics
            // 
            this.lblStatistics.AutoSize = true;
            this.lblStatistics.Dock = System.Windows.Forms.DockStyle.None;
            this.lblStatistics.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblStatistics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblStatistics.Location = new System.Drawing.Point(20, 20);
            this.lblStatistics.Name = "lblStatistics";
            this.lblStatistics.Padding = new System.Windows.Forms.Padding(10);
            this.lblStatistics.MaximumSize = new System.Drawing.Size(1350, 0);
            this.lblStatistics.TabIndex = 0;
            this.lblStatistics.Text = "📊 Здесь будет отображаться статистика по регионам...";
            this.lblStatistics.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // BadRoadsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1465, 1500);
            this.Controls.Add(this.pnlStatistics);
            this.AutoScroll = true;
            this.MinimumSize = new System.Drawing.Size(1480, 1200);
            this.Controls.Add(this.pnlChart);
            this.Controls.Add(this.pnlDataGrid);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BadRoadsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TeamSpace — Вариант 16 (Вика)";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlDataGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.pnlChart.ResumeLayout(false);
            this.pnlStatistics.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        // Объявление компонентов
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.ComboBox cmbRegions;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.TextBox txtPeriod;
        private System.Windows.Forms.Label lblForecastYears;
        private System.Windows.Forms.TextBox txtForecastYears;

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Button btnShowGraph;
        private System.Windows.Forms.Button btnExport;

        private System.Windows.Forms.Panel pnlDataGrid;
        private System.Windows.Forms.DataGridView dataGridView;

        private System.Windows.Forms.Panel pnlChart;
        private ScottPlot.WinForms.FormsPlot formsPlot;

        private System.Windows.Forms.Panel pnlStatistics;
        private System.Windows.Forms.Label lblStatistics;
    }
}