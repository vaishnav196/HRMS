﻿using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace HRMS
{
    public partial class GenerateOffer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Ensure user is logged in
            if (Session["MyUser"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            // Retrieve input data
            string empId = txtEmpId.Text;
            string name = txtName.Text;
            string email = txtEmail.Text;
            string dateOfJoining = txtDateOfJoining.Text;
            string salary = txtSalary.Text;

            // Generate offer letter content
            string offerLetter = GenerateOfferLetter(name, dateOfJoining, salary);

            // Generate PDF file name and save PDF
            string fileName = $"{name}_{DateTime.Now:yyyyMMddHHmmss}";
            string filePath = SavePdfToFileSystem(offerLetter, fileName);

            // Store offer letter metadata in the database
            StoreOfferLetterMetadata(empId, name, email, DateTime.Now, filePath);

            // Send email with attachment
            byte[] pdfBytes = File.ReadAllBytes(filePath);
            SendEmailWithAttachment(email, pdfBytes);
        }

        private void StoreOfferLetterMetadata(string empId, string name, string email, DateTime generatedDate, string filePath)
        {
            string connectionString = "Data Source=DESKTOP-567PV48\\SQLEXPRESS01;Initial Catalog=Hrms;Integrated Security=True;Encrypt=False"; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO OfferLetters (EmpId, Name, Email, GeneratedDate, FilePath) VALUES (@EmpId, @Name, @Email, @GeneratedDate, @FilePath)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmpId", empId);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@GeneratedDate", generatedDate);
                    command.Parameters.AddWithValue("@FilePath", filePath);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private string GenerateOfferLetter(string name, string dateOfJoining, string salary)
        {
            // Generate offer letter content
            return $" Offer Letter\nDear {name},\n\nWe are pleased to offer you the position. Your date of joining will be {dateOfJoining} and your salary will be {salary}.\n We are thrilled to extend to you an offer for the Software Developer position at MASSTECH. After careful consideration of your qualifications, experience, and interview performance, we are confident that you will make a valuable addition to our team.\nYour enthusiasm for [Software Industry and your impressive achievements, particularly accomplishments and skills, stood out during the selection process. We believe your expertise will contribute significantly to our ongoing success.\nSincerely,\nMasstech";
        }

        private string SavePdfToFileSystem(string content, string fileName)
        {
            string filePath = Server.MapPath($"~/OfferLetters/{fileName}.pdf");

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                PdfWriter writer = new PdfWriter(fs);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                document.Add(new Paragraph(content));
                document.Close();
            }

            return filePath;
        }

        private void SendEmailWithAttachment(string toEmail, byte[] pdfBytes)
        {
            // Email sender credentials
            string fromEmail = "vaish00721@gmail.com"; // Replace with your email
            string fromPassword = "kzuvycbbvbrdempp"; // Replace with your email password

            // Create mail message
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = "Your Offer Letter";
            mail.Body = "Please find attached your offer letter.";

            // Add PDF attachment
            using (MemoryStream ms = new MemoryStream(pdfBytes))
            {
                Attachment attachment = new Attachment(ms, "OfferLetter.pdf", "application/pdf");
                mail.Attachments.Add(attachment);

                // Send email using SMTP client
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587) // Use Gmail SMTP server
                {
                    Credentials = new NetworkCredential(fromEmail, fromPassword),
                    EnableSsl = true
                };

                smtpClient.Send(mail);
            }
        }
    }
}
