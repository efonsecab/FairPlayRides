export let _componentReference;

export function getCurrentGeoLocation(componentReference) {
    _componentReference = componentReference;
    navigator.geolocation.getCurrentPosition(
        (position) => {
            _componentReference.invokeMethodAsync("OnGeoLocationRetrieved",
                position.coords.latitude, position.coords.longitude);
        },
        (error) => {
        });
}