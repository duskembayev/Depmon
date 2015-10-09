var _ = require('underscore');
var disp = require('../dispatcher');
var utils = require('./utils.store');

var _items = [];

var ResourcesStore = utils.createStore({
    get: function() {
        return _items;
    }
});

ResourcesStore.dispatchToken = disp.register(function (payload) {
    var actionType = payload.actionType;
    switch (actionType) {
        case "resources-load":
            _items = payload.data;

            ResourcesStore.emitChange();
            break;
        case "indicators-load":
            var resourceCode = payload.request.resourceCode;
            var resource = _.find(_items, function (item) { return item.Code === resourceCode; });
            _.extend(resource, { indicators: payload.data });

            ResourcesStore.emitChange();
            break;
    }
});

module.exports = ResourcesStore;