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

        //private void GeneratePDF(string pdfFilePath)
        //{
        //    Document document = new Document();
        //    PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));
        //    document.Open();

        //    document.Add(new Paragraph("Pay Slip"));
        //    document.Add(new Paragraph("Employee Name: " + empName.Text));
        //    document.Add(new Paragraph("Designation: " + designation.Text));
        //    document.Add(new Paragraph("Monthly Salary: ₹" + monthlySalary.Text));
        //    document.Add(new Paragraph("Leaves Taken: " + leavesTaken.Text));
        //    document.Add(new Paragraph("Calculated Salary: " + calculatedSalary.Text));
        //    document.Close();
        //}

        private void GeneratePDF(string pdfFilePath)
        {
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));
            document.Open();

            // Add header
            PdfPTable headerTable = new PdfPTable(1);
            headerTable.WidthPercentage = 100;
            PdfPCell headerCell = new PdfPCell(new Phrase("Masstech", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD)));
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(headerCell);

            headerCell = new PdfPCell(new Phrase("Business Solutions", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL)));
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(headerCell);

            document.Add(headerTable);

            // Add payslip details
            document.Add(new Paragraph("\nPayslip for the Month of " + month.SelectedValue, new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD)));
            document.Add(new Paragraph("\n"));

            // Add employee details
            PdfPTable infoTable = new PdfPTable(2);
            infoTable.WidthPercentage = 100;
            infoTable.AddCell(GetCell("NAME OF EMPLOYEE: " + empName.Text, PdfPCell.ALIGN_LEFT));
            infoTable.AddCell(GetCell("DESIGNATION: " + designation.Text, PdfPCell.ALIGN_LEFT));
            infoTable.AddCell(GetCell("BANK NAME: " + bankName.Text, PdfPCell.ALIGN_LEFT));
            infoTable.AddCell(GetCell("EMPLOYEE EMAIL: " + email.Text, PdfPCell.ALIGN_LEFT));
            infoTable.AddCell(GetCell("IFSC CODE: ICIC0000092", PdfPCell.ALIGN_LEFT)); // Hardcoded value
            infoTable.AddCell(GetCell("DATE OF JOINING: " + dateOfJoining.Text, PdfPCell.ALIGN_LEFT));
            infoTable.AddCell(GetCell("BANK ACCOUNT NO: " + bankAccNo.Text, PdfPCell.ALIGN_LEFT));
            infoTable.AddCell(GetCell("CONTACT NO: " + contactNo.Text, PdfPCell.ALIGN_LEFT));
            infoTable.AddCell(GetCell("PAN: FGKPB0088L", PdfPCell.ALIGN_LEFT)); // Hardcoded value
            infoTable.AddCell(GetCell("DAYS IN MONTH: " + workingDays.Text, PdfPCell.ALIGN_LEFT));
            infoTable.AddCell(GetCell("AADHAR: 7902 8178 5003", PdfPCell.ALIGN_LEFT)); // Hardcoded value
            infoTable.AddCell(GetCell("UAN: NA", PdfPCell.ALIGN_LEFT)); // Hardcoded value
            infoTable.AddCell(GetCell("LEAVE TAKEN: " + leavesTaken.Text, PdfPCell.ALIGN_LEFT));
            document.Add(infoTable);

            // Add salary details
            PdfPTable salaryTable = new PdfPTable(4);
            salaryTable.WidthPercentage = 100;
            salaryTable.AddCell(GetCell("GROSS SALARY", PdfPCell.ALIGN_CENTER, true));
            salaryTable.AddCell(GetCell("AMOUNT", PdfPCell.ALIGN_CENTER, true));
            salaryTable.AddCell(GetCell("DEDUCTIONS", PdfPCell.ALIGN_CENTER, true));
            salaryTable.AddCell(GetCell("NET SALARY", PdfPCell.ALIGN_CENTER, true));

            salaryTable.AddCell(GetCell(monthlySalary.Text, PdfPCell.ALIGN_CENTER));
            salaryTable.AddCell(GetCell(monthlySalary.Text, PdfPCell.ALIGN_CENTER));
            salaryTable.AddCell(GetCell((decimal.Parse(monthlySalary.Text) - decimal.Parse(calculatedSalary.Text)).ToString("F2"), PdfPCell.ALIGN_CENTER));
            salaryTable.AddCell(GetCell(calculatedSalary.Text, PdfPCell.ALIGN_CENTER));

            document.Add(salaryTable);

            document.Close();
            writer.Close();
        }


        private PdfPCell GetCell(string text, int alignment, bool isBold = false, int colspan = 1)
        {
            Font font;
            if (isBold)
            {
                font = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD);
            }
            else
            {
                font = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL);
            }

            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.HorizontalAlignment = alignment;
            cell.Border = PdfPCell.NO_BORDER;
            cell.Colspan = colspan;
            return cell;
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