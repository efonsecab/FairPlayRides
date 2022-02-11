export let map;
export let datasource;


export function GetMap(coordinates, clientId, subscriptionKey) {
    debugger;
    //Initialize a map instance.
    map = new atlas.Map('mapWidget', {
        center: [coordinates.longitude, coordinates.latitude],
        zoom: 14,
        view: 'Auto',

        //Add authentication details for connecting to Azure Maps.
        authOptions: {
            //Use Azure Active Directory authentication.
            authType: 'anonymous',
            clientId: clientId, //Your Azure Active Directory client id for accessing your Azure Maps account.
            //getToken: function (resolve, reject, map) {
            //    //URL to your authentication service that retrieves an Azure Active Directory Token.
            //    var tokenServiceUrl = "https://azuremapscodesamples.azurewebsites.net/Common/TokenService.ashx";

            //    fetch(tokenServiceUrl).then(r => r.text()).then(token => resolve(token));
            //}

            //Alternatively, use an Azure Maps key. Get an Azure Maps key at https://azure.com/maps. NOTE: The primary key should be used as the key.
            authType: 'subscriptionKey',
            subscriptionKey: subscriptionKey
        }
    });

    //Wait until the map resources are ready.
    map.events.add('ready', function () {

        //Add the zoom control to the map.
        map.controls.add(new atlas.control.ZoomControl(), {
            position: 'top-right'
        });

        //Create a data source and add it to the map.
        datasource = new atlas.source.DataSource();
        map.sources.add(datasource);
    });
}