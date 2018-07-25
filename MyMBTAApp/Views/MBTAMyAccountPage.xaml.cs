using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyMBTAApp.Views
{
    public partial class MBTAMyAccountPage : ContentPage
    {
        public MBTAMyAccountPage()
        {
            InitializeComponent();
            RegisterButtonEvents();
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


        }
    }
}
