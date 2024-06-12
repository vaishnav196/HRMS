using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;

namespace HRMS
{
    public partial class ApplyLeave : Page
    {
        SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            string cnf = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;
            conn = new SqlConnection(cnf);
            if (!IsPostBack)
            {
                LoadLeaveBalance();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string fromDateText = TextBox1.Text.Trim();
            string toDateText = TextBox2.Text.Trim();
            string reason = TextBox3.Text.Trim();
            string email = Session["MyUser"]?.ToString();

            if (string.IsNullOrEmpty(email))
            {
                Response.Write("<script>alert('Session not found. Please login again.')</script>");
                return;
            }

            if (!DateTime.TryParseExact(fromDateText, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate) ||
                !DateTime.TryParseExact(toDateText, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate))
            {
                Response.Write("<script>alert('Invalid date format. Please enter dates in YYYY-MM-DD format.')</script>");
                return;
            }

            if (fromDate > toDate)
            {
                Response.Write("<script>alert('From Date cannot be greater than To Date.')</script>");
                return;
            }

            try
            {
                conn.Open();

                // Fetch employee details
                string getEmpDetailsQuery = "SELECT EmpID, Name, LeaveBalance, DateOfJoining FROM Emp WHERE Email = @Email";
                SqlCommand getEmpDetailsCmd = new SqlCommand(getEmpDetailsQuery, conn);
                getEmpDetailsCmd.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = getEmpDetailsCmd.ExecuteReader();
                if (!reader.Read())
                {
                    Response.Write("<script>alert('Employee not found.')</script>");
                    return;
                }

                int empId = reader.GetInt32(0);
                string empName = reader.GetString(1);
                int currentLeaveBalance = reader.GetInt32(2);
                DateTime dateOfJoining = reader.GetDateTime(3);
                reader.Close();

                int totalDays = (toDate - fromDate).Days + 1;
                int workingDays = CalculateWorkingDays(fromDate, toDate);

                // Calculate accumulated leaves
                int accumulatedLeaves = CalculateAccumulatedLeaves(dateOfJoining, DateTime.Now);

                // Deduct leaves from accumulated balance
                int newLeaveBalance = currentLeaveBalance - workingDays;
                int absentDays = 0;

                if (newLeaveBalance < 0)
                {
                    absentDays = Math.Abs(newLeaveBalance);
                    newLeaveBalance = 0;
                }

                // Check if there's already a leave request for the same date range
                string checkLeaveQuery = "SELECT COUNT(*) FROM LeaveRequests WHERE EmpID = @EmpID AND FromDate = @FromDate AND ToDate = @ToDate";
                SqlCommand checkLeaveCmd = new SqlCommand(checkLeaveQuery, conn);
                checkLeaveCmd.Parameters.AddWithValue("@EmpID", empId);
                checkLeaveCmd.Parameters.AddWithValue("@FromDate", fromDate);
                checkLeaveCmd.Parameters.AddWithValue("@ToDate", toDate);
                int existingRequests = (int)checkLeaveCmd.ExecuteScalar();

                if (existingRequests > 0)
                {
                    // Update existing leave request
                    string updateLeaveQuery = "UPDATE LeaveRequests SET Reason = @Reason, TotalDays = @TotalDays, AbsentDays = @AbsentDays, Status = 'Pending' WHERE EmpID = @EmpID AND FromDate = @FromDate AND ToDate = @ToDate";
                    SqlCommand updateLeaveCmd = new SqlCommand(updateLeaveQuery, conn);
                    updateLeaveCmd.Parameters.AddWithValue("@Reason", reason);
                    updateLeaveCmd.Parameters.AddWithValue("@TotalDays", totalDays);
                    updateLeaveCmd.Parameters.AddWithValue("@AbsentDays", absentDays);
                    updateLeaveCmd.Parameters.AddWithValue("@EmpID", empId);
                    updateLeaveCmd.Parameters.AddWithValue("@FromDate", fromDate);
                    updateLeaveCmd.Parameters.AddWithValue("@ToDate", toDate);
                    updateLeaveCmd.ExecuteNonQuery();
                }
                else
                {
                    // Insert new leave request
                    string applyLeaveQuery = "INSERT INTO LeaveRequests (EmpID, Name, Email, FromDate, ToDate, Reason, TotalDays, AbsentDays, Status) VALUES (@EmpID, @Name, @Email, @FromDate, @ToDate, @Reason, @TotalDays, @AbsentDays, 'Pending')";
                    SqlCommand applyLeaveCmd = new SqlCommand(applyLeaveQuery, conn);
                    applyLeaveCmd.Parameters.AddWithValue("@EmpID", empId);
                    applyLeaveCmd.Parameters.AddWithValue("@Name", empName);
                    applyLeaveCmd.Parameters.AddWithValue("@Email", email);
                    applyLeaveCmd.Parameters.AddWithValue("@FromDate", fromDate);
                    applyLeaveCmd.Parameters.AddWithValue("@ToDate", toDate);
                    applyLeaveCmd.Parameters.AddWithValue("@Reason", reason);
                    applyLeaveCmd.Parameters.AddWithValue("@TotalDays", totalDays);
                    applyLeaveCmd.Parameters.AddWithValue("@AbsentDays", absentDays);
                    applyLeaveCmd.ExecuteNonQuery();
                }

                // Update the leave balance in the employee record
                string updateLeaveBalanceQuery = "UPDATE Emp SET LeaveBalance = @NewLeaveBalance WHERE EmpID = @EmpID";
                SqlCommand updateLeaveBalanceCmd = new SqlCommand(updateLeaveBalanceQuery, conn);
                updateLeaveBalanceCmd.Parameters.AddWithValue("@NewLeaveBalance", newLeaveBalance);
                updateLeaveBalanceCmd.Parameters.AddWithValue("@EmpID", empId);
                updateLeaveBalanceCmd.ExecuteNonQuery();

                // Update the labels
                Label1.Text = newLeaveBalance.ToString();
                Label2.Text = absentDays.ToString();
                LoadLeaveBalance();

                Response.Write("<script>alert('Leave applied successfully.')</script>");
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error: {ex.Message}')</script>");
            }
            finally
            {
                conn.Close();
            }

            LoadLeaveBalance();
        }

        private void LoadLeaveBalance()
        {
            string email = Session["MyUser"]?.ToString();

            if (string.IsNullOrEmpty(email))
            {
                Response.Write("<script>alert('Session not found. Please login again.')</script>");
                return;
            }

            try
            {
                conn.Open();

                string getLeaveBalanceQuery = "SELECT LeaveBalance, DateOfJoining FROM Emp WHERE Email = @Email";
                SqlCommand getLeaveBalanceCmd = new SqlCommand(getLeaveBalanceQuery, conn);
                getLeaveBalanceCmd.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = getLeaveBalanceCmd.ExecuteReader();
                if (!reader.Read())
                {
                    Response.Write("<script>alert('Employee not found.')</script>");
                    return;
                }

                int leaveBalance = reader.GetInt32(0);
                DateTime dateOfJoining = reader.GetDateTime(1);
                reader.Close();

                // Calculate accumulated leaves
                int accumulatedLeaves = CalculateAccumulatedLeaves(dateOfJoining, DateTime.Now);

                // Display the total leave balance
                Label1.Text = (leaveBalance + accumulatedLeaves).ToString();
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

        private int CalculateAccumulatedLeaves(DateTime dateOfJoining, DateTime currentDate)
        {
            int monthsPassed = ((currentDate.Year - dateOfJoining.Year) * 12) + currentDate.Month - dateOfJoining.Month;
            return monthsPassed * 2;
        }
    }
}