var React = require('react');
var LinkContainer = require('react-router-bootstrap').LinkContainer;
var rb = require('react-bootstrap');
var ListGroup = rb.ListGroup;
var ListGroupItem = rb.ListGroupItem;
var Badge = rb.Badge;
var Grid = rb.Grid;
var Col = rb.Col;

var GroupsStore = require('../../stores/groups');
var acDashboard = require('../../actioncreators/dashboard');

module.exports = React.createClass({
    getInitialState: function () {
        return this.getStateFromStore();
    },

    getStateFromStore: function () {
        return {
            items: GroupsStore.get()
        }
    },

    updateStateFromStore: function () {
        this.setState(this.getStateFromStore())
    },

    reloadStore: function (props) {
        acDashboard.loadGroups(props.params.source);
    },

    componentDidMount: function () {
        GroupsStore.addChangeListener(this.updateStateFromStore);
        this.reloadStore(this.props);
    },

    componentWillUnmount: function () {
        GroupsStore.removeChangeListener(this.updateStateFromStore);
    },

    componentWillReceiveProps: function (nextProps) {
        if (nextProps.params.group == undefined) {
            this.reloadStore(nextProps);
        }
    },

    render: function () {
        return (
<div>
    <Col md={4}>
        {this.state.items.map(this.renderItem)}
    </Col>
    <Col md={8}>
        {this.props.children}
    </Col>
</div>
        )
    },

    renderItem: function (item) {
        var props = {};
        if (item.Level > 25) {
            props.bsStyle = "danger";
        }

        var badge = undefined;
        if (item.BugCount > 0) {
            badge = <Badge>{item.BugCount}</Badge>
        }

        var itemLink = "/sources/" + this.props.params.source + "/groups/" + item.Code;

        return (
            <LinkContainer to={itemLink}>
                <ListGroupItem {...props}>{item.Code} {badge}</ListGroupItem>
            </LinkContainer>
        );
    }
});