using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using TelerikWpfApp2.Model.TestedVIPs;

namespace TelerikWpfApp2
{

    public class ViewModel : BaseVM
    {
        private ObservableCollection<string> iconsList;
        public ObservableCollection<IconGlyph> glyphsList;

        public ObservableCollection<IconGlyph> GlyphsList
        {
            get => glyphsList;
            set { Set(ref glyphsList, value); }
        }

        private string defaultMainTitle = "Стенд ЭТТ";

        public ViewModel()
        {
            iconsList = new();
            glyphsList = new();
            StopStartTest = new ActionCommand(OnCloseAppCmdExecuted, CanCloseAppCmdExecuted);

            Start();

            items = GetItems();
        }

        #region UI preparation

        void Start()
        {
            //добавление икнок для Title
            iconsList.Add(@"Infrastructure/Resources/appIco.ico");
            iconsList.Add(@"Infrastructure/Resources/red.ico");
            iconsList.Add(@"Infrastructure/Resources/green.ico");
            //установка иконка по умолчанию для Title
            mainIco = iconsList[0];

            //глифы
            //отключен для субмодулей
            glyphsList.Add(new IconGlyph()
            {
                Glyph = "&#xe115",
                Color = Brushes.DarkGray
            });
            //ошибка
            glyphsList.Add(new IconGlyph()
            {
                Glyph = "&#xe11d;",
                Color = Brushes.Red
            });
            //ок
            glyphsList.Add(new IconGlyph()
            {
                Glyph = "&#xe11a;",
                Color = Brushes.Green
            });
            //отключен для общего модуля
            glyphsList.Add(new IconGlyph()
            {
                Glyph = "&#xe13b;",
                Color = Brushes.DarkGray
            });
            mainTitle = defaultMainTitle;
            timeLeft = "00:00:00";
        }

        private NavigationViewItemModel menuItem;

        public NavigationViewItemModel MenuItem
        {
            get { return menuItem; }
            set => Set(ref menuItem, value);
        }

        private ObservableCollection<NavigationViewItemModel> items;

        public ObservableCollection<NavigationViewItemModel> Items
        {
            get => items;
            set => Set(ref items, value);
        }

        private ObservableCollection<NavigationViewItemModel> GetItems()
        {

            IconGlyph iconNone = new IconGlyph()
            {
                Glyph = "&#xe13b;",
                Color = Brushes.DarkGray
            };
            IconGlyph iconNoneSub = new IconGlyph()
            {
                Glyph ="&#xe115", 
                Color = Brushes.DarkGray
            };
            IconGlyph iconError = new IconGlyph()
            {
                Glyph = "&#xe11d;",
                Color = Brushes.Red
            };
            IconGlyph iconOk = new IconGlyph()
            {
                Glyph = "&#xe11a;",
                Color = Brushes.Green
            };

            var inboxItem = new NavigationViewItemModel()
            {
                IconNone = iconNone,
                IconError = iconError,
                IconOk = iconOk,
                Title = "ВИПЫ",
                Status = StatusTest.None,
                NavCommand = StopStartTest
            };

            inboxItem.SubItems = new ObservableCollection<NavigationViewItemModel>();
            for (int i = 1; i <= 12; i++)
            {
                inboxItem.SubItems.Add(new NavigationViewItemModel()
                {
                    IconNone = iconNoneSub,
                    IconError = iconError,
                    IconOk = iconOk,
                    Title = $"ВИП {i}",
                    Status = StatusTest.None
                });
            }

            return new ObservableCollection<NavigationViewItemModel>
            {
            inboxItem,
            };
        }

        private IEnumerable<VIP71> AllProducts = new List<VIP71>();

        IEnumerable<VIP71> currentSelection;
       public IEnumerable<VIP71> CurrentSelection
        {
            get => currentSelection;
            set
            {
                if (Set(ref currentSelection, value))
                {
                    CurrentSelection = GetCurrentProductList();
                }
            }
        }

        IEnumerable<VIP71> GetCurrentProductList()
        {
            return AllProducts;
        }

        #endregion

        #region Properties

        private string mainTitle;

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string MainTitle
        {
            get => mainTitle;
            set => Set(ref mainTitle, value);
        }

        private string mainIco;

        /// <summary>
        /// Иконка окна
        /// </summary>
        public string MainIco
        {
            get => mainIco;
            set { Set(ref mainIco, value); }
        }

        private string timeLeft;

        /// <summary>
        /// Оставшееся время до конца испытания
        /// </summary>
        public string TimeLeft
        {
            //TODO сделать формат времени
            get => timeLeft;
            set => Set(ref timeLeft, value);
        }

        private StatusTest mainStatus;

        /// <summary>
        /// Статус исптаний
        /// </summary>
        public StatusTest MainStatus
        {
            get => mainStatus;

            set
            {
                Set(ref mainStatus, value);

                if (mainStatus == StatusTest.None)
                {
                    MainTitle = defaultMainTitle;
                    MainIco = iconsList[0];
                    Items[0].Status = StatusTest.None;
                }

                if (mainStatus == StatusTest.Error)
                {
                    MainTitle = "Ошибка";
                    MainIco = iconsList[1];
                    Items[0].Status = StatusTest.Error;
                }

                if (mainStatus == StatusTest.Ok)
                {
                    MainTitle = $"Идут испытания, до конца: {timeLeft}";
                    MainIco = iconsList[2];
                    Items[0].Status = StatusTest.Ok;
                }
            }
        }

        private string place;
        public string Place => place = "Меню/ВИП#";

        #endregion

        #region Commands

        /// <summary>
        /// Команда остановить запустиьт исптания (тест)
        /// </summary>
        public ICommand StopStartTest { get; }

        public void SS()
        {
            MessageBox.Show("Clicked Item");
        }

        /// <summary>
        /// Выполнение логики команды
        /// </summary>
        /// <param name="p"></param>
        void OnCloseAppCmdExecuted(object p)
        {
            // var clickedItem = (p as MouseButtonEventArgs).OriginalSource as TextBlock;
            // if (clickedItem != null)
            // {
            //     MessageBox.Show("Clicked Item: " + clickedItem.Text);
            // }

            if (mainStatus == StatusTest.None)
            {
                MainStatus = StatusTest.Error;
            }

            else if (mainStatus == StatusTest.Error)
            {
                MainStatus = StatusTest.Ok;
            }

            else if (mainStatus == StatusTest.Ok)
            {
                MainStatus = StatusTest.None;
            }
        }

        /// <summary>
        /// Доступность команды для выполнения
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        bool CanCloseAppCmdExecuted(object p)
        {
            //команда доступна для выполнения всегда, поэтому возварщаем труе 
            return true;
        }

        #endregion
    }
}