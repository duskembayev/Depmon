var React = require('react');

var RootLayout = React.createClass({
    render: function () {
        return (<div>Hello world!</div>);
    }
});

React.render(<RootLayout />, document.getElementById('container'));