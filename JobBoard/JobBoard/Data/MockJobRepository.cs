using JobBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace JobBoard.Data
{
    public class MockJobRepository : IJobRepository
    {
        private List<Job> _Jobs;
        public MockJobRepository()
        {
            _Jobs = new List<Job>();
            //TEST JOBS
            _Jobs.Add(new Job()
            {
                ID = 1,
                Title = ".NET Developer",
                Description = "Expert .NET Developer",
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddDays(10)
            }); ;

        }
        public Job CreateJob(Job job)
        {
            //Get Current Max ID
            var lastID = 0;
            var jobsIDs = _Jobs.Select(c => c.ID);
            if (jobsIDs.Count() > 0) {
                lastID = jobsIDs.Max();
            }
            

            //Update Dinamic Values
            job.ID = lastID + 1;
            job.CreatedAt = DateTime.Now;


            _Jobs.Add(job);

            return job;
        }

        public void DeleteJob(int ID)
        {
            var job = _Jobs.FirstOrDefault(c => c.ID == ID);

            if (job != null) {
                _Jobs.Remove(job);
            }
        }

        public IEnumerable<Job> GetAll()
        {
            return _Jobs;
        }

        public Job GetByID(int ID)
        {
            var job = _Jobs.FirstOrDefault(c => c.ID == ID);

            if (job == null) {
                return null;
            }

            return job;
        }

        public Job UpdateJob(int id, Job job)
        {
            var curjob = _Jobs.FirstOrDefault(c => c.ID == id);

            if (curjob is null) return null;

            //Update Values
            curjob.Title = job.Title;
            curjob.Description = job.Description;
            curjob.ExpiresAt = job.ExpiresAt;


            return curjob;
        }
    }
}
