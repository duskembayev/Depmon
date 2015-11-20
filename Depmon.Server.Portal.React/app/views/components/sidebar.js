import React from 'react';
import vent from '../../modules/vent';
import Component from '../../base/component';
import Api from '../../modules/api'

export default class SideBar extends Component {
  initState () {
    return {
      currentPath: '',
      sources: []
    };
  }

  componentDidMount () {
    vent.on('route:after', this.setActivePath, this);
    //Api.sources((sources) => {
    //  this.setState({sources});
    //  console.log(sources);
    //});
  }

  componentWillUnmount () {
    vent.off('route:after', this.setActivePath, this);
  }

  setActivePath (ctx) {
    this.setState({currentPath: ctx.pathname});
  }

  render () {
    let homeUrl = '/';
    let { currentPath } = this.state;
    let sourceLinks = this.props.sources.map((source, index) => {
      return <li key={index} className="pure-menu-item">
        <a href={`/source/${source.sourceCode}`} className="pure-menu-link">
          <span className="source-label">{source.sourceCode}</span>
          <span className="problem-count">{source.problemCount}</span>
        </a>
      </li>
    });

    return <div className="c-sidebar pure-u">
      <div className="pure-menu">
        <ul className="pure-menu-list">
          <li className="pure-menu-heading">Меню</li>

          <li className={this.cx('pure-menu-item', {active: currentPath === homeUrl})}>
            <a href={homeUrl} className="pure-menu-link">Главная</a>
          </li>
          <li className={this.cx('pure-menu-item', {active: currentPath === '/reports'})}>
            <a href="/reports" className="pure-menu-link">Отчеты</a>
          </li>
          <li className={this.cx('pure-menu-item', {active: currentPath === '/settings'})}>
            <a href="/settings" className="pure-menu-link">Настройки</a>
          </li>
          <li className="pure-menu-heading">Источники</li>

          {sourceLinks}

        </ul>
      </div>
    </div>
  }
}