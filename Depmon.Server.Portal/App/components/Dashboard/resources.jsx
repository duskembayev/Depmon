var React = require('react');
var rb = require('react-bootstrap');
var PanelGroup = rb.PanelGroup;
var Panel = rb.Panel;

var ResourcesStore = require('../../stores/resources');
var acDashboard = require('../../actioncreators/dashboard');

module.exports = React.createClass({
    getInitialState: function () {
        return this.getStateFromStore();
    },

    getStateFromStore: function () {
        return {
            items: ResourcesStore.get(),
            sourceCode: this.props.params.source,
            groupCode: this.props.params.group
        }
    },

    updateStateFromStore: function () {
        this.setState(this.getStateFromStore())
    },

    componentDidMount: function () {
        ResourcesStore.addChangeListener(this.updateStateFromStore);

        this.updateStateFromStore();
        acDashboard.loadResources(this.state.sourceCode, this.state.groupCode);
    },

    componentWillUnmount: function () {
        ResourcesStore.removeChangeListener(this.updateStateFromStore);
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
            <Panel collapsible={true} header={item} onSelect={this.onSelect}>
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