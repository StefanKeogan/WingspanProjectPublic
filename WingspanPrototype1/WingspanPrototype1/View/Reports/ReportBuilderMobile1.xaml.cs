using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Controller.Birds;
using WingspanPrototype1.Controller.Members;
using WingspanPrototype1.Controller.Sponsorships;
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
                    case "Sponsorships":
                        conditionPicker.ItemsSource = new string[] { "Started" };
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

                            List<CaptiveBird> allCaptiveResults = null;
                            List<WildBird> allWildResults = null;
                            ArrayList allResults = new ArrayList();

                            switch (conditionPicker.SelectedItem.ToString())
                            {
                                case "Sponsored":
                                    allWildResults = WildReport.SponsoredReport(fromDatePicker.Date, toDatePicker.Date);
                                    allCaptiveResults = CaptiveReport.SponsoredReport(fromDatePicker.Date, toDatePicker.Date);
                                    break;
                                default:
                                    break;
                            }

                            if (allCaptiveResults != null)
                            {
                                if (allCaptiveResults.Count > 0)
                                {
                                    foreach (var result in allCaptiveResults)
                                    {
                                        allResults.Add(result);
                                    }
                                }
                            }


                            if (allWildResults != null)
                            {
                                if (allWildResults.Count > 0)
                                {
                                    foreach (var result in allWildResults)
                                    {
                                        allResults.Add(result);
                                    }
                                }
                            }

                            if (allResults.Count > 0)
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    collectionPicker.SelectedIndex = -1;
                                    conditionPicker.SelectedIndex = -1;
                                    toDatePicker.Date = DateTime.Today;
                                    fromDatePicker.Date = DateTime.Today;

                                    Navigation.PushAsync(new ReportBuilderMobile2(new ArrayList(allResults)));
                                    buildingIndicator.IsRunning = false;
                                });
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DisplayAlert("No Birds Found", "No birds met those conditions", "OK");
                                    buildingIndicator.IsRunning = false;
                                });
                            }

                            break;
                        case "Wild Birds":

                            List<WildBird> wildResults = null;

                            switch (conditionPicker.SelectedItem.ToString())
                            {
                                case "Banded":
                                    wildResults = WildReport.BandedReport(fromDatePicker.Date, toDatePicker.Date);
                                    break;
                                case "Sponsored":
                                    wildResults = WildReport.SponsoredReport(fromDatePicker.Date, toDatePicker.Date);
                                    break;
                                default:
                                    break;
                            }

                            if (wildResults != null)
                            {
                                if (wildResults.Count > 0)
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        collectionPicker.SelectedIndex = -1;
                                        conditionPicker.SelectedIndex = -1;
                                        toDatePicker.Date = DateTime.Today;
                                        fromDatePicker.Date = DateTime.Today;

                                        Navigation.PushAsync(new ReportBuilderMobile2(new ArrayList(wildResults)));
                                        buildingIndicator.IsRunning = false;
                                    });
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        DisplayAlert("No Birds Found", "No birds met those conditions", "OK");
                                        buildingIndicator.IsRunning = false;
                                    });
                                }

                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DisplayAlert("No Birds Found", "No birds met those conditions", "OK");
                                    buildingIndicator.IsRunning = false;
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
                                case "Sponsored":
                                    captiveResults = CaptiveReport.SponsoredReport(fromDatePicker.Date, toDatePicker.Date);
                                    break;
                                default:
                                    captiveResults = null;
                                    break;

                            }
                            if (captiveResults != null)
                            {
                                if (captiveResults.Count > 0)
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        collectionPicker.SelectedIndex = -1;
                                        conditionPicker.SelectedIndex = -1;
                                        toDatePicker.Date = DateTime.Today;
                                        fromDatePicker.Date = DateTime.Today;

                                        Navigation.PushAsync(new ReportBuilderMobile2(new ArrayList(captiveResults)));

                                        buildingIndicator.IsRunning = false;

                                    });
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        DisplayAlert("No Birds Found", "No birds met those conditions", "OK");
                                        buildingIndicator.IsRunning = false;
                                    });
                                }

                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DisplayAlert("No Birds Found", "No birds met those conditions", "OK");
                                    buildingIndicator.IsRunning = false;
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
                                case "Sponsoring":
                                    memberResults = SponsorshipReport.MembersSponsoring(fromDatePicker.Date, toDatePicker.Date);
                                    break;
                                default:
                                    memberResults = null;
                                    break;
                            }

                            if (memberResults != null)
                            {
                                if (memberResults.Count > 0)
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        collectionPicker.SelectedIndex = -1;
                                        conditionPicker.SelectedIndex = -1;
                                        toDatePicker.Date = DateTime.Today;
                                        fromDatePicker.Date = DateTime.Today;

                                        Navigation.PushAsync(new ReportBuilderMobile2(new ArrayList(memberResults)));
                                        buildingIndicator.IsRunning = false;
                                    });
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        DisplayAlert("No Members Found", "No members met those conditions", "OK");
                                        buildingIndicator.IsRunning = false;
                                    });
                                }

                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DisplayAlert("No Members Found", "No members met those conditions", "OK");
                                    buildingIndicator.IsRunning = false;
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
                                if (volunteerResults.Count > 0)
                                {
                                    double subtotal = 0;

                                    foreach (var hours in volunteerResults)
                                    {
                                        subtotal = subtotal + hours.Amount;
                                    }

                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        collectionPicker.SelectedIndex = -1;
                                        conditionPicker.SelectedIndex = -1;
                                        toDatePicker.Date = DateTime.Today;
                                        fromDatePicker.Date = DateTime.Today;

                                        Navigation.PushAsync(new ReportBuilderMobile2(new ArrayList(volunteerResults)));
                                        buildingIndicator.IsRunning = false;

                                    });
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        DisplayAlert("No Volunteers Found", "No volunteers met those conditions", "OK");
                                        buildingIndicator.IsRunning = false;
                                    });
                                }
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DisplayAlert("No Volunteers Found", "No volunteers met those conditions", "OK");
                                    buildingIndicator.IsRunning = false;
                                });
                            }
                            break;
                        case "Sponsorships":

                            List<Sponsorship> sponsorshipResults = null;

                            switch (conditionPicker.SelectedItem.ToString())
                            {
                                case "Started":
                                    sponsorshipResults = SponsorshipReport.StartedReport(fromDatePicker.Date, toDatePicker.Date);
                                    break;
                                default:
                                    volunteerResults = null;
                                    break;
                            }

                            if (sponsorshipResults != null)
                            {
                                if (sponsorshipResults.Count > 0)
                                {
                                    double subtotal = sponsorshipResults.Count;

                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        collectionPicker.SelectedIndex = -1;
                                        conditionPicker.SelectedIndex = -1;
                                        toDatePicker.Date = DateTime.Today;
                                        fromDatePicker.Date = DateTime.Today;

                                        Navigation.PushAsync(new ReportBuilderMobile2(new ArrayList(sponsorshipResults)));
                                        buildingIndicator.IsRunning = false;

                                    });
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        DisplayAlert("No Sponsorships Found", "No sponsorships met those conditions", "OK");
                                        buildingIndicator.IsRunning = false;
                                    });
                                }

                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DisplayAlert("No Sponsorships Found", "No sponsordhips met those conditions", "OK");
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