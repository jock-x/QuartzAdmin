using BiliFor.IServices.BASE;
using BiliFor.Model.Bili;

namespace BiliFor.IServices
{
    public interface IMangaDomainService : IBaseServices<BiliUserInfo>
    {
        /// <summary>
        /// 签到
        /// </summary>
        BiMessage MangaSign(string cookie);

        /// <summary>
        /// 获取大会员权益
        /// </summary>
        /// <param name="reason_id"></param>
        /// <param name="userIfo"></param>
        //void ReceiveMangaVipReward(int reason_id, BiliUserInfo userIfo);

    }
}
