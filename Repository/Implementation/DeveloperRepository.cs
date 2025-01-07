using Database;
using Entities.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class DeveloperRepository : RepositoryBase<Developer>, IDeveloperRepository
    {
        public DeveloperRepository(ApplicationContext context): base(context)
        {
            
        }
    }
}
