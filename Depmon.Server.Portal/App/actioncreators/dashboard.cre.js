var drilldownApi = require('../api/drilldown.api');

module.exports = {
    loadSources: function () {
        drilldownApi.loadSources();
    },

    loadGroups: function (sourceCode) {
        drilldownApi.loadGroups({ sourceCode: sourceCode });
    },

    loadResources: function (sourceCode, groupCode) {
        drilldownApi.loadResources({ sourceCode: sourceCode, groupCode: groupCode });
    },

    loadIndicators: function(sourceCode, groupCode, resourceCode) {
        drilldownApi.loadIndicators({ sourceCode: sourceCode, groupCode: groupCode, resourceCode: resourceCode });
    }
};