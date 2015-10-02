'use strict';

var _ = require('underscore'),
    EventEmitter = require('events').EventEmitter,
    assign = require('object-assign'),
    shallowEqual = require('react/lib/shallowEqual'),
    CHANGE_EVENT = 'change';

var StoreUtils = {
    createStore: function (spec) {
        var store = assign({
            emitChange: function () {
                this.emit(CHANGE_EVENT);
            },

            addChangeListener: function (callback) {
                this.on(CHANGE_EVENT, callback);
            },

            removeChangeListener: function (callback) {
                this.removeListener(CHANGE_EVENT, callback);
            }
        }, spec, EventEmitter.prototype);

        _.each(store, function (val, key) {
            if (_.isFunction(val)) {
                store[key] = store[key].bind(store);
            }
        });

        store.setMaxListeners(0);
        return store;
    },

    isInBag: function (bag, id, fields) {
        var item = bag[id];
        if (!bag[id]) {
            return false;
        }

        if (fields) {
            return fields.filter(function (field) { return item.hasOwnProperty(field); });
        } else {
            return true;
        }
    },

    mergeIntoBag: function (bag, entities, transform) {
        if (!transform) {
            transform = function (x) { return x; };
        }

        for (var key in entities) {
            if (!entities.hasOwnProperty(key)) {
                continue;
            }

            if (!bag.hasOwnProperty(key)) {
                bag[key] = transform(entities[key]);
            } else if (!shallowEqual(bag[key], entities[key])) {
                bag[key] = transform(assign({}, bag[key], entities[key]));
            }
        }
    }
};

module.exports = StoreUtils;