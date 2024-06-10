<%@ Page Title="" Language="C#" MasterPageFile="~/HR.Master" AutoEventWireup="true" CodeBehind="GenerateOffer.aspx.cs" Inherits="HRMS.GenerateOffer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center mb-4">
        <h2>Generate Offer Letter</h2>
    </div>
    <div class="container w-50">
        <div class="form-group">
            <label for="empId">Employee ID</label>
            <div class="input-group">
                <asp:TextBox ID="txtEmpId" runat="server" CssClass="form-control" placeholder="Enter Employee ID" class="form-control w-50"></asp:TextBox>
                <div class="input-group-append">
                    <asp:Button ID="btnFetchDetails" runat="server" Text="Fetch Details" CssClass="btn btn-primary" OnClick="btnFetchDetails_Click" />
                </div>
            </div>
        </div>
        <div class="form-group">
            <label for="name">Name</label>
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter Name" class="form-control w-50"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="email">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email" class="form-control w-50"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="dateOfJoining">Date of Joining</label>
            <asp:TextBox ID="txtDateOfJoining" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD" TextMode="Date" class="form-control w-50"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="salary">Salary</label>
            <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" placeholder="Enter Salary" class="form-control w-50"></asp:TextBox>
        </div>
        <div class="text-center">
            <asp:Button ID="btnGenerate" runat="server" Text="Generate Offer Letter" CssClass="btn btn-success w-25" OnClick="btnGenerate_Click" />
        </div>
    </div>
</asp:Content>
