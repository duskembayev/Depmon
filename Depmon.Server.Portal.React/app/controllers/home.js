import React from 'react';
import $ from 'jquery';
import Controller from '../base/controller';
import HomeView from '../views/home';
import Api from '../modules/api'


export default class HomeController extends Controller {
  index (ctx, done) {
    //TODO use promise
    Api.sources((sources) => {
      Api.sourceInfo((info) => {
        this.renderView(<HomeView sources={sources} sourcesInfo={info} />, done);
      });
    });
  }
}
