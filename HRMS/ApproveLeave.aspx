<%@ Page Title="" Language="C#" MasterPageFile="~/HR.Master" AutoEventWireup="true" CodeBehind="ApproveLeave.aspx.cs" Inherits="HRMS.ApproveLeave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card">
        <div class="card-header">
            <h3 class="">Leave Approval</h3>
            <div class="">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1229px" OnRowCommand="GridView1_RowCommand" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
                    <Columns>
                        <asp:BoundField DataField="EmpID" HeaderText="Emp ID" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="FromDate" HeaderText="From" />
                        <asp:BoundField DataField="ToDate" HeaderText="To" />
                        <asp:BoundField DataField="AbsentDays" HeaderText="Absent Days" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button ID="btnApprove" runat="server" Text="Approve" CommandName="Approve" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-success btn-sm" />
                                <asp:Button ID="btnReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-danger btn-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>