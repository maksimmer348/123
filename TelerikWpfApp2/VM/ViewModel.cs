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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Telerik.Windows.Controls;


namespace TelerikWpfApp2;


public class ViewModel : BaseVM
{


    #region Model
   
    private Stand stand = new Stand();

    #endregion



    public ViewModel()
    {
        #region Commands

        StopStartTestCommand = new ActionCommand(OnCloseAppCmdExecuted, CanCloseAppCmdExecuted);
        SelectVipCommand = new ActionCommand(OnSelectVipCmdExecuted, CanSelectVipCmdExecuted);
        SettingsCommand = new ActionCommand(OnSettingCmdExecuted, CanSettingCmdExecuted);
        SelectMenuItemCommand = new ActionCommand(OnSelectMenuItemExecuted, CanSelectVipCmdExecuted);
        WizardOkCommand = new ActionCommand(OnWizardOkCmdExecuted, CanWizardOkCmdExecuted);
        #endregion

        Start();


    }

    #region UI preparation

    private ObservableCollection<string> iconsList;
    private string defaultMainTitle = "Стенд ЭТТ";

    void Start()
    {
        //добавление икнок для Title
        iconsList = new();
        iconsList.Add(@"Infrastructure/Resources/appIco.ico");
        iconsList.Add(@"Infrastructure/Resources/red.ico");
        iconsList.Add(@"Infrastructure/Resources/green.ico");
        //установка стартовой иконки, и заглоловка окна
        mainIco = iconsList[0];
        mainTitle = defaultMainTitle;
        timeLeft = "00:00:00";
        //заполнение боковго меню итемами
        items = GetItems();
        
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
        int numVip = 0;
        int tempVip = 10;
        int VInputVip = 14;
        IconGlyph iconWizard = new IconGlyph()
        {
            Glyph = "&#xe13b;",
            Color = Brushes.DarkGray
        };
        IconGlyph iconNone = new IconGlyph()
        {
            Glyph = "&#xe13b;",
            Color = Brushes.DarkGray
        };
        IconGlyph iconNoneSub = new IconGlyph()
        {
            Glyph = "&#xe115",
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
            TypeButton = TemplateType.AllVips,
            IconNone = iconNone,
            IconError = iconError,
            IconOk = iconOk,
            Title = "ВИПЫ",
            Status = StatusTest.None,
            //NavCommand = StopStartTestCommand,
            SelectMenuItemCommand = SelectMenuItemCommand,
            Next = WizardOkCommand
        };

        inboxItem.SubItems = new ObservableCollection<NavigationViewItemModel>();
        for (int i = 1; i <= 12; i++)
        {
            var temp = new NavigationViewItemModel();
          
            temp.TypeButton = TemplateType.Vip;
            temp.IconNone = iconNoneSub;
            temp.IconError = iconError;
            temp.IconOk = iconOk;
            temp.Title = $"ВИП {i}";
            
            temp.Status = StatusTest.None;
            if (stand.VIPs.Count > 0 && stand.VIPs.Count >= i)
            {
                temp.VIP = stand.VIPs[i - 1];
                temp.Status = StatusTest.Ok;
            }
            
            temp.NavCommand = SelectVipCommand;
            temp.SelectMenuItemCommand = SelectMenuItemCommand;
            inboxItem.SubItems.Add(temp);
            temp = null;
        }
        return new ObservableCollection<NavigationViewItemModel>
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
        set => Set(ref mainIco, value);
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
                foreach (var sub in Items[0].SubItems)
                {
                    if (!string.IsNullOrWhiteSpace(sub.NameVip))
                    {
                        sub.Status = StatusTest.None;
                    }
                }
            }

            if (mainStatus == StatusTest.Error)
            {
                MainTitle = "Ошибка";
                MainIco = iconsList[1];
                Items[0].Status = StatusTest.Error;
                foreach (var sub in Items[0].SubItems)
                {
                    if (!string.IsNullOrWhiteSpace(sub.NameVip))
                    {
                        sub.Status = StatusTest.Error;
                    }

                }
            }

            if (mainStatus == StatusTest.Ok)
            {
                MainTitle = $"Идут испытания, до конца: {timeLeft}";
                MainIco = iconsList[2];
                Items[0].Status = StatusTest.Ok;
                foreach (var sub in Items[0].SubItems)
                {
                    if (!string.IsNullOrWhiteSpace(sub.NameVip))
                    {
                        sub.Status = StatusTest.Ok;
                    }
                }
            }
        }
    }
    //TODO меняется при нажатии/ходе испытаний
    private string place;
    public string Place => place = "Меню/ВИП#";
    #endregion

    #region Commands
    //

    /// <summary>
    /// Команда остановить запустиь исптания (для тестов, после удалится)
    /// </summary>
    public ICommand StopStartTestCommand { get; }
    /// <summary>
    /// Команда смены режима испытаний (для тестов)
    /// </summary>
    /// <param name="p"></param>
    void OnCloseAppCmdExecuted(object p)
    {
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
    /// Доступность команды для смены режима испытаний (для тестов, после удалится)
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    bool CanCloseAppCmdExecuted(object p)
    {
        return true;
    }
    //

    /// <summary>
    /// Команда для выбора текущего подменю
    /// </summary>
    public ICommand SelectMenuItemCommand { get; set; }
    private void OnSelectMenuItemExecuted(object obj)
    {
        MenuItem = (NavigationViewItemModel)obj;
    }
    // private bool CanSelectMenuItemExecuted(object arg)
    // {
    //     return false;
    // }
    

    /// <summary>
    /// Дополнительная команда привязана к кнопкам ВИП 1...12 (пока что заглушка, после удалится)
    /// </summary>
    public ICommand SelectVipCommand { get; }
    private void OnSelectVipCmdExecuted(object obj)
    {
        var s = ((NavigationViewItemModel)obj).Status;
        //MessageBox.Show(s.ToString());
    }
    private bool CanSelectVipCmdExecuted(object arg)
    {
        if (((NavigationViewItemModel)arg)?.Status == StatusTest.None)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Команда настроек (пока что заглушка)
    /// </summary>
    public ICommand SettingsCommand { get; }

    private void OnSettingCmdExecuted(object obj)
    {
        foreach (var sub in Items[0].SubItems)
        {
            sub.NameVip += "+";
        }
        //MessageBox.Show("Окрыть настрокйки");
    }
    private bool CanSettingCmdExecuted(object arg)
    {
        return true;
    }
    //

    /// <summary>
    /// Команда для кнопки настроек конфигуратора ВИПОв (VIPs Wizard)
    /// </summary>
    public ICommand WizardOkCommand { get; set; }
    private void OnWizardOkCmdExecuted(object obj)
    {
        if (Items[0].TypeButton == TemplateType.AllVips)
        {
            Items[0].TypeButton = TemplateType.Wizard;
            MenuItem = (NavigationViewItemModel)obj;
            
        }
        else
        {
            Items[0].TypeButton = TemplateType.AllVips;
            MenuItem = (NavigationViewItemModel)obj;
        }
    }
    private bool CanWizardOkCmdExecuted(object arg)
    {
        return true;
    }
    //
    
    #endregion
}