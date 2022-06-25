using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using TelerikWpfApp2.Model.TestedVIPs;

namespace TelerikWpfApp2
{
    public class NavigationViewItemModel : BaseVM // можно ли разбить  вью модель на несолько классов вот так
    {
        public BaseVIP VIP { get; set; }

        private int numVip;

        /// <summary>
        /// Иконка окна
        /// </summary>
        public int NumVip
        {
            get => VIP.Number;
            set
            {
                VIP.Number = value;
                OnPropertyChanged("NumVip");
            }
        }

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
        public ICommand SelectMenuItemCommand { get; set; }
        public ICommand NavCommand { get; set; }
        
    }
}