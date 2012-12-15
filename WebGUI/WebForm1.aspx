<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebGUI.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">

        div.Outer 
    {
        width: 1275px;
        position: relative;
        clear: both;
            top: 0px;
            left: 0px;
            height: 709px;
        }
    
    div.InnerLeft
    {
        width: 29%;
        position: relative;
        float: left;
            top: 0px;
            left: 0px;
            height: 934px;
        }
    
    div.InnerRight 
    {
        width: 49%;
        position: relative;
        float: right;
    }
    

    </style>



</head>
<body style="width: 1158px">
    <form id="form1" runat="server">
    <div class="Outer">
        <div class ="InnerLeft">
    <p>
        &nbsp;<asp:TextBox ID="WelcomeTextBox" runat="server" Font-Bold="True" 
            style="margin-left: 279px; margin-right: 472px; margin-top: 26px;" 
            Width="627px" BorderStyle="None" Font-Names="Century Gothic" 
            Font-Size="XX-Large" Font-Strikeout="False" Font-Underline="True" Height="45px">Welcome to SliceOfPie!</asp:TextBox>
             <asp:Button ID="ChangeUserButton" runat="server" 
                 onclick="ChangeUserButton_Click" style="margin-left: 12px; margin-top: 0px" 
                 Text="Change User" Width="150px" Height="40px" />
    </p>
        <asp:TextBox ID="AccessTextBox" runat="server" BorderStyle="None" Font-Bold="True" 
            Font-Size="Medium" Height="35px" Width="209px"></asp:TextBox>
    &nbsp;&nbsp;
            <asp:DropDownList ID="ProjectDropDown" runat="server" Height="19px" 
                onselectedindexchanged="ProjectDropDown_SelectedIndexChanged" 
                style="margin-left: 0px" Width="141px" AutoPostBack="True">
            </asp:DropDownList>
    <br />
   
    <asp:Panel ID="Panel1" runat="server" Direction="LeftToRight" Height="381px" 
        HorizontalAlign="Left" ScrollBars="Vertical" style="margin-right: 1013px; margin-top: 9px;" 
        Width="366px">
        <div style="height: 392px; width: 347px; margin-top: 16px;">
            <asp:TreeView ID="TreeView1" runat="server" Height="375px" 
                ImageSet="XPFileExplorer" NodeIndent="15" 
                onselectednodechanged="TreeView1_SelectedNodeChanged1" ShowLines="True" 
                style="margin-left: 0px; margin-right: 0px; margin-top: 12px;" 
                Width="280px">
                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" 
                    HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" 
                    HorizontalPadding="0px" VerticalPadding="0px" />
            </asp:TreeView>
        </div>
     </asp:Panel>
            <br />
            <asp:Panel ID="ButtonPanel2" runat="server" Direction="LeftToRight" 
                Height="227px" HorizontalAlign="Left" 
                style="margin-right: 0px; margin-left: 0px; margin-top: 0px;" 
                Width="380px">
                <p>
                    <asp:Button ID="NewProjectButton" runat="server" Height="40px" 
                        style="margin-left: 12px" Text="New Project" Width="150px" 
                        onclick="NewProjectButton_Click" />
                    <asp:Button ID="DeleteProjectButton" runat="server" Height="40px" onclick="DeleteProjectButton_Click" 
                        style="margin-left: 33px; margin-bottom: 1px;" Text="Delete Project" 
                        Width="150px" />
                </p>
                <p style="width: 378px">
                    <asp:Button ID="ShareProjectButton" runat="server" Height="40px" 
                        style="margin-left: 12px" Text="Share Project" Width="150px" 
                        onclick="ShareProjectButton_Click" />
                    <asp:Button ID="RenameProjectButton" runat="server" Height="40px" onclick="RenameProjectButton_Click" 
                        style="margin-left: 32px; margin-bottom: 1px;" Text="Rename Project" 
                        Width="150px" />
                    <p>
                    </p>
                </p>
            </asp:Panel>

       
        </div>
        <asp:Panel ID="Panel2" runat="server" Height="473px" style="margin-left: 378px; margin-top: 0px;" 
            Width="687px" Visible="False">
            <asp:TextBox ID="DocumentNameBox" runat="server" BorderStyle="None" Font-Bold="True" 
            Font-Size="Medium" Height="35px" Width="651px" 
              style="margin-top: 143px"></asp:TextBox>
            <asp:TextBox ID="DocumentTextBox" runat="server" Height="406px" 
             TextMode="MultiLine" Width="611px" 
                style="margin-top: 7px"></asp:TextBox>
            <asp:Panel ID="ButtonPanel1" runat="server" Direction="LeftToRight" 
                Height="82px" HorizontalAlign="Left" 
                style="margin-right: 1013px; margin-left: 0px; margin-top: 26px;" 
                Width="699px">
                <p style="margin-top: 21px">
                    <asp:Button ID="CreateNewDocumentButton" runat="server" Height="40px" 
                        onclick="CreateNewDocumentButton_Click" 
                        style="margin-left: 29px; margin-top: 0px" Text="Create New Document" 
                        Width="150px" />
                    <asp:Button ID="SaveDocumentButton" runat="server" Height="40px" 
                        onclick="SaveDocumentButton_Click" style="margin-left: 68px" 
                        Text="Save Document" Width="150px" />
                    <asp:Button ID="DeleteDocumentButton" runat="server" Height="40px" 
                        onclick="DeleteDocumentButton_Click" 
                        style="margin-left: 65px; margin-top: 0px; margin-bottom: 0px;" 
                        Text="Delete Document" Width="150px" />
                </p>
            </asp:Panel>
        </asp:Panel>
    </div>
    <div style="height: 138px; width: 764px; margin-left: 392px">
        <div style="height: 47px; width: 757px">
            <asp:TextBox ID="ClickFolderBox" runat="server" BorderStyle="None" 
                Height="21px" style="margin-top: 6px" Visible="False" Width="359px">Click on the Folder you want to add your new document to! </asp:TextBox>
            <asp:TextBox ID="FolderBox" runat="server" BorderStyle="Dotted" 
                style="margin-top: 0px" Visible="False" Width="180px"></asp:TextBox>
        </div>
        <asp:TextBox ID="NewTitleTextbox" runat="server" BorderStyle="None" 
            Height="24px" Visible="False" 
            Width="324px" style="margin-top: 60px">Please enter the title for you new document here:</asp:TextBox>
        <asp:TextBox ID="TitleBox" runat="server" Height="27px" 
            Visible="False" Width="186px"></asp:TextBox>
        <asp:Button ID="SubmitTitle" runat="server" onclick="SubmitTitle_Click" 
            style="margin-left: 30px" Text="Submit" Width="79px" Visible="False" />
    </div>
    </form>
</body>
</html>
