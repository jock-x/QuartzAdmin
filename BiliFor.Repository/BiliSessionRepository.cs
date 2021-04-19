using BiliFor.IRepository;
using BiliFor.IRepository.UnitOfWork;
using BiliFor.Model.Models;
using BiliFor.Repository.Base;

namespace BiliFor.Repository
{	
	/// <summary>
	/// BiliSessionRepository
	/// </summary>	
	public class BiliSessionRepository : BaseRepository<BiliSession>, IBiliSessionRepository
    {
		public BiliSessionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}
	}
}

	