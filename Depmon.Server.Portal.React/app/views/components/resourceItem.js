import React from 'react';
import Component from '../../base/component';
import IndicatorItem from './indicatorItem'

export default class ResourceItem extends Component {

  render () {
    let code = this.props.resource.code;
    let indicators = this.props.resource.indicators;

    let indicatorItems = indicators.map((info, index) => {
      return <IndicatorItem key={index} indicator={info} />
    });

    return <div className="c-resource-item">
      <div className="c-r-i-heading">
        Ресурс: {code}
      </div>
     {indicatorItems}
    </div>
  }
}