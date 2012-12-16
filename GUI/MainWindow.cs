using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using SliceOfPie;
using SliceOfPieClient;


namespace GUI
{
    /**
     * The main screen for the offline client.
     */
    public partial class MainWindow : Form
    {
        private EditWindow editWindow;

        private User activeUser;

        private List<Project> projects;

        private Project selectedProject;
        private Folder selectedFolder;
        private DocumentStruct selectedDocument;
        private bool isDocument;

        public MainWindow()
        {
            InitializeComponent();

            // initialise test data


            /*
            Document doc1 = new Document("content of document 1",
                "Top level document",
                activeUser);
            Document doc2 = new Document("content of document 2",
                "Another Top level document",
                activeUser);
            Document doc3 = new Document("Some more text inside of this doc",
                "A nested document",
                activeUser);
            Document doc4 = new Document("lots\nof\nnew\nlines!",
                "A nested nested document",
                activeUser);
            Document doc5 = new Document("can't think of anything new to put in these",
                "Nested in another folder document",
                activeUser);

            root = new Folder("root");
            Folder folder1 = new Folder("Top level folder");
            Folder folder2 = new Folder("Nested folder");
            Folder folder3 = new Folder("Another top level folder");

            root.AddChild(folder1);
            folder1.AddChild(folder2);
            root.AddChild(folder3);

            root.AddChild(doc1);
            root.AddChild(doc2);
            folder1.AddChild(doc3);
            folder2.AddChild(doc4);
            folder3.AddChild(doc5);
            */
        }

        /**
         * Called when the window is loaded.
         */
        private void MainWindow_Load(object sender, EventArgs e)
        {
            // Ask for who the user is
            InputDialog inputDialog = new InputDialog("Hello and welcome! Who are you? (Input username)",
                "",
                false);
            inputDialog.ShowDialog();
            activeUser = new User(inputDialog.Input);

            userLabel.Text = "Logged in as: " + activeUser.ToString();

            RefreshTreeView();
        }

        /**
         * Recursive function, 
         * filling out the treeView with folders and documents
         * 'tag' is a property referencing the object itself.
         * 
         * Don't add the document to the tree if it is marked for deletion.
         */
        private void BuildDocumentTree(TreeNodeCollection nodes, IFileSystemComponent fsc)
        {

            if (fsc.FileType == SliceOfPie.DocType.Document) // If it's a document
            {
                if (!((DocumentStruct)fsc).Deleted)
                {
                    TreeNode n = new TreeNode(fsc.Title);
                    n.Tag = fsc;
                    nodes.Add(n);
                }
            }
            else // else, if it's a folder
            {
                TreeNode n = new TreeNode(fsc.Title);
                n.Tag = fsc;
                nodes.Add(n);
                SliceOfPie.Folder folder = (SliceOfPie.Folder)fsc;
                foreach (IFileSystemComponent f in (folder.Children))
                {
                    BuildDocumentTree(n.Nodes, f);
                }
            }
        }

        /**
         * Called when something is selected, expanded or collapsed in the treeview
         * Sets the selected folder/document here
         */
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            IFileSystemComponent fsc = (IFileSystemComponent)e.Node.Tag;
            if (fsc.FileType == DocType.Document) // If it's a document
            {
                isDocument = true;
                selectedDocument = (DocumentStruct)fsc;
                selectedFolder = (Folder)e.Node.Parent.Tag;
            }
            else // else, if it's a folder
            {
                isDocument = false;
                selectedFolder = (Folder)e.Node.Tag;
            }

            // If a document was not selected, grey out the button
            if (isDocument)
            {
                openButton.Enabled = true;
            }
            else
            {
                openButton.Enabled = false;
            }

            // You can't rename, move or delete projects in the offline client
            if (selectedFolder.FileType == DocType.Project && !isDocument)
            {
                renameButton.Enabled = false;
                moveButton.Enabled = false;
                deleteButton.Enabled = false;
            }
            else
            {
                renameButton.Enabled = true;
                moveButton.Enabled = true;
                deleteButton.Enabled = true;
            }


            createDocumentButton.Enabled = true;
        }

        /**
         * When the user hits the open document button, make sure that
         * if there's a document already being edited, that it does not contain
         * unsaved changes before switching to a new doc.
         */
        private void openButton_Click(object sender, EventArgs e)
        {
            if (editWindow == null)
            {
                OpenDocument();
            }
            else if (editWindow.Modified)
            {
                MessageBox.Show("There are unsaved changes in the current document. " +
                    "Please save or discard these changes before opening another document.");
            }
            else
            {
                editWindow.Hide();
                OpenDocument();
            }

        }

        /**
         * Open selected doc in new window
         */
        private void OpenDocument()
        {
            editWindow = new EditWindow(selectedProject,
                Controller.OpenDocument(selectedProject.Id, selectedDocument.Id),
                activeUser);
            editWindow.Show();
        }

        private void syncButton_Click(object sender, EventArgs e)
        {
            ServerController.SyncWithServer(selectedProject, activeUser); // probably make a new window for this
            RefreshTreeView();
        }

        /**
         * When the user hits the new doc button, it asks for the name and then
         * saves it to the storage.
         */
        private void createDocumentButton_Click(object sender, EventArgs e)
        {
            // Make sure a destination is selected
            if (treeView.SelectedNode == null)
                MessageBox.Show("Please select destination of new document");
            else
            {
                InputDialog inputDialog = new InputDialog("Input name of new Document");
                inputDialog.ShowDialog();

                if (!inputDialog.Canceled)
                {
                    string title = inputDialog.Input;
                    Document newDoc = new Document("", title, activeUser);

                    // Create a path for the document
                    string path = treeView.SelectedNode.FullPath;
                    if (selectedFolder.FileType == DocType.Project)
                        path = "";
                    else if (isDocument)
                        path = path.Substring(
                            selectedProject.ToString().Count() + 1,
                            path.Count() - selectedDocument.Title.Count() - selectedProject.ToString().Count() - 2);
                    else
                        path = path.Substring(selectedProject.ToString().Count() + 1);



                    path = Regex.Replace(path, @"\\", "/");

                    Controller.CreateDocument(activeUser, path, selectedProject, title);

                    RefreshTreeView();

                }
            }
        }

        private void createFolderButton_Click(object sender, EventArgs e)
        {
            // Make sure a destination is selected
            if (treeView.SelectedNode == null)
                MessageBox.Show("Please select destination of new folder");
            else
            {
                InputDialog inputDialog = new InputDialog("What should your new folder be called?", "name"); 
                inputDialog.ShowDialog(); 
                if (!inputDialog.Canceled) 
                { 
                    if (!CheckName(inputDialog.Input)) 
                        return;  
                     
                    string path; 
                    // If the project root itself is selected, just discard the whole path 
                    if (selectedFolder.FileType == DocType.Project) 
                        path = inputDialog.Input; 
                    else 
                    { 
                        

                        path = treeView.SelectedNode.FullPath; 
                        path = path.Substring(selectedProject.ToString().Count() + 1);

                        if (isDocument)
                            path = path.Substring(0, path.Count() - selectedDocument.Title.Count() - 1);

                        path = Regex.Replace(path, @"\\", "/");

                        path += "/" + inputDialog.Input;
                    }

                    Controller.CreateDocument(activeUser, path, selectedProject, "Welcome to your new folder!");  
                } 
                RefreshTreeView(); 
            }
        }

        private void renameButton_Click(object sender, EventArgs e)
        {
            // If a document was selected
            if (isDocument)
            {
                InputDialog inputDialog = new InputDialog("Input new name for Document", selectedDocument.Title);
                inputDialog.ShowDialog();

                if (!inputDialog.Canceled)
                {
                    Document doc = Controller.OpenDocument(selectedProject.Id, selectedDocument.Id);
                    doc.Title = inputDialog.Input;
                    Controller.SaveDocument(selectedProject, doc, activeUser);
                }
            }
            // or if a folder was selected
            else if (selectedFolder.FileType == DocType.Folder)
            {
                InputDialog inputDialog = new InputDialog("Input new name for Folder", selectedFolder.Title);
                inputDialog.ShowDialog();

                if (!inputDialog.Canceled)
                {
                    if (!CheckName(inputDialog.Input))
                        return;

                    // Build the old and new part of the path for documents
                    string fullpath = treeView.SelectedNode.FullPath;
                    string oldpath = fullpath.Substring(selectedProject.ToString().Count() + 1);
                    oldpath = Regex.Replace(oldpath, @"\\", "/");

                    string newpath = oldpath.Substring(0, oldpath.Count() - selectedFolder.Title.Count());
                    newpath += inputDialog.Input;

                    RenameFolderChildren(selectedFolder, oldpath, newpath);
                }
            }

            RefreshTreeView();

        }

        /**
         * Recursively visit all children in a folder and change their path
         */
        private void RenameFolderChildren(IFileSystemComponent fsc, string oldpath, string newpath)
        {
            if (fsc.FileType == DocType.Document)
            {
                Document doc = Controller.OpenDocument(selectedProject.Id, ((DocumentStruct)fsc).Id);

                //Debug.Print("Old doc path: {0}", doc.Path);

                doc.Path = newpath + doc.Path.Substring(oldpath.Count());

                //Debug.Print("New doc path: {0}", doc.Path);
                //Debug.Print("---");

                Controller.SaveDocument(selectedProject, doc, activeUser);

            }
            else if (fsc.FileType == DocType.Folder)
            {
                foreach (IFileSystemComponent f in ((Folder)fsc).Children)
                {
                    RenameFolderChildren(f, oldpath, newpath);
                }
            }

        }

        private void projectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedProject = (Project)projectBox.SelectedItem;

            // Initialize the treeView with the folders and docs
            treeView.Nodes.Clear();
            BuildDocumentTree(treeView.Nodes, selectedProject);
            treeView.ExpandAll();
        }

        /**
         * Load in the contents of the projects and refresh the treeView
         */
        private void RefreshTreeView()
        {
            openButton.Enabled = false;
            projects = Controller.GetAllProjectsForUser(activeUser);

            // Fill up with projects
            projectBox.Items.Clear();
            foreach (Project p in projects)
            {
                projectBox.Items.Add(p);
                if (selectedProject != null && p.Id == selectedProject.Id)
                    projectBox.SelectedItem = p;
            }
        }
        
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (isDocument)
            {
                DeleteDocument(selectedProject, selectedDocument);
            }

            else if (!(isDocument))
            {
                if (selectedFolder.FileType == DocType.Folder)
                    DeleteFolder(selectedFolder, selectedProject);
            }
            RefreshTreeView();
        }

        /**
         * Recursively call delete on the contents of a folder
         */
        private void DeleteFolder(Folder folder, Project project)
        {
            foreach (IFileSystemComponent component in folder.Children)
            {
                if (component.FileType == DocType.Document)
                {
                    DocumentStruct doc = (DocumentStruct)component;
                    DeleteDocument(selectedProject, ((DocumentStruct)component));
                }
                else if (component.FileType == DocType.Folder)
                {
                    Folder fold = (Folder)component;
                    DeleteFolder(fold, project);
                }
            }
        }

        /**
         * Mark a document for deletion
         */
        private void DeleteDocument(Project p, DocumentStruct d)
        {
            Document doc = Controller.OpenDocument(selectedProject.Id, selectedDocument.Id);
            doc.Deleted = true;
            Controller.SaveDocument(selectedProject, doc, activeUser);
        }

        /**
         * Recursively get a list of names of all the folders in this project
         * (including the project folder)
         */
        private List<string> GetFoldersInFolder(TreeNode node, List<string> folders)
        {
            foreach (TreeNode component in node.Nodes)
            {
                if (((IFileSystemComponent)component.Tag).FileType == DocType.Folder)
                {
                    folders.Add(component.FullPath);
                    GetFoldersInFolder(component, folders);
                }
            }
            return folders;
        }


        private void moveDocument(string path, string id)
        {
            Document doc = Controller.OpenDocument(selectedProject.Id, id);
            doc.Path = path;
            Controller.SaveDocument(selectedProject, doc, activeUser);
        }

        /**
         * Recursively move the contents of a folder to somewhere new
         */
        private void MoveFolder(Folder folder, string path)
        {
            foreach (IFileSystemComponent component in folder.Children)
            {
                if (component.FileType == DocType.Document)
                {
                    DocumentStruct docStruct = (DocumentStruct)component;
                    moveDocument(path, docStruct.Id);
                }
                else if (component.FileType == DocType.Folder)
                {
                    Folder fold = (Folder)component;
                    MoveFolder(fold, path + @"/" + component.Title);
                }

            }
        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            List<string> folders = new List<string>();

            TreeNode projectNode = treeView.Nodes[0];
            
            folders.Add(projectNode.FullPath);

            GetFoldersInFolder(projectNode, folders);


            // Move diffrently depending on whether it's a folder or document being moved
            if (isDocument)
            {
                DropdownDialog<string> folderDialog = new DropdownDialog<string>("Which folder should the document be moved to?", folders, true);
                folderDialog.ShowDialog();
                if (folderDialog.Canceled)
                    return;

                // Figure out new path
                string path = folderDialog.Selected;
                if (selectedProject.ToString() == path)
                    path = "";
                else
                    path = path.Substring(selectedProject.ToString().Count() + 1);
                path = Regex.Replace(path, @"\\", "/");

                moveDocument(path, selectedDocument.Id);
            }
            else
            {
                DropdownDialog<string> folderDialog = new DropdownDialog<string>("Which folder should the folder be moved to?", folders, true);
                folderDialog.ShowDialog();
                if (folderDialog.Canceled)
                    return;

                // Figure out new path
                string path = folderDialog.Selected;
                if (selectedProject.ToString() == path)
                    path = "";
                else
                    path = path.Substring(selectedProject.ToString().Count() + 1);
                path = Regex.Replace(path, @"\\", "/");

                MoveFolder(selectedFolder, path);
            }
            RefreshTreeView();
        }

        /*
         * If the new foldername contains forward/backward slashes, it's going to break
         * our current system, so let's prevent that
         */
        private static bool CheckName(string name)
        {
            if (name.Contains("/") || name.Contains(@"\"))
            {
                MessageBox.Show(@"Folder names cannot contain forward (/) or backward slashes (\)");
                return false;
            }
            return true;
        }

        private void getProjectButton_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                MessageBox.Show("Sorry, something went wrong with contacting the server.");
            }
        }
    }
}
