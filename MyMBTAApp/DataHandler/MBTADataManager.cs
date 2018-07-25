using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyMBTAApp.DataHandler.Trips;
namespace MyMBTAApp.DataHandler
{
    public class MBTADataManager
    {
       
        //GetSchedulesByStop
		public async static Task<List<Trip>> GetSchedulesByStop(string stop, int Direction)
		{
			string url = Utilities.BaseURL+ @"schedules?filter[route]=CR-Newburyport" 
			                      + @"&filter[direction_id]=" + Direction.ToString();
			Direction.ToString();
			HttpClient client = new HttpClient();
            // REST API Call. Get Asyncnorously
			HttpResponseMessage response = await client.GetAsync(url);

			string contentString = await response.Content.ReadAsStringAsync();
			List<Trip> retval = new List<Trip>();

			try{
				JsonTextReader reader = new JsonTextReader(new StringReader(contentString));
				JsonSerializer jsonSerializer = new JsonSerializer();
				Schedules.Rootobject rootobject = (Schedules.Rootobject)(jsonSerializer.Deserialize(reader,typeof(Schedules.Rootobject)));
                
				foreach(Schedules.Datum d in rootobject.data)
				{
					if(d.relationships.stop.data.id==stop)
					{
						DateTime SuburbanStationTime = d.attributes.departure_time;
						DateTime CentralStationTime = FindTerminusByTrainNumber(d.relationships.trip.data.id, rootobject.data, Direction);
						string[] toks = d.relationships.trip.data.id.Split(new char[]{'-'});
						string TrainNumber = toks[toks.Length - 1];

						if(Direction ==0 ){
							retval.Add(new Trip(TrainNumber, SuburbanStationTime, CentralStationTime));
						}else{
							retval.Add(new Trip(TrainNumber, CentralStationTime, SuburbanStationTime));
						}
					}
				}
                   
			}catch(Exception ex){
				Console.WriteLine("Exception thrown when getting schedules");
				Console.WriteLine(ex);
			}
			return retval.OrderBy(x => x.TrainNumber).ToList();
			
		}
        //FindTerminusByTrainNumber
		private static DateTime FindTerminusByTrainNumber(string number, Schedules.Datum[] ds, int Direction)
		{
			foreach(Schedules.Datum d in ds)
			{
				//First try to match trip id
				if(d.relationships.trip.data.id == number)
				{
					// Second try to match stop id
					if(d.relationships.stop.data.id == "North Station")
					{
						if(Direction == 0)
						{
							return d.attributes.departure_time;
						}
						else{
							return d.attributes.arrival_time;
						}
					}
				}
			}
			return new DateTime();
		}



        public async static Task<List<string>> GetAlerts(string line)
        {

            string url = Utilities.BaseURL + "alerts?filter[route]=" + line;

            HttpClient client = new HttpClient();

            // Execute the REST API call.
            HttpResponseMessage response = await client.GetAsync(url);

            // Get the JSON response.
            string contentString = await response.Content.ReadAsStringAsync();

            // Instantiate return object 

            List<string> retval = new List<string>();

            try
            {
                JsonTextReader reader = new JsonTextReader(new StringReader(contentString));
                JsonSerializer serializer = new JsonSerializer();
                Alerts.Rootobject ro = (Alerts.Rootobject)serializer.Deserialize(reader, typeof(Alerts.Rootobject));


                foreach (Alerts.Datum d in ro.data)
                {
                    retval.Add(d.attributes.header);
                }


            }
            catch (Exception ex)
            {
                retval.Add("Exception: " + ex.ToString());
            }

            return retval;
        }
    }
}
