using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobBoard.Data;
using JobBoard.Dto;
using JobBoard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobRepository _jobsRepository;
        public JobsController(IJobRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Job>> GetAllJobs()
        {
            return Ok(_jobsRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Job> GetJob(int id) {

            var job = _jobsRepository.GetByID(id);
            if (job is null) return NotFound();
            return Ok(job);
        }

        [HttpPost]
        public ActionResult CreateJob(JobDto job) {

            _jobsRepository.CreateJob(job);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateJob(int id, JobDto job) {

            var updatedJob = _jobsRepository.UpdateJob(id, job);

            if(updatedJob is null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteJob(int id) {
            _jobsRepository.DeleteJob(id);
            return NoContent();
        }

    }
}
