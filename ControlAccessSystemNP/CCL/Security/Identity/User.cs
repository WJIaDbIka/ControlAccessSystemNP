using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCL.Security.Identity
{
	public abstract class User
	{
        public User(int userId, string name, string userType)
        {
            UserId = userId;
			Name = name;
			UserType = userType;
        }

        public int UserId { get; }

		public string Name { get; }

		protected string UserType { get; }
	}
}
