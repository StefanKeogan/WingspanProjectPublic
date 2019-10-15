using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View.Reports
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportBuilderDesktop : ContentPage
    {
        public ReportBuilderDesktop(string title)
        {
            InitializeComponent();

            Title = title;

        }

        private void collectionPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = collectionPicker.SelectedItem.ToString();
            
            switch (selectedItem)
            {
                case "All Birds":
                    conditionPicker.ItemsSource = new string[] { "Sponsored" };
                    break;
                case "Wild Birds":
                    conditionPicker.ItemsSource = new string[] { "Banded" };                       
                    break;
                case "Captive Birds":
                    conditionPicker.ItemsSource = new string[] { "Ingoing", "Outgoing", "Deceased" };
                    break;
                case "Members":
                    conditionPicker.ItemsSource = new string[] { "Joined" };
                    break;
                case "Volunteers":
                    conditionPicker.ItemsSource = new string[] { "Worked" };
                    break;
                default:
                    break;
            }
        }
    }
}