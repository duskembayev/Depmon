import React from 'react';
import Component from '../../base/component';
import IndicatorItem from './indicatorItem';

export default class ResourceList extends Component {

  render () {
    let indicatorItem = this.props.indicators.map((info, index) => {
      return <IndicatorItem key={index} indicator={info} />
    });

    return <div className="c-indicator-list pure-u-1">
      {indicatorItem}
    </div>
  }
}