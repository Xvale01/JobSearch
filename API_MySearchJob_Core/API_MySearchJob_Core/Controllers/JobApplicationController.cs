using API_MySearchJob_Core.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_MySearchJob_Core.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public JobApplicationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetAllJobApplications")]
        public IActionResult GetAllJobApplications()
        {
            using (var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                return Ok(connection.Query<JobApplication>("getAllJobApplications", new { }, 
                    commandType: CommandType.StoredProcedure).ToList());
            }
        }

        [HttpGet]
        [Route("GetJobApplicationById")]
        public IActionResult GetJobApplicationById(long id)
        {
            using (var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                return Ok(connection.Query<JobApplication>("getJobApplicationById", new { id }, 
                    commandType: CommandType.StoredProcedure).FirstOrDefault());
            }
        }

        [HttpGet]
        [Route("GetAllModality")]
        public IActionResult GetAllModality()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Connection")))
            {
                //vulnerabilidades de inyección
                return Ok(connection.Query<JobModality>("SELECT id, modalityType FROM JobModality", new { }, 
                    commandType: CommandType.Text).ToList());
            }
        }

        [HttpPost]
        [Route("CreateJobApplication")]
        public IActionResult CreateJobApplication(JobApplication entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Connection")))
            {
                return Ok(connection.Execute("createJobApplication", new
                {
                    entity.DatePublication,
                    entity.ApplicationDate,
                    entity.JobName,
                    entity.Company,
                    entity.ApplicationPage,
                    entity.Salary,
                    entity.Benefits,
                    entity.Requirements,
                    entity.Functions,
                    entity.Softskills,
                    entity.Experience,
                    entity.JobModalityId,
                    entity.Country,
                    entity.Province,
                    entity.City,
                    entity.Address
                },
                commandType: CommandType.StoredProcedure));
            }
        }

        [HttpPut]
        [Route("UpdateJobApplication")]
        public IActionResult UpdateJobApplication(JobApplication entity)
        {
            using (var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                var data = connection.Execute("updateJobApplication", new
                {
                    entity.Id,
                    entity.DatePublication,
                    entity.ApplicationDate,
                    entity.JobName,
                    entity.Company,
                    entity.ApplicationPage,
                    entity.Salary,
                    entity.Benefits,
                    entity.Requirements,
                    entity.Functions,
                    entity.Softskills,
                    entity.Experience,
                    entity.JobModalityId,
                    entity.JobPlaceId,
                    entity.Country,
                    entity.Province,
                    entity.City,
                    entity.Address
                },
                commandType: CommandType.StoredProcedure);
                return Ok(data);
            }
        }

        [HttpDelete]
        [Route("DeleteJobApplication")]
        public IActionResult DeleteJobApplication(long id)
        {
            using (var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connection").Value))
            {
                return Ok(connection.Execute("deleteJobApplication", new { id }, commandType: CommandType.StoredProcedure));
            }
        }

    }
}
