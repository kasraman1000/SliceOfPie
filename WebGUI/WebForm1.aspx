<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebGUI.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <p>
        <asp:TextBox ID="TextBox1" runat="server" Font-Bold="True" 
            ontextchanged="TextBox1_TextChanged" style="margin-left: 501px" Width="198px">Hello and welcome!!</asp:TextBox>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:TreeView ID="TreeView1" runat="server" Height="233px" 
            ImageSet="XPFileExplorer" NodeIndent="15" 
            onselectednodechanged="TreeView1_SelectedNodeChanged1" 
            style="margin-left: 434px" Width="245px">
            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" 
                HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
            <ParentNodeStyle Font-Bold="False" />
            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" 
                HorizontalPadding="0px" VerticalPadding="0px" />
        </asp:TreeView>
        <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="Button2" runat="server" onclick="Button1_Click" 
            style="margin-left: 244px; margin-top: 49px" Text="Create New Document" 
            Width="164px" />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            style="margin-left: 28px; margin-top: 49px" Text="Open Document" 
            Width="156px" />
        <asp:Button ID="Button3" runat="server" onclick="Button1_Click" 
            style="margin-left: 31px; margin-top: 49px" Text="Delete Document" />
    </p>
    <p>
        &nbsp;</p>
    </form>
</body>
</html>
