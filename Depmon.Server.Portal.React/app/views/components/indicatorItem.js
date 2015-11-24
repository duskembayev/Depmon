import React from 'react';
import Component from '../../base/component';

export default class IndicatorItem extends Component {

  render () {
    let code = this.props.indicator.code;
    let values = this.props.indicator.values;

    let items = values.map((info, index) => {
      let st =  this.cx('c-i-i-value',{
      	'level-normal': info.level === 25,
        'level-warning': info.level === 50,
        'level-error': info.level === 75,
        'level-critical': info.level === 75
      });

      return (
      <span className={st} key={index}>{`${info.value} (${info.description})`}</span>
      )
    });


    return <div className="c-indicator-item">
      <span className="c-i-i-value-list">{`${code}:`} {items}</span>
    </div>
  }
}
