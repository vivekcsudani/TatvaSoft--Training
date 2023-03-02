using Ciplatform.Entities.Data;
using Ciplatform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciplatform.Repository.Repository
{
    public class UserRepository 
    {
        private readonly CiContext _ciContext;
        public UserRepository(CiContext ciContext)
        {
           _ciContext = ciContext;
         }

    }
}
