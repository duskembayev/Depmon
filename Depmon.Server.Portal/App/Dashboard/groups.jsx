var React = require('react');
var Link = require('react-router').Link;

var rb = require('react-bootstrap');

module.exports = React.createClass({
    render: function () {
        return (
            <div>
                <h1>Groups</h1>
                {this.props.children}
            </div>
        )
    }
});