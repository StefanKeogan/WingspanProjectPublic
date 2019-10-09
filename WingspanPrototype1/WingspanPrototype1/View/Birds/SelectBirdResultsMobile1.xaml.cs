using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectBirdResultsMobile1 : ContentPage
	{
		public SelectBirdResultsMobile1(ArrayList results)
		{
			InitializeComponent();

            resultsListView.ItemsSource = results;
        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem;

            //array list allows us to store either type of bird
            ArrayList bird = new ArrayList();
            bird.Add(item);

            Navigation.PushAsync(new SelectBirdResultsMobile2(bird));
        }
    }
}