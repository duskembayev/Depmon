import React from 'react';
import Component from '../base/component';
import Header from './components/header';
import TreeNode from './components/treeNode';
import Api from '../modules/api'


export default class HomeView extends Component {
  componentDidMount() {
    Api.tree((tree) => {
      this.setState({ tree });
    });

  }

  render () {
    return <div>
      <Header/>
      <TreeNode node={this.state.tree} />
    </div>;
  }
}
