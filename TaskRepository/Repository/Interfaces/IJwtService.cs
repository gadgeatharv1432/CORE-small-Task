using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTask.Entity;

namespace TaskRepository.Repository.Interfaces
{
    public class IJwtService
    {
        string GenerateToken(ModelUser user);
    }
}
