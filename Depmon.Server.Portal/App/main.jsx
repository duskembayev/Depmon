var React = require('react');
var ReactRouter = require('react-router');
var Router = ReactRouter.Router;
var Route = ReactRouter.Route;
var IndexRoute = ReactRouter.IndexRoute;

var App = require('./app');

var DashboardStatistic = require('./components/Dashboard/statistic');
var DashboardSources = require('./components/Dashboard/sources');
var DashboardGroups = require('./components/Dashboard/groups');
var DashboardResources = require('./components/Dashboard/resources');

var Reports = require('./components/Reports/main');
var Settings = require('./components/Settings/main');

React.render((
  <Router>
    <Route path="/" component={App}>
        <IndexRoute components={{
            sidebar: DashboardSources,
            content: DashboardStatistic
        }} />
        <Route path="sources/:source" components={{
            sidebar: DashboardSources,
            content: DashboardGroups
        }}>
            <IndexRoute component={DashboardResources} />
            <Route path="groups/:group" component={DashboardResources} />
        </Route>
        <Route path="reports" components={{
            content: Reports
        }} />
        <Route path="settings" components={{
            content: Settings
        }} />
    </Route>
  </Router>
), document.body)