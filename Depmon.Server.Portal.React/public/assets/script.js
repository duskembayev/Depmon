(function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require=="function"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error("Cannot find module '"+o+"'");throw f.code="MODULE_NOT_FOUND",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require=="function"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s})({1:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, '__esModule', {
    value: true
});

var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

var _get = function get(_x, _x2, _x3) { var _again = true; _function: while (_again) { var object = _x, property = _x2, receiver = _x3; _again = false; if (object === null) object = Function.prototype; var desc = Object.getOwnPropertyDescriptor(object, property); if (desc === undefined) { var parent = Object.getPrototypeOf(object); if (parent === null) { return undefined; } else { _x = parent; _x2 = property; _x3 = receiver; _again = true; desc = parent = undefined; continue _function; } } else if ('value' in desc) { return desc.value; } else { var getter = desc.get; if (getter === undefined) { return undefined; } return getter.call(receiver); } } };

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

function _inherits(subClass, superClass) { if (typeof superClass !== 'function' && superClass !== null) { throw new TypeError('Super expression must either be null or a function, not ' + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var _lodash = require('lodash');

var _lodash2 = _interopRequireDefault(_lodash);

var _react = require('react');

var _react2 = _interopRequireDefault(_react);

var _classnames = require('classnames');

var _classnames2 = _interopRequireDefault(_classnames);

var Component = (function (_React$Component) {
    _inherits(Component, _React$Component);

    function Component(props) {
        _classCallCheck(this, Component);

        _get(Object.getPrototypeOf(Component.prototype), 'constructor', this).call(this);
        this.refreshState = this.refreshState.bind(this);
        this.state = this.initState(props);
    }

    _createClass(Component, [{
        key: 'refreshState',
        value: function refreshState() {
            this.setState(this.initState());
        }
    }, {
        key: 'initState',
        value: function initState() {
            return {};
        }
    }, {
        key: 'title',
        value: function title() {
            return "Depmon";
        }
    }, {
        key: 'trigger',
        value: function trigger(eventName) {
            var _props;

            eventName = 'on' + _lodash2['default'].capitalize(eventName);

            for (var _len = arguments.length, args = Array(_len > 1 ? _len - 1 : 0), _key = 1; _key < _len; _key++) {
                args[_key - 1] = arguments[_key];
            }

            if (this.props[eventName]) (_props = this.props)[eventName].apply(_props, args);
        }
    }]);

    return Component;
})(_react2['default'].Component);

exports['default'] = Component;

Component.prototype.cx = _classnames2['default'];
module.exports = exports['default'];


},{"classnames":undefined,"lodash":undefined,"react":undefined}],2:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, '__esModule', {
  value: true
});

var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

var _react = require('react');

var _react2 = _interopRequireDefault(_react);

var _reactDom = require('react-dom');

var _reactDom2 = _interopRequireDefault(_reactDom);

var Controller = (function () {
  function Controller() {
    _classCallCheck(this, Controller);

    this.xhrs = {};
  }

  _createClass(Controller, [{
    key: 'destroy',
    value: function destroy() {
      for (var key in this.xhrs) {
        if (this.xhrs[key] !== 4) {
          this.xhrs[key].abort();
        }
      }
    }
  }, {
    key: 'renderView',
    value: function renderView(View, callback) {
      var view = _reactDom2['default'].render(_react2['default'].createElement(View, null), window.appNode, callback);
      window.titleNode.innerText = view.title();
    }
  }]);

  return Controller;
})();

exports['default'] = Controller;
module.exports = exports['default'];


},{"react":undefined,"react-dom":undefined}],3:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, '__esModule', {
  value: true
});

var _slicedToArray = (function () { function sliceIterator(arr, i) { var _arr = []; var _n = true; var _d = false; var _e = undefined; try { for (var _i = arr[Symbol.iterator](), _s; !(_n = (_s = _i.next()).done); _n = true) { _arr.push(_s.value); if (i && _arr.length === i) break; } } catch (err) { _d = true; _e = err; } finally { try { if (!_n && _i['return']) _i['return'](); } finally { if (_d) throw _e; } } return _arr; } return function (arr, i) { if (Array.isArray(arr)) { return arr; } else if (Symbol.iterator in Object(arr)) { return sliceIterator(arr, i); } else { throw new TypeError('Invalid attempt to destructure non-iterable instance'); } }; })();

var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

var _react = require('react');

var _react2 = _interopRequireDefault(_react);

var _page = require('page');

var _page2 = _interopRequireDefault(_page);

var _modulesVent = require('../modules/vent');

var _modulesVent2 = _interopRequireDefault(_modulesVent);

var Router = (function () {
  function Router() {
    _classCallCheck(this, Router);
  }

  _createClass(Router, [{
    key: '_mapRoutes',
    value: function _mapRoutes() {
      if (this.middleware) this.middleware();
      if (this.router) this.router();
    }
  }, {
    key: 'run',
    value: function run() {
      var _this = this;

      this.page = _page2['default'];
      this.ctor = null;

      this.page('*', this.createQuery);
      this._mapRoutes();
      this.page.start();

      _modulesVent2['default'].on('routeTo', function (url) {
        return _this.routeTo(url);
      });
    }
  }, {
    key: 'routeTo',
    value: function routeTo(url) {
      this.page(url);
    }
  }, {
    key: 'use',
    value: function use() {
      this.page.apply(this, arguments);
    }
  }, {
    key: 'route',
    value: function route(url, action) {
      var _this2 = this;

      var temp = action.split('.');
      var method = temp[1];
      var Controller = this.controllers[temp[0]];

      if (!Controller) {
        throw new Error('undefined controller "' + temp[0] + '"');
      }

      this.page(url, function (ctx) {
        _this2.beforeRoute(ctx);
        _this2.ctor = new Controller();

        if (!_this2.ctor[method]) {
          throw new Error('undefined method "' + method + '" of "' + temp[0] + '" controller');
        }

        if (_this2.ctor[method].length > 1) {
          _this2.ctor[method](ctx, function () {
            return _this2.afterRoute(ctx);
          });
        } else {
          _this2.ctor[method](ctx);
          _this2.afterRoute(ctx);
        }
      });
    }
  }, {
    key: 'beforeRoute',
    value: function beforeRoute(ctx) {
      if (this.ctor) {
        this.ctor.destroy();
        this.ctor = null;
      }

      _modulesVent2['default'].trigger('route:before', ctx);
    }
  }, {
    key: 'afterRoute',
    value: function afterRoute(ctx) {
      _modulesVent2['default'].trigger('route:after', ctx);
    }
  }, {
    key: 'createQuery',
    value: function createQuery(ctx, next) {
      var query = {};
      var params = ctx.querystring.split('&');

      var _iteratorNormalCompletion = true;
      var _didIteratorError = false;
      var _iteratorError = undefined;

      try {
        for (var _iterator = params.entries()[Symbol.iterator](), _step; !(_iteratorNormalCompletion = (_step = _iterator.next()).done); _iteratorNormalCompletion = true) {
          var _step$value = _slicedToArray(_step.value, 2);

          var index = _step$value[0];
          var param = _step$value[1];

          param = param.split('=');
          query[param[0]] = param[1];
        }
      } catch (err) {
        _didIteratorError = true;
        _iteratorError = err;
      } finally {
        try {
          if (!_iteratorNormalCompletion && _iterator['return']) {
            _iterator['return']();
          }
        } finally {
          if (_didIteratorError) {
            throw _iteratorError;
          }
        }
      }

      ctx.query = query;
      next();
    }
  }]);

  return Router;
})();

exports['default'] = Router;
module.exports = exports['default'];


},{"../modules/vent":9,"page":undefined,"react":undefined}],4:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, '__esModule', {
  value: true
});

var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

var _get = function get(_x, _x2, _x3) { var _again = true; _function: while (_again) { var object = _x, property = _x2, receiver = _x3; _again = false; if (object === null) object = Function.prototype; var desc = Object.getOwnPropertyDescriptor(object, property); if (desc === undefined) { var parent = Object.getPrototypeOf(object); if (parent === null) { return undefined; } else { _x = parent; _x2 = property; _x3 = receiver; _again = true; desc = parent = undefined; continue _function; } } else if ('value' in desc) { return desc.value; } else { var getter = desc.get; if (getter === undefined) { return undefined; } return getter.call(receiver); } } };

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

function _inherits(subClass, superClass) { if (typeof superClass !== 'function' && superClass !== null) { throw new TypeError('Super expression must either be null or a function, not ' + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var _baseController = require('../base/controller');

var _baseController2 = _interopRequireDefault(_baseController);

var _viewsHome = require('../views/home');

var _viewsHome2 = _interopRequireDefault(_viewsHome);

var HomeController = (function (_Controller) {
  _inherits(HomeController, _Controller);

  function HomeController() {
    _classCallCheck(this, HomeController);

    _get(Object.getPrototypeOf(HomeController.prototype), 'constructor', this).apply(this, arguments);
  }

  _createClass(HomeController, [{
    key: 'index',
    value: function index(ctx, done) {
      this.renderView(_viewsHome2['default'], done);
    }
  }]);

  return HomeController;
})(_baseController2['default']);

exports['default'] = HomeController;
module.exports = exports['default'];


},{"../base/controller":2,"../views/home":12}],5:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, '__esModule', {
  value: true
});

var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

var _get = function get(_x, _x2, _x3) { var _again = true; _function: while (_again) { var object = _x, property = _x2, receiver = _x3; _again = false; if (object === null) object = Function.prototype; var desc = Object.getOwnPropertyDescriptor(object, property); if (desc === undefined) { var parent = Object.getPrototypeOf(object); if (parent === null) { return undefined; } else { _x = parent; _x2 = property; _x3 = receiver; _again = true; desc = parent = undefined; continue _function; } } else if ('value' in desc) { return desc.value; } else { var getter = desc.get; if (getter === undefined) { return undefined; } return getter.call(receiver); } } };

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

function _inherits(subClass, superClass) { if (typeof superClass !== 'function' && superClass !== null) { throw new TypeError('Super expression must either be null or a function, not ' + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var _baseController = require('../base/controller');

var _baseController2 = _interopRequireDefault(_baseController);

var _viewsReport = require('../views/report');

var _viewsReport2 = _interopRequireDefault(_viewsReport);

var ReportController = (function (_Controller) {
  _inherits(ReportController, _Controller);

  function ReportController() {
    _classCallCheck(this, ReportController);

    _get(Object.getPrototypeOf(ReportController.prototype), 'constructor', this).apply(this, arguments);
  }

  _createClass(ReportController, [{
    key: 'index',
    value: function index(ctx, done) {
      this.renderView(_viewsReport2['default'], done);
    }
  }]);

  return ReportController;
})(_baseController2['default']);

exports['default'] = ReportController;
module.exports = exports['default'];


},{"../base/controller":2,"../views/report":13}],6:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, '__esModule', {
  value: true
});

var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

var _get = function get(_x, _x2, _x3) { var _again = true; _function: while (_again) { var object = _x, property = _x2, receiver = _x3; _again = false; if (object === null) object = Function.prototype; var desc = Object.getOwnPropertyDescriptor(object, property); if (desc === undefined) { var parent = Object.getPrototypeOf(object); if (parent === null) { return undefined; } else { _x = parent; _x2 = property; _x3 = receiver; _again = true; desc = parent = undefined; continue _function; } } else if ('value' in desc) { return desc.value; } else { var getter = desc.get; if (getter === undefined) { return undefined; } return getter.call(receiver); } } };

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

function _inherits(subClass, superClass) { if (typeof superClass !== 'function' && superClass !== null) { throw new TypeError('Super expression must either be null or a function, not ' + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var _baseController = require('../base/controller');

var _baseController2 = _interopRequireDefault(_baseController);

var _viewsSettings = require('../views/settings');

var _viewsSettings2 = _interopRequireDefault(_viewsSettings);

var SettingController = (function (_Controller) {
  _inherits(SettingController, _Controller);

  function SettingController() {
    _classCallCheck(this, SettingController);

    _get(Object.getPrototypeOf(SettingController.prototype), 'constructor', this).apply(this, arguments);
  }

  _createClass(SettingController, [{
    key: 'index',
    value: function index(ctx, done) {
      this.renderView(_viewsSettings2['default'], done);
    }
  }]);

  return SettingController;
})(_baseController2['default']);

exports['default'] = SettingController;
module.exports = exports['default'];


},{"../base/controller":2,"../views/settings":14}],7:[function(require,module,exports){
'use strict';

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

var _lodash = require('lodash');

var _lodash2 = _interopRequireDefault(_lodash);

var _jquery = require('jquery');

var _jquery2 = _interopRequireDefault(_jquery);

window.appNode = document.getElementById('app-node');
window.titleNode = document.getElementsByTagName('title')[0];

_jquery2['default'].ajaxSetup({
  crossDomain: true,
  dataType: 'json',
  contentType: 'application/json',
  processData: false,
  beforeSend: function beforeSend() {
    if (_lodash2['default'].isObject(this.data) && this.type.toLowerCase() !== 'get') {
      this.data = JSON.stringify(this.data);
    }
  }
});


},{"jquery":undefined,"lodash":undefined}],8:[function(require,module,exports){
'use strict';

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

require('./dependencies');

var _router = require('./router');

var _router2 = _interopRequireDefault(_router);

new _router2['default']().run();
console.log('info', 'app initialized');


},{"./dependencies":7,"./router":10}],9:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, '__esModule', {
  value: true
});

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

var _lodash = require('lodash');

var _lodash2 = _interopRequireDefault(_lodash);

var _backboneEventsStandalone = require('backbone-events-standalone');

var _backboneEventsStandalone2 = _interopRequireDefault(_backboneEventsStandalone);

exports['default'] = _lodash2['default'].assign({}, _backboneEventsStandalone2['default']);
module.exports = exports['default'];


},{"backbone-events-standalone":undefined,"lodash":undefined}],10:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, '__esModule', {
  value: true
});

var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

var _get = function get(_x, _x2, _x3) { var _again = true; _function: while (_again) { var object = _x, property = _x2, receiver = _x3; _again = false; if (object === null) object = Function.prototype; var desc = Object.getOwnPropertyDescriptor(object, property); if (desc === undefined) { var parent = Object.getPrototypeOf(object); if (parent === null) { return undefined; } else { _x = parent; _x2 = property; _x3 = receiver; _again = true; desc = parent = undefined; continue _function; } } else if ('value' in desc) { return desc.value; } else { var getter = desc.get; if (getter === undefined) { return undefined; } return getter.call(receiver); } } };

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

function _inherits(subClass, superClass) { if (typeof superClass !== 'function' && superClass !== null) { throw new TypeError('Super expression must either be null or a function, not ' + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var _baseRouter = require('./base/router');

var _baseRouter2 = _interopRequireDefault(_baseRouter);

var _modulesVent = require('./modules/vent');

var _modulesVent2 = _interopRequireDefault(_modulesVent);

var Router = (function (_BaseRouter) {
  _inherits(Router, _BaseRouter);

  function Router() {
    _classCallCheck(this, Router);

    _get(Object.getPrototypeOf(Router.prototype), 'constructor', this).apply(this, arguments);
  }

  _createClass(Router, [{
    key: 'router',
    value: function router() {
      this.route('/', 'home.index');
      this.route('/settings', 'setting.index');
      this.route('/reports', 'report.index');
    }
  }]);

  return Router;
})(_baseRouter2['default']);

exports['default'] = Router;

Router.prototype.controllers = {
  home: require('./controllers/home'),
  setting: require('./controllers/setting'),
  report: require('./controllers/report')
};
module.exports = exports['default'];


},{"./base/router":3,"./controllers/home":4,"./controllers/report":5,"./controllers/setting":6,"./modules/vent":9}],11:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, '__esModule', {
  value: true
});

var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

var _get = function get(_x, _x2, _x3) { var _again = true; _function: while (_again) { var object = _x, property = _x2, receiver = _x3; _again = false; if (object === null) object = Function.prototype; var desc = Object.getOwnPropertyDescriptor(object, property); if (desc === undefined) { var parent = Object.getPrototypeOf(object); if (parent === null) { return undefined; } else { _x = parent; _x2 = property; _x3 = receiver; _again = true; desc = parent = undefined; continue _function; } } else if ('value' in desc) { return desc.value; } else { var getter = desc.get; if (getter === undefined) { return undefined; } return getter.call(receiver); } } };

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

function _inherits(subClass, superClass) { if (typeof superClass !== 'function' && superClass !== null) { throw new TypeError('Super expression must either be null or a function, not ' + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var _react = require('react');

var _react2 = _interopRequireDefault(_react);

var _baseComponent = require('../../base/component');

var _baseComponent2 = _interopRequireDefault(_baseComponent);

var _modulesVent = require('../../modules/vent');

var _modulesVent2 = _interopRequireDefault(_modulesVent);

var Header = (function (_Component) {
  _inherits(Header, _Component);

  function Header() {
    _classCallCheck(this, Header);

    _get(Object.getPrototypeOf(Header.prototype), 'constructor', this).apply(this, arguments);
  }

  _createClass(Header, [{
    key: 'initState',
    value: function initState() {
      return {
        currentPath: ''
      };
    }
  }, {
    key: 'componentDidMount',
    value: function componentDidMount() {
      _modulesVent2['default'].on('route:after', this.setActivePath, this);
    }
  }, {
    key: 'componentWillUnmount',
    value: function componentWillUnmount() {
      _modulesVent2['default'].off('route:after', this.setActivePath, this);
    }
  }, {
    key: 'setActivePath',
    value: function setActivePath(ctx) {
      this.setState({ currentPath: ctx.pathname });
    }
  }, {
    key: 'render',
    value: function render() {
      var homeUrl = '/';
      var currentPath = this.state.currentPath;

      return _react2['default'].createElement(
        'header',
        { className: 'c-header' },
        _react2['default'].createElement(
          'div',
          { className: 'c-h-title' },
          _react2['default'].createElement(
            'h1',
            null,
            'Depmon'
          )
        ),
        _react2['default'].createElement(
          'nav',
          null,
          _react2['default'].createElement(
            'a',
            { href: homeUrl, className: this.cx({ active: currentPath === homeUrl }) },
            'Главная'
          ),
          _react2['default'].createElement(
            'a',
            { href: '/reports', className: this.cx({ active: currentPath === '/reports' }) },
            'Отчеты'
          ),
          _react2['default'].createElement(
            'a',
            { href: '/settings', className: this.cx({ active: currentPath === '/settings' }) },
            'Настройки'
          )
        )
      );
    }
  }]);

  return Header;
})(_baseComponent2['default']);

exports['default'] = Header;
module.exports = exports['default'];


},{"../../base/component":1,"../../modules/vent":9,"react":undefined}],12:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, '__esModule', {
  value: true
});

var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

var _get = function get(_x, _x2, _x3) { var _again = true; _function: while (_again) { var object = _x, property = _x2, receiver = _x3; _again = false; if (object === null) object = Function.prototype; var desc = Object.getOwnPropertyDescriptor(object, property); if (desc === undefined) { var parent = Object.getPrototypeOf(object); if (parent === null) { return undefined; } else { _x = parent; _x2 = property; _x3 = receiver; _again = true; desc = parent = undefined; continue _function; } } else if ('value' in desc) { return desc.value; } else { var getter = desc.get; if (getter === undefined) { return undefined; } return getter.call(receiver); } } };

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

function _inherits(subClass, superClass) { if (typeof superClass !== 'function' && superClass !== null) { throw new TypeError('Super expression must either be null or a function, not ' + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var _react = require('react');

var _react2 = _interopRequireDefault(_react);

var _baseComponent = require('../base/component');

var _baseComponent2 = _interopRequireDefault(_baseComponent);

var _componentsHeader = require('./components/header');

var _componentsHeader2 = _interopRequireDefault(_componentsHeader);

var HomeView = (function (_Component) {
  _inherits(HomeView, _Component);

  function HomeView() {
    _classCallCheck(this, HomeView);

    _get(Object.getPrototypeOf(HomeView.prototype), 'constructor', this).apply(this, arguments);
  }

  _createClass(HomeView, [{
    key: 'render',
    value: function render() {
      return _react2['default'].createElement(
        'div',
        null,
        _react2['default'].createElement(_componentsHeader2['default'], null)
      );
    }
  }]);

  return HomeView;
})(_baseComponent2['default']);

exports['default'] = HomeView;
module.exports = exports['default'];


},{"../base/component":1,"./components/header":11,"react":undefined}],13:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, '__esModule', {
  value: true
});

var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

var _get = function get(_x, _x2, _x3) { var _again = true; _function: while (_again) { var object = _x, property = _x2, receiver = _x3; _again = false; if (object === null) object = Function.prototype; var desc = Object.getOwnPropertyDescriptor(object, property); if (desc === undefined) { var parent = Object.getPrototypeOf(object); if (parent === null) { return undefined; } else { _x = parent; _x2 = property; _x3 = receiver; _again = true; desc = parent = undefined; continue _function; } } else if ('value' in desc) { return desc.value; } else { var getter = desc.get; if (getter === undefined) { return undefined; } return getter.call(receiver); } } };

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

function _inherits(subClass, superClass) { if (typeof superClass !== 'function' && superClass !== null) { throw new TypeError('Super expression must either be null or a function, not ' + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var _react = require('react');

var _react2 = _interopRequireDefault(_react);

var _baseComponent = require('../base/component');

var _baseComponent2 = _interopRequireDefault(_baseComponent);

var _componentsHeader = require('./components/header');

var _componentsHeader2 = _interopRequireDefault(_componentsHeader);

var ReportsView = (function (_Component) {
  _inherits(ReportsView, _Component);

  function ReportsView() {
    _classCallCheck(this, ReportsView);

    _get(Object.getPrototypeOf(ReportsView.prototype), 'constructor', this).apply(this, arguments);
  }

  _createClass(ReportsView, [{
    key: 'render',
    value: function render() {
      return _react2['default'].createElement(
        'div',
        null,
        'Reports'
      );
    }
  }]);

  return ReportsView;
})(_baseComponent2['default']);

exports['default'] = ReportsView;
module.exports = exports['default'];


},{"../base/component":1,"./components/header":11,"react":undefined}],14:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, '__esModule', {
  value: true
});

var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

var _get = function get(_x, _x2, _x3) { var _again = true; _function: while (_again) { var object = _x, property = _x2, receiver = _x3; _again = false; if (object === null) object = Function.prototype; var desc = Object.getOwnPropertyDescriptor(object, property); if (desc === undefined) { var parent = Object.getPrototypeOf(object); if (parent === null) { return undefined; } else { _x = parent; _x2 = property; _x3 = receiver; _again = true; desc = parent = undefined; continue _function; } } else if ('value' in desc) { return desc.value; } else { var getter = desc.get; if (getter === undefined) { return undefined; } return getter.call(receiver); } } };

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

function _inherits(subClass, superClass) { if (typeof superClass !== 'function' && superClass !== null) { throw new TypeError('Super expression must either be null or a function, not ' + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var _react = require('react');

var _react2 = _interopRequireDefault(_react);

var _baseComponent = require('../base/component');

var _baseComponent2 = _interopRequireDefault(_baseComponent);

var _componentsHeader = require('./components/header');

var _componentsHeader2 = _interopRequireDefault(_componentsHeader);

var SetingsView = (function (_Component) {
  _inherits(SetingsView, _Component);

  function SetingsView() {
    _classCallCheck(this, SetingsView);

    _get(Object.getPrototypeOf(SetingsView.prototype), 'constructor', this).apply(this, arguments);
  }

  _createClass(SetingsView, [{
    key: 'render',
    value: function render() {
      return _react2['default'].createElement(
        'div',
        null,
        'Settings'
      );
    }
  }]);

  return SetingsView;
})(_baseComponent2['default']);

exports['default'] = SetingsView;
module.exports = exports['default'];


},{"../base/component":1,"./components/header":11,"react":undefined}]},{},[8]);
