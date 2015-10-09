var _ = require('underscore');
var disp = require('../dispatcher');
var utils = require('./utils.store');

var _items = [];

var GroupsStore = utils.createStore({
    get: function() {
        return _items;
    }
});

GroupsStore.dispatchToken = disp.register(function (payload) {
    var actionType = payload.actionType;
    switch (actionType) {
        case "groups-load":
            _items = payload.data;

            GroupsStore.emitChange();
            break;
    }
});

module.exports = GroupsStore;