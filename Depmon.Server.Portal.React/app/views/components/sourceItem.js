import React from 'react';
import moment from 'moment';
import Component from '../../base/component';
import GroupItem from './groupItem';
import config from '../../config/default'

export default class SourceItem extends Component {

  render () {
    let code = this.props.source.code;
    let createdAt = moment(this.props.source.createdAt);
    let groups = this.props.source.groups;

    let groupItems = groups.map((info, index) => {
      return <GroupItem key={index} group={info} />
    });

    let dateDiff = moment() - createdAt;
    let isOldReport =  moment.duration(dateDiff).asHours() > config.oldReportThreshold;

    return <div className="l-card pure-u-1">
      <div className={this.cx('l-c-heading',{'l-c-h-old': isOldReport})}>
        <span className="l-c-h-time">{createdAt.format('DD.MM.YY HH:mm:ss')}</span>
        {code}
      </div>
      {groupItems}
    </div>
  }
}