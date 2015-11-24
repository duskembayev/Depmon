import React from 'react';
import Axios from 'axios';
import Controller from '../base/controller';
import HomeView from '../views/home';
import Api from '../modules/api'


export default class HomeController extends Controller {
  index (ctx, done) {
    Api.sourceInfo()
    .then((sourcesInfo) => {
      this.renderView(<HomeView sourcesInfo={sourcesInfo.data} />, done);
    });
  }
}
