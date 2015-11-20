import React from 'react';
import Component from '../base/component';
import SideBar from './components/sidebar'
import SourceList from './components/sourceList.js'

export default class HomeView extends Component {
  render () {

    let sourceItems = this.props.sourcesInfo.sources.map((info, index) => {
      return <SourceItem key={index} source={info} />
    });


    return <div id="layout" className="l-layout content pure-g">
      <SideBar sources={this.props.sources}/>
      {sourceItems}
      /*<SourceList sourcesInfo = {this.props.sourcesInfo} />*/
    </div>;
  }
}
