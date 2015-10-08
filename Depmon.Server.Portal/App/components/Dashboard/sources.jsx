var React = require('react');
var LinkContainer = require('react-router-bootstrap').LinkContainer;
var rb = require('react-bootstrap');
var ListGroup = rb.ListGroup;
var ListGroupItem = rb.ListGroupItem;
var Badge = rb.Badge;

var SourcesStore = require('../../stores/sources');
var acDashboard = require('../../actioncreators/dashboard');

module.exports = React.createClass({
    getInitialState: function () {
        return this.getStateFromStore();
    },

    getStateFromStore: function () {
        return {
            items: SourcesStore.get()
        }
    },

    updateStateFromStore: function () {
        this.setState(this.getStateFromStore())
    },

    reloadStore: function (props) {
        acDashboard.loadSources();
    },

    componentDidMount: function () {
        SourcesStore.addChangeListener(this.updateStateFromStore);
        this.reloadStore(this.props);
    },

    componentWillUnmount: function () {
        SourcesStore.removeChangeListener(this.updateStateFromStore);
    },

    componentWillReceiveProps: function (nextProps) {
        if (nextProps.params.source == undefined) {
            this.reloadStore(nextProps);
        }
    },

    render: function () {
        return (
<ListGroup bsStyle="pills" stacked>
    {this.state.items.map(this.renderItem)}
</ListGroup>
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

        var itemLink = "/sources/" + item.Code;

        return (
            <LinkContainer to={itemLink}>
                <ListGroupItem {...props}>{item.Code} {badge}</ListGroupItem>
            </LinkContainer>
        );
    }
});