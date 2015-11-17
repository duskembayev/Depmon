import Controller from '../base/controller';
import ReportView from '../views/report';


export default class ReportController extends Controller {
  index (ctx, done) {
    this.renderView(ReportView, done);
  }
}
