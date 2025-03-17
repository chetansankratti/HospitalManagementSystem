using System.Data.SqlClient;

namespace HospitalDetails.Models;

public class PatientModelDB
{
   // string cs="Data Source=ADVI;Initial Catalog=HospitalDataDetails;Integrated Security=True";
   //string cs = "Server=tcp:advi-server.database.windows.net,1433;Initial Catalog=HospitalDataDetails;Persist Security Info=False;User ID=your-advi-server;Password=Chetan@21;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
   string cs = "Server=tcp:advi-server.database.windows.net,1433;Initial Catalog=HospitalDataDetails;Persist Security Info=False;User ID=advi-server;Password=Chetan@21;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
   
    public List<PatientModel> getPatientsDetails()
    {
        List<PatientModel> patientModelList = new List<PatientModel>();
        using (SqlConnection con = new SqlConnection(cs))
        {
            string sql = "Select * from PatientTable;";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PatientModel patientModel = new PatientModel();
                    patientModel.Patient_ID = Convert.ToInt32(reader.GetValue(0));
                    patientModel.Patient_Name = Convert.ToString(reader.GetValue(1));
                    patientModel.Phone_Number = Convert.ToString(reader.GetValue(2));
                    patientModel.Patient_Age = Convert.ToInt32(reader.GetValue(3));
                    patientModel.Disease_Name = Convert.ToString(reader.GetValue(4));
                    patientModel.Doctor_Name = Convert.ToString(reader.GetValue(5));
                    patientModel.Password = Convert.ToString(reader.GetValue(6));
                    patientModel.Gender = Convert.ToString(reader.GetValue(7));
                    patientModel.AppointmentDate = reader.IsDBNull(8)
                        ? DateTime.Today // Default to today's date if NULL
                        : reader.GetDateTime(8); // Reads from SQL Server (DATE column)

                    patientModel.Address = Convert.ToString(reader.GetValue(9));
                    patientModel.DOB = reader.IsDBNull(10)
                        ? new DateTime(1900, 1, 1) // Default to 1900-01-01 if NULL
                        : reader.GetDateTime(10); // Reads from SQL Server (DATE column)
                    patientModel.MedicineDetails = Convert.ToString(reader.GetValue(11));

                    patientModelList.Add(patientModel);

                }

                return patientModelList;
            }
        }
    }

    public bool addPatientsAppointment(PatientModel patientModel)
    {
        using (SqlConnection con = new SqlConnection(cs))
        {

            string query =
                "INSERT INTO PatientTable (Patient_Name, Phone_Number, Patient_Age, Disease_Name, Doctor_Name, Password, Gender, AppointmentDate, Address, DOB, MedicineDetails)VALUES (@Patient_Name, @Phone_Number, @Patient_Age, @Disease_Name, @Doctor_Name, @Password, @Gender, @AppointmentDate, @Address, @DOB, @MedicineDetails);";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Patient_Name", patientModel.Patient_Name);
                cmd.Parameters.AddWithValue("@Phone_Number", patientModel.Phone_Number);
                cmd.Parameters.AddWithValue("@Patient_Age", patientModel.Patient_Age);
                cmd.Parameters.AddWithValue("@Disease_Name", patientModel.Disease_Name);
                cmd.Parameters.AddWithValue("@Password", patientModel.Password);
                cmd.Parameters.AddWithValue("@Gender", patientModel.Gender);
                cmd.Parameters.AddWithValue("@AppointmentDate", patientModel.AppointmentDate);
                cmd.Parameters.AddWithValue("@Address", patientModel.Address);
                cmd.Parameters.AddWithValue("@DOB", patientModel.DOB);

                cmd.Parameters.AddWithValue("@Doctor_Name",
                    string.IsNullOrEmpty(patientModel.Doctor_Name) ? (object)DBNull.Value : patientModel.Doctor_Name);
                cmd.Parameters.AddWithValue("@MedicineDetails",
                    string.IsNullOrEmpty(patientModel.MedicineDetails)
                        ? (object)DBNull.Value
                        : patientModel.MedicineDetails);
                con.Open();
                int rowAffect = cmd.ExecuteNonQuery();
                return rowAffect > 0;
            }
        }
    }

    public bool updatePatienDetails(UpdatePatientModel patientUpdateModel)
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            string query =
                "UPDATE PatientTable SET Doctor_Name=@Doctor_Name, MedicineDetails=@MedicineDetails WHERE Patient_ID=@Patient_ID;";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Patient_ID", patientUpdateModel.Patient_ID);
                cmd.Parameters.AddWithValue("@Doctor_Name",
                    string.IsNullOrEmpty(patientUpdateModel.Doctor_Name)
                        ? (object)DBNull.Value
                        : patientUpdateModel.Doctor_Name);
                cmd.Parameters.AddWithValue("@MedicineDetails",
                    string.IsNullOrEmpty(patientUpdateModel.MedicineDetails)
                        ? (object)DBNull.Value
                        : patientUpdateModel.MedicineDetails);
                con.Open();
                int rowAffect = cmd.ExecuteNonQuery();
                if (rowAffect > 0)
                {
                    Console.WriteLine("Successfully updated Patient Details");
                    return true;
                }

                return false;
            }
        }
    }

}