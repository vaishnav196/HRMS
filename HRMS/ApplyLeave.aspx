<%@ Page Title="" Language="C#" MasterPageFile="~/Emp.Master" AutoEventWireup="true" CodeBehind="ApplyLeave.aspx.cs" Inherits="HRMS.ApplyLeave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container w-50 mt-5">
        <h1>Apply for Leave</h1>
        <div class="form-group">
            <label for="FromDate"><strong>From</strong></label>
            <asp:TextBox ID="TextBox1" runat="server" TextMode="Date" class="form-control" placeholder="From Date"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="ToDate"><strong>To</strong></label>
            <asp:TextBox ID="TextBox2" runat="server" TextMode="Date" class="form-control" placeholder="To Date"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="Reason"><strong>Reason For Leave</strong></label>
            <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" class="form-control" placeholder="Reason"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Button ID="Button1" runat="server" Text="Apply Leave" class="btn btn-primary" OnClick="Button1_Click"/>
        </div>
        <div class="container">
            <div class="row">
                <div class="col-6">
                    <div class="form-group  mx-5 px-2 py-3 w-75" style="box-shadow:2px 7px 15px 1px white">
                        <label class="text-center"><h3>Balance Leaves</h3></label>
                        <h3 class="text-center mr-5"><asp:Label ID="Label1" runat="server" class="form-control-plaintext"></asp:Label></h3>
                    </div>
                </div>
                <div class="col-6">
                    <div class="form-group  mx-5 px-2 py-3" style="box-shadow:2px 7px 15px 1px white">
                        <label class="text-center"><h3>Absent Days</h3></label>
                        <h3 class="text-center mr-5"><asp:Label ID="Label2" runat="server" class="form-control-plaintext"></asp:Label></h3>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>