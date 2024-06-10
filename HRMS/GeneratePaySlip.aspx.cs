﻿using System;
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
                    bankName.Text = "ICICI Bank"; // Placeholder, adjust accordingly
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
            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));
            document.Open();

            // Adding the company logo
            Image logo = Image.GetInstance(Server.MapPath("~/Images/logo.png")); // Adjust the path accordingly
            logo.ScalePercent(24f);
            logo.Alignment = Element.ALIGN_CENTER;
            document.Add(logo);

            // Adding company name
            Paragraph companyName = new Paragraph("Masstech Business Solutions", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, BaseColor.BLUE));
            companyName.Alignment = Element.ALIGN_CENTER;
            document.Add(companyName);

            // Adding Payslip title
            Paragraph payslipTitle = new Paragraph("Payslip for the Month " + month.SelectedValue + " of 2023", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD));
            payslipTitle.Alignment = Element.ALIGN_CENTER;
            payslipTitle.SpacingAfter = 20f;
            document.Add(payslipTitle);

            // Adding Employee details
            PdfPTable employeeTable = new PdfPTable(2);
            employeeTable.WidthPercentage = 100;

            AddCellToTable(employeeTable, "NAME OF EMPLOYEE:", empName.Text);
            AddCellToTable(employeeTable, "DESIGNATION:", designation.Text);
            AddCellToTable(employeeTable, "BANK NAME:", bankName.Text);
            AddCellToTable(employeeTable, "EMPLOYEE EMAIL:", email.Text);
            AddCellToTable(employeeTable, "IFSC CODE:", "ICIC0000092");
            AddCellToTable(employeeTable, "DATE OF JOINING:", dateOfJoining.Text);
            AddCellToTable(employeeTable, "BANK ACCOUNT NO:", bankAccNo.Text);
            AddCellToTable(employeeTable, "CONTACT NO:", contactNo.Text);
            AddCellToTable(employeeTable, "PAN:", "FGKPB0088L");
            AddCellToTable(employeeTable, "DAYS IN MONTH:", workingDays.Text);
            AddCellToTable(employeeTable, "AADHAR:", "7902 8178 5003");
            AddCellToTable(employeeTable, "UAN:", "NA");
            AddCellToTable(employeeTable, "LEAVE TAKEN:", leavesTaken.Text);

            document.Add(employeeTable);

            // Adding Salary details
            PdfPTable salaryTable = new PdfPTable(3);
            salaryTable.WidthPercentage = 100;
            salaryTable.SpacingBefore = 20f;
            salaryTable.SpacingAfter = 20f;

            AddCellToTable(salaryTable, "GROSS SALARY", "AMOUNT", "DEDUCTION");
            AddCellToTable(salaryTable, "Basic", "5,250", "PF");
            AddCellToTable(salaryTable, "HRA", "2,625", "Professional Tax");
            AddCellToTable(salaryTable, "Travel Allowance", "1,600", "TDS");
            AddCellToTable(salaryTable, "Bonus", "700", "");
            AddCellToTable(salaryTable, "Special Allowance", "3,275", "");
            AddCellToTable(salaryTable, "Medical Re-imbursement", "1,250", "");

            decimal grossSalary = decimal.Parse(monthlySalary.Text);
            decimal netSalary = decimal.Parse(calculatedSalary.Text);
            decimal deductions = grossSalary - netSalary;

            AddCellToTable(salaryTable, "GROSS SALARY", grossSalary.ToString("F2"), "TOTAL DEDUCTION");
            AddCellToTable(salaryTable, "NET SALARY PAID", netSalary.ToString("F2"), deductions.ToString("F2"));

            document.Add(salaryTable);

            // Adding Footer
            Paragraph footer = new Paragraph("This is a computerised generated salary slip and does not require authentication", new Font(Font.FontFamily.HELVETICA, 8, Font.ITALIC));
            footer.Alignment = Element.ALIGN_CENTER;
            document.Add(footer);

            document.Close();
        }

        private void AddCellToTable(PdfPTable table, string text1, string text2, string text3 = "")
        {
            PdfPCell cell1 = new PdfPCell(new Phrase(text1));
            cell1.Border = Rectangle.NO_BORDER;
            table.AddCell(cell1);

            PdfPCell cell2 = new PdfPCell(new Phrase(text2));
            cell2.Border = Rectangle.NO_BORDER;
            table.AddCell(cell2);

            PdfPCell cell3 = new PdfPCell(new Phrase(text3));
            cell3.Border = Rectangle.NO_BORDER;
            table.AddCell(cell3);
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
