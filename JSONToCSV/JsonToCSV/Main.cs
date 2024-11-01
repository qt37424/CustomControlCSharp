using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace JsonToCSV
{
    /// <summary>
    /// Class Main
    /// </summary>
    public partial class Main : Form
    {
        #region [Variables]

        /// <summary>
        /// List level nodes in TreeView
        /// </summary>
        Dictionary<int, TreeNode> lstLevelNodes = null;

        #endregion

        #region [Constructors]

        /// <summary>
        /// Constructor
        /// </summary>
        public Main()
        {
            InitializeComponent();
            lstLevelNodes = new Dictionary<int, TreeNode>();
        }

        #endregion [Constructors]

        #region [Events]

        #region [Buttons]

        /// <summary>
        /// Event for button convert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void convertBtn_Click_old(object sender, EventArgs e)
        {
            string jsonFilePath = txtboxJSONFile.Text;
            string csvFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "output.csv";

            try
            {
                var elementsIssues = Utility.ReadJSONFile(jsonFilePath)[0]["issues"];

                List<string[]> lstData = new List<string[]>();
                foreach (var issue in (object[])elementsIssues)
                {
                    var issueCast = (Dictionary<string, object>)issue;
                    var inforCommon = (Dictionary<string, object>)issueCast["checkerProperties"];
                    foreach (var eventItem in (object[])issueCast["events"])
                    {
                        List<string> lstItem = new List<string>() {
                            Utility.FormatString(inforCommon["category"].ToString()),
                            Utility.FormatString(inforCommon["impact"].ToString()),
                            Utility.FormatString(inforCommon["cweCategory"].ToString()),
                            Utility.FormatString(inforCommon["subcategoryLocalEffect"].ToString()),
                            Utility.FormatString(inforCommon["subcategoryShortDescription"].ToString()),
                            Utility.FormatString(inforCommon["subcategoryLongDescription"].ToString()),
                        };
                        var itemCast = (Dictionary<string, object>)eventItem;
                        foreach (var item in itemCast.Values)
                        {
                            lstItem.Add(item != null ? Utility.FormatString(item.ToString()) : "null");
                        }
                        lstData.Add(lstItem.ToArray());
                    }
                }

                // Create StringBuilder
                var csvContent = new StringBuilder();
                // add header
                csvContent.AppendLine(string.Join(",", Constant.lstHeader.ToArray()));

                // Add data
                foreach (var item in lstData)
                {
                    csvContent.AppendLine(string.Join(",", item));
                }
                Utility.WriteFileCSV(csvFilePath, csvContent);
            }
            catch (Exception ex)
            {
                Utility.DisplayErrorMessage(string.Format(ConstantMessage.ErrorLoading, ex.ToString()));
            }
        }

        private void convertBtn_Click(object sender, EventArgs e)
        {
            string jsonFilePath = txtboxJSONFile.Text;
            string csvFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "output.csv";

            try
            {
                var itemSelected = GetAllCheckedNodes();

                List<string[]> lstData = new List<string[]>();
                foreach (var issue in itemSelected)
                {

                }

                // Create StringBuilder
                var csvContent = new StringBuilder();
                // add header
                csvContent.AppendLine(string.Join(",", Constant.lstHeader.ToArray()));

                // Add data
                foreach (var item in lstData)
                {
                    csvContent.AppendLine(string.Join(",", item));
                }
                Utility.WriteFileCSV(csvFilePath, csvContent);
            }
            catch (Exception ex)
            {
                Utility.DisplayErrorMessage(string.Format(ConstantMessage.ErrorLoading, ex.ToString()));
            }
        }

        /// <summary>
        /// Input Browse DreamPath click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInputBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtboxJSONFile.Text = openFileDialog.FileName;
                }
            }
        }

        #endregion [Buttons]

        #region [TextBoxs]

        /// <summary>
        /// Check file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtboxJSONFile_TextChanged(object sender, EventArgs e)
        {
            string pathfile = ((TextBox)sender).Text;
            pathfile = pathfile.Replace("\"", "");
            if (!string.IsNullOrEmpty(pathfile) && File.Exists(pathfile))
            {
                try
                {
                    /// In the future, if json file is imported more than 2 elements and 
                    /// item in each element is not equal, there will be bug in here.
                    /// Because of tree structure at here is just get in element 0 of json file.
                    Dictionary<string, object> objects = Utility.ReadJSONFile(pathfile)[0];
                    this.LoadTreeFromListObject(objects);

                    /// ============================== Template code ==============================
                    /// string[] itemArr = new string[] { "Quang", " Trần", " Trịnh" };
                    /// this.LoadTreeFromArray(itemArr);
                    /// ============================== Template code ==============================
                }
                catch (Exception ex)
                {
                    Utility.DisplayErrorMessage(string.Format(ConstantMessage.ErrorLoading, ex.ToString()));
                }
            }
            else if (!string.IsNullOrEmpty(pathfile) && !File.Exists(pathfile))
            {
                Utility.DisplayErrorMessage(ConstantMessage.FileIsNotValid);
            }
        }

        #endregion [TextBoxs]

        #endregion [Events]

        #region [Utilities]

        #region [For TreeNodes]
        
        /// <summary>
        /// Clear node selections
        /// </summary>
        /// <param name="nodes"></param>
        private void ClearNodeSelection(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.BackColor = System.Drawing.SystemColors.Window;
                node.ForeColor = System.Drawing.SystemColors.WindowText;
                if (node.Nodes.Count > 0)
                {
                    ClearNodeSelection(node.Nodes);
                }
            }
        }

        /// <summary>
        /// Get Node between two nodes
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <returns></returns>
        private List<TreeNode> GetNodesBetween(TreeNode startNode, TreeNode endNode)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            List<TreeNode> allNodes = GetAllNodes(treeView.Nodes);

            int startIndex = allNodes.IndexOf(startNode);
            int endIndex = allNodes.IndexOf(endNode);

            if (startIndex > endIndex)
            {
                int temp = startIndex;
                startIndex = endIndex;
                endIndex = temp;
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                nodes.Add(allNodes[i]);
            }

            return nodes;
        }

        /// <summary>
        /// Get All node in TreeNode
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private List<TreeNode> GetAllNodes(TreeNodeCollection nodes)
        {
            List<TreeNode> allNodes = new List<TreeNode>();
            foreach (TreeNode node in nodes)
            {
                allNodes.Add(node);
                if (node.Nodes.Count > 0)
                {
                    allNodes.AddRange(GetAllNodes(node.Nodes));
                }
            }
            return allNodes;
        }

        /// <summary>
        /// Load item into TreeView base on array items
        /// </summary>
        /// <param name="items"></param>
        private void LoadTreeFromArray(string[] items)
        {
            try
            {
                treeView.Nodes.Clear();
                lstLevelNodes.Clear();
                foreach (string item in items)
                {
                    // Get Level base on space character
                    int level = item.Length - item.TrimStart().Length;
                    string nodeName = item.Trim();
                    AddNode(nodeName, level);
                }
            }
            catch (Exception ex)
            {
                Utility.DisplayErrorMessage(String.Format(ConstantMessage.ErrorLoading, ex.ToString()));
            }
        }

        /// <summary>
        /// Load Item in TreeView base on list object
        /// </summary>
        /// <param name="lstObject"></param>
        private void LoadTreeFromListObject(Dictionary<string, object> lstObject)
        {
            try
            {
                treeView.Nodes.Clear();
                lstLevelNodes = new Dictionary<int, TreeNode>();
                lstLevelNodes.Clear();
                LoadObjectWithLevel(lstObject, 0);
            }
            catch (Exception ex)
            {
                Utility.DisplayErrorMessage(String.Format(ConstantMessage.ErrorLoading, ex.ToString()));
            }
        }

        private void LoadObjectWithLevel(Dictionary<string, object> lstObject, int level)
        {
            foreach (var obj in lstObject)
            {
                AddNode(obj.Key, level);
                if (obj.Value != null && obj.Value.GetType().Equals(typeof(object[])))
                {
                    level++;
                    var arrChildObject = ((object[])obj.Value);
                    if (arrChildObject != null && arrChildObject.Length > 0)
                    {
                        var objectConvert = (Dictionary<string, object>)(arrChildObject[0]);
                        LoadObjectWithLevel(objectConvert, level);
                    }
                }
            }
        }

        /// <summary>
        /// Add new node for tree view
        /// </summary>
        /// <param name="newNode"></param>
        /// <param name="level"></param>
        private void AddNode(string nodeName, int level)
        {
            TreeNode newNode = new TreeNode(nodeName);
            if (level == 0)
            {
                treeView.Nodes.Add(newNode);
                lstLevelNodes[level] = newNode;
            }
            else
            {
                if (lstLevelNodes.ContainsKey(level - 1))
                {
                    lstLevelNodes[level - 1].Nodes.Add(newNode);
                    lstLevelNodes[level] = newNode;
                }
            }
        }

        /// <summary>
        /// Get all Checked node
        /// </summary>
        /// <returns></returns>
        private List<TreeNode> GetAllCheckedNodes()
        {
            List<TreeNode> checkedNodes = new List<TreeNode>();
            GetCheckedNodes(treeView.Nodes, checkedNodes);
            return checkedNodes;
        }

        /// <summary>
        /// Get node is checked
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="checkedNodes"></param>
        private void GetCheckedNodes(TreeNodeCollection nodes, List<TreeNode> checkedNodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    checkedNodes.Add(node);
                }
                if (node.Nodes.Count > 0)
                {
                    GetCheckedNodes(node.Nodes, checkedNodes);
                }
            }
        }

        #endregion [For TreeNodes]

        #region [Common]

        #endregion [Common]

        #endregion [Utilities]
    }
}
