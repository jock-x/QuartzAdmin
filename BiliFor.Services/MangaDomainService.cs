using BiliFor.IRepository.Base;
using BiliFor.IServices;
using BiliFor.Model.Bili;
using BiliFor.Services.BASE;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliFor.Services
{
    public class MangaDomainService : BaseServices<BiliUserInfo>, IMangaDomainService
    {

        private readonly ILogger<MangaDomainService> _logger;
        IBaseRepository<BiliUserInfo> _dal;
        private readonly IHttpHelpService _httphelper;


        public MangaDomainService(IBaseRepository<BiliUserInfo> dal, ILogger<MangaDomainService> logger,IHttpHelpService httphelper)
        {
            this._logger = logger;
            this._dal = dal;
            base.BaseDal = dal;
            _httphelper = httphelper;
        }

        public void MangaSign(string cookie)
        {
            string url = "https://manga.bilibili.com/twirp/activity.v1.Activity/ClockIn?platform=android";
            BiliApiResponse response;
            try
            {
               var result= _httphelper.ToPost(url, cookie, "");
               response = JsonConvert.DeserializeObject<BiliApiResponse>(result.Html); 
            }
            catch (Exception)
            {
                //ignore
                //重复签到会报400异常,这里忽略掉
                _logger.LogInformation("今日已签到过，无法重复签到");
                return;
            }

            if (response.Code == 0)
            {
                _logger.LogInformation("完成漫画签到");
            }
            else
            {
                _logger.LogInformation("漫画签到异常");
            }
        }

        /// <summary>
        /// 获取大会员漫画权益
        /// </summary>
        /// <param name="reason_id">权益号，由https://api.bilibili.com/x/vip/privilege/my得到权益号数组，取值范围为数组中的整数
        /// 这里为方便直接取1，为领取漫读劵，暂时不取其他的值</param>
        //public void ReceiveMangaVipReward(int reason_id, UserInfo userInfo)
        //{
        //    int day = DateTime.Today.Day;

        //    if (day != _dailyTaskOptions.DayOfReceiveVipPrivilege)
        //    {
        //        //一个月执行一次就行
        //        _logger.LogInformation("目标领取日期为{target}号，今天是{day}号，跳过领取任务", _dailyTaskOptions.DayOfReceiveVipPrivilege, day);
        //        return;
        //    }

        //    if (userInfo.GetVipType() == 0)
        //    {
        //        _logger.LogInformation("不是会员或会员已过期，跳过领取任务");
        //        return;
        //    }

        //    var response = _mangaApi.ReceiveMangaVipReward(reason_id)
        //        .GetAwaiter().GetResult();
        //    if (response.Code == 0)
        //    {
        //        _logger.LogInformation($"大会员成功领取{response.Data.Amount}张漫读劵");
        //    }
        //    else
        //    {
        //        _logger.LogInformation($"大会员领取漫读劵失败，原因为:{response.Message}");
        //    }
        //}
    }
}
