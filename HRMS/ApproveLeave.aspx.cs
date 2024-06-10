using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace HRMS
{
    public partial class ApproveLeave : System.Web.UI.Page
    {
        SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            string cnf = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
            conn = new SqlConnection(cnf);

            if (!IsPostBack)
            {
                LoadLeaveRequests();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve" || e.CommandName == "Reject")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[rowIndex];

                string empId = row.Cells[0].Text;
                string fromDate = row.Cells[3].Text;
                string toDate = row.Cells[4].Text;

                if (e.CommandName == "Approve")
                {
                    ApproveLeaveRequest(empId, fromDate, toDate);
                }
                else if (e.CommandName == "Reject")
                {
                    RejectLeaveRequest(empId, fromDate, toDate);
                }
            }
        }

        private void LoadLeaveRequests()
        {
            try
            {
                conn.Open();
                string query = "SELECT EmpID, Name, Email, FromDate, ToDate, AbsentDays, Status FROM LeaveRequests WHERE Status = 'Pending'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();
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

        private void ApproveLeaveRequest(string empId, string fromDate, string toDate)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE LeaveRequests SET Status = 'Approved' WHERE EmpID = @EmpID AND FromDate = @FromDate AND ToDate = @ToDate", conn);
                cmd.Parameters.AddWithValue("@EmpID", empId);
                cmd.Parameters.AddWithValue("@FromDate", fromDate);
                cmd.Parameters.AddWithValue("@ToDate", toDate);
                cmd.ExecuteNonQuery();

                UpdateGridViewRow(empId, fromDate, toDate, "Approved");
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

        private void RejectLeaveRequest(string empId, string fromDate, string toDate)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE LeaveRequests SET Status = 'Rejected' WHERE EmpID = @EmpID AND FromDate = @FromDate AND ToDate = @ToDate", conn);
                cmd.Parameters.AddWithValue("@EmpID", empId);
                cmd.Parameters.AddWithValue("@FromDate", fromDate);
                cmd.Parameters.AddWithValue("@ToDate", toDate);
                cmd.ExecuteNonQuery();

                UpdateGridViewRow(empId, fromDate, toDate, "Rejected");
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

        private void UpdateGridViewRow(string empId, string fromDate, string toDate, string status)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.Cells[0].Text == empId && row.Cells[3].Text == fromDate && row.Cells[4].Text == toDate)
                {
                    row.Cells[6].Text = status;
                    break;
                }
            }
        }
    }
}
