using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Depmon.Server.Collector.ConfigBuilder.Controls;
using Depmon.Server.Collector.ConfigBuilder.Properties;
using Depmon.Server.Collector.ConfigBuilder.Template;
using Newtonsoft.Json.Linq;

namespace Depmon.Server.Collector.ConfigBuilder.ContextMenus
{
    class JArrayContextMenuStrip: JTokenContextMenuStrip
    {
        protected ToolStripMenuItem ArrayToolStripItem;
        protected ToolStripMenuItem InsertArrayToolStripItem;
        protected ToolStripMenuItem InsertObjectToolStripItem;
        protected ToolStripMenuItem InsertValueToolStripItem;

        protected ToolStripMenuItem InsertGroupToolStripItem;
        protected ToolStripMenuItem InsertResourceToolStripItem;
        protected ToolStripMenuItem InsertIndicatorToolStripItem;

        #region >> Constructors

        public JArrayContextMenuStrip()
        {
            
            ArrayToolStripItem = new ToolStripMenuItem(Resources.JsonArray);
            InsertArrayToolStripItem = new ToolStripMenuItem(Resources.InsertArray, null, InsertArray_Click);
            InsertObjectToolStripItem = new ToolStripMenuItem(Resources.InsertObject, null, InsertObject_Click);
            InsertValueToolStripItem = new ToolStripMenuItem(Resources.InsertValue, null, InsertValue_Click);

            ArrayToolStripItem.DropDownItems.Add(InsertArrayToolStripItem);
            ArrayToolStripItem.DropDownItems.Add(InsertObjectToolStripItem);
            ArrayToolStripItem.DropDownItems.Add(InsertValueToolStripItem);

            TemplateToolStripItem = new ToolStripMenuItem("Templates");
            InsertGroupToolStripItem = new ToolStripMenuItem("Insert Group", null, InsertGroup_Click);
            InsertResourceToolStripItem = new ToolStripMenuItem("Insert Resource", null, InsertResource_Click);
            InsertIndicatorToolStripItem = new ToolStripMenuItem("Insert Indicator", null, InsertIndicator_Click);
            TemplateToolStripItem.DropDownItems.Add(InsertGroupToolStripItem);
            TemplateToolStripItem.DropDownItems.Add(InsertResourceToolStripItem);
            TemplateToolStripItem.DropDownItems.Add(InsertIndicatorToolStripItem);

            Items.Add(ArrayToolStripItem);
            Items.Add(TemplateToolStripItem);
        }

        private void InsertGroup_Click(object sender, EventArgs e)
        {
            InsertJToken(JObject.Parse(JsonTemplate.CreateGroup()));
        }

        private void InsertResource_Click(object sender, EventArgs e)
        {
            InsertJToken(JObject.Parse(JsonTemplate.CreateResource()));
        }

        private void InsertIndicator_Click(object sender, EventArgs e)
        {
            InsertJToken(JObject.Parse(JsonTemplate.CreateIndicator()));
        }

        #endregion


        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible)
            {
                JTokenNode = FindSourceTreeNode<JTokenTreeNode>();

                InsertGroupToolStripItem.Visible = false;
                InsertResourceToolStripItem.Visible = false;
                InsertIndicatorToolStripItem.Visible = false;

                if (JTokenNode.Parent.Text == "objects")
                {
                    InsertGroupToolStripItem.Visible = true;
                }
                else if (JTokenNode.Parent.Text == "resources")
                {
                    InsertResourceToolStripItem.Visible = true;
                }
                else if ((JTokenNode.Parent.Text == "indicators"))
                {
                    InsertIndicatorToolStripItem.Visible = true;
                }

               
            }

            base.OnVisibleChanged(e);
        }

        /// <summary>
        /// Click event handler for <see cref="InsertValueToolStripItem"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertArray_Click(Object sender, EventArgs e)
        {
            InsertJToken(JArray.Parse("[]"));
        }

        /// <summary>
        /// Click event handler for <see cref="InsertValueToolStripItem"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertObject_Click(Object sender, EventArgs e)
        {
            InsertJToken(JObject.Parse("{}"));
        }

        /// <summary>
        /// Click event handler for <see cref="InsertValueToolStripItem"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertValue_Click(Object sender, EventArgs e)
        {
            InsertJToken(JToken.Parse("null"));
        }

        /// <summary>
        /// Add a new <see cref="JToken"/> instance in current <see cref="JArrayTreeNode"/>
        /// </summary>
        /// <param name="newJToken"></param>
        private void InsertJToken(JToken newJToken)
        {
            var jArrayTreeNode = JTokenNode as JArrayTreeNode;

            if (jArrayTreeNode == null)
            {
                return;
            }

            jArrayTreeNode.JArrayTag.AddFirst(newJToken);

            TreeNode newTreeNode = JsonTreeNodeFactory.Create(newJToken);
            jArrayTreeNode.Nodes.Insert(0, newTreeNode);

            jArrayTreeNode.TreeView.SelectedNode = newTreeNode;
        }
    }
}

