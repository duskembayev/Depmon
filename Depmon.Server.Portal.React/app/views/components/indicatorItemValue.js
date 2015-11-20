import React from 'react';
import Component from '../../base/component';

export default class IndicatorItemValue extends Component {

  render () {
    return <div className="">
      <table className="pure-table">
        <thead>
        <tr>
          <th>Значение</th>
          <th>Описание</th>
          <th>Уровень</th>
        </tr>
        </thead>

        <tbody>
        <tr>
          <td>{this.props.value.value}</td>
          <td>{this.props.value.description}</td>
          <td>{this.props.value.level}</td>
        </tr>
        </tbody>
      </table>
    </div>
  }
}