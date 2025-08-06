using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_MySearchJob_Core.Entities;
using Web_MySearchJob_Core.Interfaces;

namespace Web_MySearchJob_Core.Controllers
{
    public class JobApplicationController(IJobApplicationModel _model) : Controller
    {
        [HttpGet]
        public IActionResult GetJobApplications()
        {
            var data = _model.GetAllJobApplications()!;
            if (data != null )
                return View(data);
                return View(new List<JobApplication>());
        }

        [HttpGet]
        public IActionResult GetJobApplicationById(long id)
        {
            var data = _model.GetAllJobApplications()!;
            if (data != null)
                return View(data);
                return View(new JobApplication());
        }

        [HttpGet]
        public IActionResult CreateJobApplication()
        {
            var modality = _model.GetAllModality();
            var modalities = modality != null ? modality.ToList() : new List<JobModality>();
            ViewBag.ModalityList = new SelectList(modalities, "Id", "modalityType");
            return View();
        }

        [HttpPost]
        public IActionResult CreateJobApplication(JobApplication entity)
        {
            // All fields are optional, so no validation required
            _model.CreateJobApplication(entity);
            return RedirectToAction("GetJobApplications", "JobApplication");
        }

        //public IActionResult UpdateJobApplication(JobApplication entity)
        //{
        //}

        public IActionResult DeleteJobApplication(long id)
        {
            var resp = _model.DeleteJobApplication(id);
            return RedirectToAction("GetJobApplications", "JobApplication");
        }
    }
}
