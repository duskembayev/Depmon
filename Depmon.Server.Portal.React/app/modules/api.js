import axios from 'axios'


const rootUrl = '/api';

class Api {
  sources () {
    let url = `${rootUrl}/drilldown/sources`;
    return axios.get(url);
  }

  sourceInfo () {
    let url = `${rootUrl}/drilldown/sourceinfo`;
    return axios.get(url);
  }

  sourceInfoByCode (sourceCode) {
    let url = `${rootUrl}/drilldown/sourceinfo?sourceCode=${sourceCode}`;
    return axios.get(url);
  }

  isNewReportExist(dateTime) {
    let url = `${rootUrl}/drilldown/IsNewReportExist?dateTime=${dateTime}`;
    return axios.get(url);
  }

  lastReportDate () {
    let url = `${rootUrl}/drilldown/GetLastReportDate`;
    return axios.get(url);
  }
}

export default new Api();
