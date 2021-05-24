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
    }
}
