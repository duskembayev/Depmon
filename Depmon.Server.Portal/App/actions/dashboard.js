var disp = require('../dispatcher');

module.exports = {
    onSourcesLoaded: function (data) {
        disp.dispatch({
            actionType: "sources-load",
            data: data
        });
    },
    onGroupsLoaded: function (request, data) {
        disp.dispatch({
            actionType: "groups-load",
            request: request,
            data: data
        });
    },
    onResourcesLoaded: function (request, data) {
        disp.dispatch({
            actionType: "resources-load",
            request: request,
            data: data
        });
    },
    onIndicatorsLoaded: function (request, data) {
        disp.dispatch({
            actionType: "indicators-load",
            request: request,
            data: data
        });
    }
};