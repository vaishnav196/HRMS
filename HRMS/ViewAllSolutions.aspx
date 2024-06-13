<%@ Page Title="" Language="C#" MasterPageFile="~/HR.Master" AutoEventWireup="true" CodeBehind="ViewAllSolutions.aspx.cs" Inherits="HRMS.ViewAllSolutions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <h1 class="text-center">View Closed Tickets</h1>
  <div class="container">
        <div class="form-group">
            <asp:Label ID="lblViewBy" runat="server" Text="View by:"></asp:Label>
            <asp:DropDownList ID="ddlViewBy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlViewBy_SelectedIndexChanged" CssClass="form-control w-50">
                <asp:ListItem Text="Daily" Value="Daily"></asp:ListItem>
                <asp:ListItem Text="Weekly" Value="Weekly"></asp:ListItem>
                <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnExport" runat="server" Text="Export to CSV" OnClick="btnExport_Click" CssClass="btn btn-primary float-right" />
        </div>
      <asp:GridView ID="GridViewClosedTickets" runat="server" AutoGenerateColumns="False" DataKeyNames="TicketID" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="992px">
          <Columns>
              <asp:BoundField DataField="TicketID" HeaderText="Ticket ID" />
              <asp:BoundField DataField="RaisedByName" HeaderText="Raised By" />
              <asp:BoundField DataField="RaisedToName" HeaderText="Raised To" />
              <asp:BoundField DataField="Designation" HeaderText="Designation" />
              <asp:BoundField DataField="TicketDescription" HeaderText="Ticket Description" />
              <asp:BoundField DataField="Solution" HeaderText="Solution" />
              <asp:BoundField DataField="ClosedDate" HeaderText="Closed Date" />
          </Columns>
          <FooterStyle BackColor="White" ForeColor="#000066" />
          <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
          <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
          <RowStyle ForeColor="#000066" />
          <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
          <SortedAscendingCellStyle BackColor="#F1F1F1" />
          <SortedAscendingHeaderStyle BackColor="#007DBB" />
          <SortedDescendingCellStyle BackColor="#CAC9C9" />
          <SortedDescendingHeaderStyle BackColor="#00547E" />
      </asp:GridView>
  </div>
</asp:Content>
