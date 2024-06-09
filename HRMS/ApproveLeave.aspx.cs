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

        //private void LoadLeaveRequests()
        //{
        //    try
        //    {
        //        conn.Open();
        //        string query = "SELECT EmpID, Name, Email, FromDate, ToDate, AbsentDays FROM LeaveRequests WHERE Status = 'Pending'";
        //        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        //        DataTable dt = new DataTable();
        //        adapter.Fill(dt);

        //        if (dt.Rows.Count > 0)
        //        {
        //            GridView1.DataSource = dt;
        //            GridView1.DataBind();
        //        }
        //        else
        //        {
        //            Response.Write("<script>alert('No pending leave requests found.')</script>");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write($"<script>alert('Error: {ex.Message}')</script>");
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}


        private void LoadLeaveRequests()
        {
            try
            {
                conn.Open();
                string query = "SELECT EmpID, Name, Email, FromDate, ToDate, AbsentDays FROM LeaveRequests WHERE Status = 'Pending'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                GridView1.DataSource = null;  // Clear existing data
                GridView1.DataBind();

                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('No pending leave requests found.')</script>");
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

                // Update leave balance
                SqlCommand updateBalanceCmd = new SqlCommand("UPDATE Emp SET LeaveBalance = LeaveBalance - (SELECT DATEDIFF(day, @FromDate, @ToDate) + 1 WHERE EmpID = @EmpID)", conn);
                updateBalanceCmd.Parameters.AddWithValue("@EmpID", empId);
                updateBalanceCmd.Parameters.AddWithValue("@FromDate", fromDate);
                updateBalanceCmd.Parameters.AddWithValue("@ToDate", toDate);
                updateBalanceCmd.ExecuteNonQuery();

                Response.Write("<script>alert('Leave request approved.')</script>");

                // Refresh gridview
                LoadLeaveRequests();
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

                Response.Write("<script>alert('Leave request rejected.')</script>");

                // Refresh gridview
                LoadLeaveRequests();
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


        //private void ApproveLeaveRequest(string empId, string fromDate, string toDate)
        //{
        //    try
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("UPDATE LeaveRequests SET Status = 'Approved' WHERE EmpID = @EmpID AND FromDate = @FromDate AND ToDate = @ToDate", conn);
        //        cmd.Parameters.AddWithValue("@EmpID", empId);
        //        cmd.Parameters.AddWithValue("@FromDate", fromDate);
        //        cmd.Parameters.AddWithValue("@ToDate", toDate);
        //        cmd.ExecuteNonQuery();

        //        // Update leave balance
        //        SqlCommand updateBalanceCmd = new SqlCommand("UPDATE Emp SET LeaveBalance = LeaveBalance - (SELECT DATEDIFF(day, @FromDate, @ToDate) + 1 WHERE EmpID = @EmpID)", conn);
        //        updateBalanceCmd.Parameters.AddWithValue("@EmpID", empId);
        //        updateBalanceCmd.Parameters.AddWithValue("@FromDate", fromDate);
        //        updateBalanceCmd.Parameters.AddWithValue("@ToDate", toDate);
        //        updateBalanceCmd.ExecuteNonQuery();

        //        Response.Write("<script>alert('Leave request approved.')</script>");

        //        // Refresh gridview
        //        LoadLeaveRequests();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write($"<script>alert('Error: {ex.Message}')</script>");
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

        //private void RejectLeaveRequest(string empId, string fromDate, string toDate)
        //{
        //    try
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("UPDATE LeaveRequests SET Status = 'Rejected' WHERE EmpID = @EmpID AND FromDate = @FromDate AND ToDate = @ToDate", conn);
        //        cmd.Parameters.AddWithValue("@EmpID", empId);
        //        cmd.Parameters.AddWithValue("@FromDate", fromDate);
        //        cmd.Parameters.AddWithValue("@ToDate", toDate);
        //        cmd.ExecuteNonQuery();

        //        Response.Write("<script>alert('Leave request rejected.')</script>");

        //        // Refresh gridview
        //        LoadLeaveRequests();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write($"<script>alert('Error: {ex.Message}')</script>");
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}
    }
}
