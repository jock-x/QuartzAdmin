using BiliFor.IRepository.Base;
using BiliFor.IServices;
using BiliFor.Model.Bili;
using BiliFor.Services.BASE;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BiliFor.Services
{
    public class VipPrivilegeDomainService : BaseServices<BiliUserInfo>, IVipPrivilegeDomainService
    {
        private readonly ILogger<VipPrivilegeDomainService> _logger;
        IBaseRepository<BiliUserInfo> _dal;
         private readonly IHttpHelpService _httphelper;


        public VipPrivilegeDomainService(IHttpHelpService httphelper,IBaseRepository<BiliUserInfo> dal,ILogger<VipPrivilegeDomainService> logger)
        {
            this._httphelper = httphelper;
            this._logger = logger;
            this._dal = dal;
            base.BaseDal = dal;
        }

        /// <summary>
        /// 每月领取大会员福利（B币券、大会员权益）
        /// </summary>
        /// <param name="useInfo"></param>
        public bool ReceiveVipPrivilege(BiliUserInfo userInfo,BiliCookie bilicookie)
        {
            //if (_dailyTaskOptions.DayOfReceiveVipPrivilege == 0)
            //{
            //    _logger.LogInformation("已配置为不进行自动领取会员权益，跳过领取任务");
            //    return false;
            //}

            //int targetDay = _dailyTaskOptions.DayOfReceiveVipPrivilege == -1
            //    ? 1
            //    : _dailyTaskOptions.DayOfReceiveVipPrivilege;

            //if (DateTime.Today.Day != targetDay
            //    && DateTime.Today.Day != DateTime.Today.LastDayOfMonth().Day)
            //{
            //    _logger.LogInformation("目标领取日期为{targetDay}号，今天是{day}号，跳过领取任务", targetDay, DateTime.Today.Day);
            //    return false;
            //}

            //大会员类型
            int vipType = userInfo.GetVipType();

            if (vipType == 2)
            {
                var suc1 = ReceiveVipPrivilege(1, bilicookie);
                var suc2 = ReceiveVipPrivilege(2, bilicookie);

                //if (suc1 | suc2) return true;
                return false;
            }
            else
            {
                _logger.LogInformation("普通会员和月度大会员每月不赠送B币券，所以不需要领取权益喽");
                return false;
            }



        }

        #region private

        /// <summary>
        /// 领取大会员每月赠送福利
        /// </summary>
        /// <param name="type">1.大会员B币券；2.大会员福利</param>
        private bool ReceiveVipPrivilege(int type, BiliCookie bilicookie)
        {
            string csrf = bilicookie.BiliJct;
            string url = string.Format("http://api.bilibili.com/x/vip/privilege/receive?type={0}&csrf={1}", type, csrf);
            var response = _httphelper.ToPost(url, bilicookie.CookieStr,"");
            BiliApiResponse apiResponse = JsonConvert.DeserializeObject<BiliApiResponse>(response.Html);
            var name = GetPrivilegeName(type);
            if (apiResponse.Code == 0)
            {
                _logger.LogDebug($"{name}成功");
                return true;
            }
            else
            {
                _logger.LogError($"{name}失败，原因: {apiResponse.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取权益名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetPrivilegeName(int type)
        {
            switch (type)
            {
                case 1:
                    return "领取年度大会员每月赠送的B币券";

                case 2:
                    return "领取大会员福利/权益";
            }

            return "";
        }

        #endregion 
    }
}
