var React = require('react');
var Link = require('react-router').Link;
var LinkContainer = require('react-router-bootstrap').LinkContainer;

var rb = require('react-bootstrap');
var Navbar = rb.Navbar;
var Nav = rb.Nav;
var NavItem = rb.NavItem;
var Grid = rb.Grid;
var Col = rb.Col;

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
    <Grid fluid={true}>
        <Col xs={3}>
            {this.props.children.sidebar}
        </Col>
        <Col xs={9}>
            {this.props.children.content}
        </Col>
    </Grid>
</div>
        );
    }
});