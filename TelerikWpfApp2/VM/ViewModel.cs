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
    public ObservableCollection<IconGlyph> glyphsList;
    public ObservableCollection<IconGlyph> GlyphsList
    {
        get => glyphsList;
        set
        {
            Set(ref glyphsList, value);
        }
    }

    private string defaultMainTitle = "Стенд ЭТТ";
    public ViewModel()
    {
        iconsList = new();
        glyphsList = new();
        StopStartTest = new ActionCommand(OnCloseAppCmdExecuted, CanCloseAppCmdExecuted);

        Start();

        this.items = this.GetItems();
    }

    #region UI preparation

    void Start()
    {
        //иконки
        iconsList.Add(@"Infrastructure/Resources/appIco.ico");
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
        glyphsList.Add(new IconGlyph()
        {
            Glyph = "&#xe13b;",
        });
        mainTitle = defaultMainTitle;
        timeLeft = "00:00:00";
    }


    private List<NavigationViewItemModel> items;

    //public ObservableCollection<NavigationViewItemModel> Items
    //{
    //    get => items;
    //    set => Set(ref items, value);
    //}

    public List<NavigationViewItemModel> Items
    {
        get
        {
            return items;
        }
        set
        {
            if (value != null && items != value)
            {
                items = value;
            }
        }
    }

    private List<NavigationViewItemModel> GetItems()
    {
        var inboxItem = new NavigationViewItemModel()
        {
            Icon = glyphsList[3].Glyph,
            IconBrush = Brushes.DarkGray,
            Title = "ВИПЫ",
            Status = StatusTest.None,
            NavCommand = StopStartTest
        };

        inboxItem.SubItems = new ObservableCollection<NavigationViewItemModel>();
        for (int i = 1; i <= 12; i++)
        {
            inboxItem.SubItems.Add(new NavigationViewItemModel()
            {
                Icon = glyphsList[0].Glyph,
                IconBrush = Brushes.DarkGray,
                Title = $"ВИП {i}",
                Status = StatusTest.None
            });
        }

        return new List<NavigationViewItemModel>
        {
            inboxItem,
        };
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
    private IconGlyph itemIco;
    /// <summary>
    /// Иконка для ВИПА
    /// </summary>
    public IconGlyph ItemIco
    {
        get => itemIco;
        set => Set(ref itemIco, value);
    }

    private Color itemGlyphColor;
    /// <summary>
    /// Цвет иконка для ВИПА
    /// </summary>
    public Color ItemGlyphColor
    {
        get => itemGlyphColor;
        set => Set(ref itemGlyphColor, value);
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
    public ICommand StopStartTest{ get; }


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
