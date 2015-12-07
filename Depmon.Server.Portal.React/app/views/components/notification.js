import React from 'react';
import Component from '../../base/component';
import Api from '../../modules/api'
import config from '../../config/default'

let tid;

export default class Notification extends Component {
  initState () {
    return {
      updateExists: false,
      lastReportDate: null
    }
  }

  componentDidMount () {
    Api.lastReportDate()
    .then((response) => {
      this.setState({lastReportDate: response.data.lastReportDate});
    })
    .then(() =>{
      this.checkNewReports();
    })
  }

  componentWillUnmount () {
    clearInterval(tid);
  }

  checkNewReports () {
    clearInterval(tid);
    tid = setTimeout(this.checkNewReports.bind(this), config.reportUpdateInterval);

    if (this.state.updateExists) {
      return
    }

    Api.isNewReportExist(this.state.lastReportDate)
      .then((result) => {
        if (result.data.count !== 0) {

          this.setState(
            {
              updateExists: true,
              lastReportDate: result.data.lastReportDate
            }
          )
        }
      })
  }

  render () {
    let { updateExists } = this.state;
    return <div className="pure-u-1">
      {updateExists ? <div className="c-notification">
        <span>Доступен новый отчет. <a href="#" onClick={this.reloadPage}>Обновите страницу.</a></span>
      </div>
        : null}
    </div>
  }

  reloadPage () {
    document.location.href = '/';
  }
}