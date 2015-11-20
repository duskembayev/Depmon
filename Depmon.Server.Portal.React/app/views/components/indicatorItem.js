import React from 'react';
import Component from '../../base/component';
import IndicatorItemValue from './indicatorItemValue';

export default class IndicatorItem extends Component {

  render () {
    let code = this.props.indicator.code;
    let values = this.props.indicator.values;

    let indicatorValues = values.map((info, index) => {
      return <IndicatorItemValue key={index} value={info} />
    });

    let qwe = values.map((info, index) => {
      return (
      <span key={index}>{`${info.value} (${info.description})/`}</span>
      )
    });


    return <div className="">
      <span>{`${code}:`} {qwe}</span>
    </div>
  }
}