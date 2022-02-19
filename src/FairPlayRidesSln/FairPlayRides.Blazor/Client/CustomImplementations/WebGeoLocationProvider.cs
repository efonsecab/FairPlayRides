using BrowserInterop.Extensions;
using FairPlayRides.Blazor.Shared.GeoLocation;
using Microsoft.JSInterop;

namespace FairPlayRides.Blazor.Client.CustomImplementations
{
    public class WebGeoLocationProvider : IGeoLocationProvider
    {
        private readonly IJSRuntime JSRuntime;

        public WebGeoLocationProvider(IJSRuntime jsRuntime)
        {
            this.JSRuntime = jsRuntime;
        }
        public async Task<GeoCoordinates> GetCurrentPositionAsync()
        {
            var window = await JSRuntime.Window();
            var navigator = await window.Navigator();
            var currentPosition = await navigator.Geolocation.GetCurrentPosition();
            return new GeoCoordinates()
            {
                Latitude= currentPosition.Location.Coords.Latitude,
                Longitude = currentPosition.Location.Coords.Longitude
            };
        }
    }
}
