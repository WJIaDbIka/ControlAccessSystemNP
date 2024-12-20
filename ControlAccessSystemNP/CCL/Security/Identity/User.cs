using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCL.Security.Identity
{
	public abstract class User
	{
        public User(int userId, string name, int cardId, string userType)
        {
            UserId = userId;
			Name = name;
			CardId = cardId;
			UserType = userType;
        }

        public int UserId { get; }

		public string Name { get; }

		public int CardId { get; }

		protected string UserType { get; }
	}
}
