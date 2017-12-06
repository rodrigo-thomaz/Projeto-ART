'use strict';
app.constant('continentConstant', {
    getAllApiUri: 'api/locale/continent/getAll',
    getAllCompletedTopic: 'Locale.Continent.GetAllViewCompleted',
    initializedEventName: 'continentService.onInitialized',
});

app.constant('countryConstant', {
    getAllApiUri: 'api/locale/country/getAll',
    getAllCompletedTopic: 'Locale.Country.GetAllViewCompleted',
    initializedEventName: 'countryService.onInitialized',
});