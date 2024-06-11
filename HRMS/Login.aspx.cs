using System;
using System.Configuration;
using System.Data.SqlClient;
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
            string name = TextBox4.Text.Trim();
            string contact = TextBox5.Text.Trim();
            string email = TextBox6.Text.Trim();
            string password = TextBox7.Text.Trim();
            DateTime dateOfJoining = DateTime.Parse(TextBox9.Text.Trim());
            string designation = TextBox8.Text.Trim();

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
                        Session["Name"] = name;
                        Session["Contact"] = contact;
                        Session["Email"] = email;
                        Session["Password"] = password;
                        Session["DateOfJoining"] = dateOfJoining;
                        Session["Designation"] = designation;
                        Session["MyUser"] = email;

                        Response.Write("<script>alert('Registered Successfully')</script>");
                        Response.Redirect("Login.aspx");
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('Error: {ex.Message}')</script>");
                    }
                }
            }
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
    }
}
