import Controller from '../base/controller';
import HomeView from '../views/home';


export default class HomeController extends Controller {
  index (ctx, done) {
    this.renderView(HomeView, done);
  }
}
