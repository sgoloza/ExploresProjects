using InternetExplores.Models;
using InternetExplores.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InternetExplores.Helpers
{
    public class DbHelper
    {


        public static void RegistrationOfStudent(IConfiguration configuration, RegisterViewModel mystudent)
        {
            //creating student object
            //student = new StudentModel { StudentName = StudentName, StudentSurname = Input.StudentSurname, StudetntIdNo = Input.StudetntIdNo, StudentPhoneNo = Input.StudentPhoneNo, StudentGender = Input.StudentGender, StudentDateOfBirth = Input.StudentDateOfBirth };

            var student = mystudent;

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
        public static StudentModel  GetAllStudent(IConfiguration configuration,string email)
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
                            if (reader["StudentEmail"].ToString().Equals(email) )
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
                                    StudentDateOfBirth =Convert.ToDateTime( reader["StudentDateOfBirth"].ToString()),
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
    }
}
