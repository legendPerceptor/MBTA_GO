using System;
namespace MyMBTAApp.DataHandler.Schedules
{
	public class Rootobject
    {
        public Datum[] data { get; set; }
        public Jsonapi jsonapi { get; set; }
    }

    public class Jsonapi
    {
        public string version { get; set; }
    }

    public class Datum
    {
        public Attributes attributes { get; set; }
        public string id { get; set; }
        public Relationships relationships { get; set; }
        public string type { get; set; }
    }

    public class Attributes
    {
        public DateTime arrival_time { get; set; }
        public DateTime departure_time { get; set; }
        public int drop_off_type { get; set; }
        public int pickup_type { get; set; }
        public int stop_sequence { get; set; }
        public bool timepoint { get; set; }
    }

    public class Relationships
    {
        public Prediction prediction { get; set; }
        public Route route { get; set; }
        public Stop stop { get; set; }
        public Trip trip { get; set; }
    }

    public class Prediction
    {
    }

    public class Route
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public string id { get; set; }
        public string type { get; set; }
    }

    public class Stop
    {
        public Data1 data { get; set; }
    }

    public class Data1
    {
        public string id { get; set; }
        public string type { get; set; }
    }

    public class Trip
    {
        public Data2 data { get; set; }
    }

    public class Data2
    {
        public string id { get; set; }
        public string type { get; set; }
    }

}
