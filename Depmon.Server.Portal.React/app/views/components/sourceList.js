import React from 'react';
import Component from '../../base/component';
import SourceItem from './sourceItem';

export default class SourceList extends Component {

  render () {
    let sourceItem = this.props.sourcesInfo.sources.map((info, index) => {
      return <SourceItem key={index} source={info} />
    });



    return <div className="pure-u-1">
      {sourceItem}
    </div>
  }
}