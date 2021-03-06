﻿using System;
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
                case "Payments":
                    conditionPicker.ItemsSource = new string[] {"Made"};
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

        private void buildReportButton_Clicked(object sender, EventArgs e)
        {
            if ((collectionPicker.SelectedItem != null) && (conditionPicker.SelectedItem != null))
            {
                buildingIndicator.IsRunning = true;

                buildReportButton.IsEnabled = false;

                Task.Run(() =>
                {
                    switch (collectionPicker.SelectedItem.ToString())
                    {
                        case "All Birds":
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                captiveBirdListView.IsVisible = false;
                                wildBirdListView.IsVisible = false;
                                sponsorshipListView.IsVisible = false;
                                memberListView.IsVisible = false;
                                volunteerListView.IsVisible = false;
                                paymentListView.IsVisible = false;
                            });

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
                                    watermarkLayout.IsVisible = false;
                                    allBirdListView.IsVisible = true;
                                    allBirdListView.ItemsSource = allResults;
                                    subtotalLabel.Text = "Total Sponsored Birds";
                                    subtotalValue.Text = allResults.Count.ToString();
                                    buildingIndicator.IsRunning = false;
                                    buildReportButton.IsEnabled = true;
                                });
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DisplayAlert("No Birds Found", "No birds met those conditions", "OK");
                                    buildingIndicator.IsRunning = false;
                                    buildReportButton.IsEnabled = true;
                                });
                            }

                            break;
                        case "Wild Birds":
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                captiveBirdListView.IsVisible = false;
                                memberListView.IsVisible = false;
                                volunteerListView.IsVisible = false;                              
                                allBirdListView.IsVisible = false;                              
                                sponsorshipListView.IsVisible = false;
                                paymentListView.IsVisible = false;
                            });                          

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
                                        watermarkLayout.IsVisible = false;
                                        wildBirdListView.IsVisible = true;
                                        wildBirdListView.ItemsSource = wildResults;
                                        subtotalLabel.Text = "Total Birds Found";
                                        subtotalValue.Text = wildResults.Count.ToString();
                                        buildingIndicator.IsRunning = false;
                                        buildReportButton.IsEnabled = true;

                                    });
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        DisplayAlert("No Birds Found", "No birds met those conditions", "OK");
                                        buildingIndicator.IsRunning = false;
                                        buildReportButton.IsEnabled = true;
                                    });
                                }
                                
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DisplayAlert("No Birds Found", "No birds met those conditions", "OK");
                                    buildingIndicator.IsRunning = false;
                                    buildReportButton.IsEnabled = true;
                                });
                            }
                            break;
                        case "Captive Birds":

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                memberListView.IsVisible = false;
                                volunteerListView.IsVisible = false;
                                wildBirdListView.IsVisible = false;
                                allBirdListView.IsVisible = false;
                                sponsorshipListView.IsVisible = false;
                                paymentListView.IsVisible = false;
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
                                            case "Sponsored":
                                                subtotalLabel.Text = "Total Sponsored Birds: ";
                                                break;
                                            default:
                                                captiveResults = null;
                                                break;

                                        }

                                        subtotalValue.Text = captiveResults.Count.ToString();
                                        buildingIndicator.IsRunning = false;
                                        buildReportButton.IsEnabled = true;

                                    });
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        DisplayAlert("No Birds Found", "No birds met those conditions", "OK");
                                        buildingIndicator.IsRunning = false;
                                        buildReportButton.IsEnabled = true;
                                    });
                                }
                                
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DisplayAlert("No Birds Found", "No birds met those conditions", "OK");
                                    buildingIndicator.IsRunning = false;
                                    buildReportButton.IsEnabled = true;
                                });
                            }
                            break;
                        case "Members":
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                captiveBirdListView.IsVisible = false;
                                volunteerListView.IsVisible = false;
                                wildBirdListView.IsVisible = false;
                                allBirdListView.IsVisible = false;
                                sponsorshipListView.IsVisible = false;
                                paymentListView.IsVisible = false;
                            });

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
                                        watermarkLayout.IsVisible = false;
                                        memberListView.IsVisible = true;
                                        memberListView.ItemsSource = memberResults;
                                        subtotalLabel.Text = "Members Found: ";
                                        subtotalValue.Text = memberResults.Count.ToString();
                                        buildingIndicator.IsRunning = false;
                                        buildReportButton.IsEnabled = true;
                                    });
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        DisplayAlert("No Members Found", "No members met those conditions", "OK");
                                        buildingIndicator.IsRunning = false;
                                        buildReportButton.IsEnabled = true;
                                    });
                                }
                                
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DisplayAlert("No Members Found", "No members met those conditions", "OK");
                                    buildingIndicator.IsRunning = false;
                                    buildReportButton.IsEnabled = true;
                                });
                            }
                            break;
                        case "Payments":
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                captiveBirdListView.IsVisible = false;
                                volunteerListView.IsVisible = false;
                                wildBirdListView.IsVisible = false;
                                allBirdListView.IsVisible = false;
                                sponsorshipListView.IsVisible = false;
                                memberListView.IsVisible = false;

                            });

                            List<Payment> paymentResults = null;

                            switch (conditionPicker.SelectedItem.ToString())
                            {
                                case "Made":
                                    paymentResults = MemberReport.PaymentReport(fromDatePicker.Date, toDatePicker.Date);
                                    break;
                                default:
                                    memberResults = null;
                                    break;
                            }

                            if (paymentResults != null)
                            {
                                if (paymentResults.Count > 0)
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        watermarkLayout.IsVisible = false;
                                        paymentListView.IsVisible = true;
                                        paymentListView.ItemsSource = paymentResults;
                                        subtotalLabel.Text = "Subtotal: ";

                                        double subtotal = 0;
                                        foreach (var payment in paymentResults)
                                        {
                                            subtotal = subtotal + payment.Amount;
                                        }

                                        subtotalValue.Text = subtotal.ToString("0.00");
                                        buildingIndicator.IsRunning = false;
                                        buildReportButton.IsEnabled = true;
                                    });
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        DisplayAlert("No Payments Found", "No payments met those conditions", "OK");
                                        buildingIndicator.IsRunning = false;
                                        buildReportButton.IsEnabled = true;
                                    });
                                }

                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DisplayAlert("No Payments Found", "No payments met those conditions", "OK");
                                    buildingIndicator.IsRunning = false;
                                    buildReportButton.IsEnabled = true;
                                });
                            }
                            break;

                        case "Volunteers":

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                captiveBirdListView.IsVisible = false;
                                memberListView.IsVisible = false;
                                wildBirdListView.IsVisible = false;
                                allBirdListView.IsVisible = false;
                                sponsorshipListView.IsVisible = false;
                                paymentListView.IsVisible = false;
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
                                if (volunteerResults.Count > 0)
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
                                        buildReportButton.IsEnabled = true;

                                    });
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        DisplayAlert("No Volunteers Found", "No volunteers met those conditions", "OK");
                                        buildingIndicator.IsRunning = false;
                                        buildReportButton.IsEnabled = true;
                                    });
                                }
                            }                                                    
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DisplayAlert("No Volunteers Found", "No volunteers met those conditions", "OK");
                                    buildingIndicator.IsRunning = false;
                                    buildReportButton.IsEnabled = true;
                                });
                            }
                            break;
                        case "Sponsorships":
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                captiveBirdListView.IsVisible = false;
                                memberListView.IsVisible = false;
                                wildBirdListView.IsVisible = false;
                                volunteerListView.IsVisible = false;
                                allBirdListView.IsVisible = false;
                                paymentListView.IsVisible = false;
                            });

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
                                        watermarkLayout.IsVisible = false;
                                        sponsorshipListView.IsVisible = true;
                                        sponsorshipListView.ItemsSource = sponsorshipResults;
                                        subtotalLabel.Text = "Total Sponsorhips Started: ";
                                        subtotalValue.Text = subtotal.ToString();
                                        buildingIndicator.IsRunning = false;
                                        buildReportButton.IsEnabled = true;
                                    });
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        DisplayAlert("No Sponsorships Found", "No sponsorships met those conditions", "OK");
                                        buildingIndicator.IsRunning = false;
                                        buildReportButton.IsEnabled = true;
                                    });
                                }
                                
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DisplayAlert("No Sponsorships Found", "No sponsordhips met those conditions", "OK");
                                    buildingIndicator.IsRunning = false;
                                    buildReportButton.IsEnabled = true;
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