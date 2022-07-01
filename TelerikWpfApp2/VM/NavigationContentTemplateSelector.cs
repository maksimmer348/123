using System.Windows;
using System.Windows.Controls;


namespace TelerikWpfApp2
{
    public class NavigationContentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TemplateEmpty { get; set; }
        public DataTemplate TemplateWizard { get; set; }
        public DataTemplate TemplateAllVips { get; set; }
        public DataTemplate TemplateVip { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                NavigationViewItemModel model = (NavigationViewItemModel)item;
                if (model.TypeButton == TemplateType.Wizard)
                {
                    return TemplateWizard;
                }
                if (model.TypeButton == TemplateType.AllVips)
                {
                    //Для тестов
                    //return TemplateWizard;
                    return this.TemplateAllVips;
                }
                if (model.TypeButton == TemplateType.Vip)
                {
                    return this.TemplateVip;
                }
            }
            return this.TemplateEmpty;
        }
    }
}

