
using System.ComponentModel;

namespace BiliFor.Model.Bili
{
    public class BiliCookie
    {

        public string CookieStr { get; set; }

        /// <summary>
        /// DedeUserID
        /// </summary>
        [Description("DedeUserID")]
        public string UserId { get; set; }

        public string DedeUserID { get; set; }

        /// <summary>
        /// SESSDATA
        /// </summary>
        [Description("SESSDATA")]
        public string SessData { get; set; }

        /// <summary>
        /// bili_jct
        /// </summary>
        [Description("bili_jct")]
        public string BiliJct { get; set; }

        public string Bili_jct { get; set; }

        /// <summary>
        /// 其他Cookies
        /// </summary>
        public string OtherCookies { get; set; }
    }
}
