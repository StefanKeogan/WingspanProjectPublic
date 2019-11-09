using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.View;
using WingspanPrototype1.View.Volunteers;
using WingspanPrototype1.Functions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WingspanPrototype1.View.Reports;
using WingspanPrototype1.View.Settings;

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
            menuItems.Add(new MasterPageItem { Title = "New Wild Bird", TargetType = typeof(AddBirdForm) });
            menuItems.Add(new MasterPageItem { Title = "New Captive Bird", TargetType = typeof(AddBirdForm) });


            if (DeviceSize.ScreenArea() <= 783457) // If the device screen is smaller than 7 inches
            {
                menuItems.Add(new MasterPageItem { Title = "Add Bird Note", TargetType = typeof(AddBirdNoteMobile1) });
            }
            else
            {
                menuItems.Add(new MasterPageItem { Title = "Add Bird Note", TargetType = typeof(AddBirdNoteDesktop) });
            }

           
            menuItems.Add(new MasterPageItem { Title = "Edit Birds", TargetType = typeof(Edit) });
            menuItems.Add(new MasterPageItem { Title = "New Member", TargetType = typeof(AddMemberForm) });
            menuItems.Add(new MasterPageItem { Title = "Edit Members", TargetType = typeof(Edit) });
            menuItems.Add(new MasterPageItem { Title = "New Sponsorship", TargetType = typeof(AddSponsorshipForm) });
            menuItems.Add(new MasterPageItem { Title = "Edit Sponsorships", TargetType = typeof(Edit) });
            menuItems.Add(new MasterPageItem { Title = "New Volunteer", TargetType = typeof(AddVolunteerForm) });
            menuItems.Add(new MasterPageItem { Title = "Edit Volunteers", TargetType = typeof(Edit) });

            if (DeviceSize.ScreenArea() <= 783457) // If the device screen is smaller than 7 inches
            {
                menuItems.Add(new MasterPageItem { Title = "Log Volunteer Hours", TargetType = typeof(LogVolunteerHoursMobile1) });

            }
            else
            {
                menuItems.Add(new MasterPageItem { Title = "Log Volunteer Hours", TargetType = typeof(LogVolunteerHoursDesktop) });
            }

            if (DeviceSize.ScreenArea() <= 783457) // If the device screen is smaller than 7 inches
            {
                menuItems.Add(new MasterPageItem { Title = "Report Builder", TargetType = typeof(ReportBuilderMobile1) });
            }
            else
            {
                menuItems.Add(new MasterPageItem { Title = "Report Builder", TargetType = typeof(ReportBuilderDesktop) });               
            }

            menuItems.Add(new MasterPageItem { Title = "Settings", TargetType = typeof(SettingsMenu) });

            menuList = MenuList; // Assign MenuList to object (Makes the listview acessible from MainPage)

            // Set Master page list items source to menu item list 
            menuList.ItemsSource = menuItems;

        }
    }



}