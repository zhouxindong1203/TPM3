using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Z1.tpm;
using NodeType = Z1.tpm.NodeType;

namespace TPM3.zxd.Helper
{
    public static class TreeNodeHelper
    {
        /// <summary>
        /// 拖放操作时测试拖放效果
        /// </summary>
        /// <param name="data_object"></param>
        /// <param name="allow_node_type"></param>
        /// <returns></returns>
        public static DragDropEffects GetIDataObjectDragDropEffect(IDataObject data_object, HashSet<Z1.tpm.NodeType> allow_node_type)
        {
            if (!data_object.GetDataPresent(typeof (TreeNode)))
                return DragDropEffects.None;

            var enter_node = data_object.GetData(typeof (TreeNode)) as TreeNode;
            if (enter_node == null)
                return DragDropEffects.None;

            return GetTreeNodeDragDropEffect(enter_node, allow_node_type);
        }

        public static DragDropEffects GetObjectDragDropEffect(object obj, HashSet<Z1.tpm.NodeType> allow_node_type)
        {
            var node = obj as TreeNode;
            if (node == null)
                return DragDropEffects.None;

            return GetTreeNodeDragDropEffect(node, allow_node_type);
        }

        private static DragDropEffects GetTreeNodeDragDropEffect(TreeNode node, HashSet<Z1.tpm.NodeType> allow_node_type)
        {
            var node_info = node.Tag as NodeTagInfo;
            if (node_info == null)
                return DragDropEffects.None;

            if (allow_node_type == null)
                return DragDropEffects.None;

            return allow_node_type.Contains(node_info.nodeType) ? DragDropEffects.Move : DragDropEffects.None;
        }

        /// <summary>
        /// 获取树节点的测试数据类型
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static NodeType GetNodeType(TreeNode node)
        {
            var node_info = node.Tag as NodeTagInfo;
            return node_info == null ? NodeType.None : node_info.nodeType;
        }

        /// <summary>
        /// 检测移动的树节点是否能够拖放到目标树节点上
        /// </summary>
        /// <param name="move_type"></param>
        /// <param name="target_type"></param>
        /// <returns></returns>
        public static bool CanReceiveDrop(NodeType move_type, NodeType target_type)
        {
            switch (move_type)
            {
                case NodeType.TestCase:
                    return target_type == NodeType.TestItem;
                case NodeType.TestItem:
                    return target_type == NodeType.TestType ||
                           target_type == NodeType.TestItem;

                default:
                    return false;
            }
        }

        /// <summary>
        /// 获取当前树的鼠标位置的节点
        /// </summary>
        /// <param name="tree_obj"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static TreeNode GetCurPosTreeNode(object tree_obj, int x, int y)
        {
            if (tree_obj.GetType() != typeof (TreeView))
                return null;

            var tree = tree_obj as TreeView;
            if (tree == null)
                return null;

            var point = tree.PointToClient(new Point(x, y));
            return tree.GetNodeAt(point);
        }

        /// <summary>
        /// 检测目标节点是否能够接受被移动的节点
        /// </summary>
        /// <param name="moved_node"></param>
        /// <param name="target_node"></param>
        /// <returns></returns>
        public static bool CanReceivePos(TreeNode moved_node, TreeNode target_node)
        {
            return target_node.Parent != moved_node && target_node != moved_node && target_node != moved_node.Parent;
        }
    }
}
