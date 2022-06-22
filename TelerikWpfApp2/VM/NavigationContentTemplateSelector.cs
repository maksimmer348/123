using System.Windows;
using System.Windows.Controls;
using TelerikWpfApp2.Model.TestedVIPs;

namespace TelerikWpfApp2
{
    public class NavigationContentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TemplateEmpty { get; set; }
        public DataTemplate Template1 { get; set; }
        public DataTemplate Template2 { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                NavigationViewItemModel model = (NavigationViewItemModel)item;
                if (model.TypeButton == TemplateType.AllVips)
                {
                    return this.Template1;
                }
                if (model.TypeButton == TemplateType.Vip)
                {
                    return this.Template2;
                }
                if (model.Title == "ВИП 2")
                {
                    return this.Template2;
                }
            }

            return this.TemplateEmpty;
        }
    }
}

