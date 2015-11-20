import React from 'react';
import Component from '../base/component';
import SideBar from './components/sidebar'
import SourceList from './components/sourceList.js'

export default class SourceView extends Component {

  render () {
    return <div id="layout" className="l-layout content pure-g">
      <SideBar/>
      <SourceList/>
    </div>;
  }
}
