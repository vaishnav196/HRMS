<%@ Page Title="" Language="C#" MasterPageFile="~/HR.Master" AutoEventWireup="true" CodeBehind="PaySlipFormat.aspx.cs" Inherits="HRMS.PaySlipFormat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
        }

        .invoice {
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            padding: 30px;
            max-width: 900px;
            margin: 0 auto;
            border: 1px solid #000;
        }

        .invoice-header {
            text-align: center;
            margin-bottom: 20px;
        }

        .invoice-header h1 {
            font-size: 24px;
            margin: 0;
        }

        .invoice-header p {
            margin: 5px 0;
        }

        .invoice-info {
            width: 100%;
            margin-bottom: 20px;
            border: 1px solid #000;
            padding: 10px;
        }

        .invoice-info th, .invoice-info td {
            text-align: left;
            padding: 5px;
        }

        .invoice-table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        .invoice-table th, .invoice-table td {
            border: 1px solid #000;
            padding: 10px;
            text-align: left;
        }

        .invoice-table th {
            background-color: #f1f1f1;
        }

        .footer {
            text-align: center;
            color: #888;
        }

        .text-center {
            text-align: center;
        }

        .mb-5 {
            margin-bottom: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <div class="container">
        <div class="invoice">
            <div class="invoice-header">
                <div class='invoice-info'>
                    <div class="row justify-content-center">
                        
                    </div>
                    <div class="row justify-content-center">
                       
                    </div>
                    <h1>Masstech</h1>
                    <p>Business Solutions</p>
                   
                </div>
            </div>

            <div class="invoice-info">
                <asp:Panel ID="Panel1" runat="server">
                    <table>
                        <tr>
                            <td><strong>NAME OF EMPLOYEE :</strong>
                                <asp:Label ID="Label1" runat="server"></asp:Label></td>
                            <td><strong>DESIGNATION :</strong> Software Developer</td>
                        </tr>
                        <tr>
                            <td><strong>BANK NAME:</strong> ICICI Bank</td>
                            <td><strong>EMPLOYEE EMAIL:</strong> <asp:Label ID="Label2" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><strong>IFSC CODE:</strong> ICIC0000092</td>
                            <td><strong>DATE OF JOINING:</strong> <asp:Label ID="Label5" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><strong>BANK ACCOUNT NO:</strong> 123801537392</td>
                            <td><strong>CONTACT NO :</strong> <asp:Label ID="contact_label" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><strong>PAN:</strong> FGKPB0088L</td>
                            <td><strong>DAYS IN MONTH:</strong> 28</td>
                        </tr>
                        <tr>
                            <td><strong>AADHAR:</strong> 7902 8178 5003</td>
                            <td><strong>UAN:</strong> NA</td>
                        </tr>
                      
                    </table>
                </asp:Panel>
            </div>

            <table class="invoice-table">
                <thead>
                    <tr>
                        <th>GROSS SALARY</th>
                        <th>AMOUNT</th>
                        <th>DEDUCTION</th>
                        <th>AMOUNT</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Basic</td>
                        <td><asp:Label ID="Label4" runat="server"></asp:Label></td>
                        <td>PF</td>
                        <td>-</td>
                    </tr>
                    <tr>
                        <td>HRA</td>
                        <td>0</td>
                        <td>Professional Tax</td>
                        <td>0</td>
                    </tr>
                    <tr>
                        <td>Travel Allowance</td>
                        <td>0</td>
                        <td>TDS</td>
                        <td>-</td>
                    </tr>
                    <tr>
                        <td>Bonus</td>
                        <td>0</td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Special Allowance</td>
                        <td>0</td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Medical Re-imbursement</td>
                        <td>0</td>
                        <td></td>
                        <td></td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <th>GROSS SALARY</th>
                        <th></th>
                        <th>TOTAL DEDUCTION</th>
                        <th>0</th>
                    </tr>
                    <tr>
                        <th colspan="3">NET SALARY PAID</th>
                        <th>14,500</th>
                    </tr>
                </tfoot>
            </table>
            <div class="footer">
                This is computerised generated salary slip and does not require authentication
            </div>
        </div>
    </div>
</asp:Content>
