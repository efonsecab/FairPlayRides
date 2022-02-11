using FairPlayRides.Blazor.Shared.GeoLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayRides.MAUI.AgnosticImplementations
{
    internal class GeoLocationProvider : IGeoLocationProvider
    {
        public async Task<GeoCoordinates> GetCurrentPositionAsync()
        {
            var currentGeoLocation = await
                Microsoft.Maui.Essentials.Geolocation.GetLocationAsync();
            return new GeoCoordinates 
            {
                Latitude = currentGeoLocation.Latitude,
                Longitude = currentGeoLocation.Longitude
            };
        }
    }
}
