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
        private static List<Project> projects = null;
        private static Project activeProject;
        private static Document activeDoc;
        private static string currentPath;

        static bool firstVisit = true;

        // We make a Dictionary to keep track of which documents belong to which projects by their Id.
        /*
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
        */


        protected void Page_Load(object sender, EventArgs e)
        {
            // Checks if its a postback call
            if (firstVisit == false)
            {
            }
            else if (firstVisit == true)
            {
                firstVisit = false;
                Response.Redirect("WelcomeForm.aspx");

            }

            if (!Page.IsPostBack)
            {
                UpdateProjects();               
            }

        }
        private void UpdateProjects()
        {
            ProjectDropDown.Items.Clear();
            projects = SliceOfPie.Controller.GetAllProjectsForUser(WelcomeForm.active);
            ProjectDropDown.Items.Add("");
            foreach (Project p in projects)
            {
                ListItem li = new ListItem(p.Title, p.Id);
                ProjectDropDown.Items.Add(li);
            }
            AccessTextBox.Text = "Choose one of your projects";
        }



        protected void Button4_Click(object sender, EventArgs e)
        {
            //projectDictionary.Clear();
            Response.Redirect("WelcomeForm.aspx");

        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {



        }
        // NO NEED TO BUILD FULL TREE :(
/*
        protected void BuildFullTree(List<Project> fsc)
        {
            if (fsc.Count != 0)
            {
                AccessTextBox.Text = "Your Accesible projects: ";
                foreach (IFileSystemComponent comp in fsc)
                {
                    BuildTreeView(comp, TreeView1.Nodes);
                }
                Panel1.Visible = true;
            }
            else
            {
                AccessTextBox.Text = "No Accesible projects, why don't you create a new one?";
                Panel1.Visible = false;
            }


        }
*/
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

                    BuildTreeView(f, n.ChildNodes);

                }
            }
        }

        protected void TreeView1_SelectedNodeChanged1(object sender, EventArgs e)
        {

            TreeView tw = (TreeView)sender;
            if (tw.SelectedNode.ChildNodes.Count == 0)
            {
                Document selectedDoc = SliceOfPie.Controller.OpenDocument(activeProject.Id, tw.SelectedNode.Value);
                DocumentTextBox.Text = selectedDoc.Text;
                DocumentNameBox.Text = selectedDoc.Title;
                activeDoc = selectedDoc;
                Panel2.Visible = true;
            }
            else
            {
                FolderBox.Text = tw.SelectedNode.Value;
            }
            string path = tw.SelectedNode.ValuePath;
            currentPath = path.Substring(activeProject.Id.Length + 1);
        
            
   

        }
          

        protected void ChangeUserButton_Click(object sender, EventArgs e)
        {
            //projectDictionary.Clear();
            Response.Redirect("WelcomeForm.aspx");
        }

        protected void NewProjectButton_Click(object sender, EventArgs e)
        {

        }

        protected void DeleteProjectButton_Click(object sender, EventArgs e)
        {

        }

        protected void ShareProjectButton_Click(object sender, EventArgs e)
        {

        }

        protected void RenameProjectButton_Click(object sender, EventArgs e)
        {

        }

        protected void CreateNewDocumentButton_Click(object sender, EventArgs e)
        {
            DynamicPanelInvisible();
            DynamicPanel.Visible = true;
            NewTitleTextbox.Visible = true;
            TitleBox.Visible = true;
            SubmitTitle.Visible = true;
            FolderBox.Visible = true;
            ClickFolderBox.Visible = true;
            CancelCreateButton.Visible = true;
            
        }

        protected void SaveDocumentButton_Click(object sender, EventArgs e)
        {
            activeDoc.Text = DocumentTextBox.Text;
            Controller.SaveDocument(activeProject, activeDoc, WelcomeForm.active);
        }

        protected void DeleteDocumentButton_Click(object sender, EventArgs e)
        {
            DynamicPanelInvisible();
            DynamicPanel.Visible = true;
            AreYouSureBox.Visible = true;
            AcceptDeleteButton.Visible = true;
            DeclineDeleteButton.Visible = true;

        }


        protected void SubmitTitle_Click(object sender, EventArgs e)
        {
            string title = TitleBox.Text;
            DynamicPanelInvisible();
            CreateNewDocument(WelcomeForm.active, currentPath, activeProject, TitleBox.Text);
            UpdateProjects();
        }

        protected string GetPath()
        {

            string path = FolderBox.Text;

            return path;  
        }

        protected void CreateNewDocument(User user, string path, Project proj, string title)
        {
            Controller.CreateDocument(user, path, proj, title);
        }

        protected void ProjectDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            TreeView1.Nodes.Clear();
            //Find project where this is the title
            string pid = ((DropDownList)sender).SelectedValue;

            var project = from p in projects
                           where p.Id == pid
                           select p;

            activeProject = project.FirstOrDefault();
            if (((DropDownList)sender).SelectedItem.Text == (""))
            {
                TreeView1.Nodes.Clear();
            }
            else
            {
                BuildTreeView(project.FirstOrDefault(), TreeView1.Nodes);
            }
        }

        protected void AreYouSureBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void AcceptDeleteButton_Click(object sender, EventArgs e)
        {
            Controller.DeleteDocument(activeProject.Id,activeDoc.Id);
            DynamicPanelInvisible();
            
        }

        protected void DeclineDeleteButton_Click(object sender, EventArgs e)
        {
            DynamicPanelInvisible();
        }

        // Hides every element in dynamic panel
        protected void DynamicPanelInvisible()
        {
            AreYouSureBox.Visible = false;
            AcceptDeleteButton.Visible = false;
            DeclineDeleteButton.Visible = false;
            NewTitleTextbox.Visible = false;
            TitleBox.Visible = false;
            SubmitTitle.Visible = false;
            FolderBox.Visible = false;
            ClickFolderBox.Visible = false;
            CancelCreateButton.Visible = false;

            DynamicPanel.Visible = false;
        }

        protected void CancelCreateButton_Click(object sender, EventArgs e)
        {
            TitleBox.Text = "";
            DynamicPanelInvisible();
        }



        // Change user button redirects to the welcome form.



    }


}
