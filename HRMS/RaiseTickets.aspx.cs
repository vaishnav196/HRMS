using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMS
{
    public partial class RaiseTickets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateEmployeeDropdown(DropDownList1.SelectedValue);
        }

        private void PopulateEmployeeDropdown(string role)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT EmpID, Name FROM Emp WHERE Designation = @Role";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Role", role);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DropDownList2.DataSource = reader;
                    DropDownList2.DataTextField = "Name";
                    DropDownList2.DataValueField = "EmpID";
                    DropDownList2.DataBind();
                }
            }
            // Add a default item
            DropDownList2.Items.Insert(0, new ListItem("Select Employee", "0"));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["EmpID"] == null)
            {
                Response.Write($"<script>alert('Error: Session expired or EmpID not set.')</script>");
                return;
            }

            int raisedBy;
            if (!int.TryParse(Session["EmpID"].ToString(), out raisedBy))
            {
                Response.Write($"<script>alert('Error: Invalid EmpID.')</script>");
                return;
            }

            int raisedTo;
            if (!int.TryParse(DropDownList2.SelectedValue, out raisedTo) || raisedTo == 0)
            {
                Response.Write("<script>alert('Error: Please select a valid employee.')</script>");
                return;
            }

            string raisedByName = GetEmployeeName(raisedBy);
            string raisedToName = DropDownList2.SelectedItem.Text;
            string designation = DropDownList1.SelectedValue;
            string ticketDescription = TextBox1.Text.Trim();
            string attachment = null;

            if (FileUpload1.HasFile)
            {
                try
                {
                    string fileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Attachments/") + fileName);
                    attachment = "~/Attachments/" + fileName;
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Error saving file: {ex.Message}')</script>");
                    return;
                }
            }

            string connectionString = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Tickets (RaisedBy, RaisedByName, RaisedTo, RaisedToName, Designation, TicketDescription, Attachment) VALUES (@RaisedBy, @RaisedByName, @RaisedTo, @RaisedToName, @Designation, @TicketDescription, @Attachment)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@RaisedBy", raisedBy);
                    cmd.Parameters.AddWithValue("@RaisedByName", raisedByName);
                    cmd.Parameters.AddWithValue("@RaisedTo", raisedTo);
                    cmd.Parameters.AddWithValue("@RaisedToName", raisedToName);
                    cmd.Parameters.AddWithValue("@Designation", designation);
                    cmd.Parameters.AddWithValue("@TicketDescription", ticketDescription);
                    cmd.Parameters.AddWithValue("@Attachment", (object)attachment ?? DBNull.Value);

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        Response.Write($"<script>alert('Ticket raised successfully!')</script>");
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('Error: {ex.Message}')</script>");
                    }
                }
            }
        }

        private string GetEmployeeName(int empID)
        {
            string name = string.Empty;
            string connectionString = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Name FROM Emp WHERE EmpID = @EmpID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmpID", empID);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        name = result.ToString();
                    }
                }
            }
            return name;
        }
    }
}
