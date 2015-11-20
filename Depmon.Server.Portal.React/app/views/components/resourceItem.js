import React from 'react';
import Component from '../../base/component';
import IndicatorList from './indicatorList'

export default class ResourceItem extends Component {

  render () {
    let code = this.props.resource.code;
    let indicators = this.props.resource.indicators;

    let indicatorItems = indicators.map((info, index) => {
      return <IndicatorItem key={index} indicator={info} />
    });

    return <div className="c-resource-item pure-u-1 pure-u-md-1-2 pure-u-lg-1-3">
      <div className="c-r-i-heading">
        Ресурс: {code}
      </div>

      /*<IndicatorList indicators = {indicators}/>*/
    </div>
  }
}