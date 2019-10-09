using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditSponsorResultsMobile1 : ContentPage
	{
		public EditSponsorResultsMobile1(List<Sponsor> results)
		{
			InitializeComponent();

            resultsListView.ItemsSource = results;
        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Sponsor;

            if (item != null)
            {
                Navigation.PushAsync(new EditSponsorResultsMobile2(item));
            }
        }
    }
}