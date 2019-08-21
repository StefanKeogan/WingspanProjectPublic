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
    public partial class DetailPage : ContentPage
    {
        public DetailPage(string title)
        {
            InitializeComponent();

            Title = title;

            List<SearchFeild> searchFeilds = new List<SearchFeild>();

            switch (Device.RuntimePlatform)
            {
                case Device.Android: 
                default:
                    break;
            }

            switch (title)
            {
                case "Birds":
                    BoxTitle.Text = "Find Bird";
                    searchFeilds.Add(new SearchFeild { FeildName = "NameInput", LabelName = "Name", LabelText = "Name: " });
                    searchFeilds.Add(new SearchFeild { FeildName = "IDInput", LabelName = "ID", LabelText = "ID Number: " });
                    searchFeilds.Add(new SearchFeild { FeildName = "BreedInput", LabelName = "Breed", LabelText = "Breed: " });
                    break;
                default:
                    break;
            }

            SearchFeildsList.ItemsSource = searchFeilds;

        }
    }
}