using HospitalDetails.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalDetails.Controllers;

public class DoctorController:Controller
{
    public IActionResult DoctorDetails()
    {
        DoctorModelDB doctorModelDB = new DoctorModelDB();
        var doctorList = doctorModelDB.getDoctorDetails();
        if (doctorList != null)
        {
            Console.WriteLine("Doctor List");
            return View(new List<DoctorModel>(doctorList));
        }
        return View(doctorList);
    }
    
    public IActionResult DoctorLogin(DoctorModel doctorModel)
    {
        DoctorModelDB doctorModelDB = new DoctorModelDB();
        var doctorList = doctorModelDB.getDoctorDetails().FirstOrDefault(data=>data.Doctor_PhoneNumber == doctorModel.Doctor_PhoneNumber && data.Doctor_Password == doctorModel.Doctor_Password);
        if (doctorList != null)
        {
            HttpContext.Session.SetString("Doctor_PhoneNumber", doctorModel.Doctor_PhoneNumber);
            return RedirectToAction("AllPatientDetails", "Patient");
        }
        ViewBag.ErrorMessage = "Invalid Phone Number or Password";
        return View();
    }
    
}