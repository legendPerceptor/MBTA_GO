using System;
using Xamarin.Forms;
using MyMBTAApp.Views;
using Xamarin.Forms.Xaml;
using MyMBTAApp.DataHandler.Trips;
using MyMBTAApp.DataHandler;
using System.Collections.Generic;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyMBTAApp
{
	public partial class App : Application
	{
		MyMBTAApp.Views.MainPage LoginPage;
		static string databaseLocation;
		public static string dbLocation
		{
			get{return databaseLocation;}
		}
        public App()
        {
            InitializeComponent();

			LoginPage = new MainPage();
			MainPage = new NavigationPage(LoginPage);
        }
		public App(string dbloc){
			InitializeComponent();

            LoginPage = new MainPage();
            MainPage = new NavigationPage(LoginPage);
			databaseLocation = dbloc;
		}
		public static List<Trip> InboundSchedule { get; private set; }
        public static List<Trip> OutboundSchedule { get; private set; }

        private const string APIKey = "yIpbnwxjk0ahNtoE2EkXsA";
#if false
        private static CommuterRailStop _InboundStop, _OutboundStop;
        public static CommuterRailStop InboundStop
        {
            set
            {
                _InboundStop = value;
             //   Settings.InboundStationName = InboundStop.StopName;
             //   Settings.RailLineName = InboundStop.LineName;
            }

            get
            {
                return _InboundStop;
            }
        }
        public static CommuterRailStop OutboundStop
        {
            set
            {
                _OutboundStop = value;
        //        Settings.OutboundStationName = OutboundStop.StopName;
       //         Settings.RailLineName = OutboundStop.LineName;
            }
            get
            {
                return _OutboundStop;
            }
        }

#endif
        protected override void OnStart()
        {
            // Initialize line and stop data

            //    InitializeLinesAndStops();

            // Get settings from previous config

            string InboundStationName = "Ipswich"; // Settings.InboundStationName;
            string OutboundStationName = "North Station"; // Settings.OutboundStationName;
            string RailLineName = "CR-Newburyport"; //Settings.RailLineName;

            //     App.InboundStop = new CommuterRailStop(InboundStationName, RailLineName);
            //     App.OutboundStop = new CommuterRailStop(OutboundStationName, RailLineName);

            // Get alerts and schedules

            //RefreshAlertList();
            RefreshSchedules(DirectionEnum.Both);
            //      RefreshStops();


            // Set timer for updating of time remaining
            // until next train

            Device.StartTimer(new TimeSpan(0, 1, 0), TimerCallback);
        }

        public bool TimerCallback()
        {
            RefreshTimeToNextTrain();

            return true;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            RefreshAlertList();
            RefreshSchedules(DirectionEnum.Both);
            //    RefreshStops();

        }
        public async void RefreshAlertList()
        {
			//Utilities.AlertList = await MyMBTAApp.DataHandler.GetAlerts("CR-Newburyport");

            //((MainPage)this.MainPage).RefreshAlerts();
        }

        public async void RefreshSchedules(DirectionEnum dir)
        {

			App.OutboundSchedule = await DataHandler.MBTADataManager.GetSchedulesByStop("Ipswich", (int)DirectionEnum.Outbound);
			App.InboundSchedule = await DataHandler.MBTADataManager.GetSchedulesByStop("Ipswich", (int)DirectionEnum.Inbound);

			// Refresh anyone who is looking at the schedules
			LoginPage.RefereshSchedulePage(dir);
            
        }


        protected void RefreshTimeToNextTrain()
        {
           // ((MainPage)this.MainPage).RefreshTimeToNextTrain();
        }
    }
}
