using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCL.Security.Identity
{
	public class Worker : User
	{
        public Worker(int userId, string name, int cardId, string userType)
            : base(userId, name, cardId, userType) { }
    }
}
