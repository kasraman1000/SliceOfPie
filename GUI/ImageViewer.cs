using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class ImageViewer : Form
    {
        private Image image;

        public ImageViewer(Image image)
        {
            this.image = image;
            InitializeComponent();
        }

        private void ImageViewer_Load(object sender, EventArgs e)
        {
            pictureBox.Image = image;
            this.Size = image.Size;
        }
    }
}
