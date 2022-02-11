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
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }
        private IJSObjectReference? module;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                module = await JSRuntime!.InvokeAsync<IJSObjectReference>("import",
                $"./_content/FairPlayRides.Components/AzureMaps.razor.js");
                await module.InvokeVoidAsync("GetMap", this.GeoCoordinates);
            }
        }
    }
}
