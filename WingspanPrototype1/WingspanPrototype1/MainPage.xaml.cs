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
        public MainPage()
        {
            InitializeComponent();

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(DetailPage)));

            MasterPage.menuList.ItemSelected += Menu_Item_Selected;

        }

        public void Menu_Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;

            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType)); // Open detail page 
                MasterPage.menuList.SelectedItem = null; // Set selected item to null
                IsPresented = false; // Close master page 
            }
        }

    }
}