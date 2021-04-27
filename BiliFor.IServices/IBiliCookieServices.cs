

using BiliFor.IServices.BASE;
using BiliFor.Model.Bili;

namespace BiliFor.IServices
{
    public interface IBiliCookieServices : IBaseServices<BiliCookie>
    {
        /// <summary>
        /// 解析cookie内容成实体类
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public BiliCookie DescribeCookie(string cookie);
    }
}
