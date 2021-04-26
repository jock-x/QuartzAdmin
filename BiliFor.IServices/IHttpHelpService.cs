using BiliFor.Common.Helper;
using BiliFor.IServices.BASE;

namespace BiliFor.IServices
{
    public interface IHttpHelpService : IBaseServices<HttpResult>
    {
        HttpResult ToPost(string url, string cookie, string postdata);

        HttpResult ToGet(string url, string cookie);


    }
}
