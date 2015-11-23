import React from 'react';
import Component from '../../base/component';
import GroupItem from './groupItem'

export default class SourceItem extends Component {

  render () {
    let code = this.props.source.code;
    let groups = this.props.source.groups;

    let groupItems = groups.map((info, index) => {
      return <GroupItem key={index} group={info} />
    });

    return <div className="l-card pure-u-1">
      <div className="l-c-heading">
        {code}
      </div>
      {groupItems}
    </div>
  }
}