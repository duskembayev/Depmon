import React from 'react';
import Component from '../../base/component';
import ResourceItem from './resourceItem'

export default class GroupItem extends Component {

  render () {
    let code = this.props.group.code;
    let resources = this.props.group.resources;

    let resourcesItems = resources.map((info, index) => {
      return <ResourceItem key={index} resource={info} />
    });
    return <div className="c-group-item">
      <div className="c-g-i-heading">
        Группа: {code}
      </div>
      <div className="">{resourcesItems}</div>
    </div>
  }
}