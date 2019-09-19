using System;
using System.Collections;
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
    public partial class MemberResultsDesktop : ContentPage
    {
        public MemberResultsDesktop(ArrayList results)
        {
            InitializeComponent();

            resultsListView.ItemsSource = results;
        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Member;

            if (item != null)
            {
                DisplayMember(item);
            }
        }

        private void DisplayMember(Member member)
        {

        }

    }
}