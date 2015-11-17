import React from 'react';
import ReactDOM from 'react-dom';

export default class Controller {
  constructor () {
    this.xhrs = {};
  }

  destroy () {
    for (let key in this.xhrs) {
      if (this.xhrs[key] !== 4) {
        this.xhrs[key].abort();
      }
    }
  }

  renderView (View, callback) {
    let view = ReactDOM.render(<View />, window.appNode, callback);
    window.titleNode.innerText = view.title();
  }
}
