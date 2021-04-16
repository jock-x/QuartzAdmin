using BiliFor.IRepository.Base;
using BiliFor.IServices;
using BiliFor.Model.Models;
using BiliFor.Services.BASE;

namespace BiliFor.Services
{
    public partial class PasswordLibServices : BaseServices<PasswordLib>, IPasswordLibServices
    {
        IBaseRepository<PasswordLib> _dal;
        public PasswordLibServices(IBaseRepository<PasswordLib> dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}
