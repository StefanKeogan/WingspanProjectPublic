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
    public partial class MainPage : MasterDetailPage
    {
        // For James (Master Detail Tutorial): https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/navigation/master-detail-page
        public MainPage()
        {
            InitializeComponent();

            // Set default page to home
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage))) {
                BarBackgroundColor = Color.FromHex("#5C3838"),
                BarTextColor = Color.White
            }; 

            MasterPage.menuList.ItemSelected += Menu_Item_Selected; // Event handler

        }

        public void Menu_Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            // Get selected menu item and treat as a MasterPageItem object
            var item = e.SelectedItem as MasterPageItem; 

            if (item != null) // Validation 
            { 
                // Open detail page 
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType, item.Title)) { 
                    BarBackgroundColor = Color.FromHex("#5C3838"),
                    BarTextColor = Color.White,
                   
                }; 
                MasterPage.menuList.SelectedItem = null; // Set selected item to null (Ensures we dont get stuck on selected page)
                IsPresented = false; // Close master page 
            }
        }

    }
}