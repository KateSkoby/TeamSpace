using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TeamSpace.View
{
    public partial class ConsumerExpensesForm : Form
    {
        private SmoothMenuButton loadFileMenuButton;
        private SmoothMenuButton buildForecastMenuButton;

        private Label lblIndicator;
        private ComboBox cmbIndicator;
        private Label lblPeriodFrom;
        private DateTimePicker dtpPeriodFrom;
        private Label lblPeriodTo;
        private DateTimePicker dtpPeriodTo;

        public ConsumerExpensesForm()
        {
            InitializeComponent();

            DoubleBuffered = true;

            ConfigureStatsPanel();
            ConfigureSettingsPanel();
            ReplaceDesignerButtonsWithSmoothButtons();

            Resize += delegate { AlignCustomControls(); };
            Load += delegate { AlignCustomControls(); };
            Shown += delegate { AlignCustomControls(); };

            AlignCustomControls();
        }

        private void ConfigureStatsPanel()
        {
            cardAvg.Visible = false;
            statsLayout.Controls.Remove(cardAvg);

            statsLayout.SuspendLayout();

            statsLayout.ColumnStyles.Clear();
            statsLayout.ColumnCount = 3;

            statsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            statsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            statsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));

            statsLayout.Controls.Remove(cardMax);
            statsLayout.Controls.Remove(cardMin);
            statsLayout.Controls.Remove(cardYears);

            statsLayout.Controls.Add(cardMax, 0, 0);
            statsLayout.Controls.Add(cardMin, 1, 0);
            statsLayout.Controls.Add(cardYears, 2, 0);

            statsLayout.ResumeLayout(true);
        }

        private void ConfigureSettingsPanel()
        {
            lblMovingAverage.Visible = false;
            cmbMovingAverage.Visible = false;

            lblForecastYears.Text = "Прогноз N лет";
            lblForecastYears.AutoSize = true;

            lblIndicator = new Label
            {
                Text = "Показатель:",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(110, 118, 138),
                BackColor = Color.Transparent
            };

            cmbIndicator = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                Size = new Size(175, 31)
            };

            cmbIndicator.Items.AddRange(new object[]
            {
                "Общие расходы",
                "Продовольственные товары",
                "Непродовольственные товары",
                "Услуги"
            });

            cmbIndicator.SelectedIndex = 0;

            lblPeriodFrom = new Label
            {
                Text = "Период с:",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(110, 118, 138),
                BackColor = Color.Transparent
            };

            dtpPeriodFrom = new DateTimePicker
            {
                Font = new Font("Segoe UI", 10F),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy",
                ShowUpDown = false,
                Size = new Size(105, 30),
                Value = DateTime.Today.AddYears(-14)
            };

            lblPeriodTo = new Label
            {
                Text = "по:",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(110, 118, 138),
                BackColor = Color.Transparent
            };

            dtpPeriodTo = new DateTimePicker
            {
                Font = new Font("Segoe UI", 10F),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy",
                ShowUpDown = false,
                Size = new Size(105, 30),
                Value = DateTime.Today
            };

            settingsPanel.Controls.Add(lblIndicator);
            settingsPanel.Controls.Add(cmbIndicator);
            settingsPanel.Controls.Add(lblPeriodFrom);
            settingsPanel.Controls.Add(dtpPeriodFrom);
            settingsPanel.Controls.Add(lblPeriodTo);
            settingsPanel.Controls.Add(dtpPeriodTo);

            lblIndicator.BringToFront();
            cmbIndicator.BringToFront();
            lblPeriodFrom.BringToFront();
            dtpPeriodFrom.BringToFront();
            lblPeriodTo.BringToFront();
            dtpPeriodTo.BringToFront();
            lblForecastYears.BringToFront();
            numForecastYears.BringToFront();
        }

        private void ReplaceDesignerButtonsWithSmoothButtons()
        {
            btnLoadFile.Visible = false;
            btnLoadFile.Enabled = false;

            btnBuildForecast.Visible = false;
            btnBuildForecast.Enabled = false;

            loadFileMenuButton = CreateMenuButton("Открыть файл", 170, btnLoadFile_Click);
            buildForecastMenuButton = CreateMenuButton("Построить график", 190, btnBuildForecast_Click);

            headerPanel.Controls.Add(loadFileMenuButton);
            settingsPanel.Controls.Add(buildForecastMenuButton);

            loadFileMenuButton.BringToFront();
            buildForecastMenuButton.BringToFront();

            headerPanel.Resize += delegate { AlignCustomControls(); };
            settingsPanel.Resize += delegate { AlignCustomControls(); };
        }

        private SmoothMenuButton CreateMenuButton(string text, int width, EventHandler clickHandler)
        {
            SmoothMenuButton button = new SmoothMenuButton
            {
                Text = text,
                Size = new Size(width, 42),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };

            button.Click += clickHandler;

            return button;
        }

        private void AlignCustomControls()
        {
            AlignHeaderButton();
            AlignSettingsControls();
        }

        private void AlignHeaderButton()
        {
            if (loadFileMenuButton == null)
                return;

            int x = headerPanel.ClientSize.Width
                    - headerPanel.Padding.Right
                    - loadFileMenuButton.Width;

            loadFileMenuButton.Location = new Point(
                Math.Max(headerPanel.Padding.Left, x),
                34
            );
        }

        private void AlignSettingsControls()
        {
            if (settingsPanel == null)
                return;

            int left = 24;
            int labelY = 39;
            int controlY = 67;

            if (lblIndicator != null)
                lblIndicator.Location = new Point(left, labelY);

            if (cmbIndicator != null)
                cmbIndicator.Location = new Point(left, controlY);

            int periodFromX = left + 205;

            if (lblPeriodFrom != null)
                lblPeriodFrom.Location = new Point(periodFromX, labelY);

            if (dtpPeriodFrom != null)
                dtpPeriodFrom.Location = new Point(periodFromX, controlY);

            int periodToX = periodFromX + 130;

            if (lblPeriodTo != null)
                lblPeriodTo.Location = new Point(periodToX, labelY);

            if (dtpPeriodTo != null)
                dtpPeriodTo.Location = new Point(periodToX, controlY);

            int forecastX = periodToX + 130;

            lblForecastYears.Location = new Point(forecastX, labelY);
            numForecastYears.Location = new Point(forecastX, controlY);
            numForecastYears.Size = new Size(105, 30);

            if (buildForecastMenuButton != null)
            {
                int x = settingsPanel.ClientSize.Width
                        - settingsPanel.Padding.Right
                        - buildForecastMenuButton.Width;

                buildForecastMenuButton.Location = new Point(
                    Math.Max(forecastX + 125, x),
                    61
                );
            }
        }

        private void MenuBlueButton_Paint(object sender, PaintEventArgs e)
        {
            // Старые Panel-кнопки скрыты.
            // Новые кнопки рисуются через SmoothMenuButton.
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show(
                        "Файл выбран:\n" + openFileDialog1.FileName,
                        "Открытие файла",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при открытии файла:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnBuildForecast_Click(object sender, EventArgs e)
        {
            try
            {
                string indicator = cmbIndicator != null && cmbIndicator.SelectedItem != null
                    ? cmbIndicator.SelectedItem.ToString()
                    : "Показатель не выбран";

                int fromYear = dtpPeriodFrom != null ? dtpPeriodFrom.Value.Year : 0;
                int toYear = dtpPeriodTo != null ? dtpPeriodTo.Value.Year : 0;
                int years = (int)numForecastYears.Value;

                if (fromYear > toYear)
                {
                    MessageBox.Show(
                        "Год начала периода не может быть больше года окончания.",
                        "Ошибка периода",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                MessageBox.Show(
                    $"Построение графика.\n\n" +
                    $"Показатель: {indicator}\n" +
                    $"Период: {fromYear} — {toYear}\n" +
                    $"Прогноз: {years} лет",
                    "График",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при построении графика:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    public class SmoothMenuButton : Control
    {
        private readonly Color normalColor = Color.FromArgb(0x1A, 0x56, 0xE8);
        private readonly Color hoverColor = Color.FromArgb(21, 72, 196);
        private readonly Color pressedColor = Color.FromArgb(16, 61, 170);

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
                true);

            BackColor = Color.Transparent;
            ForeColor = Color.White;
            Size = new Size(132, 42);
            Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Cursor = Cursors.Hand;
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

            RectangleF rect = new RectangleF(0.5f, 0.5f, Width - 1f, Height - 1f);

            using (GraphicsPath path = CreateRoundedRectangle(rect, Height / 2f))
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
                TextFormatFlags.NoPadding);
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

        private GraphicsPath CreateRoundedRectangle(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();

            float diameter = radius * 2f;

            if (diameter > rect.Width)
                diameter = rect.Width;

            if (diameter > rect.Height)
                diameter = rect.Height;

            RectangleF arc = new RectangleF(rect.X, rect.Y, diameter, diameter);

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