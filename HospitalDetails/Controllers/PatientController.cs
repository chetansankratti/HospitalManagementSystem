using HospitalDetails.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalDetails.Controllers;

public class PatientController : Controller
{
    
    public IActionResult Home()
    {
        return View();
    }
    
    public IActionResult About()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult AppointmentList()
    {
        PatientModelDB db = new PatientModelDB();
        var tableModel = db.getPatientsDetails();
        if (tableModel == null || !tableModel.Any())
        {
            Console.WriteLine("Executed the Data");
            return View(new List<PatientModel>());
            
        }

        return View(tableModel);
    }
    
    public IActionResult AddAppointment(PatientModel patientModel)
    {
        if (ModelState.IsValid)
        {
           PatientModelDB db = new PatientModelDB();
            db.addPatientsAppointment(patientModel);
            return RedirectToAction("AppointmentList", "Patient");
        }
        return View(patientModel);
    }

    public IActionResult AllPatientDetails()
    {
        PatientModelDB db = new PatientModelDB();
        var patientData = db.getPatientsDetails();
        if (patientData == null || !patientData.Any())
        {
            Console.WriteLine("Executed the Data");
            return View(new List<PatientModel>());
        }
        return View(patientData);
    }
    
    public IActionResult PatientLogin(PatientModel patientModel)
    {
        PatientModelDB db = new PatientModelDB();
        var patientLoginData=db.getPatientsDetails().FirstOrDefault(data =>  data.Phone_Number== patientModel.Phone_Number && data.Password== patientModel.Password);

        if (patientLoginData != null)
        {
            //When a user logs in, we store their phone number in the session so we can use it later to get their details.
            HttpContext.Session.SetString("Phone Number", patientModel.Phone_Number);
            return RedirectToAction("IndividualPatient_Details", "Patient");
        }
        ViewBag.ErrorMessage = "Invalid Phone Number or Password";
        return View();
    }

    public IActionResult IndividualPatient_Details()
    {
       PatientModelDB db = new PatientModelDB();
        string ? phoneNumberStr =HttpContext.Session.GetString("Phone Number");

        if (string.IsNullOrEmpty(phoneNumberStr))
        {
            return RedirectToAction("PatientLogin", "Patient");
        }
       
        
        var patient=db.getPatientsDetails().FirstOrDefault(pData=>pData.Phone_Number==phoneNumberStr);
        if (patient == null)
        {
            return RedirectToAction("PatientLogin", "Patient");
        }
        return View(patient);
    }
    
 
    [HttpPost]
    public IActionResult PatientUpdate(UpdatePatientModel patientUpdateModel)
    {
        if (ModelState.IsValid)
        {
            PatientModelDB db = new PatientModelDB();
            var updateData = db.updatePatienDetails(patientUpdateModel);
            if (!updateData)
            {
                Console.WriteLine("Not Update");
                return RedirectToAction("AllPatientDetails", "Patient");
            }
            Console.WriteLine("Updated Patient Details");
            return RedirectToAction("AllPatientDetails", "Patient");
        }
        
        else
        {
            // Log validation errors
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(patientUpdateModel);
        }
    }
    
    [HttpGet]
    public IActionResult PatientUpdate(int Patient_ID, string Patient_Name, string Disease_Name)
    {
        // Create a new UpdatePatientModel and pre-fill the read-only fields
        var model = new UpdatePatientModel
        {
            Patient_ID = Patient_ID,
            Patient_Name = Patient_Name,
            Disease_Name = Disease_Name
        };

        return View(model);
    }

    
    public IActionResult Logout()
    {
        HttpContext.Session.Clear(); // Remove all session data
        return RedirectToAction("Login", "Hospital");
    }

}

