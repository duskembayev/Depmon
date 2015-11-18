import React from 'react';
import Component from '../../base/component';
import vent from '../../modules/vent';


export default class TreeNode extends Component {
  initState (props) {
    return {
      visible: true
    };
  }

  toggle () {
    this.setState({visible: !this.state.visible});
  }

  render() {
    var childNodes;
    var classObj;

    if (this.props.node.childNodes != null) {
      childNodes = this.props.node.childNodes.map(function(node, index) {
        return <li key={index}><TreeNode node={node} /></li>
      });

      classObj = {
        togglable: true,
        "togglable-down": this.state.visible,
        "togglable-up": !this.state.visible
      };
    }

    var style;
    if (!this.state.visible) {
      style = {display: "none"};
    }

    return (
      <div className="c-tree">
        <h5 onClick={this.toggle.bind(this)} className={this.cx(classObj)}>
          {this.props.node.title}
        </h5>
        <ul style={style}>
          {childNodes}
        </ul>
      </div>
    );
  }
}

TreeNode.defaultProps = {
  node: {}
};
