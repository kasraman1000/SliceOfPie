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

        //This method runs everytime the page loads
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if (firstVisit == true)
            {
                UpdateProjects();
                firstVisit = false;
                Response.Redirect("WelcomeForm.aspx");
            }

            // Checks if its a postback call
            if (!Page.IsPostBack)
            {
                UpdateProjects();               
            }

        }
        // A method that is run occasionally after we made changes to a project
        private void UpdateProjects()
        {
            // We empty the dropdownlist with project
            ProjectDropDown.Items.Clear();
            // Get all the projects by the current user
            if (WelcomeForm.active != null)
            {
                using (SliceOfPieClient.Service.SliceOfPieServiceClient serv = new SliceOfPieClient.Service.SliceOfPieServiceClient())
                {
                    projects = serv.GetAllProjectsOnServer(WelcomeForm.active);

                    ProjectDropDown.Items.Add("");
                    foreach (Project p in projects)
                    {
                        ListItem li = new ListItem(p.Title, p.Id);
                        ProjectDropDown.Items.Add(li);
                    }
                    AccessTextBox.Text = "Choose one of your projects";
                }
            }

        }
        
        // Builds the Treeview recursively
        protected void BuildTreeView(IFileSystemComponent fsc, TreeNodeCollection nodes)
        {

            //Checks if its a document
            if (fsc.FileType == SliceOfPie.DocType.Document)
            {
                // Makes a new TreeNode with the correct title and adds it to nodes
                TreeNode n = new TreeNode(fsc.Title);
                nodes.Add(n);
                n.Value = ((DocumentStruct)fsc).Id;

            }
            // else its a folder
            else
            {
                TreeNode n = new TreeNode(fsc.Title);
                nodes.Add(n);
                if (fsc.FileType == DocType.Project)
                {
                    // We save the value of the id for later use
                    n.Value = ((Project)fsc).Id;
                }

                // We make the folder object and check its children recursively
                SliceOfPie.Folder folder = (SliceOfPie.Folder)fsc;
                foreach (SliceOfPie.IFileSystemComponent f in (folder.Children))
                {

                    BuildTreeView(f, n.ChildNodes);

                }
            }
        }

        // Updates the TreeView with the selected project id, as if it was pressed in the dropdown.
        protected void UpdateTreeView(string pid)
        {
            //Clears the previous treeview
            TreeView1.Nodes.Clear();

            //Find project where the project id matches
            
            var project = from p in projects
                          where p.Id == pid
                          select p;

            //keeps track of the project
            activeProject = project.FirstOrDefault();

            // Doesn't show anything if the project id is blank
            if (pid == (""))
            {
                TreeView1.Nodes.Clear();
            }
            else
            {
                BuildTreeView(project.FirstOrDefault(), TreeView1.Nodes);
            }
        }

        // This is what happens when you press on nodes in the TreeView
        protected void TreeView1_SelectedNodeChanged1(object sender, EventArgs e)
        {
            TreeView tw = (TreeView)sender;
            

                 using (SliceOfPieClient.Service.SliceOfPieServiceClient serv = new SliceOfPieClient.Service.SliceOfPieServiceClient())
                 {
                    
                    // Checks if its a document that has been pressed, else do nothing
                    if (tw.SelectedNode.ChildNodes.Count == 0)
                    {
                    // Clears the Text on the Editor
                    ImageBox.Text = "";
                    // Opens the clicked document and sets the properties


                    Document selectedDoc = serv.OpenDocumentOnServer(activeProject.Id, tw.SelectedNode.Value);
                    DocumentTextBox.Text = selectedDoc.Text;
                    DocumentNameBox.Text = selectedDoc.Title;
                    activeDoc = selectedDoc;
                    // If it has images attached it lists them, else it doesnt show the imagebox
                    if (selectedDoc.Images.Count != 0)
                    {
                        UpdateImagePanel();
                        ImagesCurrentlyBox.Visible = true;
                        ImageBox.Visible = true;

                    }
                    DocumentTextBox.Visible = true;
                    TextButton.Visible = true;
                    LogButton.Visible = true;


                    }



                     // Keeps track of the path selected if its a folder
                    else
                    {
                    if (tw.SelectedNode.Parent != null)
                        {
                            string path = tw.SelectedNode.ValuePath;
                            currentPath = path.Substring(activeProject.Id.Length + 1);
                    
                            FolderBox.Text = path.Substring(activeProject.Id.Length + 1);
                    
                        }
                        else
                        {
                            string path = tw.SelectedNode.Text;
                            FolderBox.Text = path;
                            // Keeps track of the current path
                            path = tw.SelectedNode.Text;
                            currentPath = path;
                            
                        }
                
                    }
                ButtonPanel2.Visible = true; 
            }
        }
          
        // The change user button takes the user to the welcome form where he can change user
        protected void ChangeUserButton_Click(object sender, EventArgs e)
        {
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();
            Response.Redirect("WelcomeForm.aspx");
        }

        // The New Project button is called, and new fields is now visible, 
        // Small hack explained: I set the text from Submit to enter, so i dont have to have multiple buttons
        // So i can have one button for both rename project and new project
        // If the value is "Enter", then new project was pressed, if the value is "Submit" Rename Project was pressed
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

        // A Method called very often, it sets all the objects in the Project Panel to invisible
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

        // A similar method that makes all the components for documents and folders invisible 
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

        // Delete Button was pressed and new fields is now visible
        protected void DeleteProjectButton_Click(object sender, EventArgs e)
        {
            if (activeProject != null)
            {
                ConfirmDelete.Visible = true;
                CancelDeleteProject.Visible = true;
                ConfirmDeleteProj.Visible = true;
            }

        }

        // Share Project Button was pressed and new fields are now visible
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

        // Rename Project Button was pressed and new fields are now visible
        // Also the buttons text was changed to Submit, small hack explained above.
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

        // Create New Document Button was pressed and new fields are now visible
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

        // Save Document Button was pressed it calls the savedocument so it is saved on the system
        protected void SaveDocumentButton_Click(object sender, EventArgs e)
        {
            if (activeDoc != null)
            {
                using (SliceOfPieClient.Service.SliceOfPieServiceClient serv = new SliceOfPieClient.Service.SliceOfPieServiceClient())
                {
                    activeDoc.Text = DocumentTextBox.Text;
                    serv.SaveDocumentOnServer(activeProject, activeDoc, WelcomeForm.active);
                }
                UpdateProjects();
                UpdateTreeView(activeProject.Id);
            }
        }

        // Delete Document Button was pressed and new fields are now visible
        protected void DeleteDocumentButton_Click(object sender, EventArgs e)
        {
            if (activeDoc != null)
            {
                DynamicPanel.Visible = true;
                AreYouSureBox.Visible = true;
                AcceptDeleteButton.Visible = true;
                DeclineDeleteButton.Visible = true;
            }
                DynamicPanelInvisible();
                DynamicProjectPanelInvisible();
        }

        // This is the Submit button for creating new documents or folders
        protected void SubmitTitle_Click(object sender, EventArgs e)
        {
            //Checks if there is a folder and a document name
            if (FolderBox.Text.CompareTo("") != 0 || TitleBox.Text.CompareTo("") !=0 || activeProject != null)
                {
                    using (SliceOfPieClient.Service.SliceOfPieServiceClient serv = new SliceOfPieClient.Service.SliceOfPieServiceClient())
                    {
                        // Text value is Submit then its the new document
                        if (SubmitTitle.Text.CompareTo("Submit") == 0)
                        {
                            string title = TitleBox.Text;
                            DynamicPanelInvisible();
                            DynamicProjectPanelInvisible();
                            Document doc = new Document("Insert your text here",TitleBox.Text, currentPath, WelcomeForm.active);
                            serv.SaveDocumentOnServer(activeProject, doc, WelcomeForm.active);
                            // Updates the projects and treeview so it refreshes as it is done
                            UpdateProjects();
                            UpdateTreeView(activeProject.Id);
                        }
                        // Text value is enter so it has to create a folder.
                        else if (SubmitTitle.Text.CompareTo("Enter") == 0)
                        {
                            // Create Document actually creates a folder if given a nonexisting path

                            if (FolderBox.Text.CompareTo(activeProject.Title) == 0)
                            {
                                Document doc = new Document("Insert your text here", "New Document", TitleBox.Text, WelcomeForm.active);
                                serv.SaveDocumentOnServer(activeProject, doc, WelcomeForm.active);
                            }
                            else
                            {
                                Document doc = new Document("Insert your text here","New Document", currentPath +"/"+ TitleBox.Text, WelcomeForm.active);
                                serv.SaveDocumentOnServer(activeProject, doc, WelcomeForm.active);
                            }

                        }
                        UpdateProjects();
                        UpdateTreeView(activeProject.Id);
                    }

                }
                DynamicPanelInvisible();
                DynamicProjectPanelInvisible();
                
       
                // Expands the treeview
                TreeView1.ExpandAll();
        }

        //Called when dropdown element is pressed.
        protected void ProjectDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Gets the id of the selected project
            string pid = ((DropDownList)sender).SelectedValue;
            UpdateTreeView(pid);
            ButtonPanel2.Visible = true;

           
        }

        //The Delete was accepted
        protected void AcceptDeleteButton_Click(object sender, EventArgs e)
        {
            using (SliceOfPieClient.Service.SliceOfPieServiceClient serv = new SliceOfPieClient.Service.SliceOfPieServiceClient())
            {
                serv.DeleteDocument(activeProject.Id, activeDoc.Id, WelcomeForm.active);
                // Turns the panels invisible
                DynamicPanelInvisible();
                DynamicProjectPanelInvisible();
                // Updates the view
                UpdateProjects();
                UpdateTreeView(activeProject.Id);
                // And expands the tree for the user
                TreeView1.ExpandAll();
            }
            
        }

        // The delete call was declined
        protected void DeclineDeleteButton_Click(object sender, EventArgs e)
        {
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();
        }

        // The user cancelled the creating of a new item
        protected void CancelCreateButton_Click(object sender, EventArgs e)
        {
            TitleBox.Text = "";
            DynamicPanelInvisible();
        }

        // Submit Button was pressed for either a new project or rename a project
        protected void SubmitProjectButton_Click(object sender, EventArgs e)
        {
            using (SliceOfPieClient.Service.SliceOfPieServiceClient serv = new SliceOfPieClient.Service.SliceOfPieServiceClient())
            {
                if (activeProject != null)
                {
                    //rename project
                    if (SubmitProjectButton.Text.CompareTo("Submit") == 0)
                    {
                        activeProject.Title = NewProjectNameBox.Text;
                        serv.SaveProjectOnServer(activeProject, WelcomeForm.active);
                        UpdateProjects();
                        UpdateTreeView(activeProject.Id);
                        DynamicPanelInvisible();
                        DynamicProjectPanelInvisible();
                    }
                    //Create new Project
                    else if (SubmitProjectButton.Text.CompareTo("Enter") == 0)
                    {
                        // Empty list for the users the document is shared with
                        List<User> l = new List<User>();
                        Project p = new Project(NewProjectNameBox.Text, WelcomeForm.active, l);
                        serv.SaveProjectOnServer(p, WelcomeForm.active) ;
                    }
                     // Rename a single document
                    else if (SubmitProjectButton.Text.CompareTo("Rename") == 0)
                    {
                        activeDoc.Title = NewProjectNameBox.Text;
                        serv.SaveDocumentOnServer(activeProject ,activeDoc , WelcomeForm.active);
                        UpdateProjects();
                        UpdateTreeView(activeProject.Id);
                        DynamicPanelInvisible();
                        DynamicProjectPanelInvisible();

                    }
                    // Everything turns invisble again and the view is updated
                    UpdateProjects();
                    TreeView1.ExpandAll();
                }
            }
                DynamicPanelInvisible();
                DynamicProjectPanelInvisible();
            
        }

        // The creation of a new item was cancelled
        protected void CancelProjectButton_Click(object sender, EventArgs e)
        {
            DynamicProjectPanelInvisible();
            DynamicPanelInvisible();
        }

        // The add Picture button was pressed and it made the content needed visible
        protected void AddPictureButton_Click(object sender, EventArgs e)
        {
            if (activeDoc != null)
            {
                FileUploadControl.Visible = true;
                UploadButton.Visible = true;
            }
        }

        // The upload picture buttonw as pressed
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            using (SliceOfPieClient.Service.SliceOfPieServiceClient serv = new SliceOfPieClient.Service.SliceOfPieServiceClient())
            {
                //First it check if a file was selected
                if (FileUploadControl.HasFile)
                {
                    // gets the file name
                    string ImgName = System.IO.Path.GetFileName(FileUploadControl.FileName);
                    // It saves the picture on the filesystem
                    string ThisImg = Server.MapPath("~/images/" + ImgName);
                    FileUploadControl.SaveAs(ThisImg);
                    // It creates a new bitmap, and then a new object of our own Picture class
                    Bitmap bm = new Bitmap(@"C:\Users\Crelde\git\SliceOfPie\SliceOfPie\WebGUI\images\" + ImgName);
                    Picture pic = new Picture(bm);
                    // Finally adds the image to the documents imagelist
                    if (activeDoc != null)
                    {
                        activeDoc.Images.Add(pic);
                    }
                }
                // It also saves the document to the server, after the picture is added
                serv.SaveDocumentOnServer(activeProject, activeDoc, WelcomeForm.active);
                // Updates the panel in which you can see which pictures are added, so you can see it as soon as you click upload
                UpdateImagePanel();
            }
       }

        // This method prints out all the ids of the pictures that is included in the document in the panel
        protected void UpdateImagePanel()
        {
            // Takes every picture
            foreach (Picture p in activeDoc.Images)
            {
                // And check if it is already in the imagepanel
                if (!ImageBox.Text.Contains(p.ToString()))
                {
                    // Then it adds it to the imagepanel and includes a new line
                    ImageBox.Text += p.ToString() + "\n";
                }
            }
        }

        // Create new Folder was clicked
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

        // Delete Project was cancelled
        protected void CancelDeleteProject_Click(object sender, EventArgs e)
        {
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();

        }

        // The delete was confirmed and a call to the server is made to delete the activeProject
        protected void ConfirmDeleteProj_Click(object sender, EventArgs e)
        {
            using (SliceOfPieClient.Service.SliceOfPieServiceClient serv = new SliceOfPieClient.Service.SliceOfPieServiceClient())
            {
                serv.DeleteProject(activeProject.Id,WelcomeForm.active);
                UpdateProjects();
                UpdateTreeView("");
                DynamicPanelInvisible();
                DynamicProjectPanelInvisible();
                TreeView1.ExpandAll();
            }
        }

        // The user submitted a username to share the project with
        protected void SubmitUserButton_Click(object sender, EventArgs e)
        {
            using (SliceOfPieClient.Service.SliceOfPieServiceClient serv = new SliceOfPieClient.Service.SliceOfPieServiceClient())
            {
                if (activeProject != null)
                {
                    // creates a new user and adds it to the activeProject and calls the UpdateProject method
                    User u = new User(UserNameBox.Text);
                    activeProject.SharedWith.Add(u);

                    serv.SaveProjectOnServer(activeProject, WelcomeForm.active);
                }
                DynamicPanelInvisible();
                DynamicProjectPanelInvisible();
            }
        }

        protected void TextButton_Click(object sender, EventArgs e)
        {
            DocumentTextBox.Text = activeDoc.Text;
            ButtonPanel2.Visible = true;
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();

        }

        protected void LogButton_Click(object sender, EventArgs e)
        {
            DocumentTextBox.Text = activeDoc.Log.ToString();
            ButtonPanel2.Visible = false;
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();
        }

        protected void MoveButton_Click(object sender, EventArgs e)
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

        }

        protected void RenameDocButton_Click(object sender, EventArgs e)
        {
            DynamicPanelInvisible();
            DynamicProjectPanelInvisible();
            DynamicProjectPanel.Visible = true;
            NewProjectNameBox.Visible = true;
            ProjectNameBox.Visible = true;
            SubmitProjectButton.Text = "Rename";
            SubmitProjectButton.Visible = true;
            CancelProjectButton.Visible = true;


        }

    }

}
