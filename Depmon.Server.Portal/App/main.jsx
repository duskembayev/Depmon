var React = require('react');
var ReactRouter = require('react-router');
var Router = ReactRouter.Router;
var Route = ReactRouter.Route;

var App = require('./app');

React.render((
  <Router>
    <Route path="/" component={App} />
  </Router>
), document.body)