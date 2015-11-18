import request from 'request'


const rootUrl = 'http://localhost:9820/api';

class Api {
  tree (callback) {
    let url = `${rootUrl}/drilldown/tree`;
    request(url, (error, response, body) => {
      if(error !== null) {
        throw new Error("Cannot get API");
      }

      var tree =  JSON.parse(body);
      callback(tree);
    })
  }
}

export default new Api();