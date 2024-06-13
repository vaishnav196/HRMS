<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HRMS.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
</head>
<body class="bg-info py-5 px-5">
    <form id="form1" runat="server" class="container ms-5 py-5 pl-5 w-75" style="background-color:ghostwhite; border-radius:25px ">
        <div class="w-50 d-block my-5 mx-auto" style="font-family:Comic Sans MS; margin-left:30%; margin-top:100px">
            <div style="margin-bottom:15px; font-weight:bolder">
                <h2 class="mt-5">Welcome to Employees!</h2>
            </div>
            <div style="padding:20px; border-radius:10%" class="card-part">
                <div class="mb-3">
                    <label for="exampleInputEmail1" class="form-label">Email address</label>
                    <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter Email Address" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="exampleInputPassword1" class="form-label">Password</label>
                    <asp:TextBox ID="TextBox2" runat="server" placeholder="Enter Password" Height="45px" Width="290px" TextMode="Password" class="form-control"></asp:TextBox>
                </div>
                &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="Login" class="btn btn-success w-50 mt-3" Onclick="Button1_Click1" />

                <br />
                <p class="px-5"><a href="#" data-toggle="modal" data-target="#exampleModal" class=" ">Register Here First</a></p>


            </div>

            
        </div>

        <!-- Modal -->
        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" style="color:black;font-family:Comic Sans MS" id="exampleModalLabel">Sign Up</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body h-25 px-5">
                        <div style="color:black;font-family:Comic Sans MS;" class="mx-5">
                            <div class="mb-3">
                                <label for="exampleInputName" class="form-label">Name</label>
                                <asp:TextBox ID="TextBox4" runat="server" placeholder="Enter Name" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputContact" class="form-label">Contact</label>
                                <asp:TextBox ID="TextBox5" runat="server" placeholder="Enter Contact" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputEmail1" class="form-label">Email address</label>
                                <asp:TextBox ID="TextBox6" runat="server" placeholder="Enter Email Address" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputPassword1" class="form-label">Password</label>
                                <asp:TextBox ID="TextBox7" runat="server" placeholder="Enter Password" Height="45px" Width="290px" TextMode="Password" class="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="exampleDesignation" class="form-label">Designation</label>
                                <asp:TextBox ID="TextBox8" runat="server" placeholder="Enter Designation" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="exampleDateOfJoining" class="form-label">Date of Joining</label>
                                <asp:TextBox ID="TextBox9" runat="server" placeholder="YYYY-MM-DD" Height="45px" Width="290px" class="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <asp:Button ID="Button2" runat="server" Text="Register On Leave Portal" class="btn btn-danger w-100 mt-3" Onclick="Button2_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>--%>


<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HRMS.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
  <script>
        function checkFields() {
            var name = document.getElementById('<%= TextBox4.ClientID %>').value;
            var contact = document.getElementById('<%= TextBox5.ClientID %>').value;
            var email = document.getElementById('<%= TextBox6.ClientID %>').value;
            var password = document.getElementById('<%= TextBox7.ClientID %>').value;
            var designation = document.getElementById('<%= TextBox8.ClientID %>').value;
            var doj = document.getElementById('<%= TextBox9.ClientID %>').value;

            var registerButton = document.getElementById('<%= Button2.ClientID %>');

            if (name && contact && email && password && designation && doj) {
                registerButton.disabled = false;
            } else {
                registerButton.disabled = true;
            }
        }

        window.onload = function() {
            checkFields(); // Initial check
            var inputs = document.querySelectorAll('#exampleModal input');
            inputs.forEach(function(input) {
                input.addEventListener('input', checkFields);
            });
        }
    </script>
</head>
<body class="bg-info py-5 px-5">
    <form id="form1" runat="server" class="container ms-5 py-5 pl-5 w-75" style="background-color:ghostwhite; border-radius:25px ">
        <div class="w-50 d-block my-5 mx-auto" style="font-family:Comic Sans MS; margin-left:30%; margin-top:100px">
            <div style="margin-bottom:15px; font-weight:bolder">
                <h2 class="mt-5">Welcome to Employees!</h2>
            </div>
            <div style="padding:20px; border-radius:10%" class="card-part">
                <div class="mb-3">
                    <label for="exampleInputEmail1" class="form-label">Email address</label>
                    <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter Email Address" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="exampleInputPassword1" class="form-label">Password</label>
                    <asp:TextBox ID="TextBox2" runat="server" placeholder="Enter Password" Height="45px" Width="290px" TextMode="Password" class="form-control"></asp:TextBox>
                </div>
                &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="Login" class="btn btn-success w-50 mt-3" Onclick="Button1_Click1" />

                <br />
                <p class="px-5"><a href="#" data-toggle="modal" data-target="#exampleModal" class=" ">Register Here First</a></p>


            </div>
        </div>

        <!-- Registration Modal -->
        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" style="color:black;font-family:Comic Sans MS" id="exampleModalLabel">Sign Up</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body h-25 px-5">
                        <div style="color:black;font-family:Comic Sans MS;" class="mx-5">
                            <div class="mb-3">
                                <label for="exampleInputName" class="form-label">Name</label>
                                <asp:TextBox ID="TextBox4" runat="server" placeholder="Enter Name" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputContact" class="form-label">Contact</label>
                                <asp:TextBox ID="TextBox5" runat="server" placeholder="Enter Contact" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputEmail1" class="form-label">Email address</label>
                                <asp:TextBox ID="TextBox6" runat="server" placeholder="Enter Email Address" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputPassword1" class="form-label">Password</label>
                                <asp:TextBox ID="TextBox7" runat="server" placeholder="Enter Password" Height="45px" Width="290px" TextMode="Password" class="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="exampleDesignation" class="form-label">Designation</label>
                                <asp:TextBox ID="TextBox8" runat="server" placeholder="Enter Designation" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="exampleDateOfJoining" class="form-label">Date of Joining</label>
                                <asp:TextBox ID="TextBox9" runat="server" placeholder="YYYY-MM-DD" Height="45px" Width="290px" class="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <asp:Button ID="Button2" data-toggle="modal" data-target="#otpModal" runat="server" Text="Register On Leave Portal" class="btn btn-danger w-100 mt-3" Onclick="Button2_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- OTP Verification Modal -->
        <div class="modal fade" id="otpModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" style="color:black;font-family:Comic Sans MS" id="exampleModalLabel">OTP Verification</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div style="color:black;font-family:Comic Sans MS;" class="mx-5">
                            <div class="mb-3">
                                <label for="exampleInputOTP" class="form-label">Enter OTP</label>
                                <asp:TextBox ID="txtOTP" runat="server" placeholder="Enter OTP" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                            </div>
                            <asp:Button ID="btnVerifyOTP" runat="server" Text="Verify OTP" class="btn btn-primary" OnClick="btnVerifyOTP_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
    </form>
</body>
</html>
--%>


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HRMS.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <script>
        function checkFields() {
            var name = document.getElementById('<%= TextBox4.ClientID %>').value;
            var contact = document.getElementById('<%= TextBox5.ClientID %>').value;
            var email = document.getElementById('<%= TextBox6.ClientID %>').value;
            var password = document.getElementById('<%= TextBox7.ClientID %>').value;
            var designation = document.getElementById('<%= TextBox8.ClientID %>').value;
            var doj = document.getElementById('<%= TextBox9.ClientID %>').value;

            var registerButton = document.getElementById('<%= Button2.ClientID %>');

            if (name && contact && email && password && designation && doj) {
                registerButton.disabled = false;
            } else {
                registerButton.disabled = true;
            }
        }

        window.onload = function () {
            checkFields(); // Initial check
            var inputs = document.querySelectorAll('#exampleModal input');
            inputs.forEach(function (input) {
                input.addEventListener('input', checkFields);
            });
        }
    </script>
</head>
<body class="bg-info py-5 px-5">
    <form id="form1" runat="server" class="container ms-5 py-5 pl-5 w-75" style="background-color:ghostwhite; border-radius:25px ">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <div class="w-50 d-block my-5 mx-auto" style="font-family:Comic Sans MS; margin-left:30%; margin-top:100px">
            <div style="margin-bottom:15px; font-weight:bolder">
                <h2 class="mt-5">Welcome to Employees!</h2>
            </div>
            <div style="padding:20px; border-radius:10%" class="card-part">
                <div class="mb-3">
                    <label for="exampleInputEmail1" class="form-label">Email address</label>
                    <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter Email Address" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="exampleInputPassword1" class="form-label">Password</label>
                    <asp:TextBox ID="TextBox2" runat="server" placeholder="Enter Password" Height="45px" Width="290px" TextMode="Password" class="form-control"></asp:TextBox>
                </div>
                &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="Login" class="btn btn-success w-50 mt-3" Onclick="Button1_Click1" />

                <br />
                <p class="px-5"><a href="#" data-toggle="modal" data-target="#exampleModal" class=" ">Register Here First</a></p>
            </div>
        </div>

        <!-- Registration Modal -->
        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" style="color:black;font-family:Comic Sans MS" id="exampleModalLabel">Sign Up</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body h-25 px-5">
                        <div style="color:black;font-family:Comic Sans MS;" class="mx-5">
                            <div class="mb-3">
                                <label for="exampleInputName" class="form-label">Name</label>
                                <asp:TextBox ID="TextBox4" runat="server" placeholder="Enter Name" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputContact" class="form-label">Contact</label>
                                <asp:TextBox ID="TextBox5" runat="server" placeholder="Enter Contact" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputEmail1" class="form-label">Email address</label>
                                <asp:TextBox ID="TextBox6" runat="server" placeholder="Enter Email Address" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputPassword1" class="form-label">Password</label>
                                <asp:TextBox ID="TextBox7" runat="server" placeholder="Enter Password" Height="45px" Width="290px" TextMode="Password" class="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="exampleDesignation" class="form-label">Designation</label>
                                <asp:TextBox ID="TextBox8" runat="server" placeholder="Enter Designation" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="exampleDateOfJoining" class="form-label">Date of Joining</label>
                                <asp:TextBox ID="TextBox9" runat="server" placeholder="YYYY-MM-DD" Height="45px" Width="290px" class="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <asp:Button ID="Button2" runat="server" Text="Register On Leave Portal" class="btn btn-danger w-100 mt-3" Onclick="Button2_Click" disabled="true"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- OTP Verification Modal -->
        <div class="modal fade" id="otpModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" style="color:black;font-family:Comic Sans MS" id="exampleModalLabel">OTP Verification</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div style="color:black;font-family:Comic Sans MS;" class="mx-5">
                                    <div class="mb-3">
                                        <label for="exampleInputOTP" class="form-label">Enter OTP</label>
                                        <asp:TextBox ID="txtOTP" runat="server" placeholder="Enter OTP" Height="45px" Width="290px" class="form-control"></asp:TextBox>
                                    </div>
                                    <asp:Button ID="btnVerifyOTP" runat="server" Text="Verify OTP" class="btn btn-primary" OnClick="btnVerifyOTP_Click"/>
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        
    </form>
</body>
</html>
