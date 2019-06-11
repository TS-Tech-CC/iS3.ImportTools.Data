using iS3.ImportTools.Core.Models;
using iS3.ImportTools.DataStanardTool.StandardManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.ViewManager
{
    public class TreeViewData : System.ComponentModel.INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<TreeNode> treeNodes { get; set; }
        public List<TreeNode> TreeNodes
        {
            get { return treeNodes; }
            set
            {
                treeNodes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TreeNodes"));
            }
        }
        public TreeViewData(Tunnel tunnel, PmEntiretyDef Standard)
        {
            GenerateNodes(tunnel, Standard);
            treeNodes = TreeNodes;
        }
        public TreeViewData()
        {
            treeNodes = new List<TreeNode>();
            TreeNodes = new List<TreeNode>();
        }

        private void GenerateNodes(Tunnel tunnel, PmEntiretyDef Standard)
        {

            List<TreeNode> nodes = new List<TreeNode>();
            int index = 0;
            foreach (Stage stage in tunnel.Stages)
            {
                TreeNode stageTreeNode = new TreeNode() { NodeID = index++, Level = 1, Context = stage.LangStr, isExpanded = true };
                foreach (Category category in stage.Categories)
                {
                    TreeNode categoryTreeNode = new TreeNode() { NodeID = index++, Level = 2, Context = category.LangStr, isExpanded = true };
                    foreach (string obj in category.objList)
                    {
                        PmDGObjectDef dGObject = Standard.GetDGObjectDefByCode(obj);
                        TreeNode objTreeNode = new TreeNode() { NodeID = index++, Level = 3, Context = dGObject.LangStr };
                        categoryTreeNode.ChildNodes.Add(objTreeNode);
                    }
                    stageTreeNode.ChildNodes.Add(categoryTreeNode);
                }
                nodes.Add(stageTreeNode);
            }
            TreeNodes = nodes;
        }
    }
}
