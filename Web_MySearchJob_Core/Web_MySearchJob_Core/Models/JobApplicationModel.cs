using Web_MySearchJob_Core.Entities;
using System.Net.Http;
using Web_MySearchJob_Core.Interfaces;

namespace Web_MySearchJob_Core.Models
{
    public class JobApplicationModel (IConfiguration _configuration, HttpClient _httpClient) : IJobApplicationModel
    {
        public IEnumerable<JobApplication>? GetAllJobApplications()
        {
            string url = _configuration.GetSection("Parameters:UrlApi").Value + "api/v1/JobApplication/GetAllJobApplications";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<IEnumerable<JobApplication>>().Result;
            return null;
        }


        public JobApplication? GetJobApplicationById(long id)
        {
            string url = _configuration.GetSection("Parameters:UrlApi").Value + "api/v1/JobApplication/GetJobApplicationById?id=" + id;
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<JobApplication>().Result;
            return null;
        }


        public IEnumerable<JobModality>? GetAllModality()
        {
            string url = _configuration.GetSection("Parameters:UrlApi").Value + "api/v1/JobApplication/GetAllModality";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<IEnumerable<JobModality>>().Result;
            return null;
        }

        public int CreateJobApplication(JobApplication entity)
        {
            string url = _configuration.GetSection("Parameters:UrlApi").Value + "api/v1/JobApplication/CreateJobApplication";
            var body = JsonContent.Create(entity);
            var resp = _httpClient.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            return 0;
        }

        public int UpdateJobApplication(JobApplication entity)
        {
            string url = _configuration.GetSection("Parameters:UrlApi").Value + "api/v1/JobApplication/UpdateJobApplication";
            var body = JsonContent.Create(entity);
            var resp = _httpClient.PutAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            return 0;
        }

        public int DeleteJobApplication(long id)
        {
            string url = _configuration.GetSection("Parameters:UrlApi").Value + "api/v1/JobApplication/DeleteJobApplication?id=" + id;
            var resp = _httpClient.DeleteAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            return 0;
        }




    }
}
 