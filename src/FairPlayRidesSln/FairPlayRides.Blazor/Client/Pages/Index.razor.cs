using FairPlayRides.Blazor.Shared.GeoLocation;
using FairPlayRides.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FairPlayRides.Blazor.Client.Pages
{
    public partial class Index
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        [Inject]
        private AzureMapsConfiguration AzureMapsConfiguration { get; set; }
        [Inject]
        public IGeoLocationProvider GeoLocationProvider { get; set; }
        private IJSObjectReference? module;
        private bool ShowMapsControl { get; set; }
        private DotNetObjectReference<Index> ComponentReference { get; set; }
        private GeoCoordinates GeoCoordinates { get; set; }
        private List<GeoCoordinates> GeoCoordinatesList { get; set; }=new List<GeoCoordinates>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            this.ComponentReference = DotNetObjectReference.Create(this);
            if (firstRender)
            {
                module = await JSRuntime!.InvokeAsync<IJSObjectReference>("import",
                $"./Pages/Index.razor.js");
                //await module.InvokeVoidAsync("getCurrentGeoLocation", ComponentReference);
                var location  = await GeoLocationProvider.GetCurrentPositionAsync();
                this.GeoCoordinates = new GeoCoordinates
                {
                    Latitude = (double)location.Latitude,
                    Longitude = (double)location.Longitude
                };
                this.ShowMapsControl = true;

                StateHasChanged();
            }
        }

        [JSInvokable]
        public void OnGeoLocationRetrieved(double latitude, double longitude)
        {
            this.GeoCoordinates = new GeoCoordinates
            {
                Latitude = latitude,
                Longitude = longitude
            };
            this.ShowMapsControl = true;

            StateHasChanged();
        }

        public void Dispose()
        {
            this.ComponentReference.Dispose();
        }
    }
}
