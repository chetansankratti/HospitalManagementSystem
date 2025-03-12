using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalDetails.Models;

public class PatientModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-generate ID
    public int Patient_ID { get; set; } // Auto-incremented Primary Key

    [Required]
    public string Patient_Name { get; set; }

    [Required]
    [Phone]
    public string Phone_Number { get; set; }

    [Required]
    [Range(0, 150, ErrorMessage = "Invalid Age")]
    public int  Patient_Age { get; set; }

    [Required]
    public string Disease_Name { get; set; }

    //[Required]
   public string? Doctor_Name { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9])(?=.*[a-z]).{8,}$",ErrorMessage = "UpperCase,LowerCase,Number,Symbol,Min 8 character")]
    public string Password { get; set; } // Should be stored in hashed format

    [Required]
    public string Gender { get; set; } // Should be 'Male', 'Female', or 'Other'

    [Required]
    [DataType(DataType.Date)]
    public DateTime AppointmentDate { get; set; }= DateTime.Today;

    [Required]
    public string Address { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DOB { get; set; }= new DateTime(1950, 1, 1);

   public string? MedicineDetails { get; set; } // Can be null

}