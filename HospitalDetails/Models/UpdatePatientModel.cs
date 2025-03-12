using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalDetails.Models;

public class UpdatePatientModel
{
    public int Patient_ID { get; set; } // Auto-incremented Primary Key
    
    public string Patient_Name { get; set; } // For display only
    public string Disease_Name { get; set; } // For display only
    public string? Doctor_Name { get; set; }
    
    public string? MedicineDetails { get; set; } // Can be null
}