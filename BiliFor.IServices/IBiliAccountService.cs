using BiliFor.IServices.BASE;
using BiliFor.Model.Bili;


namespace BiliFor.IServices
{
    public interface IBiliAccountService : IBaseServices<BiliUserInfo>
    {
        /// <summary>
        /// 使用Cookie登录
        /// </summary>
        /// <returns></returns>
        HttpResponse<BiliUserInfo> LoginByCookie(string cookie);

        /// <summary>
        /// 获取每日任务完成情况
        /// </summary>
        /// <returns></returns>
        HttpResponse<DailyTaskInfo> GetDailyTaskStatus(string cookie);

    }
}
