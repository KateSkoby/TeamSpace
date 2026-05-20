using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
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
            Width = 1280;
            Height = 820;
            MinimumSize = new Size(1100, 700);
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
                Width = 1120,
                Height = 360,
                BackColor = Color.Transparent
            };

            cardsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            cardsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            cardsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));

            ModernCard vikaCard = CreateCard(
                "Вика — вариант 16",
                "Модуль анализа по варианту 16",
                "Открыть",
                AppResources.vika
            );

            ModernCard leshaCard = CreateCard(
                "Леша — вариант 17",
                "Модуль анализа по варианту 17",
                "Открыть",
                AppResources.lesha
            );

            ModernCard katyaCard = CreateCard(
                "Катя — вариант 18",
                "Модуль анализа по варианту 18",
                "Открыть",
                AppResources.katya
            );

            vikaCard.Click += delegate { MessageBox.Show("Открыт модуль: Вика — вариант 16"); };
            leshaCard.Click += delegate { MessageBox.Show("Открыт модуль: Леша — вариант 17"); };
            katyaCard.Click += delegate { MessageBox.Show("Открыт модуль: Катя — вариант 18"); };

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

        private ModernCard CreateCard(string title, string subtitle, string buttonText, Image avatarImage)
        {
            return new ModernCard(title, subtitle, buttonText, avatarImage, PrimaryBlue, LightSurface, DarkText, MutedText, BorderColor);
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

            Width = 330;
            Height = 320;
            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
            DoubleBuffered = true;
            Margin = new Padding(20);

            CircularPictureBox avatarPicture = new CircularPictureBox
            {
                Width = 96,
                Height = 96,
                BackColor = Color.FromArgb(238, 244, 255),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point((Width - 96) / 2, 26),
                Image = _avatarImage
            };

            Label titleLabel = new Label
            {
                Text = _title,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = _darkText,
                AutoSize = false,
                Width = 280,
                Height = 62,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(25, 132),
                BackColor = Color.Transparent
            };

            Label subtitleLabel = new Label
            {
                Text = _subtitle,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = _mutedText,
                AutoSize = false,
                Width = 270,
                Height = 45,
                TextAlign = ContentAlignment.TopCenter,
                Location = new Point(30, 198),
                BackColor = Color.Transparent
            };

            Panel buttonPanel = new Panel
            {
                Width = 132,
                Height = 42,
                BackColor = Color.Transparent,
                Location = new Point((Width - 132) / 2, 255)
            };
            buttonPanel.Paint += ButtonPanel_Paint;

            Label buttonLabel = new Label
            {
                Text = _buttonText,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Width = 132,
                Height = 42,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };

            buttonPanel.Controls.Add(buttonLabel);

            Controls.Add(avatarPicture);
            Controls.Add(titleLabel);
            Controls.Add(subtitleLabel);
            Controls.Add(buttonPanel);

            foreach (Control c in Controls)
                c.Click += delegate (object sender, EventArgs e) { OnClick(e); };

            MouseEnter += delegate { _hovered = true; Invalidate(); };
            MouseLeave += delegate { _hovered = false; Invalidate(); };
        }

        private void ButtonPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, ((Panel)sender).Width - 1, ((Panel)sender).Height - 1);

            using (GraphicsPath path = GetRoundedRect(rect, 20))
            using (SolidBrush brush = new SolidBrush(_primaryBlue))
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