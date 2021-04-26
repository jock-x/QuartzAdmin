
using BiliFor.Common.Helper;
using BiliFor.IRepository.Base;
using BiliFor.IServices;
using BiliFor.Model.Bili;
using BiliFor.Services.BASE;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace BiliFor.Services
{
    public class BiliAccountService : BaseServices<BiliUserInfo>, IBiliAccountService
    {
        private readonly ILogger<BiliAccountService> _logger;
        IBaseRepository<BiliUserInfo> _dal;
    


        public BiliAccountService( IBaseRepository<BiliUserInfo> dal, ILogger<BiliAccountService> logger)
        {
            this._logger = logger;
           
            this._dal = dal;
            base.BaseDal = dal;
        }


        public BiliUserInfo LoginByCookie(string cookie)
        {
            _logger.LogInformation("");
            _logger.LogInformation("开始登录");
            HttpHelper _http = new HttpHelper();
            string BiLiLoginForSure = "http://api.bilibili.com/x/web-interface/nav";
            HttpItem item = new HttpItem()
            {
                URL = BiLiLoginForSure,//URL 必需项
                Method = "get",//URL     可选项 默认为Get
                Timeout = 100000,//连接超时时间     可选项默认为100000
                Accept = "application/x-www-form-urlencoded",//可选项有默认值
                Cookie = cookie
            };
            HttpResult result = _http.GetHtml(item);
            BiliApiResponse<BiliUserInfo> apiResponse = JsonConvert.DeserializeObject<BiliApiResponse<BiliUserInfo>>(result.Html);

            if (apiResponse.Code != 0 || !apiResponse.Data.IsLogin)
            {
                _logger.LogWarning("登录异常，请检查Cookie是否错误或过期");
                return null;
            }
            BiliUserInfo useInfo = apiResponse.Data;

            _logger.LogInformation("登录成功，经验+{0} √", 5);
            _logger.LogInformation("用户名: {0}", useInfo.GetFuzzyUname());
            _logger.LogInformation("硬币余额: {0}", useInfo.Money ?? 0);

            if (useInfo.Level_info.Current_level < 6)
            {
                _logger.LogInformation("如每日做满65点经验，距离升级到 Lv{0} 还有: {1}天",
                    useInfo.Level_info.Current_level + 1,
                    (useInfo.Level_info.GetNext_expLong() - useInfo.Level_info.Current_exp) / 65);
            }
            else
            {
                _logger.LogInformation("您已是 Lv6 的大佬了，当前经验：{0}，无敌是多么寂寞~", useInfo.Level_info.Current_exp);
            }

            return useInfo;
        }

    }
}
