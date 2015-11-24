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
    Api.sources().then((sources) => {
      this.setState({sources: sources.data});
    });
    vent.on('route:after', this.setActivePath, this);
  }

  componentWillUnmount () {
    vent.off('route:after', this.setActivePath, this);
  }

  setActivePath (ctx) {
    this.setState({currentPath: ctx.pathname});
  }

  render () {
    let homeUrl = '/';
    let { currentPath, sources } = this.state;
    let sourceLinks = sources.map((source, index) => {
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
          <li>
            <a href={homeUrl} className={this.cx('pure-menu-link', {active: currentPath === homeUrl})}>Главная</a>
          </li>
          <li>
            <a href="/reports" className={this.cx('pure-menu-link', {active: currentPath === '/reports'})}>Отчеты</a>
          </li>
          <li>
            <a href="/settings" className={this.cx('pure-menu-link', {active: currentPath === '/settings'})}>Настройки</a>
          </li>
          <li className="pure-menu-heading">Источники</li>
          {sourceLinks}
        </ul>
      </div>
    </div>
  }
}
