<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebGUI.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">

        div.Outer 
    {
        width: 1397px;
        position: relative;
        clear: both;
            top: 0px;
            left: 0px;
            height: 717px;
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
<body style="width: 1495px; height: 956px;">
    <form id="form1" runat="server">
    <div class="Outer">
        <div class ="InnerLeft">
    <p>
        &nbsp;</p>
            <p>
                &nbsp;</p>
            <p>
                &nbsp;</p>
            <p>
             <asp:Button ID="ChangeUserButton" runat="server" 
                 onclick="ChangeUserButton_Click" style="margin-left: 12px; margin-top: 0px" 
                 Text="Change User" Width="150px" Height="40px" />
    </p>
    &nbsp;&nbsp;<asp:Label ID="AccessTextBox" runat="server" Font-Bold="True" 
                Font-Size="Medium" Height="35px" Text=" " Width="209px"></asp:Label>
&nbsp;<asp:DropDownList ID="ProjectDropDown" runat="server" Height="19px" 
                onselectedindexchanged="ProjectDropDown_SelectedIndexChanged" 
                style="margin-left: 0px" Width="141px" AutoPostBack="True">
            </asp:DropDownList>
    <br />
   
    <asp:Panel ID="Panel1" runat="server" Direction="LeftToRight" Height="381px" 
        HorizontalAlign="Left" ScrollBars="Vertical" style="margin-right: 1013px; margin-top: 9px;" 
        Width="366px">
        <div style="height: 371px; width: 376px; margin-top: 16px;">
            <asp:TreeView ID="TreeView1" runat="server" Height="356px" 
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
            <asp:Label ID="ConfirmDelete" runat="server" 
                Text="Are you sure you want to delete the Project?" Visible="False"></asp:Label>
            <br />
            <asp:Button ID="ConfirmDeleteProj" runat="server" 
                onclick="ConfirmDeleteProj_Click" Text="Yes" Visible="False" Width="73px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="CancelDeleteProject" runat="server" 
                onclick="CancelDeleteProject_Click" Text="Cancel" Visible="False" 
                Width="73px" />
&nbsp;<asp:Panel ID="ButtonPanel2" runat="server" Direction="LeftToRight" 
                Height="111px" HorizontalAlign="Left" 
                style="margin-right: 0px; margin-left: 0px; margin-top: 31px;" 
                Width="1112px">
                <p>
                    <asp:Button ID="NewProjectButton" runat="server" Height="40px" 
                        style="margin-left: 12px" Text="New Project" Width="150px" 
                        onclick="NewProjectButton_Click" />
                    <asp:Button ID="DeleteProjectButton" runat="server" Height="40px" onclick="DeleteProjectButton_Click" 
                        style="margin-left: 33px; " Text="Delete Project" 
                        Width="150px" />
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
                    <p style="width: 1108px">
                        <asp:Button ID="ShareProjectButton" runat="server" Height="40px" 
                            onclick="ShareProjectButton_Click" style="margin-left: 12px" 
                            Text="Share Project" Width="150px" />
                        <asp:Button ID="RenameProjectButton" runat="server" Height="40px" 
                            onclick="RenameProjectButton_Click" 
                            style="margin-left: 32px; margin-bottom: 1px;" Text="Rename Project" 
                            Width="150px" />
                        <asp:Button ID="CreateNewFolderButton" runat="server" Height="40px" 
                            onclick="CreateNewFolderButton_Click" 
                            style="margin-left: 144px; margin-top: 8px" Text="Create New Folder" 
                            Width="150px" />
                        <asp:Button ID="AddPictureButton" runat="server" Height="40px" 
                            onclick="AddPictureButton_Click" style="margin-left: 68px; margin-top: 8px" 
                            Text="Add a Picture" Width="150px" />
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                </p>
            </asp:Panel>

       
            <div style="height: 198px; width: 376px; margin-top: 9px">
                <asp:Panel ID="DynamicProjectPanel" runat="server" Height="175px" 
                    Visible="False" Width="371px">
                    <asp:Label ID="ProjectNameBox" runat="server" Text="Project name:" 
                        Visible="False"></asp:Label>
                    <asp:TextBox ID="NewProjectNameBox" runat="server" Height="24px" 
                        style="margin-top: 0px; margin-left: 70px;" Width="199px"></asp:TextBox>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="SubmitProjectButton" runat="server" Height="27px" 
                        onclick="SubmitProjectButton_Click" style="margin-left: 102px" Text="Submit" 
                        Visible="False" Width="90px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="CancelProjectButton" runat="server" Height="27px" 
                        onclick="CancelProjectButton_Click" style="margin-left: 0px" Text="Cancel" 
                        Visible="False" Width="90px" />
                    <asp:Panel ID="SharePanel" runat="server" Height="61px" Width="383px">
                        <asp:Label ID="EnterNameBox" runat="server" Text="Enter a users name:" 
                            Visible="False"></asp:Label>
                        <asp:TextBox ID="UserNameBox" runat="server" Height="28px" 
                            style="margin-left: 5px" Visible="False" Width="201px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="SubmitUserButton" runat="server" Height="27px" 
                            onclick="SubmitUserButton_Click" Text="Submit" Visible="False" 
                            Width="90px" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="CancelSharingButton" runat="server" Height="27px" 
                            onclick="CancelProjectButton_Click" Text="Cancel" Visible="False" 
                            Width="90px" />
                    </asp:Panel>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </asp:Panel>
            </div>
            <br />

       
        </div>
        <br />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" 
            Text="Welcome to SliceOfPie!"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        &nbsp;
        <asp:Label ID="DocumentNameBox" runat="server" Font-Bold="True" 
            Font-Size="Medium" Height="44px" Text=" " Width="440px"></asp:Label>
&nbsp;&nbsp;&nbsp;<asp:Button ID="TextButton" runat="server" onclick="TextButton_Click" 
            Text="Text" Visible="False" Width="66px" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="LogButton" runat="server" onclick="LogButton_Click" Text="Log" 
            Visible="False" Width="66px" />
&nbsp;
        <asp:Label ID="ImagesCurrentlyBox" runat="server" 
            Text="Images currently in document:" Visible="False"></asp:Label>
            <asp:TextBox ID="DocumentTextBox" runat="server" Height="384px" 
             TextMode="MultiLine" Width="611px" 
                style="margin-top: 7px" Visible="False"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="ImageBox" runat="server" BorderStyle="None" Enabled="False" 
            EnableTheming="True" Font-Size="Medium" Height="402px" ReadOnly="True" 
            style="margin-top: 1px" TextMode="MultiLine" Visible="False" Width="275px"></asp:TextBox>
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:FileUpload ID="FileUploadControl" runat="server" style="margin-left: 0px; margin-top: 13px;" 
                        Visible="False" Width="221px" Height="22px" />

                    &nbsp;

                    <asp:Button ID="UploadButton" runat="server" onclick="UploadButton_Click" 
                        Text="Upload" Height="23px" Width="61px" Visible="False" />

    </div>
    <div style="height: 202px; width: 1168px; margin-left: 385px">
        <asp:Panel ID="DynamicPanel" runat="server" Height="208px" 
            Width="789px">
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Label ID="ClickFolderBox" runat="server" 
                Text="Click on the Folder you want to add your item to!" Visible="False"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="FolderBox" runat="server" BorderStyle="Dotted" 
                style="margin-top: 0px" Visible="False" Width="180px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br /> &nbsp;
            <asp:Label ID="AreYouSureBox" runat="server" 
                Text="Are you sure you want to delete this document?" Visible="False"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="AcceptDeleteButton" runat="server" 
                onclick="AcceptDeleteButton_Click" Text="Yes" Visible="False" Width="89px" 
                Height="32px" style="margin-left: 0px; margin-top: 0px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:Button ID="DeclineDeleteButton" runat="server" 
                onclick="DeclineDeleteButton_Click" Text="No" Visible="False" Width="89px" 
                Height="32px" />
            <br />
            <asp:Label ID="NewTitleTextBox" runat="server" 
                Text="Enter the title for your new item:" Visible="False"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TitleBox" runat="server" Height="28px" Visible="False" 
                Width="186px" ></asp:TextBox>
            <asp:Button ID="SubmitTitle" runat="server" onclick="SubmitTitle_Click" 
                style="margin-left: 30px" Text="Submit" Visible="False" Width="79px" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="CancelCreateButton" runat="server" 
                onclick="CancelCreateButton_Click" Text="Cancel" Visible="False" Width="72px" />
        </asp:Panel>
    </div>
    </form>
</body>
</html>
