using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SliceOfPie;


namespace WebGUI
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            TreeViewSomething();

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {

        }

        protected void TreeViewSomething()
        {
            TreeView1.Nodes.Clear();
            TreeNode parentNode1 = new TreeNode("Parent1");
            TreeNode childNode1 = new TreeNode("Child1");
            TreeNode parentNode2 = new TreeNode("Parent2");
            TreeNode childNode2 = new TreeNode("Child2");
            SliceOfPie.Controller.
            TreeView1.Nodes.Add(parentNode1);

            TreeView1.Nodes[0].ChildNodes.Add(parentNode2);
            TreeView1.Nodes[0].ChildNodes.Add(childNode1);
            TreeView1.Nodes[0].ChildNodes[0].ChildNodes.Add(childNode2);
            


        }


    }
}