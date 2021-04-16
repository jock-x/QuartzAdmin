using BiliFor.IRepository.Base;
using BiliFor.IServices;
using BiliFor.Model.Models;
using BiliFor.Services.BASE;

namespace BiliFor.Services
{
    public partial class OperateLogServices : BaseServices<OperateLog>, IOperateLogServices
    {
        IBaseRepository<OperateLog> _dal;
        public OperateLogServices(IBaseRepository<OperateLog> dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}
