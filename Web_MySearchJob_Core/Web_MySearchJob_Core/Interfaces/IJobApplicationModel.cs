using Web_MySearchJob_Core.Entities;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace Web_MySearchJob_Core.Interfaces
{
    public interface IJobApplicationModel
    {
        public IEnumerable<JobApplication>? GetAllJobApplications();
        public JobApplication? GetJobApplicationById(long id);
        public IEnumerable<JobModality>? GetAllModality();
        public int CreateJobApplication(JobApplication entity);
        public int UpdateJobApplication(JobApplication entity);
        public int DeleteJobApplication(long id);
    }
}
