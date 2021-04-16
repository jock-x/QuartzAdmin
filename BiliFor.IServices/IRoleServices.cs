using BiliFor.IServices.BASE;
using BiliFor.Model.Models;
using System.Threading.Tasks;

namespace BiliFor.IServices
{	
	/// <summary>
	/// RoleServices
	/// </summary>	
    public interface IRoleServices :IBaseServices<Role>
	{
        Task<Role> SaveRole(string roleName);
        Task<string> GetRoleNameByRid(int rid);

    }
}
