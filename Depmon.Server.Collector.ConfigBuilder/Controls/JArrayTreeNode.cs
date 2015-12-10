using System.Windows.Forms;
using Depmon.Server.Collector.ConfigBuilder.ContextMenus;
using Depmon.Server.Collector.ConfigBuilder.Generic;
using Newtonsoft.Json.Linq;

namespace Depmon.Server.Collector.ConfigBuilder.Controls
{
    /// <summary>
    /// Specialized <see cref="TreeNode"/> for handling <see cref="JArray"/> representation in a <see cref="TreeView"/>.
    /// </summary>
    sealed class JArrayTreeNode : JTokenTreeNode
    {
        #region >> Properties

        public JArray JArrayTag
        {
            get { return Tag as JArray; }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JArrayTreeNode"/> class.
        /// </summary>
        public JArrayTreeNode(JArray jArray)
            : base(jArray)
        {
            ContextMenuStrip = SingleInstanceProvider<JArrayContextMenuStrip>.Value;
        }

        #endregion

        #region >> JTokenTreeNode

        /// <inheritdoc />
        public override void AfterCollapse()
        {
            Text = "[" + JArrayTag.Type + "] " + Tag;
        }

        /// <inheritdoc />
        public override void AfterExpand()
        {
            Text = "[" + JArrayTag.Type + "]";
        }

        #endregion
    }
}
