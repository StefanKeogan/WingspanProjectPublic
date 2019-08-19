using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        public MasterPage()
        {
            InitializeComponent();

            Title = "Menu"; 

            // Menu items source
            List<MasterPageItem> menuItems = new List<MasterPageItem>();

            // Menu list items
            menuItems.Add(new MasterPageItem { Title = "Birds", TargetType = typeof(DetailPage) });
            menuItems.Add(new MasterPageItem { Title = "Members", TargetType = typeof(DetailPage) });
            menuItems.Add(new MasterPageItem { Title = "Sponsors", TargetType = typeof(DetailPage) });
            menuItems.Add(new MasterPageItem { Title = "Volunteers", TargetType = typeof(DetailPage) });
            menuItems.Add(new MasterPageItem { Title = "Sightings", TargetType = typeof(DetailPage) });
            menuItems.Add(new MasterPageItem { Title = "Reports", TargetType = typeof(ReportPage) });

            // Set Master page list items source to menu item list 
            MenuList.ItemsSource = menuItems;

        }
    }



}