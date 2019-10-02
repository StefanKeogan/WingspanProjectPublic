using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View.Volunteers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VolunteerResultsMobile1 : ContentPage
    {
        public VolunteerResultsMobile1()
        {
            InitializeComponent();
        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Volunteer;

            if (item != null)
            {
                Navigation.PushAsync(new VolunteerResultsMobile2(item));
            }


        }
    }
}