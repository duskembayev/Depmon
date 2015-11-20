import React from 'react';
import Component from '../../base/component';
import GroupItem from './groupItem';

export default class GroupList extends Component {

  render () {
    let groupItem = this.props.groups.map((info, index) => {
      return <GroupItem key={index} group={info} />
    });

    return <div className="c-group-list pure-u-1">
      {groupItem}
    </div>
  }
}