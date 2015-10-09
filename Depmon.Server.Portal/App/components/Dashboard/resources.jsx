var React = require('react');
var rb = require('react-bootstrap');
var PanelGroup = rb.PanelGroup;
var Panel = rb.Panel;

var ResourcesStore = require('../../stores/resources.store');
var acDashboard = require('../../actioncreators/dashboard.cre');

module.exports = React.createClass({
    getInitialState: function () {
        return this.getStateFromStore();
    },

    getStateFromStore: function () {
        return {
            items: ResourcesStore.get()
        }
    },

    updateStateFromStore: function () {
        this.setState(this.getStateFromStore())
    },

    reloadStore: function (props) {
        acDashboard.loadResources(props.params.source, props.params.group);
    },

    componentDidMount: function () {
        ResourcesStore.addChangeListener(this.updateStateFromStore);
        this.reloadStore(this.props);
    },

    componentWillUnmount: function () {
        ResourcesStore.removeChangeListener(this.updateStateFromStore);
    },

    componentWillReceiveProps: function (nextProps) {
        this.reloadStore(nextProps);
    },

    onSelect: function(a, b, c, d, e, f) {

    },

    render: function () {
        return (
            <PanelGroup>
                {this.state.items.map(this.renderItem)}
            </PanelGroup>
        )
    },

    renderItem: function (item) {
        var header = this.renderHeader(item);
        var content = this.renderIndicator(item);
        return (
            <Panel collapsible={true} header={header} onSelect={this.onSelect}>
                {content}
            </Panel>
        )
    },

    renderHeader: function (item) {
        return item.Code;
    },

    renderIndicator: function (item) {

    }
});