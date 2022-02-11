using FairPlayRides.Blazor.Shared.GeoLocation;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayRides.Components
{
    public partial class RideMap
    {
        [Parameter]
        public AzureMapsConfiguration? AzureMapsConfiguration { get; set; }
        [Parameter]
        public GeoCoordinates GeoCoordinates { get; set; }
        private List<GeoCoordinates> GeoCoordinatesList { get; set; } = new List<GeoCoordinates>();

        protected override void OnInitialized()
        {
            this.GeoCoordinatesList.Add(this.GeoCoordinates);
        }

        public void OnNewCoordinatesReceived(GeoCoordinates geoCoordinates)
        {
            this.GeoCoordinatesList.Add(geoCoordinates);
        }

    }
}
