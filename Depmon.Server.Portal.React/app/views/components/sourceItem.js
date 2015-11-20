import React from 'react';
import Component from '../../base/component';
import GroupList from './groupList'

export default class SourceItem extends Component {

  render () {
    let code = this.props.source.code;
    let groups = this.props.source.groups;

    let groupItems = groups.map((info, index) => {
      return <GroupItem key={index} group={info} />
    });

    return <div className="l-card">
      <div className="l-c-heading">
        {code}
      </div>
      {groupItems}
      /*<GroupList  groups = {groups}/>*/
    </div>
  }
}