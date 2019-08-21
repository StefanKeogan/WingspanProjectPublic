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
        public ListView menuList; // Create an object of MenuList so that we can access it from MainPage

        public MasterPage()
        {
            InitializeComponent();

            Title = "Menu"; 

            // Menu items source
            List<MasterPageItem> menuItems = new List<MasterPageItem>();

            // Menu list items
            menuItems.Add(new MasterPageItem { Title = "Home", TargetType = typeof(HomePage) });
            menuItems.Add(new MasterPageItem { Title = "Birds", TargetType = typeof(DetailPage) });
            menuItems.Add(new MasterPageItem { Title = "Members", TargetType = typeof(DetailPage) });
            menuItems.Add(new MasterPageItem { Title = "Sponsors", TargetType = typeof(DetailPage) });
            menuItems.Add(new MasterPageItem { Title = "Volunteers", TargetType = typeof(DetailPage) });
            menuItems.Add(new MasterPageItem { Title = "Sightings", TargetType = typeof(DetailPage) });
            menuItems.Add(new MasterPageItem { Title = "Reports", TargetType = typeof(ReportPage) });

            menuList = MenuList; // Assign MenuList to object (Makes the listview acessible from MainPage)

            // Set Master page list items source to menu item list 
            menuList.ItemsSource = menuItems;

        }
    }



}