<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WelcomeForm.aspx.cs" Inherits="WebGUI.WelcomeForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <p>
        &nbsp;</p>
    <asp:Panel ID="Panel1" runat="server" Height="263px" Width="488px">
        <asp:Label ID="Label1" runat="server" 
    Text="Please Enter Your Username!"></asp:Label>
        <asp:TextBox ID="UserTextBox" runat="server" Height="32px" onclick="OnClick" 
            ontextchanged="UserTextBox_TextChanged" 
            style="margin-left: 145px; margin-right: 133px; margin-top: 47px" Width="213px"></asp:TextBox>
        <asp:Button ID="Button4" runat="server" Height="34px" onclick="Button4_Click" 
            style="margin-left: 179px; margin-top: 0px" Text="Submit" Width="140px" />
    </asp:Panel>
    </form>
</body>
</html>
