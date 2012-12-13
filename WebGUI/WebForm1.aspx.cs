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
        private List<Project> projects;
        private User activeUser = new User("Crelde");


        protected void Page_Load(object sender, EventArgs e)
        {
            // Checks if its a postback call
            if (Page.IsPostBack)
            {
                string trwe = "";
            }
            else
            {


            }
        }



        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TreeViewSomething();
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {

        }

        protected void TreeViewSomething()
        {
            TreeView1.Nodes.Clear();
            //TreeNode parentNode1 = new TreeNode("Parent1");
            //TreeNode childNode1 = new TreeNode("Child1");
            //TreeNode parentNode2 = new TreeNode("Parent2");
            //TreeNode childNode2 = new TreeNode("Child2");
            projects = SliceOfPie. Controller.GetAllProjectsForUser(activeUser);
            //TreeView1.Nodes.Add(parentNode1);
            TreeNode parentNode1;


            Int32 x=0;
            foreach (Project myproj in projects)
            {
                parentNode1 = new TreeNode(myproj.Id);
                TreeView1.Nodes.Add(parentNode1);
                //TreeView1.Nodes[x].ChildNodes.Add(parentNode1);
                //x++;
            }


            //TreeView1.Nodes[0].ChildNodes.Add(parentNode2);
            //TreeView1.Nodes[0].ChildNodes.Add(childNode1);
            //TreeView1.Nodes[0].ChildNodes[0].ChildNodes.Add(childNode2);
            


        }

        protected void TreeView1_SelectedNodeChanged1(object sender, EventArgs e)
        {
            TreeView mytreeview = (TreeView)sender;
            string mystring = mytreeview.SelectedValue.ToString();


        }


    }
}