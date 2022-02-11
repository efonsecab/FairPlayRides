using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayRides.Blazor.Shared.GeoLocation
{
    public interface IGeoLocationProvider
    {
        Task<GeoCoordinates> GetCurrentPositionAsync();
    }
}
