using MongoDB.Bson;
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
    public partial class ResultsDesktop : ContentPage
    {
        public ResultsDesktop(List<BsonDocument> results)
        {
            InitializeComponent();

            ResultsListView.ItemsSource = results;



        }
    }
}