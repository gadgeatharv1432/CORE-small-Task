using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTask.DTO
{
    public class GetProfileDTO
    {
        public Guid Id { get; set; }

        // User's display name — editable on the profile page
        public string UserName { get; set; }

        // Email — shown but NOT editable (read-only in UI)
        // WHY read-only: changing email requires verification flow
        // which we don't have yet
        public string Email { get; set; }

        // Role — shown but NOT editable (admin sets roles)
        public string Role { get; set; }
    }
}
