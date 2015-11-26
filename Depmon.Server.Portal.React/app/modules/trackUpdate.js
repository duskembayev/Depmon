import Api from './api'

export default class TrackUpdate {
  constructor () {
    this.time = new Date();
  }

  _isNewReportExist() {
    Api.isNewReportExist()
  }
}