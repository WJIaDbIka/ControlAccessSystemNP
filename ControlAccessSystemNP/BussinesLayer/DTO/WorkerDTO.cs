using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.DTO
{
	public sealed class WorkerDTO
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public string Phone { get; set; } = string.Empty;

		public string Position { get; set; } = string.Empty;
	}
}
