import React from 'react';
import Component from '../base/component';
import SideBar from './components/sidebar'

export default class SourceView extends Component {
  render () {
    return <div id="layout" className="l-layout content pure-g">
      <SideBar/>
    </div>;
  }
}
