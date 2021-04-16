using BiliFor.Services.BASE;
using BiliFor.Model.Models;
using BiliFor.IRepository;
using BiliFor.IServices;
using BiliFor.IRepository.Base;

namespace BiliFor.Services
{	
	/// <summary>
	/// ModulePermissionServices
	/// </summary>	
	public class ModulePermissionServices : BaseServices<ModulePermission>, IModulePermissionServices
    {

        IBaseRepository<ModulePermission> _dal;
        public ModulePermissionServices(IBaseRepository<ModulePermission> dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
       
    }
}
