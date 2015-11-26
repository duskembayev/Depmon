import React from 'react';
import Component from '../../base/component';
import Api from '../../modules/api'

let tid;

export default class Notification extends Component {
  initState () {
    return {
      updateExists: false,
      lastTimeCheck: new Date(),
    }
  }

  componentDidMount () {
    this.checkNewReports();
  }

  componentWillUnmount () {
    clearInterval(tid);
  }

  checkNewReports () {
    clearInterval(tid);
    tid = setTimeout(this.checkNewReports.bind(this), 15000)

    if(this.state.updateExists) {
      return
    }

    Api.isNewReportExist(this.state.lastTimeCheck.toJSON())
    .then((result) => {
      if(result.data.count !== 0) {
        this.setState({updateExists: true})
      }
      this.setState({lastTimeCheck: new Date()})
    })
  }

  render () {
    let { updateExists } = this.state;
    return <div className="c-notification pure-u-1">
      {updateExists ? <span>Доступен новый отчет. Обновите страницу.</span> : null}
    </div>
  }
}