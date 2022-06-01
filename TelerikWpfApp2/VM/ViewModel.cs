using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace TelerikWpfApp2;

public class ViewModel : BaseVM
{
    private ObservableCollection<string> iconsList;
    private ObservableCollection<IconGlyph> glyphsList;
    private string defaultMainTitle = "Стенд ЭТТ";
    public ViewModel()
    {
        iconsList = new();
        glyphsList = new();
        StopStartMesaure = new ActionCommand(OnCloseAppCmdExecuted, CanCloseAppCmdExecuted);
        this.items = this.GetItems();
    }
    void Start()
    {
        //иконки
        iconsList.Add(@"Infrastructure/Resources/device.ico");
        iconsList.Add(@"Infrastructure/Resources/green.ico");
        iconsList.Add(@"Infrastructure/Resources/red.ico");
        //иконка по умолчанию
        mainIco = iconsList[0];

        //глифы
        //отключен
        glyphsList.Add(new IconGlyph()
        {
            Glyph = "&#xe115",
            Color = Color.FromRgb(192, 192, 192)
        });
        //ошибка
        glyphsList.Add(new IconGlyph()
        {
            Glyph = "&#xe11d;",
            Color = Color.FromRgb(255, 0, 0)
        });
        //ок
        glyphsList.Add(new IconGlyph()
        {
            Glyph = "&#xe11a;",
            Color = Color.FromRgb(0, 128, 1)
        });
        mainTitle = defaultMainTitle;
        timeLeft = "00:00:00";
    }


    private List<NavigationViewItemModel> items;
    public List<NavigationViewItemModel> Items
    {
        get
        {
            return this.items;
        }
        set
        {
            if (this.items != value)
            {
                this.items = value;
            }
        }
    }
    private List<NavigationViewItemModel> GetItems()
    {
        var inboxItem = new NavigationViewItemModel() { Icon = "&#xe802;", Title = "In box" };
        inboxItem.SubItems = new ObservableCollection<NavigationViewItemModel>
        {
            new NavigationViewItemModel() { Icon = "&#xe811;", Title = "LinkedIn" },
            new NavigationViewItemModel() { Icon = "&#xe81f;", Title = "Twitter" },
            new NavigationViewItemModel() { Icon = "&#xe815;", Title = "Pinterest" },
            new NavigationViewItemModel() { Icon = "&#xe63d;", Title = "Subscriptions" },
            new NavigationViewItemModel() { Icon = "&#xe400;", Title = "Orders Updates" }
        };

        var importantItem = new NavigationViewItemModel() { Icon = "&#xe303;", Title = "Important" };
        importantItem.SubItems = new ObservableCollection<NavigationViewItemModel>
        {
            new NavigationViewItemModel() { Title = "University" },
            new NavigationViewItemModel() { Title = "Work" }
        };

        return new List<NavigationViewItemModel>
        {
            inboxItem,
            new NavigationViewItemModel() { Icon = "&#xe906;", Title = "Drafts"},
            new NavigationViewItemModel() { Icon = "&#xe11a;", Title = "Sent"},
            importantItem,
            new NavigationViewItemModel() { Icon = "&#xe809;", Title = "All Mail"},
            new NavigationViewItemModel() { Icon = "&#xe403;", Title = "Spam"},
            new NavigationViewItemModel() { Icon = "&#xe10c;", Title = "Deleted"},
        };
    }

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
        set
        {
            Set(ref mainIco, value);
        }
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

    //TODO иконки для випов
    private IconGlyph vIPIco;
    /// <summary>
    /// Иконка для ВИПА
    /// </summary>
    public IconGlyph VIPIco
    {
        get => vIPIco;
        set => Set(ref vIPIco, value);
    }

    private Color vIPGlyphColor;
    /// <summary>
    /// Цвет иконка для ВИПА
    /// </summary>
    public Color VIPGlyphColor
    {
        get => vIPGlyphColor;
        set => Set(ref vIPGlyphColor, value);
    }

    private StatusTest status;
    /// <summary>
    /// Статус исптаний
    /// </summary>
    public StatusTest Status
    {
        get => status;
        set
        {
            Set(ref status, value);

            if (status == StatusTest.None)
            {
                MainTitle = defaultMainTitle;
                MainIco = iconsList[0];
            }
            if (status == StatusTest.Error)
            {
                MainTitle = "Ошибка";
                MainIco = iconsList[1];
            }
            if (status == StatusTest.Ok)
            {
                MainTitle = $"Идут испытания, до конца: {timeLeft}";
                MainIco = iconsList[2];
            }
        }
    }
    private string place;
    public string Place => place = "Меню/ВИП#";

    #endregion

    #region Commands

    /// <summary>
    /// Свойство команды закрытия окна (те сама команда)
    /// </summary>
    public ICommand StopStartMesaure { get; }


    /// <summary>
    /// Выполнение логики команды
    /// </summary>
    /// <param name="p"></param>
    void OnCloseAppCmdExecuted(object p)
    {
        if (status == StatusTest.None)
        {
            status = StatusTest.Error;
        }
        if (status == StatusTest.Error)
        {
            status = StatusTest.Ok;
        }
        if (status == StatusTest.Ok)
        {
            status = StatusTest.None;
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
