import React from 'react';
import Component from '../../base/component';
import vent from '../../modules/vent';


export default class Header extends Component{
  initState () {
    return {
      currentPath: '',
    };
  }

  componentDidMount () {
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
    let { currentPath } = this.state;

    return <header className="c-header">

        <div className="c-h-title"><h1>Depmon</h1></div>
        <nav>
          <a href={homeUrl} className={this.cx({active: currentPath === homeUrl })}>Главная</a>
          <a href="/reports" className={this.cx({active: currentPath === '/reports' })}>Отчеты</a>
          <a href="/settings" className={this.cx({active: currentPath === '/settings' })}>Настройки</a>
        </nav>
    </header>;
  }
}
