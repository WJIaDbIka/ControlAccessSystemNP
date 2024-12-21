using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCL.Security.Identity
{
	public class WorkerIdentity : User
	{
        public WorkerIdentity(int userId, string name, string userType)
            : base(userId, name, userType) { }
    }
}
