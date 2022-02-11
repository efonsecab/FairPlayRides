using FairPlayRides.Blazor.Shared.GeoLocation;
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
        private bool ShowMapsControl { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            this.InitialGeoLocation = await this.GeoLocationProvider.GetCurrentPositionAsync();
            this.ShowMapsControl = true;
        }
    }
}
