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
        private System.Timers.Timer Timer;
        private List<string> Path { get; set; } = new List<string>();
        protected override async Task OnInitializedAsync()
        {
            this.InitialGeoLocation = await this.GeoLocationProvider.GetCurrentPositionAsync();
            this.ShowMapsControl = true;
            this.Timer = new System.Timers.Timer();
            Timer.Interval = TimeSpan.FromSeconds(10).TotalMilliseconds;
            this.Timer.Elapsed += async (sender, args) => 
            {
                this.Path.Add(Random.Shared.Next(10000).ToString());
                await InvokeAsync(() => StateHasChanged());
            };
            this.Timer.Start();
        }
    }
}
