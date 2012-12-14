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
            height: 828px;
        }
    
    div.InnerLeft
    {
        width: 105%;
        position: relative;
        float: left;
            top: 0px;
            left: 0px;
            height: 127px;
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
    <p>
        &nbsp;</p>
    <div class="Outer">
        <div class ="InnerLeft">
    <p>
        <asp:TextBox ID="TextBox1" runat="server" Font-Bold="True" 
            ontextchanged="TextBox1_TextChanged" 
            style="margin-left: 279px; margin-right: 472px; margin-top: 26px;" 
            Width="627px" BorderStyle="None" Font-Names="Century Gothic" 
            Font-Size="XX-Large" Font-Strikeout="False" Font-Underline="True" Height="45px">Welcome to SliceOfPie!</asp:TextBox>
    </p>
        <asp:TextBox ID="TextBox3" runat="server" BorderStyle="None" Font-Bold="True" 
            Font-Size="Medium" Height="35px" Width="589px"></asp:TextBox>
    <br />
   
    <asp:Panel ID="Panel1" runat="server" Direction="LeftToRight" Height="381px" 
        HorizontalAlign="Left" ScrollBars="Vertical" style="margin-right: 1013px" 
        Width="366px">
        <asp:TreeView ID="TreeView1" runat="server" Height="375px" 
            ImageSet="XPFileExplorer" NodeIndent="15" 
            onselectednodechanged="TreeView1_SelectedNodeChanged1" ShowLines="True" 
            style="margin-left: 0px; margin-right: 0px; margin-top: 0px;" Width="280px">
            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" 
                HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
            <ParentNodeStyle Font-Bold="False" />
            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" 
                HorizontalPadding="0px" VerticalPadding="0px" />
        </asp:TreeView>
     </asp:Panel>
     <asp:Panel ID="ButtonPanel1" runat="server" Direction="LeftToRight" Height="381px"
             HorizontalAlign="Left" style="margin-right: 1013px" Width="372px">
    <p>
        <asp:Button ID="Button2" runat="server" onclick="Button1_Click" 
            style="margin-left: 12px; margin-top: 0px" Text="Create New Document" 
            Width="164px" />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            style="margin-left: 28px; margin-top: 0px" Text="Open Document" 
            Width="164px" />
    </p>

    <p style="width: 410px">
        <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
            style="margin-left: 12px" Text="Change User" Width="165px" />
        <asp:Button ID="Button3" runat="server" onclick="Button1_Click" 
            style="margin-left: 28px; margin-top: 0px; margin-bottom: 0px;" 
            Text="Delete Document" Width="163px" />
    </p>
    </asp:Panel>

       
        </div>
        <asp:Panel ID="Panel2" runat="server" Height="628px" style="margin-left: 378px; margin-top: 0px;" 
            Width="687px" Visible="False">
            <asp:TextBox ID="TextBox4" runat="server" BorderStyle="None" Font-Bold="True" 
            Font-Size="Medium" Height="35px" Width="651px" 
                ontextchanged="TextBox4_TextChanged1" style="margin-top: 15px">Your active document:</asp:TextBox>
            <asp:TextBox ID="TextBox5" runat="server" Height="406px" 
                ontextchanged="TextBox5_TextChanged" TextMode="MultiLine" Width="611px">Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text Text text </asp:TextBox>
        </asp:Panel>
        
    </div>
    </form>
</body>
</html>
