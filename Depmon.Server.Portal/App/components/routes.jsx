var React = require('react');
var ReactRouter = require('react-router');
var Router = ReactRouter.Router;
var Route = ReactRouter.Route;
var RouteHandler = ReactRouter.RouteContext;
var IndexRoute = ReactRouter.IndexRoute;

var App = require('./app');

var DashboardStatistic = require('./Dashboard/statistic');
var DashboardSources = require('./Dashboard/sources');
var DashboardGroups = require('./Dashboard/groups');
var DashboardSourceResources = require('./Dashboard/resources');
var DashboardGroupResources = require('./Dashboard/resources');

var Reports = require('./Reports/main');
var Settings = require('./Settings/main');

var RoutesLayout = React.createClass({
    render: function () { return (
        <Router>
            <Route path="/" component={App}>
                <IndexRoute components={{ sidebar: DashboardSources, content: DashboardStatistic }} />
                <Route path="sources/:source" components={{ sidebar: DashboardSources, content: DashboardGroups }}>
                    <IndexRoute component={DashboardSourceResources} />
                    <Route path="groups/:group" component={DashboardGroupResources} />
                </Route>
                <Route path="reports" components={{ content: Reports }} />
                <Route path="settings" components={{ content: Settings }} />
            </Route>
        </Router>
    )}
});


module.exports.init = function () {
    React.render(<RoutesLayout />, document.body);
};
