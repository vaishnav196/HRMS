using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace HRMS
{
    public partial class ViewPaySlip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPaySlips();
            }
        }
        private void LoadPaySlips()
        {
            string empNo = Session["EmpId"].ToString(); // Assuming you store employee number in a session
            string connStr = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Id,Month, FilePath FROM PaySlips WHERE EmpNo = @EmpNo", conn);
                    cmd.Parameters.AddWithValue("@EmpNo", empNo);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        PaySlipGridView.DataSource = reader;
                        PaySlipGridView.DataKeyNames = new string[] { "Id" }; // Set the DataKeyNames to "Id"
                        PaySlipGridView.DataBind();
                    }
                    else
                    {
                        // No pay slips found for the employee
                        // You can display a message or handle this scenario as needed
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                Response.Write("Error: " + ex.Message);
            }
        }


        //private void LoadPaySlips()
        //{
        //    string empNo = Session["EmpId"].ToString(); // Assuming you store employee number in a session
        //    string connStr = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;

        //    using (SqlConnection conn = new SqlConnection(connStr))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT Id, Month, FilePath FROM PaySlips WHERE EmpNo = @EmpNo", conn);
        //        cmd.Parameters.AddWithValue("@EmpNo", empNo);
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        PaySlipGridView.DataSource = reader;
        //        PaySlipGridView.DataKeyNames = new string[] { "Id" }; // Set the DataKeyNames to "Id"
        //        PaySlipGridView.DataBind();
        //    }
        //}

        protected void PaySlipGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download" || e.CommandName == "View")
            {
                string filePath = e.CommandArgument.ToString();

                if (!string.IsNullOrEmpty(filePath))
                {
                    if (e.CommandName == "Download")
                    {
                        DownloadFile(filePath);
                    }
                    else if (e.CommandName == "View")
                    {
                        ViewFile(filePath);
                    }
                }
            }
        }

        private void DownloadFile(string filePath)
        {
            string physicalFilePath = filePath; // Construct the physical file path

            if (System.IO.File.Exists(physicalFilePath))
            {
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", $"attachment; filename={System.IO.Path.GetFileName(filePath)}");
                Response.TransmitFile(physicalFilePath);
                Response.End();
            }
            else
            {
                // Handle the case where the file does not exist
                Response.Write("The file does not exist.");
            }
        }

        //private void ViewFile(string filePath)
        //{
        //    string physicalFilePath = Server.MapPath("~/PaySlips/") + filePath; // Construct the physical file path

        //    if (System.IO.File.Exists(physicalFilePath))
        //    {
        //        Response.ContentType = "application/pdf";
        //        Response.AppendHeader("Content-Disposition", $"inline; filename={System.IO.Path.GetFileName(filePath)}");
        //        Response.TransmitFile(physicalFilePath);
        //        Response.End();
        //    }
        //    else
        //    {
        //        // Handle the case where the file does not exist
        //        Response.Write("The file does not exist.");
        //    }
        //}
        private void ViewFile(string filePath)
        {
            string physicalFilePath = filePath; // Construct the physical file path

            if (System.IO.File.Exists(physicalFilePath))
            {
                try
                {
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", $"inline; filename={System.IO.Path.GetFileName(filePath)}");
                    Response.TransmitFile(physicalFilePath);
                    Response.Flush(); // Flush the response to send content to the client
                    Response.End();
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    Response.Write("Error: " + ex.Message);
                }
                finally
                {
                    // Ensure that the file stream is closed
                    Response.Close();
                }
            }
            else
            {
                // Handle the case where the file does not exist
                Response.Write("The file does not exist.");
            }
        }

    }
}
