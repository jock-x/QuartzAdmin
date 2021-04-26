
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
        private readonly IHttpHelpService _httphelper;


        public BiliAccountService(IBaseRepository<BiliUserInfo> dal, ILogger<BiliAccountService> logger,IHttpHelpService httphelper)
        {
            this._logger = logger;
            this._dal = dal;
            this._httphelper = httphelper;
            base.BaseDal = dal;
        }

        #region
        public BiliUserInfo LoginByCookie(string cookie)
        {
            HttpHelper _http = new HttpHelper();
            _logger.LogInformation("");
            _logger.LogInformation("开始登录");

            string BiLiLoginForSure = "http://api.bilibili.com/x/web-interface/nav";
            HttpResult result =  _httphelper.ToGet(BiLiLoginForSure, cookie);

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

        #endregion



        /// <summary>
        /// 获取每日任务完成情况
        /// </summary>
        /// <returns></returns>
        public DailyTaskInfo GetDailyTaskStatus(string cookie)
        {
            DailyTaskInfo result = new();

            string url = "http://api.bilibili.com/x/member/web/exp/reward";
            HttpResult httpresult = _httphelper.ToGet(url, cookie);

            BiliApiResponse<DailyTaskInfo> apiResponse = JsonConvert.DeserializeObject<BiliApiResponse<DailyTaskInfo>>(httpresult.Html);

           
            if (apiResponse.Code == 0)
            {
                _logger.LogDebug("请求本日任务完成状态成功");
                result = apiResponse.Data;
            }
            else
            {
                _logger.LogWarning("获取今日任务完成状态失败：{result}", JsonConvert.SerializeObject(apiResponse));
                result = apiResponse.Data;
            }

            return result;
        }
    }
}
