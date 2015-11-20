import React from 'react';
import vent from '../../modules/vent';
import Component from '../../base/component';
import Api from '../../modules/api'
import SideBar from './components/sidebar'
import SourceList from './components/sourceList.js'

export default class LayoutView extends Component {
  initState () {
    return {
      sourcesInfo: [],
      currentPath: '',
    };
  }

  componentDidMount () {
    vent.on('route:after', this.setActivePath, this);
    Api.sources((sources) => {
      this.setState({sources});
      console.log(sources);
    });
  }

  componentWillUnmount () {
    vent.off('route:after', this.setActivePath, this);
  }

  setActivePath (ctx) {
    this.setState({currentPath: ctx.pathname});
  }


  render () {
    return <div id="layout" className="l-layout content pure-g">
      <SideBar/>
      <SourceList/>
    </div>;
  }
}
