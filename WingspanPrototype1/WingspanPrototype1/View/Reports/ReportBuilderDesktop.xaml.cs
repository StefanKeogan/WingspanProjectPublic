using System;
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
    public partial class ReportBuilderDesktop : ContentPage
    {
        public ReportBuilderDesktop(string title)
        {
            InitializeComponent();

            Title = title;

            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    watermarkImage.Source = "Assets/watermark-logo.png";
                    break;
                case Device.Android:
                    watermarkImage.Source = "watermarklogo.png";
                    break;
                default:
                    watermarkImage.Source = "watermark-logo.png";
                    break;
            }

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
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                captiveBirdListView.IsVisible = false;
                                memberListView.IsVisible = false;
                                volunteerListView.IsVisible = false;                              

                            });                          

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
                                    watermarkLayout.IsVisible = false;
                                    wildBirdListView.IsVisible = true;
                                    wildBirdListView.ItemsSource = wildResults;
                                    subtotalLabel.Text = "Total Banded Birds";
                                    subtotalValue.Text = wildResults.Count.ToString();
                                    buildingIndicator.IsRunning = false;
                                });
                            }
                            break;
                        case "Captive Birds":

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                memberListView.IsVisible = false;
                                volunteerListView.IsVisible = false;
                                wildBirdListView.IsVisible = false;
                                
                            });

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
                                    watermarkLayout.IsVisible = false;
                                    captiveBirdListView.IsVisible = true;
                                    captiveBirdListView.ItemsSource = captiveResults;

                                    switch (conditionPicker.SelectedItem.ToString())
                                    {
                                        case "Ingoing":
                                            subtotalLabel.Text = "Total Ingoing Birds: ";
                                            break;
                                        case "Outgoing":
                                            subtotalLabel.Text = "Total Outgoing Birds: ";
                                            break;
                                        case "Deceased":
                                            subtotalLabel.Text = "Total Deceased Birds: ";
                                            break;
                                        default:
                                            captiveResults = null;
                                            break;

                                    }
                                    
                                    subtotalValue.Text = captiveResults.Count.ToString();

                                    buildingIndicator.IsRunning = false;

                                });
                            }
                            break;
                        case "Members":
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                captiveBirdListView.IsVisible = false;
                                volunteerListView.IsVisible = false;
                                wildBirdListView.IsVisible = false;
                                
                            });

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
                                    watermarkLayout.IsVisible = false;
                                    memberListView.IsVisible = true;
                                    memberListView.ItemsSource = memberResults;
                                    subtotalLabel.Text = "Members Joined: ";
                                    subtotalValue.Text = memberResults.Count.ToString();
                                    buildingIndicator.IsRunning = false;
                                });
                            }
                            break;
                        case "Volunteers":

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                captiveBirdListView.IsVisible = false;
                                memberListView.IsVisible = false;
                                wildBirdListView.IsVisible = false;
                            });

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
                                double subtotal = 0;

                                foreach (var hours in volunteerResults)
                                {
                                    subtotal = subtotal + hours.Amount;
                                }

                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    watermarkLayout.IsVisible = false;
                                    volunteerListView.IsVisible = true;
                                    volunteerListView.ItemsSource = volunteerResults;
                                    subtotalLabel.Text = "Total Wours Worked: ";
                                    subtotalValue.Text = subtotal.ToString();
                                    buildingIndicator.IsRunning = false;
                                    
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