using FairPlayRides.Blazor.Shared.GeoLocation;
using FairPlayRides.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayRides.MAUI.Pages
{
    public partial class Index
    {
        private GeoCoordinates InitialGeoLocation { get; set; }
        [Inject]
        private IGeoLocationProvider GeoLocationProvider { get; set; }
        [Inject]
        private AzureMapsConfiguration AzureMapsConfiguration { get; set; }
        private bool ShowMapsControl { get; set; } = false;
        private List<GeoCoordinates> GeoCoordinatesList { get; set; } = new List<GeoCoordinates>();
        protected override async Task OnInitializedAsync()
        {
            var locationPermissionStatus = await
            Permissions.RequestAsync<Microsoft.Maui.Essentials.Permissions.LocationWhenInUse>();
            if (locationPermissionStatus != PermissionStatus.Granted)
            {
                locationPermissionStatus = await
                            Permissions.RequestAsync<Microsoft.Maui.Essentials.Permissions.LocationWhenInUse>();
            }

            this.InitialGeoLocation = await this.GeoLocationProvider
                .GetCurrentPositionAsync();
            this.ShowMapsControl = true;
        }
    }
}
