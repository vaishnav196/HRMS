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

                LoadLeaveRequests();
            }
        }

        private void LoadLeaveRequests()
        {
            try
            {
                conn.Open();
                string query = "SELECT RequestID, EmpID, Name, Email, FromDate, ToDate, TotalDays, AbsentDays, Status FROM LeaveRequests WHERE Status = 'Pending'";
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

                string getDetailsQuery = @"
                    SELECT e.LeaveBalance, lr.TotalDays, e.DateOfJoining
                    FROM Emp e
                    JOIN LeaveRequests lr ON e.EmpID = lr.EmpID
                    WHERE e.EmpID = @EmpID AND lr.FromDate = @FromDate AND lr.ToDate = @ToDate";
                SqlCommand getDetailsCmd = new SqlCommand(getDetailsQuery, conn);
                getDetailsCmd.Parameters.AddWithValue("@EmpID", empId);
                getDetailsCmd.Parameters.AddWithValue("@FromDate", fromDate);
                getDetailsCmd.Parameters.AddWithValue("@ToDate", toDate);

                SqlDataReader reader = getDetailsCmd.ExecuteReader();
                if (!reader.Read())
                {
                    Response.Write("<script>alert('Employee or leave request not found.')</script>");
                    return;
                }

                int currentLeaveBalance = reader.GetInt32(0);
                int totalDays = reader.GetInt32(1);
                DateTime dateOfJoining = reader.GetDateTime(2);
                reader.Close();

                int workingDays = CalculateWorkingDays(DateTime.Parse(fromDate), DateTime.Parse(toDate));

                int monthsPassed = ((DateTime.Now.Year - dateOfJoining.Year) * 12) + DateTime.Now.Month - dateOfJoining.Month;
                int maxLeaveDays = monthsPassed * 2;

                int leaveDays = Math.Min(workingDays, maxLeaveDays);
                int absentDays = workingDays - leaveDays;
                int newLeaveBalance = currentLeaveBalance - leaveDays;

                string updateLeaveQuery = @"
                    UPDATE LeaveRequests
                    SET Status = 'Approved', AbsentDays = @AbsentDays
                    WHERE EmpID = @EmpID AND FromDate = @FromDate AND ToDate = @ToDate";
                SqlCommand updateLeaveCmd = new SqlCommand(updateLeaveQuery, conn);
                updateLeaveCmd.Parameters.AddWithValue("@AbsentDays", absentDays);
                updateLeaveCmd.Parameters.AddWithValue("@EmpID", empId);
                updateLeaveCmd.Parameters.AddWithValue("@FromDate", fromDate);
                updateLeaveCmd.Parameters.AddWithValue("@ToDate", toDate);
                updateLeaveCmd.ExecuteNonQuery();

                string updateEmpQuery = "UPDATE Emp SET LeaveBalance = @NewLeaveBalance WHERE EmpID = @EmpID";
                SqlCommand updateEmpCmd = new SqlCommand(updateEmpQuery, conn);
                updateEmpCmd.Parameters.AddWithValue("@NewLeaveBalance", newLeaveBalance);
                updateEmpCmd.Parameters.AddWithValue("@EmpID", empId);
                updateEmpCmd.ExecuteNonQuery();

                Response.Write("<script>alert('Leave approved successfully.')</script>");
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
                string updateLeaveQuery = @"
                    UPDATE LeaveRequests
                    SET Status = 'Rejected'
                    WHERE EmpID = @EmpID AND FromDate = @FromDate AND ToDate = @ToDate";
                SqlCommand updateLeaveCmd = new SqlCommand(updateLeaveQuery, conn);
                updateLeaveCmd.Parameters.AddWithValue("@EmpID", empId);
                updateLeaveCmd.Parameters.AddWithValue("@FromDate", fromDate);
                updateLeaveCmd.Parameters.AddWithValue("@ToDate", toDate);
                updateLeaveCmd.ExecuteNonQuery();

                Response.Write("<script>alert('Leave rejected successfully.')</script>");
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

        private int CalculateWorkingDays(DateTime fromDate, DateTime toDate)
        {
            int totalDays = (toDate - fromDate).Days + 1;
            int workingDays = 0;

            for (int i = 0; i < totalDays; i++)
            {
                DateTime currentDate = fromDate.AddDays(i);
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDays++;
                }
            }

            return workingDays;
        }
    }
}
