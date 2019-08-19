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

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(DetailPage))); // Set default page 

            MasterPage.menuList.ItemSelected += Menu_Item_Selected; // Event handler

        }

        public void Menu_Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            // Get selected menu item and treat as a MasterPageItem object
            var item = e.SelectedItem as MasterPageItem; 

            if (item != null) // Validation 
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType)); // Open detail page 
                MasterPage.menuList.SelectedItem = null; // Set selected item to null
                IsPresented = false; // Close master page 
            }
        }

    }
}