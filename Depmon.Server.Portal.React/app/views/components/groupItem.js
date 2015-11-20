import React from 'react';
import Component from '../../base/component';
import ResourceList from './resourceList'

export default class GroupItem extends Component {

  render () {
    let code = this.props.group.code;
    let resources = this.props.group.resources;

    return <div className="c-group-item">
      <div className="c-g-i-heading">
        Группа: {code}
      </div>

      <ResourceList resources = {resources}/>
    </div>
  }
}