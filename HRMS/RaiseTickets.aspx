<%@ Page Title="" Language="C#" MasterPageFile="~/Emp.Master" AutoEventWireup="true" CodeBehind="RaiseTickets.aspx.cs" Inherits="HRMS.RaiseTickets" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container w-50">
        <h1 class="text-center">Raise Tickets</h1>
        <div class="py-3 px-5" style="border:0px solid black; border-radius:60px;">
            <div class="mb-3">
                <label for="exampleInputEmail1" class="form-label">Designation</label>
                <br />
                <asp:DropDownList ID="DropDownList1" runat="server" class="form-control w-75" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem>HR</asp:ListItem>
                    <asp:ListItem>Trainer</asp:ListItem>
                    <asp:ListItem>Trainee</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="mb-3">
                <label for="RaiseTicketTo" class="form-label">Raise Ticket To</label>
                <br />
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control w-75">
                </asp:DropDownList>
            </div>

            <div class="mb-3">
                <label for="InputTicket" class="form-label">Ticket</label>
                <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter your issues here..." class="form-control w-75" TextMode="MultiLine"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="Attachment" class="form-label">Attachment</label>
                <asp:FileUpload ID="FileUpload1" runat="server" class="form-control w-75" />
                <asp:Button ID="Button1" runat="server" Text="Raise Ticket" CssClass="btn btn-success w-25 mt-3" OnClick="Button1_Click" />
            </div>
        </div>
    </div>
</asp:Content>
