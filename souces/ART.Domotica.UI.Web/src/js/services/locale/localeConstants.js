'use strict';
app.constant('continentConstant', {
    getAllApiUri: 'api/locale/continent/getAll',
    getAllCompletedTopic: 'Locale.Continent.GetAllViewCompleted',
    getAllCompletedEventName: 'continentService.GetAllCompleted',
});

app.constant('countryConstant', {
    getAllApiUri: 'api/locale/country/getAll',
    getAllCompletedTopic: 'Locale.Country.GetAllViewCompleted',
    getAllCompletedEventName: 'countryService.GetAllCompleted',
});