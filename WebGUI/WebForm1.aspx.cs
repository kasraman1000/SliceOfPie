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
        static bool firstVisit = true;

        // We make a Dictionary to keep track of which documents belong to which projects by their Id.
        private static Dictionary<string, string> projectDictionary = new Dictionary<string, string>();
        protected void InitiateDictionary()
        {
            foreach (Project p in projects)
            {
                FillDictionary(p, p.Id, projectDictionary);

            }
        }

        protected void FillDictionary(IFileSystemComponent fsc, string pid, Dictionary<string, string> dic)
        {
            if (fsc.FileType == DocType.Document)
            {
                dic.Add(((DocumentStruct)fsc).Id, pid);
            }
            else
            {
                foreach (IFileSystemComponent f in ((Folder)fsc).Children)
                {
                    FillDictionary(f, pid, dic);
                }
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            // Checks if its a postback call
            if (firstVisit == false)
            {
                  
            }
            else if(firstVisit == true)
            {
                firstVisit = false;
                Response.Redirect("WelcomeForm.aspx");

            }
            if (!Page.IsPostBack)
            {
                projects = SliceOfPie.Controller.GetAllProjectsForUser(WelcomeForm.active);
                BuildFullTree(projects);
                TreeView1.CollapseAll();
                InitiateDictionary();
            }
            
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

        protected void BuildFullTree(List<Project> fsc)
        {
            if (fsc.Count != 0)
            {
                TextBox3.Text = "Your Accesible projects: ";
                foreach (IFileSystemComponent comp in fsc)
                {
                    BuildTreeView(comp, TreeView1.Nodes);
                }
                Panel1.Visible = true;
            }
            else
            {
                TextBox3.Text = "No Accesible projects, why don't you create a new one?";
                Panel1.Visible = false;
            }

            
        }

        protected void BuildTreeView(IFileSystemComponent fsc, TreeNodeCollection nodes)
        {

            
            if (fsc.FileType == SliceOfPie.DocType.Document)
            {
                TreeNode n = new TreeNode(fsc.Title);
                nodes.Add(n);
                n.Value = ((DocumentStruct)fsc).Id;

            }
            else
            {
                TreeNode n = new TreeNode(fsc.Title);
                nodes.Add(n);
                if (fsc.FileType == DocType.Project)
                {
                    n.Value = ((Project)fsc).Id;
                }

            
                SliceOfPie.Folder folder = (SliceOfPie.Folder)fsc;
                foreach (SliceOfPie.IFileSystemComponent f in (folder.Children))
                {
                    
                    BuildTreeView(f,n.ChildNodes);
                    
                }
            }
        }

       

            

        protected void TreeView1_SelectedNodeChanged1(object sender, EventArgs e)
        {
            
            TreeView tw = (TreeView)sender;
            if (tw.SelectedNode.ChildNodes.Count == 0)
            {
                Document selectedDoc = SliceOfPie.Controller.OpenDocument(projectDictionary[tw.SelectedNode.Value], tw.SelectedNode.Value);
                TextBox5.Text = selectedDoc.Text;
                TextBox4.Text = selectedDoc.Title;
                Panel2.Visible = true;
            }
            

        }

        // Change user button redirects to the welcome form.
        protected void Button4_Click(object sender, EventArgs e)
        {
            projectDictionary.Clear();
            Response.Redirect("WelcomeForm.aspx");
        }

        protected void TextBox4_TextChanged1(object sender, EventArgs e)
        {
            
        }

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        }

        


    }
