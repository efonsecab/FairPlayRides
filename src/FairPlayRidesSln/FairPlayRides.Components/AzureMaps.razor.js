export let map;
export let datasource;
export let previousLongitude;
export let previousLatitude;
export let _controlObjectReference;

export function GetMap(coordinates, clientId, subscriptionKey, controlObjectReference) {
    _controlObjectReference = controlObjectReference;
    //Initialize a map instance.
    previousLongitude = coordinates.longitude;
    previousLatitude = coordinates.latitude;
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

        var symbolDataSource = new atlas.source.DataSource();
        map.sources.add(symbolDataSource);
        var point = new atlas.Shape(new atlas.data.Point([coordinates.longitude, coordinates.latitude]));
        //Add the symbol to the data source.
        symbolDataSource.add([point]);
        map.layers.add(new atlas.layer.SymbolLayer(symbolDataSource, null));
    });

    map.events.add('click', (e) =>
    {
        var longitude = e.position[0];
        var latitude = e.position[1];
        var dataSource = new atlas.source.DataSource();
        map.sources.add(dataSource);
        var point = new atlas.Shape(new atlas.data.Point([longitude, latitude]));
        //Add the symbol to the data source.
        dataSource.add([point]);
        map.layers.add(new atlas.layer.SymbolLayer(dataSource, null));

        //Create a line and add it to the data source.
        dataSource.add(new atlas.data.LineString([[previousLongitude, previousLatitude], [longitude, latitude]]));

        //Create a line layer to render the line to the map.
        map.layers.add(new atlas.layer.LineLayer(dataSource, null, {
            strokeColor: 'blue',
            strokeWidth: 5
        }));

        previousLatitude = latitude;
        previousLongitude = longitude;

        _controlObjectReference.invokeMethodAsync('OnMapClicked', latitude, longitude);

    });

}

export function renderLineFromPreviousCoordinates(endLatitude, endLongitude) {
    renderLine(previousLatitude, previousLongitude, endLatitude, endLongitude);
}

export function renderLine(startLatitude, startLongitude, endLatitude, endLongitude) {
    var dataSource = new atlas.source.DataSource();
    map.sources.add(dataSource);
    //var point = new atlas.Shape(new atlas.data.Point([longitude, latitude]));
    //Add the symbol to the data source.
    //dataSource.add([point]);
    //map.layers.add(new atlas.layer.SymbolLayer(dataSource, null));

    //Create a line and add it to the data source.
    dataSource.add(new atlas.data.LineString([[startLongitude, startLatitude], [endLongitude, endLatitude]]));

    //Create a line layer to render the line to the map.
    map.layers.add(new atlas.layer.LineLayer(dataSource, null, {
        strokeColor: 'blue',
        strokeWidth: 5
    }));
}
export function updatePreviousCoordinates(newlatitude, newlongitude) {
    previousLatitude = newlatitude;
    previousLongitude = newlongitude;

}