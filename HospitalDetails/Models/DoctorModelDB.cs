using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HospitalDetails.Models;

public class DoctorModelDB
{
   // string cs = "Data Source=ADVI;Initial Catalog=HospitalDataDetails;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
   //string cs = "Server=tcp:advi-server.database.windows.net,1433;Initial Catalog=HospitalDataDetails;Persist Security Info=False;User ID=your-advi-server;Password=Chetan@21;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
   string cs = "Server=tcp:advi-server.database.windows.net,1433;Initial Catalog=HospitalDataDetails;Persist Security Info=False;User ID=advi-server;Password=Chetan@21;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    public List <DoctorModel> getDoctorDetails()
    {
      List<DoctorModel> doctorsList = new List<DoctorModel>();
      SqlConnection conn = new SqlConnection(cs);
      string sql = "select * from DoctorTable;";
      SqlCommand cmd = new SqlCommand(sql, conn);
      conn.Open();
      SqlDataReader reader = cmd.ExecuteReader();
      while (reader.Read())
      {
          DoctorModel doctorModel = new DoctorModel();
          doctorModel.Doctor_ID = Convert.ToInt32(reader.GetValue(0));
          doctorModel.Doctor_Name = Convert.ToString(reader.GetValue(1));
          doctorModel.Doctor_PhoneNumber = Convert.ToString(reader.GetValue(2));
          doctorModel.Specialist= Convert.ToString(reader.GetValue(3));
          doctorModel.Doctor_Password = Convert.ToString(reader.GetValue(4));
          doctorsList.Add(doctorModel);
      }
      return doctorsList;
    }

    
}