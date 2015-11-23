import axios from 'axios'


const rootUrl = 'http://localhost:9820/api';

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
}

export default new Api();
