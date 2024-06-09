using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HRMS
{
    public partial class GeneratePaySlip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void getDetails_Click(object sender, EventArgs e)
        {
            string empNoValue = empNo.Text.Trim();
            string connStr = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Emp WHERE EmpID = @EmpNo", conn);
                cmd.Parameters.AddWithValue("@EmpNo", empNoValue);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    empName.Text = reader["Name"].ToString();
                    // Assuming Bank Name, Contact No, and Bank Account No are stored in different columns
                    bankName.Text = "Yes Bank"; // Placeholder, adjust accordingly
                    contactNo.Text = reader["Contact"].ToString();
                    bankAccNo.Text = "123456789"; // Placeholder, adjust accordingly
                    email.Text = reader["Email"].ToString();
                    designation.Text = reader["Role"].ToString();
                    dateOfJoining.Text = reader["DateOfJoining"].ToString();
                    monthlySalary.Text = reader["Salary"].ToString();
                }
            }
        }

        protected void calculate_Click(object sender, EventArgs e)
        {
            int workingDaysValue = int.Parse(workingDays.Text.Trim());
            int leavesTakenValue = int.Parse(leavesTaken.Text.Trim());
            decimal monthlySalaryValue = decimal.Parse(monthlySalary.Text.Trim());
            decimal perDaySalary = monthlySalaryValue / workingDaysValue;
            decimal calculatedSalaryValue = monthlySalaryValue - (perDaySalary * leavesTakenValue);

            calculatedSalary.Text = calculatedSalaryValue.ToString("F2");
        }

        protected void generatePaySlipPDF_Click(object sender, EventArgs e)
        {
            string pdfFilePath = Server.MapPath("~/PaySlips/") + "PaySlip.pdf";
            GeneratePDF(pdfFilePath);
            SendEmail(pdfFilePath);
            SavePaySlipToDatabase(pdfFilePath);
        }

        private void GeneratePDF(string pdfFilePath)
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));
            document.Open();
            document.Add(new Paragraph("Pay Slip"));
            document.Add(new Paragraph("Employee Name: " + empName.Text));
            document.Add(new Paragraph("Designation: " + designation.Text));
            document.Add(new Paragraph("Monthly Salary: ₹" + monthlySalary.Text));
            document.Add(new Paragraph("Leaves Taken: " + leavesTaken.Text));
            document.Add(new Paragraph("Calculated Salary: " + calculatedSalary.Text));
            document.Close();
        }

        private void SendEmail(string pdfFilePath)
        {
            string toEmail = email.Text;
            MailMessage mail = new MailMessage("vaish00721@gmail.com", toEmail);
            mail.Subject = "Your Pay Slip";
            mail.Body = "Please find attached your pay slip.";
            mail.Attachments.Add(new Attachment(pdfFilePath));

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Credentials = new System.Net.NetworkCredential("vaish00721@gmail.com", "kzuvycbbvbrdempp");
            client.EnableSsl = true;
            client.Send(mail);
        }


        private void SavePaySlipToDatabase(string pdfFilePath)
        {
            string connStr = ConfigurationManager.ConnectionStrings["hrms"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO PaySlips (EmpNo, Month, FilePath) VALUES (@EmpNo, @Month, @FilePath)", conn);
                cmd.Parameters.AddWithValue("@EmpNo", empNo.Text.Trim());
                cmd.Parameters.AddWithValue("@Month", month.SelectedValue);
                cmd.Parameters.AddWithValue("@FilePath", pdfFilePath);
                cmd.ExecuteNonQuery();
            }
        }

    }
}