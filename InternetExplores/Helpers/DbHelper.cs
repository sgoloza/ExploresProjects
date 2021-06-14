using InternetExplores.Models;
using InternetExplores.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Data;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using InternetExplores.Helpers;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Collections.Generic;

namespace InternetExplores.Helpers
{
    public class DbHelper
    {

        //InternetExplores@University
        public static void RegistrationOfStudent(IConfiguration configuration, RegisterViewModel mystudent)
        {
            //creating student object
            //student = new StudentModel { StudentName = StudentName, StudentSurname = Input.StudentSurname, StudetntIdNo = Input.StudetntIdNo, StudentPhoneNo = Input.StudentPhoneNo, StudentGender = Input.StudentGender, StudentDateOfBirth = Input.StudentDateOfBirth };



            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("InsertStudent", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@myStudentIdNo", mystudent.StudetntIdNo);
                    cmd.Parameters.AddWithValue("@myStudentName", mystudent.StudentName);
                    cmd.Parameters.AddWithValue("@myStudentSurname", mystudent.StudentSurname);
                    cmd.Parameters.AddWithValue("@myStudentPhoneNo", mystudent.StudentPhoneNo);
                    cmd.Parameters.AddWithValue("@myStudentGender", mystudent.StudentGender);
                    cmd.Parameters.AddWithValue("@myStudentDateOfBirth", mystudent.StudentDateOfBirth);
                    cmd.Parameters.AddWithValue("@myStudentEmail", mystudent.Email);
                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i >= 1)
                    {

                        //ViewData["Message"] = "New Employee Added Successfully";

                    }

                }

                connection.Close();
            }
        }
        public static int UpdatetudentDetails(IConfiguration configuration, StudentModel mystudent)
        {
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            int i;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("UpdateStudentDetail", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StreetName", mystudent.StreetName);
                    cmd.Parameters.AddWithValue("@Province", mystudent.Province);
                    cmd.Parameters.AddWithValue("@City", mystudent.City);
                    cmd.Parameters.AddWithValue("@PostalCode", mystudent.PostalCode);
                    cmd.Parameters.AddWithValue("@StudentlevelOfStudy", mystudent.StudentlevelOfStudy);
                    cmd.Parameters.AddWithValue("@StudentFaculty", mystudent.StudentFaculty);
                    cmd.Parameters.AddWithValue("@StudentNeedAccommodation", mystudent.StudentNeedAccommodation);
                    cmd.Parameters.AddWithValue("@StudentFinincialStatus", mystudent.StudentFinincialStatus);
                    cmd.Parameters.AddWithValue("@StudentDegree", mystudent.StudentDegree);
                    cmd.Parameters.AddWithValue("@StudentNo", mystudent.StudentNo);

                    connection.Open();
                    i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i >= 1)
                    {

                        //ViewData["Message"] = "New Employee Added Successfully";

                    }

                }

                connection.Close();
            }
            return i;
        }
        public static int insertSudentsDocuments(IConfiguration configuration, StudentModel mystudent)
        {
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            int i;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("InsertStudentDocuments", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentNo", mystudent.StudentNo);
                    cmd.Parameters.AddWithValue("@IdUrl", mystudent.idcopyUrl);
                    cmd.Parameters.AddWithValue("@KinUrl", mystudent.nextofKinUrl);
                    cmd.Parameters.AddWithValue("@MatricUrl", mystudent.matricResultUrl);
                    cmd.Parameters.AddWithValue("@FProofUrl", mystudent.financialProofUrl);
                    connection.Open();
                    i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i >= 1)
                    {

                        //ViewData["Message"] = "New Employee Added Successfully";

                    }

                }

                connection.Close();
            }
            return i;
        }
        public static StudentFiles GetAllStudentDocuments(IConfiguration configuration, int StudentNO)
        {
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            StudentFiles studentfiles = new StudentFiles();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("SelectAllStudentFiles", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentNO", StudentNO);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            studentfiles = new StudentFiles
                            {
                                idcopyUrl = reader["IdUrl"].ToString(),
                                matricResultUrl = reader["MatricUrl"].ToString(),
                                financialProofUrl = reader["FProofUrl"].ToString(),
                                nextofKinUrl = reader["KinUrl"].ToString()
                            };
                        }
                    }
                }
            }
            return studentfiles;
        }
        public static StudentModel GetAllStudent(IConfiguration configuration, string email)
        {
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            StudentModel student = new StudentModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("SelectAllStudent", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader["StudentEmail"].ToString().Equals(email))
                            {
                                student = new StudentModel
                                {
                                    StudentNo = Convert.ToInt32(reader["StudentNo"]),
                                    StudentIdNo = reader["StudentIdNo"].ToString(),
                                    StudentName = reader["StudentName"].ToString(),
                                    StudentSurname = reader["StudentSurname"].ToString(),
                                    StudentPhoneNo = reader["StudentPhoneNo"].ToString(),
                                    StudentEmail = reader["StudentEmail"].ToString(),
                                    StudentGender = reader["StudentGender"].ToString(),
                                    StudentDateOfBirth = Convert.ToDateTime(reader["StudentDateOfBirth"].ToString()),
                                    StudentHomeLanguage = reader["StudentHomeLanguage"].ToString(),
                                    StudentFinincialStatus = reader["StudentFinincialStatus"].ToString(),
                                    StudentDegree = reader["StudentDegree"].ToString(),
                                    StudentlevelOfStudy = reader["StudentlevelOfStudy"].ToString(),
                                    StudentFaculty = reader["StudentFaculty"].ToString(),
                                    StudentNeedAccommodation = reader["StudentNeedAccommodation"].ToString(),
                                    StudentRiskStatus = reader["StudentRiskStatus"].ToString(),
                                    StreetName = reader["StreetName"].ToString(),
                                    Province = reader["Province"].ToString(),
                                    City = reader["City"].ToString(),
                                    PostalCode = Convert.ToInt32(reader["PostalCode"]),
                                    RoomID = Convert.ToInt32(reader["RoomID"]),
                                    StudentBalance = Convert.ToInt32(reader["StudentBalance"])
                                };
                            }
                        }
                    }
                }

                connection.Close();
            }
            return student;
        }
        public static List<StudentModel> SelectAllStudents(IConfiguration configuration , List<StudentModel> allStudent)
        {
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            StudentModel student = new StudentModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("SelectAllStudents", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (true)
                            {
                                student = new StudentModel
                                {
                                    StudentNo = Convert.ToInt32(reader["StudentNo"]),
                                    StudentIdNo = reader["StudentIdNo"].ToString(),
                                    StudentName = reader["StudentName"].ToString(),
                                    StudentSurname = reader["StudentSurname"].ToString(),
                                    StudentPhoneNo = reader["StudentPhoneNo"].ToString(),
                                    StudentEmail = reader["StudentEmail"].ToString(),
                                    StudentGender = reader["StudentGender"].ToString(),
                                    StudentDateOfBirth = Convert.ToDateTime(reader["StudentDateOfBirth"].ToString()),
                                    StudentHomeLanguage = reader["StudentHomeLanguage"].ToString(),
                                    StudentFinincialStatus = reader["StudentFinincialStatus"].ToString(),
                                    StudentDegree = reader["StudentDegree"].ToString(),
                                    StudentlevelOfStudy = reader["StudentlevelOfStudy"].ToString(),
                                    StudentFaculty = reader["StudentFaculty"].ToString(),
                                    StudentNeedAccommodation = reader["StudentNeedAccommodation"].ToString(),
                                    StudentRiskStatus = reader["StudentRiskStatus"].ToString(),
                                    StreetName = reader["StreetName"].ToString(),
                                    Province = reader["Province"].ToString(),
                                    City = reader["City"].ToString(),
                                    PostalCode = Convert.ToInt32(reader["PostalCode"]),
                                    RoomID = Convert.ToInt32(reader["RoomID"]),
                                    StudentBalance = Convert.ToInt32(reader["StudentBalance"]),
                                    idcopyUrl = reader["IdUrl"].ToString(),
                                    matricResultUrl = reader["MatricUrl"].ToString(),
                                    financialProofUrl = reader["FProofUrl"].ToString(),
                                    nextofKinUrl = reader["KinUrl"].ToString()
                                };
                            }
                            allStudent.Add(student);
                        }
                    }
                }

                connection.Close();
            }
            return allStudent;
        }
        public static async void SendEmails(string messageType, StudentModel myStudent)
        {
            string html= "<!DOCTYPE html>";
            string error = string.Empty;

            try
            {
                string emailBody = string.Empty, subject = string.Empty , adminsubject = string.Empty;


                html += "<head>";
                html += "<meta charset='UTF -8'>";
                html += "<meta name='viewport' content='width=device-width,initial-scale=1'>";
                html += "<meta name='x-apple-disable-message-reformatting'>";
                html += "<title></title>";
                html += "<!--[if mso]>";
                html += "<noscript>";
                html += "<xml>";
                html += "<o:OfficeDocumentSettings>";
                html += "<o:PixelsPerInch>96</o:PixelsPerInch>";
                html += "</o:OfficeDocumentSettings>";
                html += "</xml>";
                html += "</noscript>";
                html += "<![endif]-->";
                html += "<style>";
                html += "table, td, div, h1, p {font-family: Arial, sans-serif;}";
                html += "</style>";
                html += "</head>";
                html += "<body style='margin:0;padding:0;'>";
                html += "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;'>";
                html += "<tr>";
                html += "<td align='center' style='padding:0;'>";
                html += "<table role='presentation' style='width:602px;border-collapse:collapse;border:1px solid #cccccc;border-spacing:0;text-align:left;'>";
                html += "<tr>";
                html += "<td align='center' style='padding:40px 0 30px 0;background:#70bbd9;'>";
                html += "<p style='margin:0;font-size:30px;line-height:16px;font-family:Arial,sans-serif;color:#ffffff;'>";
                html += " <b>Internet Explores UNIVERSITY</b><br/>";
                html += "</p>";
                html += "</td>";
                html += "</tr>";
                html += "<tr>";
                html += "<td style='padding:36px 30px 42px 30px;'>";
                html += "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'>";
                html += "<tr>";
                string adminhtml = html;





                switch (messageType)
                {
                    case "Registration":
                        subject = "Application to Study at Internet Explores University";
                        html += "<h4 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>Dear <b> "+ myStudent.StudentName +" "+ myStudent.StudentSurname + "</b></h4><br />";
                        html += "<td style='padding:0 0 36px 0;color:#153643;'>";
                        html += "<h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'> <b>We have received your application.</b></h1>";
                        html += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Thank you for taking the first steps toward personal development, career growth, and a brighter future.</p>";
                        html += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Visit <a href='https://localhost:44339/Student/Registration' style='color:#ee4c50;text-decoration:underline;'>Internet Explores university </a>for application follow up</p><br />";
                        html += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Thank you</p>";

                        html += "</td>";
                        emailBody += "<p>Many thanks, <b>the team at <b> Internet Explores University</b></p></body></html>";


                        adminhtml += "<td style='padding:0 0 36px 0;color:#153643;'>";
                        adminhtml += "<h4 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>Dear <b>Admin</b></h4><br />";
                        adminhtml += "<h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'> Student <b>" + myStudent.StudentName + " " + myStudent.StudentSurname + " have placed an application.</b></h1>";
                        adminhtml += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'></p>";
                        adminhtml += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Visit <a href='#' style='color:#ee4c50;text-decoration:underline;'>Internet Explores university </a>To view student application</p>";
                        adminhtml += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Thank you</p><br />";
                        adminsubject = "New Student Application";
                        html += "</td>";
                        adminhtml += "</td>";
                        break;
                    case "Registration1":



                        break;
                    case "reseller dispatch":

                        break;
                    default:
                        break;
                }
                string lowerHtmlBody = string.Empty;
                lowerHtmlBody += "</tr>";
                lowerHtmlBody += "<tr>";
                lowerHtmlBody += "<td style='padding:0;'>";
                lowerHtmlBody += "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'>";
                lowerHtmlBody += "<tr>";
                lowerHtmlBody += "<td style='width:260px;padding:0;vertical-align:top;color:#153643;'>";
                lowerHtmlBody += "<p style='margin:0 0 25px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'><img src='https://assets.codepen.io/210284/left.gif' alt='' width='260' style='height:auto;display:block;' /></p>";
                lowerHtmlBody += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tempus adipiscing felis, sit amet blandit ipsum volutpat sed. Morbi porttitor, eget accumsan dictum, est nisi libero ultricies ipsum, in posuere mauris neque at erat.</p>";
                lowerHtmlBody += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'><a href='http://www.example.com' style='color:#ee4c50;text-decoration:underline;'>Blandit ipsum volutpat sed</a></p>";
                lowerHtmlBody += "</td>";
                lowerHtmlBody += "<td style='width:20px;padding:0;font-size:0;line-height:0;'>&nbsp;</td>";
                lowerHtmlBody += "<td style='width:260px;padding:0;vertical-align:top;color:#153643;'>";
                lowerHtmlBody += "<p style='margin:0 0 25px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'><img src='https://assets.codepen.io/210284/right.gif' alt='' width='260' style='height:auto;display:block;' /></p>";
                lowerHtmlBody += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Morbi porttitor, eget est accumsan dictum, nisi libero ultricies ipsum, in posuere mauris neque at erat. Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tempus adipiscing felis, sit amet blandit ipsum volutpat sed.</p>";
                lowerHtmlBody += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'><a href='http://www.example.com' style='color:#ee4c50;text-decoration:underline;'>In tempus felis blandit</a></p>";
                lowerHtmlBody += "</td>";
                lowerHtmlBody += "</tr>";
                lowerHtmlBody += "</table>";
                lowerHtmlBody += "</td>";
                lowerHtmlBody += "</tr>";
                lowerHtmlBody += "</table>";
                lowerHtmlBody += "</td>";
                lowerHtmlBody += "</tr>";
                lowerHtmlBody += "<tr>";
                lowerHtmlBody += "<td style='padding:30px;background:#ee4c50;'>";
                lowerHtmlBody += "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:9px;font-family:Arial,sans-serif;'>";
                lowerHtmlBody += "<tr>";
                lowerHtmlBody += "<td style='padding:0;width:50%;' align='left'>";
                lowerHtmlBody += "<p style='margin:0;font-size:14px;line-height:16px;font-family:Arial,sans-serif;color:#ffffff;'>";
                lowerHtmlBody += " &copy; 2021 - Internet Explores <b>UNIVERSITY</b><br/><a href='#' style='color:#ffffff;text-decoration:underline;'></a>";
                lowerHtmlBody += "</p>";
                lowerHtmlBody += "</td>";
                lowerHtmlBody += "<td style='padding:0;width:50%;' align='right'>";
                lowerHtmlBody += "<table role='presentation' style='border-collapse:collapse;border:0;border-spacing:0;'>";
                lowerHtmlBody += "<tr>";
                lowerHtmlBody += "<td style='padding:0 0 0 10px;width:38px;'>";
                lowerHtmlBody += "<a href='#' class='fa fa-twitter' style='color:#ffffff;'></a>";
                lowerHtmlBody += "</td>";
                lowerHtmlBody += "<td style='padding:0 0 0 10px;width:38px;'>";
                lowerHtmlBody += "<a href='http://www.facebook.com/' style='color:#ffffff;'></a>";
                lowerHtmlBody += "</td>";
                lowerHtmlBody += "</tr>";
                lowerHtmlBody += "</table>";
                lowerHtmlBody += "</tr>";
                lowerHtmlBody += "</table>";
                lowerHtmlBody += "</td>";
                lowerHtmlBody += "</tr>";
                lowerHtmlBody += "</table>";
                lowerHtmlBody += "</td>";
                lowerHtmlBody += "</tr>";
                lowerHtmlBody += "</table>";
                lowerHtmlBody += "</body>";
                lowerHtmlBody += "</html>";
                adminhtml += lowerHtmlBody;
                html += lowerHtmlBody;



                if (messageType.Equals("Registration"))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        var senderEmail = new MailAddress("internetexploresuniversity@gmail.com", "Internet Explores");
                        var receiverEmail = new MailAddress("218027046@stu.ukzn.ac.za", myStudent.StudentName.ToString() + " " + myStudent.StudentSurname.ToString());
                        var password = "InternetExplores@University";
                        var smtp = new System.Net.Mail.SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };

                        if (i == 0)
                        {
                            receiverEmail = new MailAddress("218027046@stu.ukzn.ac.za", myStudent.StudentName.ToString() + " " + myStudent.StudentSurname.ToString());
                            using (var mess = new MailMessage(senderEmail, receiverEmail))
                            {
                                emailBody = html;
                                mess.Subject = subject;
                                mess.Body = emailBody;
                                mess.IsBodyHtml = true;
                                {
                                    await smtp.SendMailAsync(mess);
                                }
                            }
                        }
                        else if(i == 1)
                        {
                            receiverEmail = new MailAddress("internetexploresuniversity@gmail.com","Admin");
                            using (var mess = new MailMessage(senderEmail, receiverEmail))
                            {
                                emailBody = adminhtml;
                                mess.Subject = subject;
                                mess.Body = emailBody;
                                mess.IsBodyHtml = true;
                                {
                                    await smtp.SendMailAsync(mess);
                                }
                            }
                        }

                        
                    }

                }
                else
                {
                    var senderEmail = new MailAddress("internetexploresuniversity@gmail.com", "Internet Explores");
                    var receiverEmail = new MailAddress("218027046@stu.ukzn.ac.za", myStudent.StudentName.ToString() + " " + myStudent.StudentSurname.ToString());
                    var password = "InternetExplores@University";
                 
                    var smtp = new System.Net.Mail.SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };

                    using (var mess = new MailMessage(senderEmail, receiverEmail))
                    {
                        emailBody = html;
                        mess.Subject = subject;
                        mess.Body = emailBody;
                        mess.IsBodyHtml = true;
                        {
                            await smtp.SendMailAsync(mess);
                        }
                    }
                }

                error = "Success";
            }
            catch (Exception)
            {
                error =  "Some Error";
            }

        }
    }
}
