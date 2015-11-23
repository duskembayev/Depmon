import React from 'react';
import Component from '../base/component';
import SideBar from './components/sidebar';

export default class LayoutView extends Component {
  render () {
    return <div id="layout" className="l-layout content pure-g">
      <SideBar sources={this.props.sources}/>
      {this.props.children}
    </div>;
  }
}
