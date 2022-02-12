using FairPlayRides.Blazor.Shared.GeoLocation;
using Microsoft.AspNetCore.Components;

namespace FairPlayRides.Components
{
    public partial class RideMap
    {
        private System.Timers.Timer _timer;

        [Parameter]
        public AzureMapsConfiguration? AzureMapsConfiguration { get; set; }
        [Parameter]
        public GeoCoordinates GeoCoordinates { get; set; }
        [CascadingParameter(Name = "GeoCoordinatesList")]
        public List<GeoCoordinates> GeoCoordinatesList { get; set; }
        [Inject]
        private IGeoLocationProvider GeoLocationProvider { get; set; }
        private AzureMaps AzureMaps { get; set; }

        protected override void OnInitialized()
        {
            this.GeoCoordinatesList.Add(this.GeoCoordinates);
        }

        public void OnNewCoordinatesReceived(GeoCoordinates geoCoordinates)
        {
            this.GeoCoordinatesList.Add(geoCoordinates);
        }

        private void Start()
        {
            this._timer = new System.Timers.Timer();
            _timer.Interval = TimeSpan.FromSeconds(10).TotalMilliseconds;
            _timer.Elapsed += async (sender, e) =>
            {
                var geoCoordinates =
                await this.GeoLocationProvider.GetCurrentPositionAsync().ConfigureAwait(false);
                if (!GeoCoordinatesList.Contains(geoCoordinates))
                {
                    this.GeoCoordinatesList.Add(geoCoordinates);
                    await this.AzureMaps.RenderLineFromPreviousCoordinates(geoCoordinates);
                    await this.AzureMaps.UpdatePreviousCoordinates(geoCoordinates);
                };
                await InvokeAsync(() => StateHasChanged());
            };
            _timer.Start();
        }
    }
}
