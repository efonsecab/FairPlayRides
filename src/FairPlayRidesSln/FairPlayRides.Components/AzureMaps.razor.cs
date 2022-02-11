using FairPlayRides.Blazor.Shared.GeoLocation;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayRides.Components
{
    public partial class AzureMaps
    {
        [Parameter]
        public GeoCoordinates GeoCoordinates { get; set; }
        [Parameter]
        public AzureMapsConfiguration AzureMapsConfiguration { get; set; }
        [Parameter]
        public EventCallback<GeoCoordinates> OnNewCoordinatesReceived { get; set; }
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }
        private IJSObjectReference? module;
        private DotNetObjectReference<AzureMaps> _dotNetObjectReference { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _dotNetObjectReference = DotNetObjectReference.Create(this);
                module = await JSRuntime!.InvokeAsync<IJSObjectReference>("import",
                $"./_content/FairPlayRides.Components/AzureMaps.razor.js");
                await module.InvokeVoidAsync("GetMap", this.GeoCoordinates,
                    this.AzureMapsConfiguration.ClientId, this.AzureMapsConfiguration.SubscriptionKey, 
                    _dotNetObjectReference);
            }
        }

        [JSInvokable]
        public async Task OnMapClicked(double latitude, double longitude)
        {
            await
            OnNewCoordinatesReceived.InvokeAsync(new GeoCoordinates() 
            {
                Latitude = latitude,
                Longitude = longitude
            });
        }
    }
}
