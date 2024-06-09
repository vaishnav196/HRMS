<%@ Page Title="" Language="C#" MasterPageFile="~/Emp.Master" AutoEventWireup="true" CodeBehind="ViewPaySlip.aspx.cs" Inherits="HRMS.ViewPaySlip" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1>PaySlips</h1>
        <asp:GridView ID="PaySlipGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1035px" CellSpacing="2" OnRowCommand="PaySlipGridView_RowCommand">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Sr No" />
                <asp:BoundField DataField="Month" HeaderText="Month" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton ID="DownloadLinkButton" runat="server" CommandName="Download" CommandArgument='<%#Eval("FilePath") %>' Text="Download" />
                        <asp:LinkButton ID="ViewLinkButton" runat="server" CommandName="View" CommandArgument='<%#Eval("FilePath") %>' Text="View" />
                    </ItemTemplate>
                    <ControlStyle BackColor="#FFCC99" ForeColor="Black" BorderStyle="None" />
                    <ItemStyle BackColor="White" BorderStyle="None" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView>
    </div>
</asp:Content>
