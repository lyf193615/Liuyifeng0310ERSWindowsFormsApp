using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liuyifeng0310ERSWindowsFormsApp
{
    public partial class EmployeeRecordsForm : Form
    {
        private TreeNode tvRootNode;
        public EmployeeRecordsForm()
        {
            InitializeComponent();
            PopuLateTreeView();
            initalizeListViem();
        }
        private void PopuLateTreeView()
        {
            statusBarPanel1.Tag = "Refreshing Employee Code. Please Wait...";
            this.Cursor = Cursors.WaitCursor;
            treeView1.Nodes.Clear();
            tvRootNode = new TreeNode("Emplyoee Records");
            this.Cursor= Cursors.Default;
            treeView1.Nodes.Add(tvRootNode);

            TreeNodeCollection nodeCollection = tvRootNode.Nodes;
            XmlTextReader reader = new XmlTextReader("C:\\Users\\lyf\\source\\repos\\Liuyifeng0310ERSWindowsFormsApp\\Liuyifeng0310ERSWindowsFormsApp\\EmpRec.xml");
            reader.MoveToElement();
            try
            {
                while(reader.Read())
                {
                    if(reader.HasAttributes && reader.NodeType == XmlNodeType.Element)
                    {
                        reader.MoveToElement();
                        reader.MoveToElement();

                        reader.MoveToAttribute("Id");
                        String strVal = reader.Value;

                        reader.Read();
                        reader.Read();
                        if(reader.Name == "Dept")
                        {
                            reader.Read();
                        }
                        TreeNode EcodeNode = new TreeNode(strVal);
                        nodeCollection.Add(EcodeNode);
                    }
                }
                statusBarPanel1.Text = "Click on an emplyoee code to see their record.";

            }
            catch (XmlException ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }//end
        protected void initalizeListViem()
        {
            listView1.Items.Clear();
            listView1.Columns.Add("Emplyoee Name",255,HorizontalAlignment.Left);
            listView1.Columns.Add("Date of Join",70,HorizontalAlignment.Right);
            listView1.Columns.Add("Gread",105,HorizontalAlignment.Left);
            listView1.Columns.Add("Salary",105,HorizontalAlignment.Left);
        }
        protected void PopulateListView(TreeNode crrNode)
        {
            initalizeListViem();
            XmlTextReader listRead = new XmlTextReader("C:\\Users\\lyf\\source\\repos\\Liuyifeng0310ERSWindowsFormsApp\\Liuyifeng0310ERSWindowsFormsApp\\EmpRec.xml");
            listRead.MoveToElement();
            while(listRead.Read())
            {
                String strNodeName;
                String strNodePath;
                String name;
                String gread;
                String doj;
                String sal;
                String[] strItemsArr = new string[4];
                listRead.MoveToFirstAttribute();
                strNodeName = listRead.Value;
                strNodePath = crrNode.FullPath.Remove(0, 17);
                if(strNodePath == strNodeName)
                {
                    ListViewItem lvi;
                    listRead.MoveToNextAttribute();
                    name= listRead.Value;
                    lvi =listView1.Items.Add(listRead.Value);

                    listRead.Read();
                    listRead.Read();

                    listRead.MoveToFirstAttribute();
                    doj = listRead.Value;
                    lvi.SubItems.Add(doj);

                    listRead.MoveToNextAttribute() ;
                    gread = listRead.Value;
                    lvi.SubItems.Add(gread);

                    listRead.MoveToNextAttribute() ;
                    sal = listRead.Value;
                    lvi.SubItems.Add(sal);

                    listRead.MoveToNextAttribute() ;
                    listRead.MoveToElement() ;
                    listRead.ReadString();
                }
            }
        }//end

        private void treeViem1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode currNode = e.Node;
            if(tvRootNode==currNode)
            {
                initalizeListViem();
                statusBarPanel1.Text = "Double click the Employee Records";
                return;
            }
            else
            {
                statusBarPanel1.Text = "Click an Employee code to viem individual record";

            }
            PopulateListView(currNode);
        }   
    }
}
