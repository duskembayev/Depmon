var React = require('react');
var rb = require('react-bootstrap');
var Nav = rb.Nav;
var NavItem = rb.NavItem;

module.exports = React.createClass({
    render: function () {
        return (
<Nav bsStyle="pills" stacked>
    <NavItem>KTZH</NavItem>
    <NavItem>TTK</NavItem>
    <NavItem>SK</NavItem>
    <NavItem>TS</NavItem>
</Nav>
        )
    }
});