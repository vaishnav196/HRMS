using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMS
{
    public partial class ViewTickets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTickets();
            }
        }

        protected void BindTickets()
        {
            int currentUserID = GetCurrentUserID();

            string connectionString = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TicketID, RaisedByName, TicketDescription, Designation, Attachment FROM Tickets WHERE RaisedTo = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", currentUserID);
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DownloadAttachment")
            {
                string filePath = e.CommandArgument.ToString();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filePath);
                Response.TransmitFile(Server.MapPath(filePath));
                Response.End();
            }
            else if (e.CommandName == "OpenModal")
            {
                int ticketID = Convert.ToInt32(e.CommandArgument);
                ViewState["TicketID"] = ticketID;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModal", "openModal();", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["TicketID"] != null)
            {
                int ticketID = Convert.ToInt32(ViewState["TicketID"]);
                string solution = txtSolution.Text.Trim();
                if (!string.IsNullOrEmpty(solution))
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Insert into ClosedTickets
                        string insertQuery = @"INSERT INTO ClosedTickets 
                                               (TicketID, RaisedBy, RaisedByName, RaisedTo, RaisedToName, Designation, TicketDescription, Solution) 
                                               SELECT TicketID, RaisedBy, RaisedByName, RaisedTo, RaisedToName, Designation, TicketDescription, @Solution 
                                               FROM Tickets WHERE TicketID = @TicketID";
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@TicketID", ticketID);
                            insertCommand.Parameters.AddWithValue("@Solution", solution);
                            insertCommand.ExecuteNonQuery();
                        }

                        // Delete from Tickets
                        string deleteQuery = "DELETE FROM Tickets WHERE TicketID = @TicketID";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@TicketID", ticketID);
                            deleteCommand.ExecuteNonQuery();
                        }
                    }

                    BindTickets();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModal", "closeModal();", true);
                }
            }
        }

        protected int GetCurrentUserID()
        {
            return int.Parse(Session["EmpId"].ToString());
        }
    }
}
