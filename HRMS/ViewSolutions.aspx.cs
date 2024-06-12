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

        protected void BindClosedTickets()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TicketID, RaisedByName, RaisedToName, Designation, TicketDescription, Solution, ClosedDate FROM ClosedTickets";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    GridViewClosedTickets.DataSource = dt;
                    GridViewClosedTickets.DataBind();
                }
            }
        }
    }
}
