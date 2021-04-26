using BiliFor.IRepository.Base;
using BiliFor.IServices;
using BiliFor.Model.Bili;
using BiliFor.Services.BASE;
using Microsoft.Extensions.Logging;


namespace BiliFor.Services
{
    public class DonateCoinDomainService : BaseServices<BiliUserInfo>, IDonateCoinDomainService
    {

        private readonly ILogger<DonateCoinDomainService> _logger;
        IBaseRepository<BiliUserInfo> _dal;
      

        public DonateCoinDomainService(IBaseRepository<BiliUserInfo> dal, ILogger<DonateCoinDomainService> logger)
        {
            this._logger = logger;
            this._dal = dal;
            base.BaseDal = dal;
        }

        //public void AddCoinsForVideos()
        //{


        //}

        //public Tuple<string, string> TryGetCanDonatedVideo()
        //{
        //    Tuple<string, string> result = null;

        //    //从配置的up中随机尝试获取1次
        //    result = TryGetCanDonateVideoByConfigUps(1);
        //    if (result != null) return result;

        //    //然后从特别关注列表尝试获取1次
        //    result = TryGetCanDonateVideoBySpecialUps(1);
        //    if (result != null) return result;

        //    //然后从普通关注列表获取1次
        //    result = TryGetCanDonateVideoByFollowingUps(1);
        //    if (result != null) return result;

        //    //最后从排行榜尝试5次
        //    result = TryGetCanDonateVideoByRegion(5);

        //    return result;
        //}

        //public bool DoAddCoinForVideo(string aid, int multiply, bool select_like, string title = "")
        //{
        //    BiliApiResponse result;
        //    //try
        //    //{
        //    //    var request = new AddCoinRequest(long.Parse(aid), _biliBiliCookie.BiliJct);
        //    //    request.Multiply = multiply;
        //    //    request.Select_like = select_like ? 1 : 0;
        //    //    result = _videoApi.AddCoinForVideo(request)
        //    //        .GetAwaiter().GetResult();
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    return false;
        //    //}

        //    //if (result.Code == 0)
        //    //{
        //    //    _expDic.TryGetValue("每日投币", out int exp);
        //    //    _logger.LogInformation("为“{title}”投币成功，经验+{exp} √", title, exp);
        //    //    return true;
        //    //}

        //    //if (_donateContinueStatusDic.Any(x => x.Key == result.Code.ToString()))
        //    //{
        //    //    _logger.LogError("尝试为“{title}”投币失败，原因：{msg}", title, result.Message);
        //    //    return false;
        //    //}

        //    //else
        //    //{
        //    //    string errorMsg = $"投币发生未预计异常。接口返回：{result.Message}";
        //    //    _logger.LogError(errorMsg);
        //    //    throw new Exception(errorMsg);
        //    //}

        //    return false;
        //}


        //#region private

        ///// <summary>
        ///// 获取今日的目标投币数
        ///// </summary>
        ///// <returns></returns>
        //private int GetNeedDonateCoinNum()
        //{
        //    //获取自定义配置投币数
        //    int configCoins = _dailyTaskOptions.NumberOfCoins;
        //    if (configCoins <= 0)
        //    {
        //        _logger.LogInformation("已配置为跳过投币任务");
        //        return configCoins;
        //    }

        //    //已投的硬币
        //    int alreadyCoins = _coinDomainService.GetDonatedCoins();
        //    //目标
        //    int targetCoins = configCoins > Constants.MaxNumberOfDonateCoins
        //        ? Constants.MaxNumberOfDonateCoins
        //        : configCoins;

        //    if (targetCoins > alreadyCoins)
        //    {
        //        int needCoins = targetCoins - alreadyCoins;
        //        _logger.LogInformation("今日已投{already}枚硬币，目标是投{target}枚，还需再投{need}枚", alreadyCoins, targetCoins, needCoins);
        //        return needCoins;
        //    }

        //    _logger.LogInformation("今日已投{already}枚硬币，已完成投币任务，不需要再投啦~", alreadyCoins);
        //    return 0;
        //}

        ///// <summary>
        ///// 尝试从配置的up主里随机获取一个可以投币的视频
        ///// </summary>
        ///// <param name="tryCount"></param>
        ///// <returns></returns>
        //private Tuple<string, string> TryGetCanDonateVideoByConfigUps(int tryCount)
        //{
        //    //是否配置了up主
        //    if (_dailyTaskOptions.SupportUpIdList.Count == 0) return null;

        //    return TryCanDonateVideoByUps(_dailyTaskOptions.SupportUpIdList, tryCount); ;
        //}

        ///// <summary>
        ///// 尝试从特别关注的Up主中随机获取一个可以投币的视频
        ///// </summary>
        ///// <param name="tryCount"></param>
        ///// <returns></returns>
        //private Tuple<string, string> TryGetCanDonateVideoBySpecialUps(int tryCount)
        //{
        //    //获取特别关注列表
        //    var request = new GetSpecialFollowingsRequest(long.Parse(_biliBiliCookie.UserId));
        //    BiliApiResponse<List<UpInfo>> specials = _relationApi.GetSpecialFollowings(request)
        //        .GetAwaiter().GetResult();
        //    if (specials.Data == null || specials.Data.Count == 0) return null;

        //    return TryCanDonateVideoByUps(specials.Data.Select(x => x.Mid).ToList(), tryCount);
        //}

        ///// <summary>
        ///// 尝试从普通关注的Up主中随机获取一个可以投币的视频
        ///// </summary>
        ///// <param name="tryCount"></param>
        ///// <returns></returns>
        //private Tuple<string, string> TryGetCanDonateVideoByFollowingUps(int tryCount)
        //{
        //    //获取特别关注列表
        //    var request = new GetFollowingsRequest(long.Parse(_biliBiliCookie.UserId));
        //    BiliApiResponse<GetFollowingsResponse> result = _relationApi.GetFollowings(request)
        //        .GetAwaiter().GetResult();
        //    if (result.Data.Total == 0) return null;

        //    return TryCanDonateVideoByUps(result.Data.List.Select(x => x.Mid).ToList(), tryCount);
        //}

        ///// <summary>
        ///// 尝试从排行榜中获取一个没有看过的视频
        ///// </summary>
        ///// <param name="tryCount"></param>
        ///// <returns></returns>
        //private Tuple<string, string> TryGetCanDonateVideoByRegion(int tryCount)
        //{
        //    for (int i = 0; i < tryCount; i++)
        //    {
        //        var video = _videoDomainService.GetRandomVideoOfRanking();
        //        if (!IsCanDonate(video.Aid.ToString())) continue;
        //        return Tuple.Create<string, string>(video.Aid.ToString(), video.Title);
        //    }

        //    return null;
        //}

        ///// <summary>
        ///// 尝试从指定的up主集合中随机获取一个可以尝试投币的视频
        ///// </summary>
        ///// <param name="upIds"></param>
        ///// <param name="tryCount"></param>
        ///// <returns></returns>
        //private Tuple<string, string> TryCanDonateVideoByUps(List<long> upIds, int tryCount)
        //{
        //    if (upIds == null || upIds.Count == 0) return null;

        //    //尝试tryCount次
        //    for (int i = 1; i <= tryCount; i++)
        //    {
        //        //获取随机Up主Id
        //        long randomUpId = upIds[new Random().Next(0, upIds.Count)];

        //        if (randomUpId == 0 || randomUpId == long.MinValue) continue;

        //        if (randomUpId.ToString() == _biliBiliCookie.UserId)
        //        {
        //            _logger.LogDebug("不能为自己投币");
        //            continue;
        //        }

        //        //该up的视频总数
        //        if (!_upVideoCountDicCatch.TryGetValue(randomUpId, out int videoCount))
        //        {
        //            videoCount = _videoDomainService.GetVideoCountOfUp(randomUpId);
        //            _upVideoCountDicCatch.Add(randomUpId, videoCount);
        //        }
        //        if (videoCount == 0) continue;

        //        UpVideoInfo videoInfo = _videoDomainService.GetRandomVideoOfUp(randomUpId, videoCount);
        //        _logger.LogDebug("获取到视频{aid}({title})", videoInfo.Aid, videoInfo.Title);

        //        //检查是否可以投
        //        if (!IsCanDonate(videoInfo.Aid.ToString())) continue;

        //        return Tuple.Create(videoInfo.Aid.ToString(), videoInfo.Title);
        //    }

        //    return null;
        //}

        ///// <summary>
        ///// 已为视频投币个数是否小于最大限制
        ///// </summary>
        ///// <param name="aid">av号</param>
        ///// <returns></returns>
        //private bool IsDonatedLessThenLimitCoinsForVideo(string aid)
        //{
        //    //获取已投币数量
        //    if (!_alreadyDonatedCoinCountCatch.TryGetValue(aid, out int multiply))
        //    {
        //        multiply = _videoApi.GetDonatedCoinsForVideo(new GetAlreadyDonatedCoinsRequest(long.Parse(aid)))
        //            .GetAwaiter().GetResult()
        //            .Data.Multiply;
        //        _alreadyDonatedCoinCountCatch.TryAdd(aid, multiply);
        //    }

        //    _logger.LogDebug("已为Av{aid}投过{num}枚硬币", aid, multiply);

        //    if (multiply >= 2) return false;

        //    //获取该视频可投币数量
        //    int limitCoinNum = _videoDomainService.GetVideoDetail(aid).Copyright == 1
        //        ? 2 //原创，最多可投2枚
        //        : 1;//转载，最多可投1枚
        //    _logger.LogDebug("该视频的最大投币数为{num}", limitCoinNum);

        //    return multiply < limitCoinNum;
        //}

        ///// <summary>
        ///// 检查获取到的视频是否可以投币
        ///// </summary>
        ///// <param name="aid"></param>
        ///// <returns></returns>
        //private bool IsCanDonate(string aid)
        //{
        //    //本次运行已经尝试投过的,不进行重复投（不管成功还是失败，凡取过尝试过的，不重复尝试）
        //    if (_alreadyDonatedCoinCountCatch.Any(x => x.Key == aid))
        //    {
        //        _logger.LogDebug("重复视频，丢弃处理");
        //        return false;
        //    }

        //    //已经投满2个币的，不能再投
        //    if (!IsDonatedLessThenLimitCoinsForVideo(aid))
        //    {
        //        _logger.LogDebug("超出单个视频投币数量限制，丢弃处理", aid);
        //        return false;
        //    }

        //    return true;
        //}

        //#endregion
    }

}
