var RootLayout = React.createClass({
    render: function () {
        return (
<div>
    <header></header>
    <main>Hello world!</main>
    <footer></footer>
</div>);
    }
});

React.render(<RootLayout />, document.body);