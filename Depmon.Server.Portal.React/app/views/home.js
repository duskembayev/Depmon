import React from 'react';
import Component from '../base/component';
import Layout from './layout';
import SourceItem from './components/sourceItem';

export default class HomeView extends Component {
  render () {

    let sourceItems = this.props.sourcesInfo.sources.map((info, index) => {
      return <SourceItem key={index} source={info} />
    });

    return  <Layout sources={this.props.sources}>
      {sourceItems}
    </Layout>;
  }
}
