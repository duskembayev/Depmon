import Controller from '../base/controller';
import SourceView from '../views/source';


export default class SourceController extends Controller {
  index (ctx, done) {
    this.renderView(SourceView, done);
  }
}
