using JobBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Data
{
    public interface IJobRepository
    {
        public IEnumerable<Job> GetAll();

        public Job GetByID(int ID);

        public void DeleteJob(int ID);

        public Job CreateJob(Job job);

        public Job UpdateJob(Job job);
    }
}
