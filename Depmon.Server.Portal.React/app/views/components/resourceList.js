import React from 'react';
import Component from '../../base/component';
import ResourceItem from './resourceItem';

export default class ResourceList extends Component {

  render () {
    let resourceItem = this.props.resources.map((info, index) => {
      return <ResourceItem key={index} resource={info} />
    });

    return <div className="c-resource-list pure-u-1">
      {resourceItem}
    </div>
  }
}