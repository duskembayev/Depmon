var $ = require('jquery');
var acurr = require('../actions/dashboard');
var acomm = require('../actions/common');

module.exports = {
    loadSources: function () {
        $.ajax({
            url: "api/drilldown/sources",
            method: "GET",
            datatype: "json"
        }).fail(acomm.onAjaxFailed)
          .done(function (data) {
              acurr.onSourcesLoaded(data);
          });
    },

    loadGroups: function (request) {
        $.ajax({
            url: "api/drilldown/groups",
            data: request,
            method: "GET",
            datatype: "json"
        }).fail(acomm.onAjaxFailed)
          .done(function (data) {
              acurr.onGroupsLoaded(request, data);
          });
    },

    loadResources: function (request) {
        $.ajax({
            url: "api/drilldown/resources",
            data: request,
            method: "GET",
            datatype: "json"
        }).fail(acomm.onAjaxFailed)
          .done(function (data) {
              acurr.onResourcesLoaded(data);
          });
    },

    loadIndicators: function (request) {
        $.ajax({
            url: "api/drilldown/indicators",
            data: request,
            method: "GET",
            datatype: "json"
        }).fail(acomm.onAjaxFailed)
          .done(function (data) {
              acurr.onIndicatorsLoaded(data);
          });
    }
};