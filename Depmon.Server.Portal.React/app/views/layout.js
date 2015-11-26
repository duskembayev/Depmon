import React from 'react';
import Component from '../base/component';
import SideBar from './components/sidebar';
import Notification from './components/notification';

export default class LayoutView extends Component {
  initState () {

  }

  render () {
    return <div id="layout" className="l-layout pure-g">
      <SideBar />
      <Notification />
      {this.props.children}
    </div>;
  }
}
