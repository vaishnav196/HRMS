<%@ Page Title="" Language="C#" MasterPageFile="~/HR.Master" AutoEventWireup="true" CodeBehind="GeneratePaySlip.aspx.cs" Inherits="HRMS.GeneratePaySlip" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container w-50">
        <div class="card">
            <div class="card-header">
                <h3>Employee Salary Calculator</h3>
            </div>
            <div class="card-body">
                <div class="form-group ms-5">
                    <label for="empNo">Enter Emp No:</label>
                    <asp:TextBox ID="empNo" runat="server" CssClass="form-control w-75" placeholder="Enter Employee Number"></asp:TextBox>
                    <asp:Button ID="getDetails" runat="server" CssClass="btn btn-success mt-2 w-25" Text="Enter to get details" OnClick="getDetails_Click" />
                </div>
                <div class="form-group">
                    <label for="empName">Employee Name:</label>
                    <asp:TextBox ID="empName" runat="server" CssClass="form-control w-75" ReadOnly="true" placeholder="Employee Name"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="bankName">Bank Name:</label>
                    <asp:TextBox ID="bankName" runat="server" CssClass="form-control w-75" ReadOnly="true" placeholder="Bank Name"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="contactNo">Contact No:</label>
                    <asp:TextBox ID="contactNo" runat="server" CssClass="form-control w-75" ReadOnly="true" placeholder="Contact Number"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="bankAccNo">Bank Account No:</label>
                    <asp:TextBox ID="bankAccNo" runat="server" CssClass="form-control w-75" ReadOnly="true" placeholder="Bank Account Number"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="email">Email:</label>
                    <asp:TextBox ID="email" runat="server" CssClass="form-control w-75" ReadOnly="true" placeholder="Email"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="designation">Designation:</label>
                    <asp:TextBox ID="designation" runat="server" CssClass="form-control w-75" ReadOnly="true" placeholder="Designation"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="dateOfJoining">Date Of Joining:</label>
                    <asp:TextBox ID="dateOfJoining" runat="server" CssClass="form-control w-75" ReadOnly="true" placeholder="Date Of Joining"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="monthlySalary">Monthly Salary (₹):</label>
                    <asp:TextBox ID="monthlySalary" runat="server" CssClass="form-control w-75" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="workingDays">Total Working Days in Month:</label>
                    <asp:TextBox ID="workingDays" runat="server" CssClass="form-control w-75"></asp:TextBox>
                </div>
                <div class="form-group ">
                    <label for="leavesTaken">Leaves Taken:</label>
                    <asp:TextBox ID="leavesTaken" runat="server" CssClass="form-control w-75"></asp:TextBox>
                </div>
                <asp:Button ID="calculate" runat="server" CssClass="btn btn-danger" Text="Calculate" OnClick="calculate_Click" />
                <div class="form-group mt-3">
                    <label for="calculatedSalary">Calculated Salary (₹):</label>
                    <asp:TextBox ID="calculatedSalary" runat="server" CssClass="form-control w-75" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="month">Select Month:</label>
                    <asp:DropDownList ID="month" runat="server" CssClass="form-control w-75">
                        <asp:ListItem Text="January" Value="January"></asp:ListItem>
                        <asp:ListItem Text="February" Value="February"></asp:ListItem>
                        <asp:ListItem Text="March" Value="March"></asp:ListItem>
                        <asp:ListItem Text="April" Value="April"></asp:ListItem>
                        <asp:ListItem Text="May" Value="May"></asp:ListItem>
                        <asp:ListItem Text="June" Value="June"></asp:ListItem>
                        <asp:ListItem Text="July" Value="July"></asp:ListItem>
                        <asp:ListItem Text="August" Value="August"></asp:ListItem>
                        <asp:ListItem Text="September" Value="September"></asp:ListItem>
                        <asp:ListItem Text="October" Value="October"></asp:ListItem>
                        <asp:ListItem Text="November" Value="November"></asp:ListItem>
                        <asp:ListItem Text="December" Value="December"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:Button ID="generatePaySlipPDF" runat="server" CssClass="btn btn-success w-50 d-block m-auto px-3 py-2" Text="Generate Pay Slip PDF" OnClick="generatePaySlipPDF_Click" />
            </div>
        </div>
    </div>
</asp:Content>
