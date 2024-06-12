using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMS
{
    public partial class ViewSolution : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindClosedTickets();
            }
        }

        //protected void BindClosedTickets()
        //{
        //    string connectionString = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        string query = "SELECT TicketID, RaisedByName, RaisedToName, Designation, TicketDescription, Solution, ClosedDate FROM ClosedTickets";
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            connection.Open();
        //            SqlDataAdapter adapter = new SqlDataAdapter(command);
        //            DataTable dt = new DataTable();
        //            adapter.Fill(dt);
        //            GridViewClosedTickets.DataSource = dt;
        //            GridViewClosedTickets.DataBind();
        //        }
        //    }
        //}



        protected void BindClosedTickets()
        {
            // Get the current user's ID from the session
            int currentUserID = GetCurrentUserID();

            string connectionString = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Modify the query to filter tickets based on the current user's ID
                string query = "SELECT TicketID, RaisedByName, RaisedToName, Designation, TicketDescription, Solution, ClosedDate FROM ClosedTickets WHERE RaisedBy = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter for the current user's ID
                    command.Parameters.AddWithValue("@UserID", currentUserID);

                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    GridViewClosedTickets.DataSource = dt;
                    GridViewClosedTickets.DataBind();
                }
            }
        }

        protected int GetCurrentUserID()
        {
            // Assuming you have the user's ID stored in a session variable named "EmpID"
            if (Session["EmpID"] != null)
            {
                return Convert.ToInt32(Session["EmpID"]);
            }
            else
            {
                // Handle the case where the user ID is not available
                // For example, redirect to a login page or display an error message
                Response.Redirect("Login.aspx");
                return 0;
            }
        }

    }
}