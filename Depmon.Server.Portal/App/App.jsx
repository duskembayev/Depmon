var React = require('react');
var rb = require('react-bootstrap');

var Navbar = rb.Navbar;

var App = React.createClass({
    render : function(){
        return (
<Navbar brand='Depmon'>
</Navbar>
        );
    }
});

module.exports = App;