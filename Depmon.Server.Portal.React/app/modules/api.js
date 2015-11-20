import request from 'request'


const rootUrl = 'http://localhost:9820/api';

class Api {
  sources (callback) {
    let url = `${rootUrl}/drilldown/sources`;
    request(url, (error, response, body) => {
      if(error !== null) {
        throw new Error("Cannot get API");
      }

      var sources =  JSON.parse(body);
      callback(sources);
    })
  }

  sourceInfo (callback) {
    let url = `${rootUrl}/drilldown/sourceinfo`;
    request(url, (error, response, body) => {
      if(error !== null) {
        throw new Error("Cannot get API");
      }

      var info =  JSON.parse(body);
      callback(info);
    })
  }
}

export default new Api();