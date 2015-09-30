var React = require('react');
var ReactRouter = require('react-router');
var Router = ReactRouter.Router;
var Route = ReactRouter.Route;
var IndexRoute = ReactRouter.IndexRoute;

var App = require('./app');

var DashboardStatistic = require('./Dashboard/statistic');
var DashboardSources = require('./Dashboard/sources');
var DashboardGroups = require('./Dashboard/groups');
var DashboardResources = require('./Dashboard/resources');

var Reports = require('./Reports/main');
var Settings = require('./Settings/main');

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