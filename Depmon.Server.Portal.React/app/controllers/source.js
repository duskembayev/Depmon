import React from 'react';
import Axios from 'axios';
import Controller from '../base/controller';
import HomeView from '../views/home';
import Api from '../modules/api'


export default class SourceController extends Controller {
  index (ctx, done) {
    var self = this;
    Axios.all([
        Api.sources(),
        Api.sourceInfoByCode(ctx.params.sourceCode)
      ])
      .then(Axios.spread(function (sources, sourcesInfo) {
        self.renderView(<HomeView sources={sources.data} sourcesInfo={sourcesInfo.data} />, done);
      }));
  }
}
