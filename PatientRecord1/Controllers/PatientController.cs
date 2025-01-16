using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientRecord1.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            HospitalRecordEntities hr = new HospitalRecordEntities();
            List<Patient> pat = hr.Patients.ToList();
            return View(pat);
        }
        public JsonResult add(Patient pat)
        {
            try
            {
                HospitalRecordEntities hr = new HospitalRecordEntities();
                hr.Patients.Add(pat);
                hr.SaveChanges();
                return Json(pat);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public ActionResult update(Patient pat)
        {
            try
            {
                HospitalRecordEntities hr = new HospitalRecordEntities();
                Patient patientUpdate = (from x in hr.Patients where x.ID == pat.ID select x).FirstOrDefault();
                patientUpdate.Firstname = pat.Firstname;
                patientUpdate.Middlename = pat.Middlename;
                patientUpdate.Lastname = pat.Lastname;
                patientUpdate.Suffixname = pat.Suffixname;
                patientUpdate.BirthDate = pat.BirthDate;
                patientUpdate.Gender = pat.Gender;
                patientUpdate.InitialDiagnosis = pat.InitialDiagnosis;
                hr.SaveChanges();
                return Json(new { success = true, message = "Patient updated successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        
        }

        public ActionResult deletePatient(int id)
        {
            HospitalRecordEntities hr = new HospitalRecordEntities();
            Patient patientDel = (from x in hr.Patients where x.ID == id select x).FirstOrDefault();
            hr.Patients.Remove(patientDel);
            hr.SaveChanges();
            return Json(new { success = true, message = "Patient record deleted" });
        }
    }
}