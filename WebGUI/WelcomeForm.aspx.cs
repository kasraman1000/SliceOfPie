using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SliceOfPie;

namespace WebGUI
{
    public partial class WelcomeForm : System.Web.UI.Page
    {
        public static User active;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UserTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            active = new User(UserTextBox.Text);
            Response.Redirect("WebForm1.aspx");
        }
    }
}