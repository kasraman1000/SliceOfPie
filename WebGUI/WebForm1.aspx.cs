using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SliceOfPie;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace WebGUI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static List<Project> projects = null;
        public static Project activeProject;
        public static Document activeDoc;
        private static string currentPath;

        static bool firstVisit = true;

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
            TreeView1.ExpandAll();
        }



        protected void Button4_Click(object sender, EventArgs e)
        {

            Response.Redirect("WelcomeForm.aspx");

        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {



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

                    BuildTreeView(f, n.ChildNodes);

                }
            }
        }

        protected void TreeView1_SelectedNodeChanged1(object sender, EventArgs e)
        {

            TreeView tw = (TreeView)sender;
            if (tw.SelectedNode.ChildNodes.Count == 0)
            {
                ImageBox.Text = "";
                Document selectedDoc = SliceOfPie.Controller.OpenDocument(activeProject.Id, tw.SelectedNode.Value);
                DocumentTextBox.Text = selectedDoc.Text;
                DocumentNameBox.Text = selectedDoc.Title;
                activeDoc = selectedDoc;
                if (selectedDoc.Images.Count != 0)
                {
                    UpdateImagePanel();
                    ImagesCurrentlyBox.Visible = true;
                    ImageBox.Visible = true;

                }
                DocumentTextBox.Visible = true;

                activeDoc = selectedDoc;

               
                string path = tw.SelectedNode.ValuePath;
                currentPath = path.Substring(activeProject.Id.Length + 1);

            }
            else
            {
                string path = tw.SelectedNode.ValuePath;
                FolderBox.Text = path.Substring(activeProject.Id.Length + 1);
                
            }

        }
          

        protected void ChangeUserButton_Click(object sender, EventArgs e)
        {
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();
            Response.Redirect("WelcomeForm.aspx");
        }

        protected void NewProjectButton_Click(object sender, EventArgs e)
        {
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();
            DynamicProjectPanel.Visible = true;
            NewProjectNameBox.Visible = true;
            ProjectNameBox.Visible = true;
            SubmitProjectButton.Text = "Enter";
            SubmitProjectButton.Visible = true;
            CancelProjectButton.Visible = true;

        }

        protected void DynamicProjectPanelInvisible()
        {
            DynamicProjectPanel.Visible = false;
            FileUploadControl.Visible = false;
            NewProjectNameBox.Visible = false;
            ProjectNameBox.Visible = false;
            SubmitProjectButton.Visible = false;
            CancelProjectButton.Visible = false;
            SharePanel.Visible = false;
            EnterNameBox.Visible = false;
            SubmitUserButton.Visible = false;
            CancelSharingButton.Visible = false;
            UserNameBox.Visible = false;
            ConfirmDelete.Visible = false;
            CancelDeleteProject.Visible = false;
            ConfirmDeleteProj.Visible = false;

        }

        protected void DeleteProjectButton_Click(object sender, EventArgs e)
        {
            
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();
            ConfirmDelete.Visible = true;
            CancelDeleteProject.Visible = true;
            ConfirmDeleteProj.Visible = true;
        }

        protected void ShareProjectButton_Click(object sender, EventArgs e)
        {
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();
            DynamicProjectPanel.Visible = true;
            EnterNameBox.Visible = true;
            SharePanel.Visible = true;
            SubmitUserButton.Visible = true;
            CancelSharingButton.Visible = true;
            UserNameBox.Visible = true;

        }

        protected void RenameProjectButton_Click(object sender, EventArgs e)
        {
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();
            DynamicProjectPanel.Visible = true;
            NewProjectNameBox.Visible = true;
            ProjectNameBox.Visible = true;
            SubmitProjectButton.Text = "Submit";
            SubmitProjectButton.Visible = true;
            CancelProjectButton.Visible = true;

             
        }

        protected void CreateNewDocumentButton_Click(object sender, EventArgs e)
        {
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();
            DynamicPanel.Visible = true;
            NewTitleTextBox.Visible = true;
            TitleBox.Visible = true;
            SubmitTitle.Visible = true;
            FolderBox.Visible = true;
            ClickFolderBox.Visible = true;
            CancelCreateButton.Visible = true;
            SubmitTitle.Text = "Submit";
            
        }

        protected void SaveDocumentButton_Click(object sender, EventArgs e)
        {
            activeDoc.Text = DocumentTextBox.Text;
            Controller.SaveDocument(activeProject, activeDoc, WelcomeForm.active);
        }

        protected void DeleteDocumentButton_Click(object sender, EventArgs e)
        {
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();
            DynamicPanel.Visible = true;
            AreYouSureBox.Visible = true;
            AcceptDeleteButton.Visible = true;
            DeclineDeleteButton.Visible = true;

        }


        protected void SubmitTitle_Click(object sender, EventArgs e)
        {
            if (SubmitTitle.Text.CompareTo("Submit") == 0)
            {
                string title = TitleBox.Text;
                DynamicPanelInvisible();
                DynamicProjectPanelInvisible();
                CreateNewDocument(WelcomeForm.active, currentPath, activeProject, TitleBox.Text);
                UpdateProjects();
                UpdateTreeView(activeProject.Id);
            }

            else if(SubmitTitle.Text.CompareTo("Enter")==0)
            {

                Controller.CreateDocument(WelcomeForm.active, FolderBox.Text + "/" + TitleBox.Text, activeProject, "New Document");
                DynamicPanelInvisible();
                DynamicProjectPanelInvisible();
                UpdateProjects();
                UpdateTreeView(activeProject.Id);

            }
            TreeView1.ExpandAll();
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
        protected void UpdateTreeView(string pid)
        {

            TreeView1.Nodes.Clear();
            //Find project where this is the title
            

            var project = from p in projects
                          where p.Id == pid
                          select p;

            activeProject = project.FirstOrDefault();
            if (pid == (""))
            {
                TreeView1.Nodes.Clear();
            }
            else
            {
                BuildTreeView(project.FirstOrDefault(), TreeView1.Nodes);
            }
        }
        protected void ProjectDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pid = ((DropDownList)sender).SelectedValue;
            UpdateTreeView(pid);
           
        }

        protected void AcceptDeleteButton_Click(object sender, EventArgs e)
        {
            Controller.DeleteDocument(activeProject.Id,activeDoc.Id);
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();
            UpdateProjects();   
            UpdateTreeView(activeProject.Id);
            TreeView1.ExpandAll();
            
        }

        protected void DeclineDeleteButton_Click(object sender, EventArgs e)
        {
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();
        }

        // Hides every element in dynamic panel
        protected void DynamicPanelInvisible()
        {
            AreYouSureBox.Visible = false;
            AcceptDeleteButton.Visible = false;
            DeclineDeleteButton.Visible = false;
            NewTitleTextBox.Visible = false;
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


        protected void SubmitProjectButton_Click(object sender, EventArgs e)
        {
            if (SubmitProjectButton.Text.CompareTo("Submit") == 0)
            {
                activeProject.Title = NewProjectNameBox.Text;
                Controller.UpdateProject(activeProject);
                UpdateProjects();
                UpdateTreeView(activeProject.Id);
                DynamicPanelInvisible();
                DynamicProjectPanelInvisible();
            }
            else if (SubmitProjectButton.Text.CompareTo("Enter") == 0)
            {
                List<User> l = new List<User>();
                Controller.CreateProject(NewProjectNameBox.Text,WelcomeForm.active,l);
            }
            DynamicProjectPanelInvisible();
            DynamicPanelInvisible();
            UpdateProjects();
            TreeView1.ExpandAll();
        }

        protected void CancelProjectButton_Click(object sender, EventArgs e)
        {
            DynamicProjectPanelInvisible();
            DynamicPanelInvisible();
        }

        protected void EnterNameButton_TextChanged(object sender, EventArgs e)
        {
           
        }

        protected void AddPictureButton_Click(object sender, EventArgs e)
        {
            FileUploadControl.Visible = true;
            UploadButton.Visible = true;

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                string ImgName = System.IO.Path.GetFileName(FileUploadControl.FileName);
                string ThisImg = Server.MapPath("~/images/" + ImgName);
                FileUploadControl.SaveAs(ThisImg);
                string s = FileUploadControl.PostedFile.FileName;
                Bitmap bm = new Bitmap(@"C:\Users\Crelde\git\SliceOfPie\SliceOfPie\WebGUI\images\"+ImgName);
                Picture pic = new Picture(bm);
                activeDoc.Images.Add(pic);                
            }
            Controller.SaveDocument(activeProject, activeDoc, WelcomeForm.active);
            UpdateImagePanel();   
       }
        protected void UpdateImagePanel()
        {
            foreach (Picture p in activeDoc.Images)
            {
                if (!ImageBox.Text.Contains(p.ToString()))
                {
                    ImageBox.Text += p.ToString() + "\n";
                }
            }
        }


        protected void CreateNewFolderButton_Click(object sender, EventArgs e)
        {
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();
            DynamicPanel.Visible = true;
            FolderBox.Visible = true;
            TitleBox.Visible = true;
            ClickFolderBox.Visible = true;
            NewTitleTextBox.Visible = true;
            SubmitTitle.Visible = true;
            CancelCreateButton.Visible = true;
            SubmitTitle.Text = "Enter";
        }

        protected void CancelDeleteProject_Click(object sender, EventArgs e)
        {
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();

        }

        protected void ConfirmDeleteProj_Click(object sender, EventArgs e)
        {
            Controller.DeleteProject(activeProject.Id);
            UpdateProjects();
            UpdateTreeView("");
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();
            TreeView1.ExpandAll();
        }

        protected void SubmitUserButton_Click(object sender, EventArgs e)
        {
            User u = new User(UserNameBox.Text);
            activeProject.SharedWith.Add(u);
            Controller.UpdateProject(activeProject);
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();

        }

        protected void ClickFolderBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TitleBox_TextChanged(object sender, EventArgs e)
        {

        } 




    }


}
