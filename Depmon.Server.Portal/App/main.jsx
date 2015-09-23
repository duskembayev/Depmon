var React = require('react');
var Rb = require('react-bootstrap');
var Navbar = Rb.Navbar;

var RootLayout = React.createClass({
    render: function () {
        return (
<div>
    <Navbar brand="Depmon"></Navbar>
</div>);
    }
});

React.render(<RootLayout />, document.getElementById('container'));