using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using TeamSpace.View;
using AppResources = TeamSpace.Properties.Resources;

namespace TeamSpace
{
    public class MainMenuForm : Form
    {
        private readonly Color PageBackground = Color.White;
        private readonly Color PrimaryBlue = Color.FromArgb(0x1A, 0x56, 0xE8);
        private readonly Color LightSurface = Color.FromArgb(0xF7, 0xFB, 0xFE);
        private readonly Color DarkText = Color.FromArgb(0x1E, 0x21, 0x2E);
        private readonly Color MutedText = Color.FromArgb(110, 118, 138);
        private readonly Color BorderColor = Color.FromArgb(228, 236, 248);

        public MainMenuForm()
        {
            Text = "TeamSpace — Главное меню";
            StartPosition = FormStartPosition.CenterScreen;
            Width = 1320;
            Height = 860;
            MinimumSize = new Size(1320, 860);
            BackColor = PageBackground;
            DoubleBuffered = true;

            BuildInterface();
        }

        private void BuildInterface()
        {
            Panel root = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = PageBackground
            };
            Controls.Add(root);

            Panel heroPanel = new Panel
            {
                Width = 1100,
                Height = 190,
                BackColor = Color.Transparent
            };

            Label smallLabel = new Label
            {
                Text = "TEAMSPACE",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = PrimaryBlue,
                AutoSize = true,
                BackColor = Color.Transparent
            };

            Label titleLabel = new Label
            {
                Text = "Главное меню",
                Font = new Font("Georgia", 32, FontStyle.Bold),
                ForeColor = DarkText,
                AutoSize = true,
                BackColor = Color.Transparent
            };

            Label subtitleLabel = new Label
            {
                Text = "Выберите вариант участника разработки",
                Font = new Font("Segoe UI", 15, FontStyle.Regular),
                ForeColor = MutedText,
                AutoSize = true,
                BackColor = Color.Transparent
            };

            heroPanel.Controls.Add(smallLabel);
            heroPanel.Controls.Add(titleLabel);
            heroPanel.Controls.Add(subtitleLabel);

            smallLabel.Location = new Point((heroPanel.Width - smallLabel.Width) / 2, 30);
            titleLabel.Location = new Point((heroPanel.Width - titleLabel.Width) / 2, 58);
            subtitleLabel.Location = new Point((heroPanel.Width - subtitleLabel.Width) / 2, 125);

            TableLayoutPanel cardsPanel = new TableLayoutPanel
            {
                ColumnCount = 3,
                RowCount = 1,
                Width = 1260,
                Height = 430,
                BackColor = Color.Transparent
            };

            cardsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            cardsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            cardsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));

            ModernCard vikaCard = CreateCard(
                "Вика — вариант 16",
                "Модуль анализа доли плохих дорог\nпо субъектам РФ",
                "Открыть",
                AppResources.vika
            );

            ModernCard leshaCard = CreateCard(
                "Леша — вариант 17",
                "Модуль анализа процента детей,\nрожденных вне брака",
                "Открыть",
                AppResources.lesha
            );

            ModernCard katyaCard = CreateCard(
                "Катя — вариант 18",
                "Модуль анализа\nпотребительских расходов",
                "Открыть",
                AppResources.katya
            );

            vikaCard.Click += delegate { OpenBadRoadsForm(); };
            leshaCard.Click += delegate { LaunchBirthsOutOfWedlockApp(); };
            katyaCard.Click += delegate { OpenConsumerExpensesForm(); };

            cardsPanel.Controls.Add(WrapCentered(vikaCard), 0, 0);
            cardsPanel.Controls.Add(WrapCentered(leshaCard), 1, 0);
            cardsPanel.Controls.Add(WrapCentered(katyaCard), 2, 0);

            root.Controls.Add(heroPanel);
            root.Controls.Add(cardsPanel);

            root.Resize += delegate
            {
                heroPanel.Location = new Point((root.ClientSize.Width - heroPanel.Width) / 2, 35);
                cardsPanel.Location = new Point((root.ClientSize.Width - cardsPanel.Width) / 2, 210);
            };

            heroPanel.Location = new Point((root.ClientSize.Width - heroPanel.Width) / 2, 35);
            cardsPanel.Location = new Point((root.ClientSize.Width - cardsPanel.Width) / 2, 210);
        }

        private void OpenBadRoadsForm()
        {
            try
            {
                BadRoadsForm form = new BadRoadsForm();
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при открытии формы BadRoadsForm:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void OpenConsumerExpensesForm()
        {
            try
            {
                ConsumerExpensesForm form = new ConsumerExpensesForm();
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при открытии формы ConsumerExpensesForm:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LaunchBirthsOutOfWedlockApp()
        {
            try
            {
                string baseDir = Application.StartupPath;
                string projectDir = Path.Combine(baseDir, "BirthsOutOfWedlockApp");

                string exePath = Path.Combine(
                    projectDir,
                    "bin",
                    "Release",
                    "net8.0-windows",
                    "BirthsOutOfWedlockApp.exe"
                );

                if (!File.Exists(exePath))
                {
                    exePath = Path.Combine(
                        projectDir,
                        "bin",
                        "Debug",
                        "net8.0-windows",
                        "BirthsOutOfWedlockApp.exe"
                    );
                }

                if (!File.Exists(exePath))
                {
                    MessageBox.Show(
                        "Не найден файл BirthsOutOfWedlockApp.exe.\n" +
                        "Проверь, что папка BirthsOutOfWedlockApp лежит рядом с приложением и проект собран.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                Process.Start(new ProcessStartInfo
                {
                    FileName = exePath,
                    WorkingDirectory = Path.GetDirectoryName(exePath),
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при запуске приложения:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private ModernCard CreateCard(string title, string subtitle, string buttonText, Image avatarImage)
        {
            return new ModernCard(
                title,
                subtitle,
                buttonText,
                avatarImage,
                PrimaryBlue,
                LightSurface,
                DarkText,
                MutedText,
                BorderColor
            );
        }

        private Control WrapCentered(Control control)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            panel.Controls.Add(control);

            panel.Resize += delegate
            {
                control.Location = new Point((panel.Width - control.Width) / 2, 10);
            };

            control.Location = new Point((panel.Width - control.Width) / 2, 10);
            return panel;
        }
    }

    public class ModernCard : Panel
    {
        private readonly string _title;
        private readonly string _subtitle;
        private readonly string _buttonText;
        private readonly Image _avatarImage;

        private readonly Color _primaryBlue;
        private readonly Color _lightSurface;
        private readonly Color _darkText;
        private readonly Color _mutedText;
        private readonly Color _borderColor;

        private bool _hovered;
        private bool _buttonHovered;

        public ModernCard(
            string title,
            string subtitle,
            string buttonText,
            Image avatarImage,
            Color primaryBlue,
            Color lightSurface,
            Color darkText,
            Color mutedText,
            Color borderColor)
        {
            _title = title;
            _subtitle = subtitle;
            _buttonText = buttonText;
            _avatarImage = avatarImage;
            _primaryBlue = primaryBlue;
            _lightSurface = lightSurface;
            _darkText = darkText;
            _mutedText = mutedText;
            _borderColor = borderColor;

            Width = 380;
            Height = 380;
            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
            DoubleBuffered = true;
            Margin = new Padding(20);

            CircularPictureBox avatarPicture = new CircularPictureBox
            {
                Width = 112,
                Height = 112,
                BackColor = Color.FromArgb(238, 244, 255),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point((Width - 112) / 2, 28),
                Image = _avatarImage,
                Cursor = Cursors.Hand
            };

            Label titleLabel = new Label
            {
                Text = _title,
                Font = new Font("Segoe UI", 19, FontStyle.Bold),
                ForeColor = _darkText,
                AutoSize = false,
                Width = 340,
                Height = 56,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 148),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            Label subtitleLabel = new Label
            {
                Text = _subtitle,
                Font = new Font("Segoe UI", 11.5F, FontStyle.Regular),
                ForeColor = _mutedText,
                AutoSize = false,
                Width = 338,
                Height = 72,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(21, 204),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            Panel buttonPanel = new Panel
            {
                Width = 154,
                Height = 46,
                BackColor = Color.Transparent,
                Location = new Point((Width - 154) / 2, 305),
                Cursor = Cursors.Hand
            };

            buttonPanel.Paint += ButtonPanel_Paint;

            Label buttonLabel = new Label
            {
                Text = _buttonText,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Width = 154,
                Height = 46,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            buttonPanel.Controls.Add(buttonLabel);

            // Затемнение кнопки при наведении.
            buttonPanel.MouseEnter += delegate
            {
                _buttonHovered = true;
                buttonPanel.Invalidate();
            };

            buttonPanel.MouseLeave += delegate
            {
                UpdateButtonHoverState(buttonPanel);
            };

            buttonLabel.MouseEnter += delegate
            {
                _buttonHovered = true;
                buttonPanel.Invalidate();
            };

            buttonLabel.MouseLeave += delegate
            {
                UpdateButtonHoverState(buttonPanel);
            };

            Controls.Add(avatarPicture);
            Controls.Add(titleLabel);
            Controls.Add(subtitleLabel);
            Controls.Add(buttonPanel);

            Click += delegate { };
            AttachClickHandlers(this);

            MouseEnter += delegate { _hovered = true; Invalidate(); };
            MouseLeave += delegate { _hovered = false; Invalidate(); };
        }

        private void AttachClickHandlers(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                child.Click += delegate (object sender, EventArgs e) { OnClick(e); };
                AttachClickHandlers(child);
            }
        }

        private void UpdateButtonHoverState(Panel buttonPanel)
        {
            Point cursorPoint = buttonPanel.PointToClient(Cursor.Position);

            if (!buttonPanel.ClientRectangle.Contains(cursorPoint))
            {
                _buttonHovered = false;
                buttonPanel.Invalidate();
            }
        }

        private void ButtonPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel buttonPanel = sender as Panel;

            if (buttonPanel == null)
                return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            Rectangle rect = new Rectangle(
                0,
                0,
                buttonPanel.Width - 1,
                buttonPanel.Height - 1
            );

            Color buttonColor = _buttonHovered
                ? Color.FromArgb(21, 72, 196)
                : _primaryBlue;

            using (GraphicsPath path = GetRoundedRect(rect, 22))
            using (SolidBrush brush = new SolidBrush(buttonColor))
            {
                e.Graphics.FillPath(brush, path);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle shadowRect = new Rectangle(12, 16, Width - 24, Height - 24);
            Rectangle cardRect = new Rectangle(8, 8, Width - 16, Height - 16);

            using (GraphicsPath shadowPath = GetRoundedRect(shadowRect, 28))
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(_hovered ? 28 : 18, 26, 86, 232)))
            {
                e.Graphics.FillPath(shadowBrush, shadowPath);
            }

            using (GraphicsPath cardPath = GetRoundedRect(cardRect, 28))
            using (SolidBrush cardBrush = new SolidBrush(_lightSurface))
            using (Pen borderPen = new Pen(_borderColor, 1))
            {
                e.Graphics.FillPath(cardBrush, cardPath);
                e.Graphics.DrawPath(borderPen, cardPath);
            }
        }

        private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }
    }

    public class CircularPictureBox : PictureBox
    {
        public CircularPictureBox()
        {
            SizeMode = PictureBoxSizeMode.Zoom;
            Cursor = Cursors.Hand;
            Resize += delegate { UpdateRegion(); };
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            base.OnPaint(pe);

            using (Pen borderPen = new Pen(Color.White, 4))
            {
                pe.Graphics.DrawEllipse(borderPen, 2, 2, Width - 5, Height - 5);
            }
        }

        private void UpdateRegion()
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, Width, Height);
                Region = new Region(path);
            }
        }
    }
}