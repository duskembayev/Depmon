var disp = require('../dispatcher');
var drilldownApi = require('../api/drilldown');

module.exports = {
    loadSources: function () {
        drilldownApi.loadSources();
    }
};