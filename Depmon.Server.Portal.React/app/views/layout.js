import React from 'react';
import Component from '../base/component';
import SideBar from './components/sidebar';

export default class LayoutView extends Component {
  render () {
    return <div id="layout" className="l-layout pure-g">
      <SideBar />
      {this.props.children}
    </div>;
  }
}
