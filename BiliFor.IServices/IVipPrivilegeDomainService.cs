

using BiliFor.IServices.BASE;
using BiliFor.Model.Bili;

namespace BiliFor.IServices
{
    public interface IVipPrivilegeDomainService : IBaseServices<BiliUserInfo>
    {

        /// <summary>
        /// 获取大会员权益
        /// </summary>
        /// <param name="useInfo"></param>
        BiMessage ReceiveVipPrivilege(BiliUserInfo userInfo, BiliCookie bilicookie);
    }
}
