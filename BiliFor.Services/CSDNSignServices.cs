using BiliFor.Common.Helper;
using BiliFor.IRepository.Base;
using BiliFor.IServices;
using BiliFor.Model.Bili;
using BiliFor.Services.BASE;
using Microsoft.Extensions.Logging;

namespace BiliFor.Services
{
    public class CSDNSignServices : BaseServices<BiliUserInfo>, ICSDNSignServices
    {

        private readonly ILogger<CSDNSignServices> _logger;
        IBaseRepository<BiliUserInfo> _dal;
      


        public CSDNSignServices(IBaseRepository<BiliUserInfo> dal, ILogger<CSDNSignServices> logger)
        {
            this._logger = logger;
            this._dal = dal;
           
            base.BaseDal = dal;
        }


        public void CSDNSign(string cookie)
        {
            string url = "https://me.csdn.net/api/LuckyDraw_v2/signIn";
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = url,//URL 必需项
                Method = "get",//URL     可选项 默认为Get
                Timeout = 100000,//连接超时时间     可选项默认为100000
                Accept = "application/x-www-form-urlencoded",//可选项有默认值
                Cookie = cookie,
                Postdata = ""
            };
            item.Header.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.96 Safari/537.36");
            item.Header.Add("Cookie", cookie);
            HttpResult result = http.GetHtml(item);
        }

    }
}
