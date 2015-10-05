var drilldownApi = require('../api/drilldown');

module.exports = {
    loadSources: function () {
        drilldownApi.loadSources();
    },

    loadGroups: function(sourceCode) {
        drilldownApi.loadGroups({ sourceCode:sourceCode });
    },

    loadResources: function(sourceCode, groupCode) {
        drilldownApi.loadResources({ sourceCode:sourceCode, groupCode:groupCode });
    }
};