var _ = require('underscore');
var disp = require('../dispatcher');
var utils = require('./utils');

var _items = [];
var _selected = null;

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
    }
});

module.exports = ResourcesStore;