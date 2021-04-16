using BiliFor.IRepository.Base;
using BiliFor.Model.IDS4DbModels;
using BiliFor.Services.BASE;

namespace BiliFor.IServices
{
    public class ApplicationUserServices : BaseServices<ApplicationUser>, IApplicationUserServices
    {

        IBaseRepository<ApplicationUser> _dal;
        public ApplicationUserServices(IBaseRepository<ApplicationUser> dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}