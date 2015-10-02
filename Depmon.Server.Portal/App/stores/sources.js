var _ = require('underscore');
var disp = require('../dispatcher');
var utils = require('./utils');

var _items = [];
var _selected = null;

var SourcesStore = utils.createStore({
    get: function() {
        return _items;
    },
    getSelected: function() {
        return _selected;
    } 
});

SourcesStore.dispatchToken = disp.register(function(payload) {
    var actionType = payload.actionType;
    switch (actionType) {
        case "sources-load":
            _items = payload.data;

            SourcesStore.emitChange();
            break;
        case "source-select":
            var selected = null;
            if (payload.selected !== null) {
                selected = _.find(_items, function(item) { return item.Code === payload.selected; });
            }
            if (selected !== _selected) {
                _selected = selected;
            }
            
            SourcesStore.emitChange();
            break;
    }
});

module.exports = SourcesStore;