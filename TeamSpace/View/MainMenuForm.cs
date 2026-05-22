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
    /// Главная форма приложения, предназначенная для выбора
    /// одного из модулей анализа.

    /// На форме размещаются три карточки участников разработки:
    /// - модуль анализа состояния дорог;
    /// - модуль анализа процента детей, рождённых вне брака;
    /// - модуль анализа потребительских расходов.

    /// Для внутренних модулей выполняется открытие соответствующих форм.
    /// Для внешнего модуля выполняется поиск и запуск отдельного
    /// исполняемого файла.
    public class MainMenuForm : Form
    {
        // ------------------------------------------------------------
        // Цветовая схема главного меню
        // ------------------------------------------------------------

        /// Основной цвет фона страницы.
        /// Используется для поверхности главной формы
        /// и корневой панели интерфейса.
        private readonly Color PageBackground = Color.White;

        /// Основной акцентный синий цвет.
        /// Используется для заголовка TEAMSPACE,
        /// кнопок карточек и визуальных акцентов.
        private readonly Color PrimaryBlue =
            Color.FromArgb(0x1A, 0x56, 0xE8);

        /// Светлый цвет поверхности карточек.
        /// Обеспечивает визуальное отделение карточек
        /// от белого фона основной формы.
        private readonly Color LightSurface =
            Color.FromArgb(0xF7, 0xFB, 0xFE);

        /// Основной тёмный цвет текста.
        /// Используется для крупных заголовков
        /// и названий модулей.
        private readonly Color DarkText =
            Color.FromArgb(0x1E, 0x21, 0x2E);

        /// Приглушённый цвет дополнительного текста.
        /// Используется для описаний карточек
        /// и поясняющего подзаголовка формы.
        private readonly Color MutedText =
            Color.FromArgb(110, 118, 138);

        /// Цвет границы карточек.
        /// Используется при пользовательской отрисовке
        /// контейнеров вариантов.
        private readonly Color BorderColor =
            Color.FromArgb(228, 236, 248);

        // ------------------------------------------------------------
        // Инициализация главной формы
        // ------------------------------------------------------------

        /// Создаёт и первоначально настраивает главное меню приложения.

        /// В конструкторе задаются параметры окна,
        /// после чего формируется весь визуальный интерфейс формы.
        public MainMenuForm()
        {
            // Устанавливается текст, отображаемый
            // в заголовке окна операционной системы.
            Text = "TeamSpace — Главное меню";

            // Форма должна открываться по центру экрана.
            StartPosition = FormStartPosition.CenterScreen;

            // Задаются начальные размеры окна главного меню.
            Width = 1320;
            Height = 860;

            // Устанавливается минимальный допустимый размер формы.
            // Это предотвращает нарушение расположения карточек
            // при чрезмерном уменьшении окна.
            MinimumSize = new Size(1320, 860);

            // Назначается основной цвет фона страницы.
            BackColor = PageBackground;

            // Включается двойная буферизация,
            // уменьшающая мерцание при перерисовке формы.
            DoubleBuffered = true;

            // Выполняется создание всех визуальных элементов
            // и их размещение на главной форме.
            BuildInterface();
        }

        // ------------------------------------------------------------
        // Формирование интерфейса главного меню
        // ------------------------------------------------------------

        /// Создаёт визуальную структуру главного меню:
        /// корневую панель, заголовочную область и панель карточек.

        /// Также в методе назначаются обработчики,
        /// отвечающие за открытие выбранных модулей.
        private void BuildInterface()
        {
            // Создаётся корневая панель, занимающая всю область формы.
            // Все остальные элементы интерфейса добавляются именно в неё.
            Panel root = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = PageBackground
            };

            // Корневая панель добавляется на форму.
            Controls.Add(root);

            // Создаётся верхняя область страницы,
            // предназначенная для названия приложения,
            // основного заголовка и поясняющего текста.
            Panel heroPanel = new Panel
            {
                Width = 1100,
                Height = 190,
                BackColor = Color.Transparent
            };

            // Создаётся небольшой акцентный заголовок
            // с названием приложения.
            Label smallLabel = new Label
            {
                Text = "TEAMSPACE",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = PrimaryBlue,
                AutoSize = true,
                BackColor = Color.Transparent
            };

            // Создаётся основной крупный заголовок страницы.
            Label titleLabel = new Label
            {
                Text = "Главное меню",
                Font = new Font("Georgia", 32, FontStyle.Bold),
                ForeColor = DarkText,
                AutoSize = true,
                BackColor = Color.Transparent
            };

            // Создаётся поясняющая надпись,
            // сообщающая о назначении карточек ниже.
            Label subtitleLabel = new Label
            {
                Text = "Выберите вариант участника разработки",
                Font = new Font("Segoe UI", 15, FontStyle.Regular),
                ForeColor = MutedText,
                AutoSize = true,
                BackColor = Color.Transparent
            };

            // Текстовые элементы добавляются
            // в верхнюю заголовочную панель.
            heroPanel.Controls.Add(smallLabel);
            heroPanel.Controls.Add(titleLabel);
            heroPanel.Controls.Add(subtitleLabel);

            // Каждая надпись размещается по горизонтальному центру
            // верхней панели с собственным вертикальным отступом.
            smallLabel.Location = new Point(
                (heroPanel.Width - smallLabel.Width) / 2,
                30
            );

            titleLabel.Location = new Point(
                (heroPanel.Width - titleLabel.Width) / 2,
                58
            );

            subtitleLabel.Location = new Point(
                (heroPanel.Width - subtitleLabel.Width) / 2,
                125
            );

            // Создаётся табличная панель для трёх карточек.
            // Каждая карточка занимает отдельную колонку.
            TableLayoutPanel cardsPanel = new TableLayoutPanel
            {
                ColumnCount = 3,
                RowCount = 1,
                Width = 1260,
                Height = 430,
                BackColor = Color.Transparent
            };

            // Ширина панели делится между тремя карточками
            // практически равными долями.
            cardsPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 33.33f)
            );

            cardsPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 33.33f)
            );

            cardsPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 33.33f)
            );

            // Создаётся карточка модуля анализа состояния дорог.
            // Для изображения используется ресурс приложения,
            // соответствующий участнику разработки.
            ModernCard vikaCard = CreateCard(
                "Вика — вариант 16",
                "Модуль анализа доли плохих дорог\nпо субъектам РФ",
                "Открыть",
                AppResources.vika
            );

            // Создаётся карточка внешнего модуля анализа
            // процента детей, рождённых вне брака.
            ModernCard leshaCard = CreateCard(
                "Леша — вариант 17",
                "Модуль анализа процента детей,\nрожденных вне брака",
                "Открыть",
                AppResources.lesha
            );

            // Создаётся карточка модуля анализа
            // потребительских расходов.
            ModernCard katyaCard = CreateCard(
                "Катя — вариант 18",
                "Модуль анализа\nпотребительских расходов",
                "Открыть",
                AppResources.katya
            );

            // При выборе карточки первого модуля
            // открывается форма анализа состояния дорог.
            vikaCard.Click += delegate
            {
                OpenBadRoadsForm();
            };

            // При выборе карточки второго модуля
            // выполняется запуск внешнего приложения.
            leshaCard.Click += delegate
            {
                LaunchBirthsOutOfWedlockApp();
            };

            // При выборе карточки третьего модуля
            // открывается форма анализа потребительских расходов.
            katyaCard.Click += delegate
            {
                OpenConsumerExpensesForm();
            };

            // Каждая карточка оборачивается в отдельную панель,
            // обеспечивающую её выравнивание по центру колонки.
            cardsPanel.Controls.Add(WrapCentered(vikaCard), 0, 0);
            cardsPanel.Controls.Add(WrapCentered(leshaCard), 1, 0);
            cardsPanel.Controls.Add(WrapCentered(katyaCard), 2, 0);

            // Верхняя область и панель карточек
            // добавляются в корневой контейнер формы.
            root.Controls.Add(heroPanel);
            root.Controls.Add(cardsPanel);

            // При изменении размеров корневой панели
            // центральное расположение основных блоков
            // рассчитывается повторно.
            root.Resize += delegate
            {
                heroPanel.Location = new Point(
                    (root.ClientSize.Width - heroPanel.Width) / 2,
                    35
                );

                cardsPanel.Location = new Point(
                    (root.ClientSize.Width - cardsPanel.Width) / 2,
                    210
                );
            };

            // После создания элементов задаётся
            // их первоначальное центральное расположение.
            heroPanel.Location = new Point(
                (root.ClientSize.Width - heroPanel.Width) / 2,
                35
            );

            cardsPanel.Location = new Point(
                (root.ClientSize.Width - cardsPanel.Width) / 2,
                210
            );
        }

        // ------------------------------------------------------------
        // Открытие внутренних форм приложения
        // ------------------------------------------------------------

        /// Открывает форму анализа доли плохих дорог.

        /// Создание и отображение формы помещены в блок обработки ошибок,
        /// чтобы возможная ошибка инициализации не завершала работу
        /// главного меню приложения.
        private void OpenBadRoadsForm()
        {
            try
            {
                // Создаётся новый экземпляр формы анализа дорог.
                BadRoadsForm form = new BadRoadsForm();

                // Форма открывается в немодальном режиме.
                // Главное меню при этом остаётся доступным.
                form.Show();
            }
            catch (Exception ex)
            {
                // При возникновении ошибки выводится информационное окно
                // с описанием причины невозможности открыть форму.
                MessageBox.Show(
                    "Ошибка при открытии формы BadRoadsForm:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// Открывает форму анализа потребительских расходов.

        /// Форма создаётся как внутреннее окно текущего приложения
        /// и отображается без блокировки главного меню.
        private void OpenConsumerExpensesForm()
        {
            try
            {
                // Создаётся экземпляр формы потребительских расходов.
                ConsumerExpensesForm form = new ConsumerExpensesForm();

                // Выполняется немодальное отображение созданной формы.
                form.Show();
            }
            catch (Exception ex)
            {
                // При ошибке создания или открытия формы
                // отображается сообщение с подробностями исключения.
                MessageBox.Show(
                    "Ошибка при открытии формы ConsumerExpensesForm:\n" +
                    ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ------------------------------------------------------------
        // Запуск внешнего приложения
        // ------------------------------------------------------------

        /// Выполняет запуск внешнего приложения,
        /// связанного с модулем анализа процента детей,
        /// рождённых вне брака.
        /// 
        /// Перед запуском выполняется поиск исполняемого файла
        /// в известных каталогах сборки Debug и Release.
        private void LaunchBirthsOutOfWedlockApp()
        {
            try
            {
                // Выполняется поиск готового исполняемого файла
                // внешнего проекта относительно текущего приложения.
                string exePath = FindBirthsOutOfWedlockExePath();

                // Если путь отсутствует или найденный файл
                // фактически не существует, запуск невозможен.
                if (string.IsNullOrEmpty(exePath) || !File.Exists(exePath))
                {
                    // Отображается сообщение о необходимости наличия
                    // собранного внешнего проекта в ожидаемой структуре папок.
                    MessageBox.Show(
                        "Не найден файл BirthsOutOfWedlockApp.exe.\n\n" +
                        "Проверьте, что проект Лёши собран и находится в структуре:\n" +
                        "BirthsOutOfWedlockApp\\BirthsOutOfWedlockApp\\bin\\Debug\\net8.0-windows",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return;
                }

                // Запуск отдельного приложения выполняется
                // через Process.Start.

                // В качестве рабочей директории задаётся папка,
                // содержащая исполняемый файл. Это обеспечивает
                // корректный доступ внешней программы
                // к собственным ресурсам и зависимостям.
                Process.Start(new ProcessStartInfo
                {
                    FileName = exePath,
                    WorkingDirectory = Path.GetDirectoryName(exePath),
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                // При невозможности запуска внешнего приложения
                // выводится сообщение с описанием возникшей ошибки.
                MessageBox.Show(
                    "Ошибка при запуске приложения:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// Выполняет поиск исполняемого файла внешнего приложения
        /// BirthsOutOfWedlockApp.

        /// Поиск начинается из папки запуска текущего приложения
        /// и постепенно поднимается к родительским каталогам.
        /// На каждом уровне проверяются стандартные каталоги
        /// сборки Debug и Release внешнего проекта.
        private string FindBirthsOutOfWedlockExePath()
        {
            // В качестве начальной позиции используется каталог,
            // из которого запущено основное приложение TeamSpace.
            DirectoryInfo currentDirectory =
                new DirectoryInfo(Application.StartupPath);

            // Поиск выполняется до достижения корневого каталога диска.
            while (currentDirectory != null)
            {
                // Формируется предполагаемый путь к исполняемому файлу,
                // созданному при сборке внешнего проекта
                // в конфигурации Debug.
                string debugPath = Path.Combine(
                    currentDirectory.FullName,
                    "BirthsOutOfWedlockApp",
                    "BirthsOutOfWedlockApp",
                    "bin",
                    "Debug",
                    "net8.0-windows",
                    "BirthsOutOfWedlockApp.exe"
                );

                // При обнаружении Debug-версии приложения
                // её путь немедленно возвращается для запуска.
                if (File.Exists(debugPath))
                    return debugPath;

                // Если Debug-версия отсутствует, формируется путь
                // к приложению, собранному в конфигурации Release.
                string releasePath = Path.Combine(
                    currentDirectory.FullName,
                    "BirthsOutOfWedlockApp",
                    "BirthsOutOfWedlockApp",
                    "bin",
                    "Release",
                    "net8.0-windows",
                    "BirthsOutOfWedlockApp.exe"
                );

                // При обнаружении Release-версии
                // возвращается её полный путь.
                if (File.Exists(releasePath))
                    return releasePath;

                // Если ни один вариант на текущем уровне не найден,
                // поиск продолжается в родительском каталоге.
                currentDirectory = currentDirectory.Parent;
            }

            // Значение null возвращается в случае,
            // если исполняемый файл не найден
            // ни в одном из проверенных каталогов.
            return null;
        }

        // ------------------------------------------------------------
        // Создание карточек главного меню
        // ------------------------------------------------------------

        /// Создаёт карточку модуля с единым стилевым оформлением
        /// главного меню.
        private ModernCard CreateCard(
            string title,
            string subtitle,
            string buttonText,
            Image avatarImage)
        {
            // Новый объект карточки получает текстовое содержимое,
            // изображение и общие цвета оформления главной формы.
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

        /// Оборачивает переданный элемент управления в панель,
        /// обеспечивающую его центральное расположение
        /// внутри ячейки табличного контейнера.
        private Control WrapCentered(Control control)
        {
            // Создаётся прозрачная панель,
            // полностью занимающая предоставленную ей область.
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            // Переданный элемент добавляется внутрь панели.
            panel.Controls.Add(control);

            // При изменении размеров панели положение вложенного
            // элемента пересчитывается для сохранения центрирования.
            panel.Resize += delegate
            {
                control.Location = new Point(
                    (panel.Width - control.Width) / 2,
                    10
                );
            };

            // Назначается начальное положение элемента управления.
            control.Location = new Point(
                (panel.Width - control.Width) / 2,
                10
            );

            // Возвращается готовый контейнер для размещения
            // в табличной панели карточек.
            return panel;
        }
    }

    // ============================================================
    // Пользовательская карточка модуля
    // ============================================================

    /// Пользовательский элемент управления,
    /// отображающий информацию об одном модуле приложения.

    /// Карточка включает:
    /// - круглое изображение участника;
    /// - заголовок;
    /// - описание модуля;
    /// - стилизованную кнопку открытия.

    /// Для карточки реализована пользовательская отрисовка
    /// фона, границы, тени и состояния наведения.
    public class ModernCard : Panel
    {
        // ------------------------------------------------------------
        // Содержимое карточки
        // ------------------------------------------------------------

        /// Заголовок карточки, содержащий имя участника
        /// и номер варианта.
        private readonly string _title;

        /// Описание модуля, отображаемое под заголовком.
        private readonly string _subtitle;

        /// Текст, отображаемый внутри кнопки карточки.
        private readonly string _buttonText;

        /// Изображение, используемое как аватар карточки.
        private readonly Image _avatarImage;

        // ------------------------------------------------------------
        // Цветовое оформление карточки
        // ------------------------------------------------------------

        /// Основной цвет кнопки карточки.
        private readonly Color _primaryBlue;

        /// Цвет основной поверхности карточки.
        private readonly Color _lightSurface;

        /// Цвет основного текста карточки.
        private readonly Color _darkText;

        /// Цвет второстепенного текста карточки.
        private readonly Color _mutedText;

        /// Цвет тонкой внешней границы карточки.
        private readonly Color _borderColor;

        // ------------------------------------------------------------
        // Состояние интерактивных элементов
        // ------------------------------------------------------------

        /// Признак нахождения указателя мыши над карточкой.
        /// Используется для изменения интенсивности тени.
        private bool _hovered;

        /// Признак нахождения указателя мыши над кнопкой карточки.
        /// Используется для затемнения кнопки при наведении.
        private bool _buttonHovered;

        /// Создаёт и настраивает карточку одного модуля приложения.

        /// В конструкторе сохраняются данные карточки,
        /// создаются вложенные элементы управления
        /// и назначаются обработчики взаимодействия.
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
            // Сохраняются переданные текстовые данные и изображение.
            _title = title;
            _subtitle = subtitle;
            _buttonText = buttonText;
            _avatarImage = avatarImage;

            // Сохраняется переданная цветовая схема карточки.
            _primaryBlue = primaryBlue;
            _lightSurface = lightSurface;
            _darkText = darkText;
            _mutedText = mutedText;
            _borderColor = borderColor;

            // Задаются размеры пользовательской карточки.
            Width = 380;
            Height = 380;

            // Использование прозрачного фона позволяет выполнять
            // собственную отрисовку поверхности и тени карточки.
            BackColor = Color.Transparent;

            // Для всей поверхности карточки назначается указатель,
            // обозначающий возможность нажатия.
            Cursor = Cursors.Hand;

            // Включается двойная буферизация
            // для плавной пользовательской отрисовки.
            DoubleBuffered = true;

            // Задаются внешние отступы карточки
            // относительно соседних элементов интерфейса.
            Margin = new Padding(20);

            // Создаётся круглое изображение участника,
            // размещаемое в верхней части карточки.
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

            // Создаётся заголовок карточки.
            // Фиксированная область позволяет выровнять текст
            // независимо от его фактической длины.
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

            // Создаётся текстовое описание модуля.
            // Для многострочного текста выделяется отдельная
            // центрально выровненная область.
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

            // Создаётся панель кнопки.
            // Фон панели отрисовывается вручную,
            // чтобы получить скруглённую форму.
            Panel buttonPanel = new Panel
            {
                Width = 154,
                Height = 46,
                BackColor = Color.Transparent,
                Location = new Point((Width - 154) / 2, 305),
                Cursor = Cursors.Hand
            };

            // К панели привязывается обработчик пользовательской
            // отрисовки скруглённой синей кнопки.
            buttonPanel.Paint += ButtonPanel_Paint;

            // Создаётся надпись, размещаемая поверх поверхности кнопки.
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

            // Текст кнопки добавляется внутрь панели кнопки.
            buttonPanel.Controls.Add(buttonLabel);

            // При попадании указателя мыши на панель кнопки
            // устанавливается состояние наведения
            // и инициируется её повторная отрисовка.
            buttonPanel.MouseEnter += delegate
            {
                _buttonHovered = true;
                buttonPanel.Invalidate();
            };

            // При выходе указателя из панели
            // выполняется уточнённая проверка его положения.
            // Это требуется из-за наличия вложенной надписи кнопки.
            buttonPanel.MouseLeave += delegate
            {
                UpdateButtonHoverState(buttonPanel);
            };

            // Наведение на вложенную надпись также должно
            // сохранять активное состояние кнопки.
            buttonLabel.MouseEnter += delegate
            {
                _buttonHovered = true;
                buttonPanel.Invalidate();
            };

            // При выходе указателя из надписи выполняется проверка,
            // покинул ли он всю область кнопки.
            buttonLabel.MouseLeave += delegate
            {
                UpdateButtonHoverState(buttonPanel);
            };

            // Все созданные визуальные элементы
            // добавляются внутрь карточки.
            Controls.Add(avatarPicture);
            Controls.Add(titleLabel);
            Controls.Add(subtitleLabel);
            Controls.Add(buttonPanel);

            // Обработчик сохраняется в исходной логике карточки.
            // Фактические действия при выборе карточки
            // назначаются из главной формы.
            Click += delegate { };

            // Для вложенных элементов назначается перенаправление клика
            // на событие Click самой карточки.
            // Благодаря этому нажатие по изображению, тексту
            // или кнопке воспринимается как выбор всей карточки.
            AttachClickHandlers(this);

            // При наведении на карточку включается визуальное состояние,
            // влияющее на интенсивность её тени.
            MouseEnter += delegate
            {
                _hovered = true;
                Invalidate();
            };

            // При выходе указателя из области карточки
            // визуальное состояние наведения отключается.
            MouseLeave += delegate
            {
                _hovered = false;
                Invalidate();
            };
        }

        // ------------------------------------------------------------
        // Передача кликов вложенных элементов карточке
        // ------------------------------------------------------------

        /// Рекурсивно назначает обработчики клика
        /// всем вложенным элементам карточки.

        /// При нажатии на любой дочерний элемент
        /// вызывается событие Click самой карточки.
        private void AttachClickHandlers(Control parent)
        {
            // Последовательно просматриваются
            // все непосредственно вложенные элементы управления.
            foreach (Control child in parent.Controls)
            {
                // При нажатии на дочерний элемент
                // инициируется событие нажатия основной карточки.
                child.Click += delegate (object sender, EventArgs e)
                {
                    OnClick(e);
                };

                // Для поддержки элементов с собственными дочерними
                // компонентами выполняется рекурсивный обход.
                AttachClickHandlers(child);
            }
        }

        // ------------------------------------------------------------
        // Обработка состояния наведения на кнопку
        // ------------------------------------------------------------

        /// Проверяет, остался ли указатель мыши
        /// внутри панели кнопки.

        /// Метод используется при переходе указателя
        /// между панелью кнопки и вложенной текстовой надписью.
        private void UpdateButtonHoverState(Panel buttonPanel)
        {
            // Текущие экранные координаты указателя преобразуются
            // в координаты относительно панели кнопки.
            Point cursorPoint =
                buttonPanel.PointToClient(Cursor.Position);

            // Если указатель действительно покинул область кнопки,
            // состояние наведения отключается и кнопка перерисовывается.
            if (!buttonPanel.ClientRectangle.Contains(cursorPoint))
            {
                _buttonHovered = false;
                buttonPanel.Invalidate();
            }
        }

        // ------------------------------------------------------------
        // Отрисовка кнопки карточки
        // ------------------------------------------------------------

        /// Выполняет пользовательскую отрисовку
        /// скруглённой кнопки карточки.

        /// Цвет кнопки изменяется в зависимости
        /// от состояния наведения указателя мыши.
        private void ButtonPanel_Paint(object sender, PaintEventArgs e)
        {
            // Источник события преобразуется к типу Panel.
            Panel buttonPanel = sender as Panel;

            // Если преобразование выполнить невозможно,
            // дальнейшая отрисовка не выполняется.
            if (buttonPanel == null)
                return;

            // Включаются параметры качественной отрисовки,
            // обеспечивающие плавность скруглённых краёв кнопки.
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.CompositingQuality =
                CompositingQuality.HighQuality;

            // Создаётся прямоугольная область,
            // в пределах которой будет нарисована кнопка.
            Rectangle rect = new Rectangle(
                0,
                0,
                buttonPanel.Width - 1,
                buttonPanel.Height - 1
            );

            // Цвет кнопки определяется текущим состоянием наведения.
            // При наведении применяется более тёмный оттенок синего.
            Color buttonColor = _buttonHovered
                ? Color.FromArgb(21, 72, 196)
                : _primaryBlue;

            // Формируется скруглённый контур кнопки,
            // после чего его внутренняя область
            // заполняется выбранным цветом.
            using (GraphicsPath path = GetRoundedRect(rect, 22))
            using (SolidBrush brush = new SolidBrush(buttonColor))
            {
                e.Graphics.FillPath(brush, path);
            }
        }

        // ------------------------------------------------------------
        // Отрисовка поверхности карточки
        // ------------------------------------------------------------

        /// Выполняет пользовательскую отрисовку карточки:
        /// создаёт мягкую тень, светлую поверхность
        /// и тонкую внешнюю границу.
        protected override void OnPaint(PaintEventArgs e)
        {
            // Сначала выполняется стандартная отрисовка панели.
            base.OnPaint(e);

            // Включается сглаживание для качественного отображения
            // скруглённых контуров карточки.
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Задаётся прямоугольная область тени.
            // Она немного смещена относительно поверхности карточки,
            // создавая визуальный эффект глубины.
            Rectangle shadowRect = new Rectangle(
                12,
                16,
                Width - 24,
                Height - 24
            );

            // Задаётся основная область карточки,
            // внутри которой отображается содержимое.
            Rectangle cardRect = new Rectangle(
                8,
                8,
                Width - 16,
                Height - 16
            );

            // Создаётся и рисуется тень карточки.

            // При наведении прозрачность тени увеличивается,
            // благодаря чему карточка визуально выделяется.
            using (GraphicsPath shadowPath =
                   GetRoundedRect(shadowRect, 28))
            using (SolidBrush shadowBrush = new SolidBrush(
                Color.FromArgb(
                    _hovered ? 28 : 18,
                    26,
                    86,
                    232
                )))
            {
                e.Graphics.FillPath(shadowBrush, shadowPath);
            }

            // Создаётся скруглённая поверхность карточки.
            // Сначала она заполняется светлым цветом,
            // затем обводится тонкой границей.
            using (GraphicsPath cardPath =
                   GetRoundedRect(cardRect, 28))
            using (SolidBrush cardBrush =
                   new SolidBrush(_lightSurface))
            using (Pen borderPen =
                   new Pen(_borderColor, 1))
            {
                e.Graphics.FillPath(cardBrush, cardPath);
                e.Graphics.DrawPath(borderPen, cardPath);
            }
        }

        // ------------------------------------------------------------
        // Формирование скруглённого контура
        // ------------------------------------------------------------

        /// Формирует графический контур прямоугольника
        /// со скруглёнными углами.

        /// Метод применяется как для поверхности карточки,
        /// так и для кнопки действия.
        private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            // Создаётся пустой графический путь,
            // в который последовательно добавляются дуги углов.
            GraphicsPath path = new GraphicsPath();

            // Диаметр дуги определяется удвоенным радиусом.
            int d = radius * 2;

            // Добавляется дуга верхнего левого угла.
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);

            // Добавляется дуга верхнего правого угла.
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);

            // Добавляется дуга нижнего правого угла.
            path.AddArc(
                rect.Right - d,
                rect.Bottom - d,
                d,
                d,
                0,
                90
            );

            // Добавляется дуга нижнего левого угла.
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);

            // Контур замыкается для последующего
            // корректного заполнения цветом.
            path.CloseFigure();

            // Возвращается готовый скруглённый контур.
            return path;
        }
    }

    // ============================================================
    // Круглое изображение участника
    // ============================================================

    /// Пользовательский элемент изображения,
    /// отображающий картинку в круглой области.

    /// Дополнительно поверх изображения рисуется белая рамка,
    /// подчёркивающая визуальное оформление аватара.
    public class CircularPictureBox : PictureBox
    {
        /// Создаёт круглый элемент изображения
        /// и назначает обработку изменения его размеров.
        public CircularPictureBox()
        {
            // Изображение масштабируется с сохранением пропорций
            // внутри доступной области элемента управления.
            SizeMode = PictureBoxSizeMode.Zoom;

            // Для изображения отображается указатель,
            // показывающий возможность выбора карточки.
            Cursor = Cursors.Hand;

            // При изменении размера элемента
            // его область отображения пересчитывается,
            // чтобы сохранить круглую форму.
            Resize += delegate
            {
                UpdateRegion();
            };
        }

        /// Выполняет стандартную отрисовку изображения
        /// и добавляет белую круглую рамку поверх него.
        protected override void OnPaint(PaintEventArgs pe)
        {
            // Включается сглаживание,
            // необходимое для плавного контура окружности.
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Выполняется стандартная отрисовка изображения.
            base.OnPaint(pe);

            // Поверх изображения рисуется белая круглая рамка.
            // Толщина рамки составляет четыре пикселя.
            using (Pen borderPen = new Pen(Color.White, 4))
            {
                pe.Graphics.DrawEllipse(
                    borderPen,
                    2,
                    2,
                    Width - 5,
                    Height - 5
                );
            }
        }

        /// Изменяет отображаемую область элемента управления
        /// на круглую форму.

        /// Метод вызывается при изменении размеров изображения,
        /// чтобы круглая маска соответствовала текущей ширине и высоте.
        private void UpdateRegion()
        {
            // Создаётся графический путь в форме эллипса,
            // занимающего всю область изображения.
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, Width, Height);

                // Регион элемента управления заменяется
                // созданной круглой областью.
                Region = new Region(path);
            }
        }
    }
}