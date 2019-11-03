using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Controller.Birds;
using WingspanPrototype1.Controller.Members;
using WingspanPrototype1.Controller.Volunteers;
using WingspanPrototype1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View.Reports
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportBuilderMobile1 : ContentPage
    {
        public ReportBuilderMobile1(string title)
        {
            InitializeComponent();

            Title = title;
        }

        private void collectionPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (collectionPicker.SelectedIndex != -1)
            {
                string selectedItem = collectionPicker.SelectedItem.ToString();

                switch (selectedItem)
                {
                    case "All Birds":
                        conditionPicker.ItemsSource = new string[] { "Sponsored" };
                        break;
                    case "Wild Birds":
                        conditionPicker.ItemsSource = new string[] { "Banded", "Sponsored" };
                        break;
                    case "Captive Birds":
                        conditionPicker.ItemsSource = new string[] { "Ingoing", "Outgoing", "Deceased", "Sponsored" };
                        break;
                    case "Members":
                        conditionPicker.ItemsSource = new string[] { "Joined", "Sponsoring" };
                        break;
                    case "Volunteers":
                        conditionPicker.ItemsSource = new string[] { "Worked" };
                        break;
                    default:
                        break;
                }
            }
            
        }


        private void buildReportButton_Clicked(object sender, EventArgs e)
        {
            if ((collectionPicker.SelectedItem != null) && (conditionPicker.SelectedItem != null))
            {
                buildingIndicator.IsRunning = true;

                Task.Run(() =>
                {
                    switch (collectionPicker.SelectedItem.ToString())
                    {
                        case "All Birds":
                            break;
                        case "Wild Birds":

                            List<WildBird> wildResults = null;

                            switch (conditionPicker.SelectedItem.ToString())
                            {
                                case "Banded":
                                    wildResults = WildReport.BandedReport(fromDatePicker.Date, toDatePicker.Date);
                                    break;
                                case "Sponsored":
                                default:
                                    break;
                            }

                            if ((wildResults != null) && (wildResults.Count > 0))
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    collectionPicker.SelectedIndex = -1;
                                    conditionPicker.SelectedIndex = -1;
                                    toDatePicker.Date = DateTime.Today;
                                    fromDatePicker.Date = DateTime.Today;

                                    buildingIndicator.IsRunning = false;
                                    Navigation.PushAsync(new ReportBuilderMobile2(new ArrayList(wildResults)));
                                });

                                
                            }
                            break;
                        case "Captive Birds":

                            List<CaptiveBird> captiveResults = null;

                            switch (conditionPicker.SelectedItem.ToString())
                            {
                                case "Ingoing":
                                    captiveResults = CaptiveReport.IngoingOutgoingReport("Ingoing", fromDatePicker.Date, toDatePicker.Date);
                                    break;
                                case "Outgoing":
                                    captiveResults = CaptiveReport.IngoingOutgoingReport("Outgoing", fromDatePicker.Date, toDatePicker.Date);
                                    break;
                                case "Deceased":
                                    captiveResults = CaptiveReport.IngoingOutgoingReport("Deceased", fromDatePicker.Date, toDatePicker.Date);
                                    break;
                                default:
                                    captiveResults = null;
                                    break;

                            }
                            if ((captiveResults != null) && (captiveResults.Count > 0))
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    collectionPicker.SelectedIndex = -1;
                                    conditionPicker.SelectedIndex = -1;
                                    toDatePicker.Date = DateTime.Today;
                                    fromDatePicker.Date = DateTime.Today;

                                    buildingIndicator.IsRunning = false;
                                    Navigation.PushAsync(new ReportBuilderMobile2(new ArrayList(captiveResults)));
                                });                               
                            }
                            break;
                        case "Members":

                            List<Member> memberResults = null;

                            switch (conditionPicker.SelectedItem.ToString())
                            {
                                case "Joined":
                                    memberResults = MemberReport.JoinedReport(fromDatePicker.Date, toDatePicker.Date);
                                    break;
                                default:
                                    memberResults = null;
                                    break;
                            }

                            if ((memberResults != null) && (memberResults.Count > 0))
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    collectionPicker.SelectedIndex = -1;
                                    conditionPicker.SelectedIndex = -1;
                                    toDatePicker.Date = DateTime.Today;
                                    fromDatePicker.Date = DateTime.Today;

                                    buildingIndicator.IsRunning = false;
                                    Navigation.PushAsync(new ReportBuilderMobile2(new ArrayList(memberResults)));
                                });
                                
                            }
                            break;
                        case "Volunteers":

                            List<VolunteerHours> volunteerResults = null;

                            switch (conditionPicker.SelectedItem.ToString())
                            {
                                case "Worked":
                                    volunteerResults = VolunteerReport.WorkedReport(fromDatePicker.Date, toDatePicker.Date);
                                    break;
                                default:
                                    volunteerResults = null;
                                    break;
                            }

                            if (volunteerResults != null)
                            {

                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    collectionPicker.SelectedIndex = -1;
                                    conditionPicker.SelectedIndex = -1;
                                    toDatePicker.Date = DateTime.Today;
                                    fromDatePicker.Date = DateTime.Today;

                                    buildingIndicator.IsRunning = false;
                                    Navigation.PushAsync(new ReportBuilderMobile2(new ArrayList(volunteerResults)));
                                });
                              
                            }
                            break;
                        default:
                            break;
                    }
                });

            }
        }

    }
}