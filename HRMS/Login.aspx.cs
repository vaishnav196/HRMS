//using System;
//using System.Configuration;
//using System.Data.SqlClient;
//using System.Web.UI;

//namespace HRMS
//{
//    public partial class Login : System.Web.UI.Page
//    {
//        SqlConnection conn;

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            string cnf = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
//            conn = new SqlConnection(cnf);
//        }

//        protected void Button2_Click(object sender, EventArgs e)
//        {
//            string name = TextBox4.Text.Trim();
//            string contact = TextBox5.Text.Trim();
//            string email = TextBox6.Text.Trim();
//            string password = TextBox7.Text.Trim();
//            DateTime dateOfJoining = DateTime.Parse(TextBox9.Text.Trim());
//            string designation = TextBox8.Text.Trim();

//            string connectionString = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                string query = "INSERT INTO Emp (Name, Contact, Email, Password, Role, DateOfJoining, Designation) VALUES (@Name, @Contact, @Email, @Password, 'Employee', @DateOfJoining, @Designation); SELECT SCOPE_IDENTITY()";
//                using (SqlCommand cmd = new SqlCommand(query, connection))
//                {
//                    cmd.Parameters.AddWithValue("@Name", name);
//                    cmd.Parameters.AddWithValue("@Contact", contact);
//                    cmd.Parameters.AddWithValue("@Email", email);
//                    cmd.Parameters.AddWithValue("@Password", password);
//                    cmd.Parameters.AddWithValue("@DateOfJoining", dateOfJoining);
//                    cmd.Parameters.AddWithValue("@Designation", designation);

//                    try
//                    {
//                        connection.Open();
//                        int empID = Convert.ToInt32(cmd.ExecuteScalar()); // Retrieve the auto-generated EmpID
//                        Session["EmpID"] = empID; // Store EmpID in session
//                        Session["Name"] = name;
//                        Session["Contact"] = contact;
//                        Session["Email"] = email;
//                        Session["Password"] = password;
//                        Session["DateOfJoining"] = dateOfJoining;
//                        Session["Designation"] = designation;
//                        Session["MyUser"] = email;

//                        Response.Write("<script>alert('Registered Successfully')</script>");
//                        Response.Redirect("Login.aspx");
//                    }
//                    catch (Exception ex)
//                    {
//                        Response.Write($"<script>alert('Error: {ex.Message}')</script>");
//                    }
//                }
//            }
//        }

//        protected void Button1_Click1(object sender, EventArgs e)
//        {
//            string em = TextBox1.Text;
//            string pass = TextBox2.Text;

//            try
//            {
//                conn.Open();
//                string q = "SELECT * FROM Emp WHERE Email = @Email AND Password = @Password";
//                SqlCommand cmd = new SqlCommand(q, conn);
//                cmd.Parameters.AddWithValue("@Email", em);
//                cmd.Parameters.AddWithValue("@Password", pass);

//                SqlDataReader rdr = cmd.ExecuteReader();
//                if (rdr.HasRows)
//                {
//                    while (rdr.Read())
//                    {
//                        string role = rdr["Role"].ToString(); // Retrieve role as string

//                        if (em.Equals(rdr["Email"]) && pass.Equals(rdr["Password"]))
//                        {
//                            Session["MyUser"] = em;
//                            Session["EmpID"] = rdr["EmpID"]; // Store EmpID in session

//                            if (role == "Employee")
//                            {
//                                Response.Redirect("UserHome.aspx");
//                            }
//                            else if (role == "admin")
//                            {
//                                Response.Redirect("HRHome.aspx");
//                            }
//                        }
//                    }
//                }
//                else
//                {
//                    Response.Write("<script>alert('Invalid email or password. Please try again.')</script>");
//                }
//            }
//            catch (Exception ex)
//            {
//                Response.Write($"<script>alert('Error: {ex.Message}')</script>");
//            }
//            finally
//            {
//                conn.Close();
//            }
//        }
//    }
//}


//using System;
//using System.Configuration;
//using System.Data.SqlClient;
//using System.Net;
//using System.Net.Mail;
//using System.Web.UI;

//namespace HRMS
//{
//    public partial class Login : System.Web.UI.Page
//    {
//        SqlConnection conn;

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            string cnf = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
//            conn = new SqlConnection(cnf);
//        }

//        protected void Button2_Click(object sender, EventArgs e)
//        {
//            string name = TextBox4.Text.Trim();
//            string contact = TextBox5.Text.Trim();
//            string email = TextBox6.Text.Trim();
//            string password = TextBox7.Text.Trim();
//            DateTime dateOfJoining = DateTime.Parse(TextBox9.Text.Trim());
//            string designation = TextBox8.Text.Trim();

//            string connectionString = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                string query = "INSERT INTO Emp (Name, Contact, Email, Password, Role, DateOfJoining, Designation) VALUES (@Name, @Contact, @Email, @Password, 'Employee', @DateOfJoining, @Designation); SELECT SCOPE_IDENTITY()";
//                using (SqlCommand cmd = new SqlCommand(query, connection))
//                {
//                    cmd.Parameters.AddWithValue("@Name", name);
//                    cmd.Parameters.AddWithValue("@Contact", contact);
//                    cmd.Parameters.AddWithValue("@Email", email);
//                    cmd.Parameters.AddWithValue("@Password", password);
//                    cmd.Parameters.AddWithValue("@DateOfJoining", dateOfJoining);
//                    cmd.Parameters.AddWithValue("@Designation", designation);

//                    try
//                    {
//                        connection.Open();
//                        int empID = Convert.ToInt32(cmd.ExecuteScalar()); // Retrieve the auto-generated EmpID
//                        Session["EmpID"] = empID; // Store EmpID in session
//                        Session["Name"] = name;
//                        Session["Contact"] = contact;
//                        Session["Email"] = email;
//                        Session["Password"] = password;
//                        Session["DateOfJoining"] = dateOfJoining;
//                        Session["Designation"] = designation;
//                        Session["MyUser"] = email;

//                        // Generate OTP
//                        string otp = GenerateOTP();
//                        Session["OTP"] = otp;

//                        // Send OTP via email
//                        SendOTPByEmail(email, otp);

//                        // Show OTP verification modal
//                        ScriptManager.RegisterStartupScript(this, this.GetType(), "exampleModal", "$('#exampleModal').modal('show');", true);
//                    }
//                    catch (Exception ex)
//                    {
//                        Response.Write($"<script>alert('Error: {ex.Message}')</script>");
//                    }
//                }
//            }
//        }

//        protected void Button1_Click1(object sender, EventArgs e)
//        {
//            string em = TextBox1.Text;
//            string pass = TextBox2.Text;

//            try
//            {
//                conn.Open();
//                string q = "SELECT * FROM Emp WHERE Email = @Email AND Password = @Password";
//                SqlCommand cmd = new SqlCommand(q, conn);
//                cmd.Parameters.AddWithValue("@Email", em);
//                cmd.Parameters.AddWithValue("@Password", pass);

//                SqlDataReader rdr = cmd.ExecuteReader();
//                if (rdr.HasRows)
//                {
//                    while (rdr.Read())
//                    {
//                        string role = rdr["Role"].ToString(); // Retrieve role as string

//                        if (em.Equals(rdr["Email"]) && pass.Equals(rdr["Password"]))
//                        {
//                            Session["MyUser"] = em;
//                            Session["EmpID"] = rdr["EmpID"]; // Store EmpID in session

//                            if (role == "Employee")
//                            {
//                                Response.Redirect("UserHome.aspx");
//                            }
//                            else if (role == "admin")
//                            {
//                                Response.Redirect("HRHome.aspx");
//                            }
//                        }
//                    }
//                }
//                else
//                {
//                    Response.Write("<script>alert('Invalid email or password. Please try again.')</script>");
//                }
//            }
//            catch (Exception ex)
//            {
//                Response.Write($"<script>alert('Error: {ex.Message}')</script>");
//            }
//            finally
//            {
//                conn.Close();
//            }
//        }



//            // Generate random OTP
//            private string GenerateOTP()
//        {
//            Random rnd = new Random();
//            int otp = rnd.Next(100000, 999999);
//            return otp.ToString();
//        }

//        // Send OTP via email
//        private void SendOTPByEmail(string email, string otp)
//        {
//            MailMessage mail = new MailMessage("vaish00721@gmail.com", email);
//            mail.Subject = "OTP Verification";
//            mail.Body = "Your OTP for registration is: " + otp;

//            SmtpClient client = new SmtpClient("smtp.gmail.com");
//            client.Port = 587;
//            client.Credentials = new NetworkCredential("vaish00721@gmail.com", "kzuvycbbvbrdempp");
//            client.EnableSsl = true;
//            client.Send(mail);
//        }
//        protected void btnVerifyOTP_Click(object sender, EventArgs e)
//        {
//            string enteredOTP = txtOTP.Text;
//            string expectedOTP = Session["OTP"].ToString();

//            if (enteredOTP == expectedOTP)
//            {
//                // OTP is correct, proceed with registration
//                // Add your logic here
//                Response.Redirect("UserHome.aspx"); // Example redirection to UserHome.aspx
//            }
//            else
//            {
//                // Invalid OTP, display error message
//                Response.Write("<script>alert('Invalid OTP. Please try again.');</script>");
//            }
//        }
//    }
//}

//-----------------------------
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.UI;

namespace HRMS
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            string cnf = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
            conn = new SqlConnection(cnf);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // Get the registration details and store them in session variables
            Session["Name"] = TextBox4.Text.Trim();
            Session["Contact"] = TextBox5.Text.Trim();
            Session["Email"] = TextBox6.Text.Trim();
            Session["Password"] = TextBox7.Text.Trim();
            Session["DateOfJoining"] = DateTime.Parse(TextBox9.Text.Trim());
            Session["Designation"] = TextBox8.Text.Trim();

            // Generate OTP
            string otp = GenerateOTP();
            Session["OTP"] = otp;

            // Send OTP via email
            SendOTPByEmail(Session["Email"].ToString(), otp);

            // Show OTP verification modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowOTPModal", "$('#otpModal').modal('show');", true);
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            string em = TextBox1.Text;
            string pass = TextBox2.Text;

            try
            {
                conn.Open();
                string q = "SELECT * FROM Emp WHERE Email = @Email AND Password = @Password";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@Email", em);
                cmd.Parameters.AddWithValue("@Password", pass);

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        string role = rdr["Role"].ToString(); // Retrieve role as string

                        if (em.Equals(rdr["Email"]) && pass.Equals(rdr["Password"]))
                        {
                            Session["MyUser"] = em;
                            Session["EmpID"] = rdr["EmpID"]; // Store EmpID in session

                            if (role == "Employee")
                            {
                                Response.Redirect("UserHome.aspx");
                            }
                            else if (role == "admin")
                            {
                                Response.Redirect("HRHome.aspx");
                            }
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert('Invalid email or password. Please try again.')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error: {ex.Message}')</script>");
            }
            finally
            {
                conn.Close();
            }
        }

        // Generate random OTP
        private string GenerateOTP()
        {
            Random rnd = new Random();
            int otp = rnd.Next(100000, 999999);
            return otp.ToString();
        }

        // Send OTP via email
        private void SendOTPByEmail(string email, string otp)
        {
            MailMessage mail = new MailMessage("vaish00721@gmail.com", email);
            mail.Subject = "OTP Verification";
            mail.Body = "Your OTP for registration is: " + otp;

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.Credentials = new NetworkCredential("vaish00721@gmail.com", "kzuvycbbvbrdempp");
            client.EnableSsl = true;
            client.Send(mail);
        }

        protected void btnVerifyOTP_Click(object sender, EventArgs e)
        {
            string enteredOTP = txtOTP.Text;
            string expectedOTP = Session["OTP"].ToString();

            if (enteredOTP == expectedOTP)
            {
                string name = Session["Name"].ToString();
                string contact = Session["Contact"].ToString();
                string email = Session["Email"].ToString();
                string password = Session["Password"].ToString();
                DateTime dateOfJoining = (DateTime)Session["DateOfJoining"];
                string designation = Session["Designation"].ToString();

                string connectionString = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Emp (Name, Contact, Email, Password, Role, DateOfJoining, Designation) VALUES (@Name, @Contact, @Email, @Password, 'Employee', @DateOfJoining, @Designation); SELECT SCOPE_IDENTITY()";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Contact", contact);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@DateOfJoining", dateOfJoining);
                        cmd.Parameters.AddWithValue("@Designation", designation);

                        try
                        {
                            connection.Open();
                            int empID = Convert.ToInt32(cmd.ExecuteScalar()); // Retrieve the auto-generated EmpID
                            Session["EmpID"] = empID; // Store EmpID in session
                            Session["MyUser"] = email;

                            lblMessage.ForeColor = System.Drawing.Color.Green;
                            lblMessage.Text = "OTP Verified Successfully!";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideOTPModal", "$('#otpModal').modal('hide');", true);
                            // Redirect to UserHome.aspx after OTP verification
                            Response.Redirect("UserHome.aspx");
                        }
                        catch (Exception ex)
                        {
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Text = $"Error: {ex.Message}";
                        }
                    }
                }
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Invalid OTP. Please try again.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowOTPModal", "$('#otpModal').modal('show');", true);
            }
        }
    }
}
