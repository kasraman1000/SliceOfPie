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
        private Document selectedDocument;
        
        private Folder root;

        public MainWindow()
        {
            InitializeComponent();

            // initialise test data
            User dummyUser = new User("Dummy User");

            Document doc1 = new Document("content of document 1",
                "Top level document",
                dummyUser);
            Document doc2 = new Document("content of document 2",
                "Another Top level document",
                dummyUser);
            Document doc3 = new Document("Some more text inside of this doc",
                "A nested document",
                dummyUser);
            Document doc4 = new Document("lots\nof\nnew\nlines!",
                "A nested nested document",
                dummyUser);
            Document doc5 = new Document("can't think of anything new to put in these",
                "Nested in another folder document",
                dummyUser);

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
        }

        /**
         * Called when the window is loaded
         */
        private void MainWindow_Load(object sender, EventArgs e)
        {
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
                selectedDocument = (Document) fsc;
                selectedFolder = (Folder) e.Node.Parent.Tag;
            }
            else // else, if it's a folder
            {
                selectedDocument = null;
                selectedFolder = (Folder) e.Node.Tag;
            }

            // If a document was not selected, grey out the button
            if (selectedDocument == null)
            {
                openButton.Enabled = false;
            }
            else
            {
                openButton.Enabled = true;
            }


        }

        private void openButton_Click(object sender, EventArgs e)
        {
            if (editWindow == null)
            {
                editWindow = new EditWindow(selectedDocument);
                editWindow.Show();
            }
            else if (editWindow.Modified)
            {
                MessageBox.Show("Please save changes in the Edit Document Window");
            }
            else
            {
                editWindow.Hide();
                editWindow = new EditWindow(selectedDocument);
                editWindow.Show();
            }

        }
    }
}
