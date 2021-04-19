using System;
using BiliFor.Model.Models;
using BiliFor.IRepository;
using BiliFor.IServices;
using BiliFor.IRepository.Base;
using BiliFor.Services.BASE;

namespace BiliFor.Services
{
    /// <summary>
    /// BiliSessionService
    /// </summary>	
    public class BiliSessionService : BaseServices<BiliSession>, IBiliSessionService
    {

        IBaseRepository<BiliSession> _dal;

        public BiliSessionService(IBaseRepository<BiliSession> dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}
