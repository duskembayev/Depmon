var React = require('react');
var rb = require('react-bootstrap');
var ListGroup = rb.ListGroup;
var ListGroupItem = rb.ListGroupItem;

module.exports = React.createClass({
    render: function () {
        return (
<ListGroup bsStyle="pills" stacked>
    <ListGroupItem>KTZH</ListGroupItem>
    <ListGroupItem>TTK</ListGroupItem>
    <ListGroupItem>SK</ListGroupItem>
    <ListGroupItem>TS</ListGroupItem>
</ListGroup>
        )
    }
});