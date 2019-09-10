using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.View;
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
            menuItems.Add(new MasterPageItem { Title = "New Wild Bird", TargetType = typeof(AddBird) });
            menuItems.Add(new MasterPageItem { Title = "New Captive Bird", TargetType = typeof(AddBird) });
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    menuItems.Add(new MasterPageItem { Title = "Add Bird Note", TargetType = typeof(AddBirdNote) });
                    break;
                case Device.Android:
                    menuItems.Add(new MasterPageItem { Title = "Add Bird Note", TargetType = typeof(AddBirdNoteMobile1) });
                    break;
                default:
                    break;
            }
           
            menuItems.Add(new MasterPageItem { Title = "Edit Birds", TargetType = typeof(Edit) });
            menuItems.Add(new MasterPageItem { Title = "New Member", TargetType = typeof(Edit) });
            menuItems.Add(new MasterPageItem { Title = "Edit Members", TargetType = typeof(Edit) });
            menuItems.Add(new MasterPageItem { Title = "New Sponsorship", TargetType = typeof(Edit) });
            menuItems.Add(new MasterPageItem { Title = "Add Volunteer", TargetType = typeof(Edit) });
            menuItems.Add(new MasterPageItem { Title = "Edit Volunteers", TargetType = typeof(Edit) });
            menuItems.Add(new MasterPageItem { Title = "Log Volunteer Hours", TargetType = typeof(Edit) });
            menuItems.Add(new MasterPageItem { Title = "New Sighting", TargetType = typeof(Edit) });
            menuItems.Add(new MasterPageItem { Title = "Reports", TargetType = typeof(ReportPage) });

            menuList = MenuList; // Assign MenuList to object (Makes the listview acessible from MainPage)

            // Set Master page list items source to menu item list 
            menuList.ItemsSource = menuItems;

        }
    }



}