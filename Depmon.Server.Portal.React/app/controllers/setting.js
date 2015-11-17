import Controller from '../base/controller';
import SettingView from '../views/settings';


export default class SettingController extends Controller {
  index (ctx, done) {
    this.renderView(SettingView, done);
  }
}
