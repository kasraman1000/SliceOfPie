using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SliceOfPie;


namespace GUI
{
    public partial class MainWindow : Form
    {
        private User activeUser;

        private EditWindow editWindow;
        
        private Folder selectedFolder;
        private DocumentStruct selectedDocument;
        private Project selectedProject;
        private bool isDocument;
        
        private Folder root;

        public MainWindow()
        {
            InitializeComponent();

            // initialise test data
            activeUser = new User("karsten");

            root = Storage.GetHierachy(selectedProject.Id);


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

            // Initialize the treeView with the folders and docs
            BuildDocumentTree(treeView.Nodes, root);


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
                editWindow = new EditWindow(Controller.OpenDocument(selectedProject.Id, selectedDocument.Id));
                editWindow.Show();
            }
            else if (editWindow.Modified)
            {
                MessageBox.Show("There are unsaved changes in the current document. " + 
                    "Please save or discard these changes before opening another document.");
            }
            else
            {
                editWindow.Hide();
                editWindow = new EditWindow(Controller.OpenDocument(selectedProject.Id, selectedDocument.Id));
                editWindow.Show();
            }

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
            InputDialog inputDialog = new InputDialog("Input name of new Document");
            inputDialog.ShowDialog();

            if (!inputDialog.Canceled)
            {
                string title = inputDialog.Input;
                Document newDoc = new Document("", title, activeUser);

                // TODO figure out a way to make a path for the new doc
                // and then send it to Controller.SaveDocument(),
                // maybe possibly reload the hierachy

            }

        }

        private void createFolderButton_Click(object sender, EventArgs e)
        {

        }

    }
}
