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
            ontextchanged="TextBox1_TextChanged" 
            style="margin-left: 279px; margin-right: 472px; margin-top: 26px;" 
            Width="627px" BorderStyle="None" Font-Names="Century Gothic" 
            Font-Size="XX-Large" Font-Strikeout="False" Font-Underline="True" Height="45px">Welcome to SliceOfPie!</asp:TextBox>
    </p>
    <p>
        <asp:TextBox ID="TextBox3" runat="server" BorderStyle="None" Font-Bold="True" 
            Font-Size="Medium" Height="35px" Width="651px"></asp:TextBox>
    </p>
    <asp:Panel ID="Panel1" runat="server" Direction="LeftToRight" Height="381px" 
        HorizontalAlign="Left" ScrollBars="Vertical" style="margin-right: 1013px" 
        Width="345px">
        <asp:TreeView ID="TreeView1" runat="server" Height="375px" 
            ImageSet="XPFileExplorer" NodeIndent="15" 
            onselectednodechanged="TreeView1_SelectedNodeChanged1" 
            style="margin-left: 0px; margin-right: 0px; margin-top: 0px;" 
    Width="280px" ShowLines="True">
            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" 
                HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
            <ParentNodeStyle Font-Bold="False" />
            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" 
                HorizontalPadding="0px" VerticalPadding="0px" />
        </asp:TreeView>
    </asp:Panel>
    <p>
        <asp:Button ID="Button2" runat="server" onclick="Button1_Click" 
            style="margin-left: 59px; margin-top: 0px" Text="Create New Document" 
            Width="164px" />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            style="margin-left: 28px; margin-top: 0px" Text="Open Document" 
            Width="164px" />
    </p>
    <p>
        <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
            style="margin-left: 58px" Text="Change User" Width="165px" />
        <asp:Button ID="Button3" runat="server" onclick="Button1_Click" 
            style="margin-left: 28px; margin-top: 0px; margin-bottom: 0px;" 
            Text="Delete Document" Width="163px" />
    </p>
    <p>
        &nbsp;</p>
    </form>
</body>
</html>
