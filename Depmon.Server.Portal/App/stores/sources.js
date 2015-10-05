var _ = require('underscore');
var disp = require('../dispatcher');
var utils = require('./utils');

var _items = [];
var _selected = null;

var SourcesStore = utils.createStore({
    get: function() {
        return _items;
    }
});

SourcesStore.dispatchToken = disp.register(function(payload) {
    var actionType = payload.actionType;
    switch (actionType) {
        case "sources-load":
            _items = payload.data;

            SourcesStore.emitChange();
            break;
    }
});

module.exports = SourcesStore;