using System;
namespace MyMBTAApp.DataHandler.Trips
{
	public class Trip
    {
        public Trip(string TrainNumber, DateTime arr, DateTime dep)
        {
            this.TrainNumber = TrainNumber;
            this.Arrival = arr;
            this.Departure = dep;
        }

        public string TrainNumber { set; get; }
        public DateTime Arrival { set; get; }
        public DateTime Departure { set; get; }
    }
}
