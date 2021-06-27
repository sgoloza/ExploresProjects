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
                    cmd.Parameters.AddWithValue("@myStudentName", mystudent.StudentName);
                    cmd.Parameters.AddWithValue("@myStudentSurname", mystudent.StudentSurname);
                    cmd.Parameters.AddWithValue("@myStudentPhoneNo", mystudent.StudentPhoneNo);
                    cmd.Parameters.AddWithValue("@myStudentGender", mystudent.StudentGender);
                    cmd.Parameters.AddWithValue("@myStudentEmail", mystudent.Email);
                    cmd.Parameters.AddWithValue("@myStudentDateOfBirth", mystudent.StudentDateOfBirth);
                    cmd.Parameters.AddWithValue("@myStudentIdNo", mystudent.StudentIdNo);
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

        public static void RegistrationOfAdmin(IConfiguration configuration, AdminModel adminModel)
        {
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("InsertAdmin", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@myAdminIdNo", adminModel.AdminIDNo);
                    cmd.Parameters.AddWithValue("@myAdminName", adminModel.AdminSurname);
                    cmd.Parameters.AddWithValue("@myAdminSurname", adminModel.AdminSurname);
                    cmd.Parameters.AddWithValue("@myAdminPhoneNo", adminModel.AdminPhoneNo);
                    cmd.Parameters.AddWithValue("@status", adminModel.AdminStatus);
                    cmd.Parameters.AddWithValue("@type", adminModel.AdminType);
                    cmd.Parameters.AddWithValue("@myAdminEmail", adminModel.AdminEmail);
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
        public static void UpdateAdmin(IConfiguration configuration, AdminModel adminModel)
        {
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("UpdateAdmin", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", adminModel.AdminID);
                    cmd.Parameters.AddWithValue("@phonenumber", adminModel.AdminPhoneNo);
                    cmd.Parameters.AddWithValue("@status", adminModel.AdminStatus);
                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
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
        public static int UpdatetudentProfile(IConfiguration configuration, StudentModel mystudent)
        {
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            int i;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("UpdateStudentProfile", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StreetName", mystudent.StreetName);
                    cmd.Parameters.AddWithValue("@Province", mystudent.Province);
                    cmd.Parameters.AddWithValue("@City", mystudent.City);
                    cmd.Parameters.AddWithValue("@PostalCode", mystudent.PostalCode);
                    cmd.Parameters.AddWithValue("@PhoneNo", mystudent.StudentPhoneNo); 
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
        public static int UpdateStudentStatus(IConfiguration configuration, StudentAppModel mystudent)
        {
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            int i;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("UpdateStudentStatus", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@application", mystudent.status);
                    cmd.Parameters.AddWithValue("@email", mystudent.Email);
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
        public static void UpdatesModuleDetails(IConfiguration configuration, ModuleDeatilsModel myModule)
        {
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            int i;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("UpadteModule", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@myModuleDescription", myModule.ModuleDescription);
                    cmd.Parameters.AddWithValue("@myModuleCost", myModule.ModuleCost);
                    cmd.Parameters.AddWithValue("@myModuleCredit", myModule.ModuleCredit);
                    cmd.Parameters.AddWithValue("@myModulePre_requisites", myModule.ModulePre_requisites);
                    cmd.Parameters.AddWithValue("@myModulesStatus", myModule.ModulesStatus);
                    cmd.Parameters.AddWithValue("@myModuleCode", myModule.ModuleCode);

                    connection.Open();
                    i = cmd.ExecuteNonQuery();
                    connection.Close();

                }

                connection.Close();
            }
            
        }
        public static void InsertModulesDetails(IConfiguration configuration, ModuleModel myModule)
        {
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            int i;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("InsertModule", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@des", myModule.ModuleDescription);
                    cmd.Parameters.AddWithValue("@name", myModule.Modulename);
                    cmd.Parameters.AddWithValue("@cost", myModule.ModuleCost);
                    cmd.Parameters.AddWithValue("@preR", myModule.ModulePre_requisites);
                    cmd.Parameters.AddWithValue("@status", myModule.ModulesStatus);
                    cmd.Parameters.AddWithValue("@level", myModule.levelOdstudy);
                    cmd.Parameters.AddWithValue("@credit", myModule.ModuleCredit);
                    cmd.Parameters.AddWithValue("@code", myModule.ModuleCode);
                    cmd.Parameters.AddWithValue("@facult", myModule.faculty);


                    connection.Open();
                    i = cmd.ExecuteNonQuery();
                    connection.Close();

                }

                connection.Close();
            }

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

        public static string GetAllStudentFacultys(IConfiguration configuration, string Degree)
        {
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            string degreename = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("getFaculty", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@degree", Degree);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            degreename = reader["F_code"].ToString();
                            
                        }
                    }
                }
            }
            return degreename;
        }
        public static List<AdminModel>  GetListOfAdmin(IConfiguration configuration) {
            List<AdminModel> adList = new List<AdminModel>();
            AdminModel mymodel = new AdminModel();
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            string degreename = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("getAdminList", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            mymodel = new AdminModel
                            {
                                AdminID = Convert.ToInt32(reader["AdminID"]),
                                AdminName = reader["AdminName"].ToString(),
                                AdminSurname = reader["AdminSurname"].ToString(),
                                AdminPhoneNo = reader["AdminPhoneNo"].ToString(),
                                AdminEmail = reader["AdminEmail"].ToString(),
                                AdminIDNo = reader["AdminIDNo"].ToString(),
                                AdminStatus = reader["AdminType"].ToString(),
                                AdminType = reader["AdminStatus"].ToString(),
                            };
                            adList.Add(mymodel);
                        }
                    }
                }
            }
            return adList;
        }
        public static int ActiveStudents(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            StudentFiles studentfiles = new StudentFiles();
            int i = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("ActiveStudents", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            i++;
                        }
                    }
                 }
            }
            return i;
        }
        public static List<PaymentModel> getStudentsOfDayPayments(IConfiguration configuration)
        {
            List<PaymentModel> studentpayments = new List<PaymentModel>();
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            PaymentModel payments = new PaymentModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("NewPaymentsOnTheDay", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            payments = new PaymentModel
                            {
                                paymentID = Convert.ToInt32(reader["paymentID"]),
                                StudentNo = Convert.ToInt32(reader["StudentNo"]),
                                paymenttype = reader["paymentType"].ToString(),
                                paymentAmount = Decimal.Parse(reader["paymentAmount"].ToString()),
                                paymentDescription = reader["paymentDescription"].ToString(),
                                bankName = reader["bankName"].ToString(),
                                paymentDate = DateTime.Parse(reader["paymentDate"].ToString()),
                                PayemntNowdate = DateTime.Parse(reader["dateuploaded"].ToString()),
                                paymentProofUrl = reader["paymentUrl"].ToString()
                            };
                            studentpayments.Add(payments);

                        }

                    }
                }

                connection.Close();
            }
            return studentpayments;
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
                                    StudentGender = reader["StudentGender"].ToString()
                                };
                                if (reader["StudentDateOfBirth"] != DBNull.Value)
                                {
                                    student.StudentDateOfBirth = Convert.ToDateTime(reader["StudentDateOfBirth"].ToString());
                                }
                                if (reader["StudentHomeLanguage"] != DBNull.Value) {
                                    student.StudentHomeLanguage = reader["StudentHomeLanguage"].ToString();
                                }
                                if (reader["StudentFinincialStatus"] != DBNull.Value) {
                                    student.StudentFinincialStatus = reader["StudentFinincialStatus"].ToString();
                                }
                                if (reader["StudentDegree"] != DBNull.Value) {
                                    student.StudentDegree = reader["StudentDegree"].ToString();
                                }
                                if (reader["StudentlevelOfStudy"] != DBNull.Value)
                                {
                                    student.StudentlevelOfStudy = reader["StudentlevelOfStudy"].ToString();
                                }
                                if (reader["StudentFaculty"] != DBNull.Value)
                                {
                                    student.StudentFaculty = reader["StudentFaculty"].ToString();
                                }
                                if (reader["StudentNeedAccommodation"] != DBNull.Value)
                                {
                                    student.StudentNeedAccommodation = reader["StudentNeedAccommodation"].ToString();
                                }
                                if (reader["StudentRiskStatus"] != DBNull.Value)
                                {
                                    student.StudentRiskStatus = reader["StudentRiskStatus"].ToString();
                                }
                                if (reader["StreetName"] != DBNull.Value)
                                {
                                    student.StreetName = reader["StreetName"].ToString();
                                }
                                if (reader["StreetName"] != DBNull.Value)
                                {
                                    student.StreetName = reader["StreetName"].ToString();
                                }
                                if (reader["Province"] != DBNull.Value)
                                {
                                    student.Province = reader["Province"].ToString();
                                }
                                if (reader["City"] != DBNull.Value)
                                {
                                    student.City = reader["City"].ToString();
                                }
                                if (reader["PostalCode"] != DBNull.Value)
                                {
                                    student.PostalCode = Convert.ToInt32(reader["PostalCode"]);
                                }
                                if (reader["RoomID"] != DBNull.Value)
                                {
                                    student.RoomID = Convert.ToInt32(reader["RoomID"]);
                                }
                                if (reader["StudentBalance"] != DBNull.Value) {
                                    student.StudentBalance = Convert.ToInt32(reader["StudentBalance"]);
                                }
                                if (reader["ApplicationStatus"] != DBNull.Value)
                                {
                                    student.ApplicationStatus = reader["ApplicationStatus"].ToString();
                                }
                                if (reader["registered"] != DBNull.Value) {
                                    student.registeredStatus = reader["registered"].ToString();
                                }
                            }
                        }  
                    } 
                }
                connection.Close();
            }
            return student;
        }

        public static List<ModuleModel> GetModulelist(IConfiguration configuration, StudentModel student)
        {
            List<ModuleModel> modules = new List<ModuleModel>();

            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            ModuleModel module = new ModuleModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("getModuleList", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@studyLevel", student.StudentlevelOfStudy);
                    cmd.Parameters.AddWithValue("@degree", student.StudentFaculty);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            module = new ModuleModel {
                                ModuleCode = reader["ModuleCode"].ToString(),
                                Modulename = reader["Modulename"].ToString(),
                                ModuleDescription = reader["ModuleDescription"].ToString(),
                                ModuleCost =decimal.Parse(reader["ModuleCost"].ToString()),
                                ModuleCredit = Convert.ToInt32(reader["ModuleCredit"]),
                                ModulePre_requisites = reader["ModulePre_requisites"].ToString(),
                                levelOdstudy = reader["Module_level_of_Study"].ToString(),
                                faculty = reader["F_name"].ToString()
                            };
                            modules.Add(module);
                        }
                    }
                }
                connection.Close();
            }
            return modules;
        }
        
        public static void StudentEnrollemt(IConfiguration configuration,EnrollmentsModel myenroll) {
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            int i;
            string module0 = myenroll.ModuleCode0;
            string module1 = myenroll.ModuleCode1;
            string module2 = myenroll.ModuleCode2;
            string module3 = myenroll.ModuleCode3;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("StudentEnroll", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (module0 != null) {
                        cmd.Parameters.AddWithValue("@studentNo", myenroll.StudentNo);
                        cmd.Parameters.AddWithValue("@ModuleCode", myenroll.ModuleCode0);
                        connection.Open();
                        i = cmd.ExecuteNonQuery();
                         connection.Close();
                    }

                }

                connection.Close();



                using (SqlCommand cmd = new SqlCommand("StudentEnroll", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    if (module1 != null)
                    {
                        cmd.Parameters.AddWithValue("@studentNo", myenroll.StudentNo);
                        cmd.Parameters.AddWithValue("@ModuleCode", myenroll.ModuleCode1);
                        connection.Open();
                        i = cmd.ExecuteNonQuery();
                        connection.Close();
                    }

                }

                connection.Close();


                using (SqlCommand cmd = new SqlCommand("StudentEnroll", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    if (module2 != null)
                    {
                        cmd.Parameters.AddWithValue("@studentNo", myenroll.StudentNo);
                        cmd.Parameters.AddWithValue("@ModuleCode", myenroll.ModuleCode2);
                        connection.Open();
                        i = cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                   

                }

                connection.Close();


                using (SqlCommand cmd = new SqlCommand("StudentEnroll", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    if (module3 != null)
                    {
                        cmd.Parameters.AddWithValue("@studentNo", myenroll.StudentNo);
                        cmd.Parameters.AddWithValue("@ModuleCode", myenroll.ModuleCode3);
                        connection.Open();
                        i = cmd.ExecuteNonQuery();
                        connection.Close();
                    }

                }

                connection.Close();

                using (SqlCommand cmd = new SqlCommand("Enrolled", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                   
                        cmd.Parameters.AddWithValue("@studentNo", myenroll.StudentNo);
                        connection.Open();
                        i = cmd.ExecuteNonQuery();
                        connection.Close();

                }

                connection.Close();

            }
        }

        public static ModuleModel getModule(IConfiguration configuration, string Modulecode) {

            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            ModuleModel module = new ModuleModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("moduleCost", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ModuleCode", Modulecode);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            module = new ModuleModel
                            {
                                ModuleCode = reader["ModuleCode"].ToString(),
                                Modulename = reader["Modulename"].ToString(),
                                ModuleDescription = reader["ModuleDescription"].ToString(),
                                ModuleCost = decimal.Parse(reader["ModuleCost"].ToString()),
                                ModuleCredit = Convert.ToInt32(reader["ModuleCredit"]),
                                ModulePre_requisites = reader["ModulePre_requisites"].ToString(),
                                levelOdstudy = reader["Module_level_of_Study"].ToString(),
                                ModulesStatus = reader["ModulesStatus"].ToString(),
                                faculty = reader["DegreeCode"].ToString()

                            };
                            
                        }
                    }
                }
                connection.Close();
            }
            return module;
        }
        public static void setStudentBalance(IConfiguration configuration, double balance , int studentNo)
        {

            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            ModuleModel module = new ModuleModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateStudentBalance", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StudentBalance", balance);
                    cmd.Parameters.AddWithValue("@StudentNo", studentNo);

                  
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                connection.Close();
            }

        }
        public static void setStudentBalanceAdmin(IConfiguration configuration, double balance, int studentNo)
        {

            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            ModuleModel module = new ModuleModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("StudentPaymentUpadte", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StdentPayment", balance);
                    cmd.Parameters.AddWithValue("@StudentNo", studentNo);


                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                connection.Close();
            }

        }
        public static List<ModuleModel> allmodules(IConfiguration configuration)
        {
            List<ModuleModel> modules = new List<ModuleModel>();

            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            ModuleModel module = new ModuleModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("allmodules", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            module = new ModuleModel
                            {
                                ModuleCode = reader["ModuleCode"].ToString(),
                                Modulename = reader["Modulename"].ToString(),
                                ModuleDescription = reader["ModuleDescription"].ToString(),
                                ModuleCost = decimal.Parse(reader["ModuleCost"].ToString()),
                                ModuleCredit = Convert.ToInt32(reader["ModuleCredit"]),
                                ModulePre_requisites = reader["ModulePre_requisites"].ToString(),
                                levelOdstudy = reader["Module_level_of_Study"].ToString(),
                                faculty = reader["F_name"].ToString(),
                                ModulesStatus = reader["ModulesStatus"].ToString()
                            };
                            modules.Add(module);
                        }
                    }
                }
                connection.Close();
            }
            return modules;
        }


        public static List<string> enrolled(IConfiguration configuration, int studentNo)
        {
            List<string> modules = new List<string>();

            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("getStudentEnrolledModules", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentNo", studentNo);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            modules.Add( reader["ModuleCode"].ToString());
                        }
                    }
                }
                connection.Close();
            }
            return modules;
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
                                    StudentGender = reader["StudentGender"].ToString()
                                };
                                if (reader["StudentDateOfBirth"] != DBNull.Value)
                                {
                                    student.StudentDateOfBirth = Convert.ToDateTime(reader["StudentDateOfBirth"].ToString());
                                }
                                if (reader["StudentHomeLanguage"] != DBNull.Value)
                                {
                                    student.StudentHomeLanguage = reader["StudentHomeLanguage"].ToString();
                                }
                                if (reader["StudentFinincialStatus"] != DBNull.Value)
                                {
                                    student.StudentFinincialStatus = reader["StudentFinincialStatus"].ToString();
                                }
                                if (reader["StudentDegree"] != DBNull.Value)
                                {
                                    student.StudentDegree = reader["StudentDegree"].ToString();
                                }
                                if (reader["StudentlevelOfStudy"] != DBNull.Value)
                                {
                                    student.StudentlevelOfStudy = reader["StudentlevelOfStudy"].ToString();
                                }
                                if (reader["StudentFaculty"] != DBNull.Value)
                                {
                                    student.StudentFaculty = reader["StudentFaculty"].ToString();
                                }
                                if (reader["StudentNeedAccommodation"] != DBNull.Value)
                                                        {
                                    student.StudentNeedAccommodation = reader["StudentNeedAccommodation"].ToString();
                                }
                                if (reader["StudentRiskStatus"] != DBNull.Value)
                                {
                                    student.StudentRiskStatus = reader["StudentRiskStatus"].ToString();
                                }
                                if (reader["StreetName"] != DBNull.Value)
                                {
                                    student.StreetName = reader["StreetName"].ToString();
                                }
                                if (reader["StreetName"] != DBNull.Value)
                                {
                                    student.StreetName = reader["StreetName"].ToString();
                                }
                                if (reader["Province"] != DBNull.Value)
                                {
                                    student.Province = reader["Province"].ToString();
                                }
                                if (reader["City"] != DBNull.Value)
                                {
                                    student.City = reader["City"].ToString();
                                }
                                if (reader["PostalCode"] != DBNull.Value)
                                {
                                    student.PostalCode = Convert.ToInt32(reader["PostalCode"]);
                                }
                                if (reader["RoomID"] != DBNull.Value)
                                {
                                    student.RoomID = Convert.ToInt32(reader["RoomID"]);
                                }
                                if (reader["StudentBalance"] != DBNull.Value)
                                {
                                    student.StudentBalance = Convert.ToInt32(reader["StudentBalance"]);
                                }
                                if (reader["ApplicationStatus"] != DBNull.Value)
                                {
                                    student.ApplicationStatus = reader["ApplicationStatus"].ToString();
                                }

                                if (reader["IdUrl"] != DBNull.Value)
                                {
                                    student.idcopyUrl = reader["IdUrl"].ToString();
                                }
                                if (reader["KinUrl"] != DBNull.Value)
                                {
                                    student.nextofKinUrl = reader["KinUrl"].ToString();
                                }
                                if (reader["MatricUrl"] != DBNull.Value)
                                {
                                    student.matricResultUrl = reader["MatricUrl"].ToString();
                                }
                                if (reader["FProofUrl"] != DBNull.Value)
                                {
                                    student.financialProofUrl = reader["FProofUrl"].ToString();
                                }

                            }
                            allStudent.Add(student);
                        }
                    }
                }

                connection.Close();
            }
            return allStudent;
        }
        public static int InsertStudentpayment(IConfiguration configuration, PaymentModel studentpayment) {
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            int i;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand("InsertStudentPayment", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@paymentType", studentpayment.paymenttype);
                    cmd.Parameters.AddWithValue("@paymentAmount", decimal.Parse(studentpayment.paymentAmount.ToString()));
                    cmd.Parameters.AddWithValue("@paymentDescription", studentpayment.paymentDescription);
                    cmd.Parameters.AddWithValue("@bankName", studentpayment.bankName);
                    cmd.Parameters.AddWithValue("@paymentDate", studentpayment.paymentDate);
                    cmd.Parameters.AddWithValue("@StudentNo", studentpayment.StudentNo);
                    cmd.Parameters.AddWithValue("@paymentUrl", studentpayment.paymentProofUrl);
                    cmd.Parameters.AddWithValue("@dateuploaded", DateTime.Now);

                    connection.Open();
                    i = cmd.ExecuteNonQuery();
                    connection.Close();
                   

                }

                connection.Close();
            }

            return i;
        }
        public static async void SendEmails(string messageType, StudentModel myStudent , string comment = "")
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
                html += "<td align='center' style='padding:40px 0 30px 0;background:#333333;'>";
                html += "<p style='margin:0;font-size:30px;line-height:16px;font-family:Arial,sans-serif;color:#ffffff;'>";
                html += " <b>Internet Explorers UNIVERSITY</b><br/>";
                html += "</p>";
                html += "</td>";
                html += "</tr>";
                html += "<tr>";
                html += "<td style='padding:36px 30px 42px 30px;'>";
                html += "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'>";

              
                string adminhtml = html;





                switch (messageType)
                {
                    case "Registration":
                        html += "<tr>";
                        adminhtml += "<tr>";
                        subject = "Application to Study at Internet Explorers University";
                        html += "<h4 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>Dear <b> "+ myStudent.StudentName +" "+ myStudent.StudentSurname + "</b></h4><br />";
                        html += "<td style='padding:0 0 36px 0;color:#153643;'>";
                        adminhtml += "<td style='padding:0 0 36px 0;color:#153643;'>";
                        html += "<h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'> <b>We have received your application.</b></h1>";
                        html += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Thank you for taking the first steps toward personal development, career growth, and a brighter future.</p>";
                        html += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Visit <a href='https://localhost:44339/' style='color:#ee4c50;text-decoration:underline;'>Internet Explorers university </a>for application follow up</p><br />";
                        html += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Thank you</p>";

                        html += "</td>";
                        adminhtml += "</td>";
                        emailBody += "<p>Many thanks, <b>the team at <b> Internet Explorers University</b></p></body></html>";


                        adminhtml += "<td style='padding:0 0 36px 0;color:#153643;'>";
                        adminhtml += "<h4 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>Dear <b>Admin</b></h4><br />";
                        adminhtml += "<h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'> Student <b>" + myStudent.StudentName + " " + myStudent.StudentSurname + " have placed an application.</b></h1>";
                        adminhtml += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'></p>";
                        adminhtml += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Visit <a href='https://localhost:44339/Account/Login' style='color:#ee4c50;text-decoration:underline;'>Internet Explorers university </a>To view student application</p>";
                        adminhtml += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Thank you</p><br />";
                        adminsubject = "New Student Application";
                        html += "</td>";
                        adminhtml += "</td>";
                        html += "</tr>";
                        adminhtml += "</tr>";
                        break;
                    case "Status":
                        html += "<tr>";
                        subject = "Application Status";
                        html += "<h4 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>Dear <b> " + myStudent.StudentName + " " + myStudent.StudentSurname + "</b></h4><br />";
                        html += "<td style='padding:0 0 36px 0;color:#153643;'>";



                        html += "<h4 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'> <b>Update on your applucation</b></h4>";

                        html += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'> " + comment+ " </p>";
                        html += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Visit <a href='https://localhost:44339/Account/Login' style='color:#ee4c50;text-decoration:underline;'>Internet Explorers university </a>for application follow up</p><br />";
                        html += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Thank you</p>";

                        



                        html += "</td>";
                        emailBody += "<p>Many thanks, <b>the team at <b> Internet Explorers University</b></p></body></html>";


                        adminhtml += "<td style='padding:0 0 36px 0;color:#153643;'>";
                        adminhtml += "<h4 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>Dear <b>Admin</b></h4><br />";
                        adminhtml += "<h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'> Student <b>" + myStudent.StudentName + " " + myStudent.StudentSurname + " have placed an application.</b></h1>";
                        adminhtml += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'></p>";
                        adminhtml += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Visit <a href='https://localhost:44339/Account/Login' style='color:#ee4c50;text-decoration:underline;'>Internet Explorers university </a>To view student application</p>";
                        adminhtml += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Thank you</p><br />";
                        adminsubject = "New Student Application";
                        html += "</td>";
                        adminhtml += "</td>";
                        html += "</tr>";
                        adminhtml += "</tr>";


                        break;
                    case "Payment":
                        html += "<tr>";
                        subject = "Payment Notification";
                        html += "<h4 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>Dear <b> " + myStudent.StudentName + " " + myStudent.StudentSurname + "</b></h4><br />";
                        html += "<td style='padding:0 0 36px 0;color:#153643;'>";



                        html += "<h4 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'> <b>We have reciecied your payments</b></h4>";

                        html += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'><b>Description :</b> " + comment + " </p>";
                        html += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Visit <a href='https://localhost:44339/Account/Login' style='color:#ee4c50;text-decoration:underline;'>Internet Explorers university </a>to view your payment</p><br />";
                        html += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Thank you</p>";





                        html += "</td>";
                       
                        adminhtml += "<td style='padding:0 0 36px 0;color:#153643;'>";
                        adminhtml += "<h4 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>Dear <b>Admin</b></h4><br />";
                        adminhtml += "<h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'> Student <b>" + myStudent.StudentName + " " + myStudent.StudentSurname + " have made payment.</b></h1>";
                        adminhtml += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'><b>Description :</b>" + comment + " </p>";
                        adminhtml += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'></p>";
                        adminhtml += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Visit <a href='https://localhost:44339/Account/Login' style='color:#ee4c50;text-decoration:underline;'>Internet Explorers university </a>To view student payment</p>";
                        adminhtml += "<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Thank you</p><br />";
                        adminsubject = "Student Payment";
                        html += "</td>";
                        adminhtml += "</td>";
                        html += "</tr>";
                        adminhtml += "</tr>";


                        break;
                    default:
                        break;
                }
                string lowerHtmlBody = string.Empty;
                lowerHtmlBody += "<tr>";
                lowerHtmlBody += "<td style='padding:0;'>";
                lowerHtmlBody += "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'>";
                lowerHtmlBody += "<tr>";
                lowerHtmlBody += "<td style='width:260px;padding:0;vertical-align:top;color:#153643;'>";
                lowerHtmlBody += "<p style='margin:0 0 25px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'><img src='	https://www.uopeople.edu/wp-content/uploads/2019/08/iStock-475794203.jpg' alt='' width='260' style='height:auto;display:block;' /></p>";
                lowerHtmlBody += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Thanks to <b>Internet Explorers UNIVERSITY</b> small college classes, students receive personalized attention and a supportive learning experience to fuel their success.</p>";
                lowerHtmlBody += "</td>";
                lowerHtmlBody += "<td style='width:20px;padding:0;font-size:0;line-height:0;'>&nbsp;</td>";
                lowerHtmlBody += "<td style='width:260px;padding:0;vertical-align:top;color:#153643;'>";
                lowerHtmlBody += "<p style='margin:0 0 25px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'><img src='https://www.uopeople.edu/wp-content/uploads/2019/08/iStock-947127220.jpg' alt='' width='260' style='height:auto;display:block;' /></p>";
                lowerHtmlBody += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'><b>Internet Explorers UNIVERSITY</b> is an accredited, South African University. Our academic leadership includes scholars from the best Schools around the South African.</p>";
                lowerHtmlBody += "</td>";
                lowerHtmlBody += "</tr>";
                lowerHtmlBody += "</table>";
                lowerHtmlBody += "</td>";
                lowerHtmlBody += "</tr>";
                lowerHtmlBody += "</table>";
                lowerHtmlBody += "</td>";
                lowerHtmlBody += "</tr>";
                lowerHtmlBody += "<tr>";
                lowerHtmlBody += "<td style='padding:30px;background:#333333;'>";
                lowerHtmlBody += "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:9px;font-family:Arial,sans-serif;'>";
                lowerHtmlBody += "<tr>";
                lowerHtmlBody += "<td style='padding:0;width:50%;' align='center'>";
                lowerHtmlBody += "<p style='margin:0;font-size:14px;line-height:16px;font-family:Arial,sans-serif;color:#ffffff;'>";
                lowerHtmlBody += " &copy; 2021 - Internet Explorers <b>UNIVERSITY</b><br/><a href='#' style='color:#ffffff;text-decoration:underline;'></a>";
                lowerHtmlBody += "</p>";
                lowerHtmlBody += "</td>";
                lowerHtmlBody += "<td style='padding:0;width:50%;' align='right'>";
                lowerHtmlBody += "<table role='presentation' style='border-collapse:collapse;border:0;border-spacing:0;'>";
                lowerHtmlBody += "<tr>";
                lowerHtmlBody += "<td style='padding:0 0 0 10px;width:38px;'>";
                lowerHtmlBody += "<a href='#' class='fa fa-twitter' style='color:#ffffff;'></a>";
                lowerHtmlBody += "</td>";
                lowerHtmlBody += "<td style='padding:0 0 0 10px;width:38px;'>";
                lowerHtmlBody += "<a href='#' style='color:#ffffff;'></a>";
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



                if (messageType.Equals("Registration") || messageType.Equals("Payment"))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        var senderEmail = new MailAddress("internetexploresuniversity@gmail.com", "Internet Explores");
                        var receiverEmail = new MailAddress(myStudent.StudentEmail, myStudent.StudentName.ToString() + " " + myStudent.StudentSurname.ToString());
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
                            receiverEmail = new MailAddress(myStudent.StudentEmail, myStudent.StudentName.ToString() + " " + myStudent.StudentSurname.ToString());
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
                    var receiverEmail = new MailAddress(myStudent.StudentEmail, myStudent.StudentName.ToString() + " " + myStudent.StudentSurname.ToString());
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

        public static List<PaymentModel> getStudentsPayments(IConfiguration configuration, int StudentNO)
        {
            List<PaymentModel> studentpayments = new List<PaymentModel>();
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            PaymentModel payments = new PaymentModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("Studentpayments", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentNo", StudentNO);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            
                                payments = new PaymentModel
                                {
                                    paymentID = Convert.ToInt32(reader["paymentID"]),
                                    StudentNo = Convert.ToInt32(reader["StudentNo"]),
                                    paymenttype = reader["paymentType"].ToString(),
                                    paymentAmount =Decimal.Parse(reader["paymentAmount"].ToString()), 
                                    paymentDescription =reader["paymentDescription"].ToString(), 
                                    bankName = reader["bankName"].ToString(),
                                    paymentDate = DateTime.Parse( reader["paymentDate"].ToString()),
                                    PayemntNowdate = DateTime.Parse(reader["dateuploaded"].ToString()),
                                    paymentProofUrl = reader["paymentUrl"].ToString() };
                                    studentpayments.Add(payments) ;
                           
                        }
                       
                    }
                }

                connection.Close();
            }
            return studentpayments;
        }

        public static List<PaymentModel> getAllNewStudentsPayments(IConfiguration configuration)
        {
            List<PaymentModel> studentpayments = new List<PaymentModel>();
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            PaymentModel payments = new PaymentModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("getAllNewPayments", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            payments = new PaymentModel
                            {
                                paymentID = Convert.ToInt32(reader["paymentID"]),
                                StudentNo = Convert.ToInt32(reader["StudentNo"]),
                                paymenttype = reader["paymentType"].ToString(),
                                paymentAmount = Decimal.Parse(reader["paymentAmount"].ToString()),
                                paymentDescription = reader["paymentDescription"].ToString(),
                                bankName = reader["bankName"].ToString(),
                                paymentDate = DateTime.Parse(reader["paymentDate"].ToString()),
                                PayemntNowdate = DateTime.Parse(reader["dateuploaded"].ToString()),
                                paymentProofUrl = reader["paymentUrl"].ToString()
                            };
                            studentpayments.Add(payments);

                        }

                    }
                }

                connection.Close();
            }
            return studentpayments;
        }

        public static List<PaymentModel> getStudentModules(IConfiguration configuration)
        {
            List<PaymentModel> studentpayments = new List<PaymentModel>();
            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            PaymentModel payments = new PaymentModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("getAllNewPayments", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            payments = new PaymentModel
                            {
                                paymentID = Convert.ToInt32(reader["StudentNo"]),
                                StudentNo = Convert.ToInt32(reader["StudentNo"]),
                                paymenttype = reader["paymentType"].ToString(),
                                paymentAmount = Decimal.Parse(reader["paymentAmount"].ToString()),
                                paymentDescription = reader["paymentDescription"].ToString(),
                                bankName = reader["bankName"].ToString(),
                                paymentDate = DateTime.Parse(reader["paymentDate"].ToString()),
                                PayemntNowdate = DateTime.Parse(reader["dateuploaded"].ToString()),
                                paymentProofUrl = reader["paymentUrl"].ToString()
                            };
                            studentpayments.Add(payments);

                        }

                    }
                }

                connection.Close();
            }
            return studentpayments;
        }

        public static void updateStudentPaymentStatus(IConfiguration configuration, int paymentID) {

            string connectionString = configuration.GetConnectionString("InternetExploresDbContextConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PaymentStatus", connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PaymentId", paymentID);

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                connection.Close();
            }
        } 

    }
}
