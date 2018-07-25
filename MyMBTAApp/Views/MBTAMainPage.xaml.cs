using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;
using System.Linq;
using System.IO;

using MyMBTAApp.DataHandler;

namespace MyMBTAApp.Views
{
    public partial class MBTAMainPage : ContentPage
    {
        public MBTAMainPage()
        {
            InitializeComponent();
            RegisterButtonEvents();
            renderAlerts();
        }
        void Button_Discover_Clicked(object sender, EventArgs args)
        {
            DisplayAlert("Discover", "Button Clicked!", "OK");
        }

        void Button_Transfer_Clicked(object sender, EventArgs args)
        {
            DisplayAlert("Transfer", "Button Clicked!", "OK");
        }

        void Button_MyAccount_Clicked(object sender, EventArgs args)
        {
            DisplayAlert("My Account", "Button Clicked!", "OK");
        }

        void Button_MyOrder_Clicked(object sender,EventArgs args){
            DisplayAlert("My Order", "Button Clicked!", "OK");
        }

        void Button_TravelGuide_Clicked(object sender, EventArgs args)
        {
            DisplayAlert("Travel Guide", "Button Clicked!", "OK");
        }

        void Button_Schedule_Clicked(object sender, EventArgs args)
        {
            DisplayAlert("Schedule", "Button Clicked!", "OK");
        }
        void Button_BuyTickets_Clicked(object sender, EventArgs args)
        {
            DisplayAlert("Buy Tickets", "Button Clicked!", "OK");
        }

        void Button_ChangeAds_Clicked(object sender, EventArgs args)
        {
            DisplayAlert("Change Advertisement", "Button Clicked", "OK");
        }

        void RegisterButtonEvents()
        {
            TapGestureRecognizer discoverRec = new TapGestureRecognizer();
            discoverRec.Tapped += Button_Discover_Clicked;
            Discover_image.GestureRecognizers.Add(discoverRec);
            Discover_text.GestureRecognizers.Add(discoverRec);

            TapGestureRecognizer transferRec = new TapGestureRecognizer();
            transferRec.Tapped += Button_Transfer_Clicked;
            Transfer_text.GestureRecognizers.Add(transferRec);
            Transfer_image.GestureRecognizers.Add(transferRec);

            TapGestureRecognizer myAccountRec = new TapGestureRecognizer();
            myAccountRec.Tapped += Button_MyAccount_Clicked;
            MyAccount_text.GestureRecognizers.Add(myAccountRec);
            MyAccount_image.GestureRecognizers.Add(myAccountRec);

            TapGestureRecognizer myOrderRec = new TapGestureRecognizer();
            myOrderRec.Tapped += Button_MyOrder_Clicked;
            MyOrder_Text.GestureRecognizers.Add(myOrderRec);
            MyOrder_Image.GestureRecognizers.Add(myOrderRec);

            TapGestureRecognizer travelGuideRec = new TapGestureRecognizer();
            travelGuideRec.Tapped += Button_TravelGuide_Clicked;
            TravelGuide_Text.GestureRecognizers.Add(travelGuideRec);
            TravelGuide_Image.GestureRecognizers.Add(travelGuideRec);

            TapGestureRecognizer scheduleRec = new TapGestureRecognizer();
            scheduleRec.Tapped += Button_Schedule_Clicked;
            Schedule_Image.GestureRecognizers.Add(scheduleRec);
            Schedule_Text.GestureRecognizers.Add(scheduleRec);

            TapGestureRecognizer buyTicketsRec = new TapGestureRecognizer();
            buyTicketsRec.Tapped += Button_BuyTickets_Clicked;
            BuyTickets_Text.GestureRecognizers.Add(buyTicketsRec);
            BuyTickets_Image.GestureRecognizers.Add(buyTicketsRec);

            TapGestureRecognizer changeADRec = new TapGestureRecognizer();
            changeADRec.Tapped += Button_ChangeAds_Clicked;
            ChangeADs_image.GestureRecognizers.Add(changeADRec);
                          
        }



        async void renderAlerts()
        {
            List<string> alerts = await MBTADataManager.GetAlerts("CR-Newburyport");
            if(alerts.Count==0){
                Label noAlerts = new Label
                {
                    Text = "No Alerts",
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    VerticalTextAlignment=TextAlignment.Center,
                    HorizontalTextAlignment=TextAlignment.Center
                };
                Alerts_ScrollView.Content = noAlerts;
                return;
            }
            StackLayout alertStack = new StackLayout();
            string titleText= $"Alerts({alerts.Count})";
            Label Title = new Label
            {
                Text = titleText,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold
            };
            alertStack.Children.Add(Title);
            foreach(string alertText in alerts){
                Label alertLabel = new Label
                {
                    Text = alertText,
                    FontSize = 12
                };
                Frame frame = new Frame
                {
                    Padding = 5,
                    BorderColor = Xamarin.Forms.Color.FromHex("#f1c40f"),
                    HasShadow = false
                };
                frame.Content = alertLabel;
                alertStack.Children.Add(frame);
            }
            Alerts_ScrollView.Content = alertStack;
        }


    }
}
