import BaseRouter from './base/router';
import vent from './modules/vent';


export default class Router extends BaseRouter {
  router () {
    this.route('/', 'home.index');
    this.route('/settings', 'setting.index');
    this.route('/reports', 'report.index');
    this.route('/source/:sourceCode', 'source.index');
  }
}

Router.prototype.controllers = {
  home: require('./controllers/home'),
  setting: require('./controllers/setting'),
  report: require('./controllers/report'),
  source: require('./controllers/source'),
};
