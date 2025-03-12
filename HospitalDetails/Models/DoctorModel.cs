using System.ComponentModel.DataAnnotations;

namespace HospitalDetails.Models;

public class DoctorModel
{
    public int Doctor_ID { get; set; } // Auto-incremented Primary Key

    [Required]
    public string Doctor_Name { get; set; }

    [Required]
    [Phone]
    public string Doctor_PhoneNumber { get; set; }

    [Required]
    public string Specialist { get; set; }
    
    [Required]
     public  string Doctor_Password{ get; set; }
}