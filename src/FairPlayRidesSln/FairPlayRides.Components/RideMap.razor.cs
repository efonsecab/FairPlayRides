using FairPlayRides.Blazor.Shared.GeoLocation;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FairPlayRides.Components
{
    public partial class RideMap
    {
        private System.Timers.Timer CoordinatesTimer { get; set; }
        private System.Timers.Timer ElapsedTimeTimer { get; set; }

        [Parameter]
        public AzureMapsConfiguration? AzureMapsConfiguration { get; set; }
        [Parameter]
        public GeoCoordinates GeoCoordinates { get; set; }
        [CascadingParameter(Name = "GeoCoordinatesList")]
        public List<GeoCoordinates> GeoCoordinatesList { get; set; }
        [Inject]
        private IGeoLocationProvider GeoLocationProvider { get; set; }
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        private DateTimeOffset? TimeStarted { get; set; }
        private DateTimeOffset? CurrentTime { get; set; }
        private TimeSpan? TimeElapsed { get; set; }
        private AzureMaps AzureMaps { get; set; }

        protected override void OnInitialized()
        {
            this.GeoCoordinatesList.Add(this.GeoCoordinates);
        }

        public void OnNewCoordinatesReceived(GeoCoordinates geoCoordinates)
        {
            this.GeoCoordinatesList.Add(geoCoordinates);
        }

        private async void Start()
        {
            await this.JSRuntime.InvokeVoidAsync("alert", "Starting");
            this.CoordinatesTimer = new System.Timers.Timer();
            CoordinatesTimer.Interval = TimeSpan.FromSeconds(10).TotalMilliseconds;
            CoordinatesTimer.Elapsed += async (sender, e) =>
            {
                var geoCoordinates =
                await this.GeoLocationProvider.GetCurrentPositionAsync().ConfigureAwait(false);
                //if (!GeoCoordinatesList.Contains(geoCoordinates))
                {
                    this.GeoCoordinatesList.Add(geoCoordinates);
                    //await this.AzureMaps.RenderLineFromPreviousCoordinates(geoCoordinates);
                    //await this.AzureMaps.UpdatePreviousCoordinates(geoCoordinates);
                };
                await InvokeAsync(() => StateHasChanged());
            };
            this.ElapsedTimeTimer = new();
            this.ElapsedTimeTimer.Interval = TimeSpan.FromSeconds(1).TotalMilliseconds;
            this.ElapsedTimeTimer.Elapsed += async (sender, e) => 
            {
                CurrentTime = DateTimeOffset.UtcNow;
                this.TimeElapsed = CurrentTime.Value.Subtract(this.TimeStarted.Value);
                await InvokeAsync(() => StateHasChanged());
            };
            CoordinatesTimer.Start();
            ElapsedTimeTimer.Start();
            this.TimeStarted=DateTime.UtcNow;
        }
    }
}
