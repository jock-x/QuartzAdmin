using System;
using BiliFor.IRepository;
using BiliFor.IRepository.Base;
using BiliFor.IServices;
using BiliFor.Model.Models;
using BiliFor.Services.BASE;

namespace BiliFor.Services
{	
	/// <summary>
	/// BiliSessionService
	/// </summary>	
	public class BiliSessionService : BaseServices<BiliSession>, IBiliSessionService
    {
	
        IBiliSessionRepository _repository;

        IBaseRepository<BiliSession> _dal;
        public BiliSessionService(IBaseRepository<BiliSession> dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }


    }
}
	