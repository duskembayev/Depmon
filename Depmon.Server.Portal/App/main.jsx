var React = require('react');
var ReactRouter = require('react-router');
var Router = ReactRouter.Router;
var Route = ReactRouter.Route;
var IndexRoute = ReactRouter.IndexRoute;

var App = require('./app');
var Dashboard = require('./Dashboard/main');
var Reports = require('./Reports/main');
var Settings = require('./Settings/main');

React.render((
  <Router>
    <Route path="/" component={App}>
        <IndexRoute component={Dashboard} />
        <Route path="reports" component={Reports} />
        <Route path="settings" component={Settings} />
    </Route>
  </Router>
), document.body)