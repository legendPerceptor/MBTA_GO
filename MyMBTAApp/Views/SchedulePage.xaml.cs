using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using MyMBTAApp.DataHandler;
using MyMBTAApp.DataHandler.Trips;

namespace MyMBTAApp.Views
{
    public partial class SchedulePage : ContentPage
    {
        public SchedulePage()
        {
            InitializeComponent();
        }
		int _direction;
        public int Direction
        {
            set
            {
                _direction = value;
                if (Direction == 0)
                {
                    this.orgLabel.Text = "North Station";
                    this.destLabel.Text = "Ipswich";
                }
                else
                {
                    this.orgLabel.Text = "Ipswich";
                    this.destLabel.Text = "North Station";
                }
            }
            get { return _direction; }
        }

        public void RefreshAlerts()
        {
            // Get alerts from global area. Create a label
            // for each one, and place it in the stack that
            // holds them

            //this.AlertsLabel.Text =
            //    "Alerts (" + Utilities.AlertList.Count.ToString() + ")";

            //this.AlertStack.Children.Clear();

            //foreach (string s in Utilities.AlertList)
            //{
            //    Label l = new Label();
            //    l.Text = s;
            //    l.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));

            //    this.AlertStack.Children.Add(l);
            //}
        }

        private Trip NextDeparture = null;

        public void RefreshSchedules(DirectionEnum dir)
        {
            if ((int)dir == this.Direction ||
                dir == DirectionEnum.Both)
            {
                // Clear stack holding train lines

                this.TrainStack.Children.Clear();
                this.NextDeparture = null;

                // Get the schedules for the origin stop
                // and destination stop in the desired directions

                List<Trip> MyTrains;

                if (this.Direction == (int)DirectionEnum.Outbound)
                {
                    MyTrains = App.OutboundSchedule;
                }
                else
                {
                    MyTrains = App.InboundSchedule;
                }

                bool PrevDepartureWasPM = false;

                // For each trip in the direction that you are going

                foreach (Trip tr in MyTrains)
                {
                    // Check t see if train has already departed.
                    // If it hasn't, then add it to the schedule grid


                    if (tr.Departure > DateTime.Now)
                    {
                        // Train number -- already a string

                        // Departure time

                        string DepartureTime = tr.Departure.ToString("hh:mm tt");

                        // Arrival time

                        string ArrivalTime = tr.Arrival.ToString("hh:mm tt");

                        string LabelText = "     " + tr.TrainNumber + "     |     " +
                                 DepartureTime + "     |     " +
                                 ArrivalTime;

                        AddAlternatingLabelToGrid(LabelText);

                        // Remember the first entry as the next train to depart

                        if (this.NextDeparture == null)
                        {
                            this.NextDeparture = tr;

                            RefreshTimeToNextTrain();
                        }
                    }
                }
            }
        }


        int LineColorIndicator = 0;

        private void AddAlternatingLabelToGrid(string text)
        {
            Label l = new Label();

            l.Text = text;

            l.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));

            if (LineColorIndicator == 0)
            {
                l.BackgroundColor = Color.White;
                LineColorIndicator = 1;
            }
            else
            {
                l.BackgroundColor = Color.Silver;
                LineColorIndicator = 0;
            }

            this.TrainStack.Children.Add(l);
        }

        public void RefreshTimeToNextTrain()
        {
            this.minsLabel.Text = "W:" + this.Width.ToString()
                + " H:" + this.Height;
            /*
            TimeSpan ts = NextDeparture.Departure.Subtract(DateTime.Now);

            int RemainingMinutes = Convert.ToInt16(ts.TotalMinutes);

            // Check to see if this train has already departed
            // If it has, then refresh the schedule

            if (RemainingMinutes < 1)
            {
                ((App)(App.Current)).RefreshSchedules(((DirectionEnum)this.Direction));
            }
            // Otherwise just updated time remaining
            else
            {
                this.minsLabel.Text = RemainingMinutes.ToString("d");
            }
            */
        }

    }
}
