using System.Windows.Forms;
using Depmon.Server.Collector.ConfigBuilder.ContextMenus;
using Depmon.Server.Collector.ConfigBuilder.Generic;
using Newtonsoft.Json.Linq;

namespace Depmon.Server.Collector.ConfigBuilder.Controls
{
    /// <summary>
    /// Specialized <see cref="TreeNode"/> for handling <see cref="JValue"/> representation in a <see cref="TreeView"/>.
    /// </summary>
    sealed class JValueTreeNode : JTokenTreeNode
    {
        #region >> Properties

        public JValue JValueTag
        {
            get { return Tag as JValue; }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JValueTreeNode"/> class.
        /// </summary>
        /// <param name="jValue"></param>
        public JValueTreeNode(JToken jValue)
            : base(jValue)
        {
            ContextMenuStrip = SingleInstanceProvider<JValueContextMenuStrip>.Value;
        }

        #endregion
    }
}
