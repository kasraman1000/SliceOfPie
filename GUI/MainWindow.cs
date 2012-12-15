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


namespace GUI
{
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
		 * 'tag' is a property referencing the object per se.
		 */
		private void BuildDocumentTree(TreeNodeCollection nodes, IFileSystemComponent fsc)
		{

			if (fsc.FileType == SliceOfPie.DocType.Document) // If it's a document
			{
				TreeNode n = new TreeNode(fsc.Title);
				n.Tag = fsc;
				nodes.Add(n);
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
            IFileSystemComponent fsc = (IFileSystemComponent) e.Node.Tag;
			if (fsc.FileType == DocType.Document) // If it's a document
			{
				isDocument = true;
				selectedDocument = (DocumentStruct) fsc;
				selectedFolder = (Folder) e.Node.Parent.Tag;
			}
			else // else, if it's a folder
			{
				isDocument = false;
				selectedFolder = (Folder) e.Node.Tag;
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

            // You can't rename, move or delete projects in this client
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

		private void OpenDocument()
		{
			editWindow = new EditWindow(selectedProject, 
				Controller.OpenDocument(selectedProject.Id, selectedDocument.Id), 
				activeUser);
			editWindow.Show();
		}

		private void syncButton_Click(object sender, EventArgs e)
		{
			Controller.SyncWithServer();
		}

		/**
		 * When the user hits the new doc button, it asks for the name and then
		 * should save it to the storage.
		 */
		private void createDocumentButton_Click(object sender, EventArgs e)
		{
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
                    string fullpath = treeView.SelectedNode.FullPath;

                    string path;
                    if (isDocument)
                        path = fullpath.Substring(
                            selectedProject.ToString().Count() + 1,
                            fullpath.Count() - selectedDocument.Title.Count() - selectedProject.ToString().Count() - 2);
                    else if (selectedDocument.FileType == DocType.Folder)
                        path = fullpath.Substring(selectedProject.ToString().Count() + 1);
                    else 
                        path = "";


                    path = Regex.Replace(path, @"\\", "/");

                    Controller.CreateDocument(activeUser, path, selectedProject, title);

                    RefreshTreeView();

                }
            }
		}

		private void createFolderButton_Click(object sender, EventArgs e)
		{


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

                    // If the new foldername contains forward/backward slashes, it's going to break
                    // our current system, so let's prevent that
                    if (inputDialog.Input.Contains("/") || inputDialog.Input.Contains(@"\"))
                    {
                        MessageBox.Show(@"Folder names cannot contain forward (/) or backward slashes (\)");
                        return;
                    }


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
			selectedProject = (Project) projectBox.SelectedItem;

			// Initialize the treeView with the folders and docs
			treeView.Nodes.Clear();
			BuildDocumentTree(treeView.Nodes, selectedProject);
			treeView.ExpandAll();
		}

        /**
         */
		private void RefreshTreeView()
		{
			projects = Controller.GetAllProjectsForUser(activeUser);

            // Fill up with projects
			projectBox.Items.Clear();
			foreach (Project p in projects)
			{
				projectBox.Items.Add(p);
				if (p.Equals(selectedProject))
					projectBox.SelectedItem = p;
			}
		}

        private void DeleteDocument(string projectId, string documentId)
        {
            Controller.DeleteDocument(projectId, documentId);

        }

        private void DeleteFolder(Folder folder, Project project)
        {
            foreach (IFileSystemComponent component in folder.Children)
            {
                if (component.FileType == DocType.Document)
                {
                    DocumentStruct doc = (DocumentStruct)component;
                    DeleteDocument(project.Id, doc.Id);
                }
                else if (component.FileType == DocType.Folder)
                {
                    Folder fold = (Folder)component;
                    DeleteFolder(fold, project);
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (isDocument)
                DeleteDocument(selectedProject.Id, selectedDocument.Id);

            else if (!(isDocument))
            {
                if (selectedFolder.FileType == DocType.Folder)
                    DeleteFolder(selectedFolder, selectedProject);
            }
            Refresh();
        }

        private List<Folder> GetFoldersInFolder(Folder folder, List<Folder> folders)
        {
            foreach (IFileSystemComponent component in folder.Children)
            {
                if (component.FileType == DocType.Folder)
                {
                    folders.Add((Folder)component);
                    List<Folder> nestedFolders = GetFoldersInFolder((Folder)component, folders);
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

        private void MoveFolder(Folder folder ,string path)
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
            List<Folder> folders = new List<Folder>();
            
            GetFoldersInFolder(selectedProject, folders);

            folders.Add(selectedProject);

            if (isDocument)
            {
                DropdownDialog<Folder> folderDialog = new DropdownDialog<Folder>("Which folder should the document be moved to?", folders, true);
                folderDialog.ShowDialog();
                if (folderDialog.Canceled)
                    return;

                Folder folderToMoveTo = folderDialog.Selected;

                string path;

                if (folderToMoveTo == selectedProject)
                    path = "";
                else
                {
                    DocumentStruct neighbour = (DocumentStruct)folderToMoveTo.Children[0];
                    path = neighbour.Path;
                }

                moveDocument(path, selectedDocument.Id);
            }
            else
            {
                DropdownDialog<Folder> folderDialog = new DropdownDialog<Folder>("Which folder should the folder be moved to?", folders, true);
                folderDialog.ShowDialog();
                if (folderDialog.Canceled)
                    return;
                Folder folderToMoveTo = folderDialog.Selected;

                string path;

                if (folderToMoveTo == selectedProject)
                    path = selectedFolder.Title;
                else
                {
                    DocumentStruct neighbour = (DocumentStruct)folderToMoveTo.Children[0];
                    path = neighbour.Path+"/"+selectedFolder.Title;
                }
                MoveFolder(selectedFolder, path);
            }
            RefreshTreeView();
        }
	}
}
