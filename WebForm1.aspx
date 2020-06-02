<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="LiPrize.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id">
            <Columns>
                <asp:BoundField DataField="年度" HeaderText="年度" SortExpression="年度" />
                <asp:BoundField DataField="得獎類別" HeaderText="得獎類別" SortExpression="得獎類別" />
                <asp:BoundField DataField="區別" HeaderText="區別" SortExpression="區別" />
                <asp:BoundField DataField="里別" HeaderText="里別" SortExpression="里別" />
                <asp:BoundField DataField="姓名" HeaderText="姓名" SortExpression="姓名" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:prizeLiConnectionString %>" SelectCommand="SELECT * FROM [Li]"></asp:SqlDataSource>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="讀取檔案資料" />
    </form>
</body>
</html>
