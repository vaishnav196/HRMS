//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data.SqlClient;
//using System.Data;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace HRMS
//{
//    public partial class ViewAllSolutions : System.Web.UI.Page
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                BindClosedTickets();
//            }
//        }

//        protected void BindClosedTickets()
//        {
//            string connectionString = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                string query = "SELECT TicketID, RaisedByName, RaisedToName, Designation, TicketDescription, Solution, ClosedDate FROM ClosedTickets";
//                using (SqlCommand command = new SqlCommand(query, connection))
//                {
//                    connection.Open();
//                    SqlDataAdapter adapter = new SqlDataAdapter(command);
//                    DataTable dt = new DataTable();
//                    adapter.Fill(dt);
//                    GridViewClosedTickets.DataSource = dt;
//                    GridViewClosedTickets.DataBind();
//                }
//            }
//        }
//    }
//}


using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace HRMS
{
    public partial class ViewAllSolutions : System.Web.UI.Page
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
                string query = "SELECT TicketID, RaisedBy, RaisedByName, RaisedTo, RaisedToName, Designation, TicketDescription, Solution, RaisedDate, ClosedDate " +
                               "FROM ClosedTickets";

                switch (ddlViewBy.SelectedValue)
                {
                    case "Daily":
                        query += " WHERE CONVERT(date, ClosedDate) = CONVERT(date, GETDATE())";
                        break;
                    case "Weekly":
                        query += " WHERE ClosedDate >= DATEADD(DAY, 1 - DATEPART(WEEKDAY, GETDATE()), CONVERT(date, GETDATE())) " +
                                 " AND ClosedDate < DATEADD(DAY, 8 - DATEPART(WEEKDAY, GETDATE()), CONVERT(date, GETDATE()))";
                        break;
                    case "Monthly":
                        query += " WHERE YEAR(ClosedDate) = YEAR(GETDATE()) AND MONTH(ClosedDate) = MONTH(GETDATE())";
                        break;
                }

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

        protected void ddlViewBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindClosedTickets();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ClosedTickets.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";

            GridViewClosedTickets.AllowPaging = false;
            BindClosedTickets();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int k = 0; k < GridViewClosedTickets.Columns.Count; k++)
            {
                // Add header
                sb.Append(GridViewClosedTickets.Columns[k].HeaderText + ',');
            }
            sb.Append("\r\n");

            for (int i = 0; i < GridViewClosedTickets.Rows.Count; i++)
            {
                for (int k = 0; k < GridViewClosedTickets.Columns.Count; k++)
                {
                    // Add data
                    sb.Append(GridViewClosedTickets.Rows[i].Cells[k].Text.Replace(",", string.Empty) + ',');
                }
                sb.Append("\r\n");
            }
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();

            GridViewClosedTickets.AllowPaging = true;
            BindClosedTickets(); // Rebind data after exporting
        }
    }
}
