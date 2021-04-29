using BiliFor.IServices.BASE;
using BiliFor.Model.Bili;

namespace BiliFor.IServices
{
    public interface ICSDNSignServices : IBaseServices<BiliUserInfo>
    {
         string CSDNSign(string cookie);
    }
}
