using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;


namespace TelerikWpfApp2
{
    public class NavigationViewItemModel : BaseVM
    {
        #region VIP

        public BaseVIP? VIP { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string NameVip
        {
            get
            {
                if (VIP != null)
                    return VIP.Name;
                return "";
            }
            set
            {
                if (VIP == null)
                {
                    VIP = new BaseVIP
                    {
                        Name = value
                    };
                }
                else
                {
                    VIP.Name = value;
                }
                OnPropertyChanged("NumVip");
            }
        }

        private string typeVip;

        public string TypeVip
        {
            get
            {
                if (VIP != null)
                    return VIP.Type;
                return "";
                
            }
            set
            {
                
            }
        }

        #endregion

        #region Property

        public TemplateType TypeButton;

        public IconGlyph IconNone;
        public IconGlyph IconError;
        public IconGlyph IconOk;

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
                    Icon = IconNone.Glyph;
                    IconBrush = IconNone.Color;
                }

                if (status == StatusTest.Error)
                {
                    Icon = IconError.Glyph;
                    IconBrush = IconError.Color;
                }

                if (status == StatusTest.Ok)
                {
                    Icon = IconOk.Glyph;
                    IconBrush = IconOk.Color;
                }
            }
        }

        public string icon;

        public string Icon
        {
            get => icon;
            set => Set(ref icon, value);
        }

        private Brush iconBrush;

        public Brush IconBrush

        {
            get => iconBrush;
            set => Set(ref iconBrush, value);
        }

        public string Title { get; set; }
        public ObservableCollection<NavigationViewItemModel> SubItems { get; set; }

        #endregion

        #region Command

        public ICommand SelectMenuItemCommand { get; set; }
        public ICommand Next { get; set; }
        public ICommand NavCommand { get; set; }

        #endregion
    }
}