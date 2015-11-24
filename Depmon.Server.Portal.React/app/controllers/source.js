import React from 'react';
import Axios from 'axios';
import Controller from '../base/controller';
import HomeView from '../views/home';
import Api from '../modules/api'


export default class SourceController extends Controller {
  index (ctx, done) {
    Api.sourceInfoByCode(ctx.params.sourceCode)
      .then((sourcesInfo) => {
        this.renderView(<HomeView sourcesInfo={sourcesInfo.data}/>, done);
      });
  }
}
