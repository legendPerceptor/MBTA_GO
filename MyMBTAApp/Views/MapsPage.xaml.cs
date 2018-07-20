using System;
using System.Collections.Generic;

using Xamarin.Forms;

using Xamarin.Essentials;
namespace MyMBTAApp.Views
{
    public partial class MapsPage : ContentPage
    {
		Location lastLocation = null;
        public MapsPage()
        {
            InitializeComponent();
        }
		protected async override void OnAppearing(){
			base.OnAppearing();
			//var location = await Geolocation.GetLastKnownLocationAsync();
           
			try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);
				if (lastLocation is null) lastLocation = location;
                else if (location.CalculateDistance(lastLocation, DistanceUnits.Miles) > 100)
                {
                    lastLocation = location;
					this.locationMap.MoveToRegion(new Xamarin.Forms.Maps.MapSpan(
                        new Xamarin.Forms.Maps.Position(location.Latitude, location.Longitude), 2, 2));
                }
                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
		}
    }
}
