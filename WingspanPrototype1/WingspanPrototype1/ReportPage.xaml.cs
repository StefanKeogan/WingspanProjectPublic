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
    public partial class ReportPage : ContentPage
    {
        // Currently blank, will use to display report builder in future sprints 
        public ReportPage(string title)
        {
            InitializeComponent();

            Title = title;
        }
    }
}