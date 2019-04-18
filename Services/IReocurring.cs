using DavidProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Services
{
	public interface IReocurring
	{
		Reoccurring GetReoccurring(int? id);
		IEnumerable<Reoccurring> GetReoccurrings();
		void Create(Reoccurring task);
        void Update(int? id, Reoccurring task);
		void Delete(int? id);
	}
}
