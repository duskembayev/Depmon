import React from 'react';
import classnames from 'classnames';

export default class Component extends React.Component {
    constructor (props) {
        super();
        this.refreshState = this.refreshState.bind(this);
        this.state = this.initState(props);
    }

    refreshState () {
        this.setState(this.initState());
    }

    initState () {
        return {};
    }

    title () {
        return "Depmon";
    }

    trigger (eventName, ...args) {
        eventName = `on${_.capitalize(eventName)}`;
        if (this.props[eventName]) this.props[eventName](...args);
    }
}

Component.prototype.cx = classnames;
