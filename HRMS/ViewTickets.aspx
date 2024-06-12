<%@ Page Title="" Language="C#" MasterPageFile="~/Emp.Master" AutoEventWireup="true" CodeBehind="ViewTickets.aspx.cs" Inherits="HRMS.ViewTickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .modal {
            display: none;
            position: absolute;
            z-index: 1;
            right: 20%;
            top: 50%;
            width: 80%;
            height: 70%;
            overflow: auto;
        }

        .modal-content {
            background-color: #fefefe;
            margin: 15% 50%;
            padding: 20px;
            border: 1px solid #888;
            width: 80%;
        }

        .close {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

        .close:hover,
        .close:focus {
            color: black;
            text-decoration: none;
            cursor: pointer;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center">View Tickets</h1>
    <div class="container">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="TicketID" OnRowCommand="GridView1_RowCommand" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="992px">
            <Columns>
                <asp:BoundField DataField="TicketID" HeaderText="Ticket ID" />
                <asp:BoundField DataField="RaisedByName" HeaderText="Raised By" />
                <asp:BoundField DataField="TicketDescription" HeaderText="Ticket Description" />
                <asp:BoundField DataField="Designation" HeaderText="Designation" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDownload" runat="server" CommandName="DownloadAttachment" CommandArgument='<%# Eval("Attachment") %>' Text="Download Attachment" CssClass="btn btn-danger btn-sm px-3 py-2" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Solution">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnOpenModal" runat="server" CommandName="OpenModal" CommandArgument='<%# Eval("TicketID") %>' Text="Submit Solution" CssClass="btn btn-info btn-sm px-3 py-2" />
                    </ItemTemplate>
                </asp:TemplateField>
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

    <!-- Modal for Solution -->
    <div id="myModal" class="modal">
        <div class="modal-content w-50">
            <span class="close" onclick="closeModal()">&times;</span>
            <asp:TextBox ID="txtSolution" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Enter solution..."></asp:TextBox>
            <asp:Button ID="btnSubmitSolution" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm w-75 mt-3 d-block text-center mx-5" OnClick="btnSubmit_Click" />
        </div>
    </div>

    <script>
        function openModal() {
            var modal = document.getElementById("myModal");
            modal.style.display = "block";
        }

        function closeModal() {
            var modal = document.getElementById("myModal");
            modal.style.display = "none";
        }

        window.onclick = function (event) {
            var modal = document.getElementById("myModal");
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }
    </script>
</asp:Content>
