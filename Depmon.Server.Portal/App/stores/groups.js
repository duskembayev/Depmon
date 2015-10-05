var _ = require('underscore');
var disp = require('../dispatcher');
var utils = require('./utils');

var _items = [];
var _selected = null;

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