var React = require('react');
var Link = require('react-router').Link;
var LinkContainer = require('react-router-bootstrap').LinkContainer;
var rb = require('react-bootstrap');

var Navbar = rb.Navbar;
var Nav = rb.Nav;
var NavItem = rb.NavItem;
var Panel = rb.Panel;

module.exports = React.createClass({
    render: function () {
        return (
<div>
    <Navbar brand={<Link to="/">Depmon</Link>}>
        <Nav>
            <LinkContainer to="/reports">
                <NavItem>Reports</NavItem>
            </LinkContainer>
            <LinkContainer to="/settings">
                <NavItem>Settings</NavItem>
            </LinkContainer>
        </Nav>
    </Navbar>
    <Panel>{this.props.children}</Panel>
</div>
        );
    }
});