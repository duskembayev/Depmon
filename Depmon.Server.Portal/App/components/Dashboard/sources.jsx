var React = require('react');
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
            items: SourcesStore.get(),
            selected: SourcesStore.getSelected()
        }
    },

    updateStateFromStore: function () {
        this.setState(this.getStateFromStore())
    },

    componentDidMount: function () {
        SourcesStore.addChangeListener(this.updateStateFromStore.bind(this));

        this.updateStateFromStore();
        acDashboard.loadSources();
    },

    componentWillUnmount: function () {
        SourcesStore.removeChangeListener(this.updateStateFromStore.bind(this));
    },

    onSelected:function (item) {

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
        if (this.state.selected === item) {
            props.active = true;
        }

        var badge = undefined;
        if (item.BugCount > 0) {
            badge = <Badge>{item.BugCount}</Badge>
        }

        return <ListGroupItem onClick={function() { this.onSelected(item); }} {...props}>{item.Code} {badge}</ListGroupItem>;
    }
});