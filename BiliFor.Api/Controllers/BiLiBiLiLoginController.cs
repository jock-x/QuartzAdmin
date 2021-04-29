using BiliFor.Common;
using BiliFor.Common.Helper;
using BiliFor.IServices;
using BiliFor.Model.Bili;
using BiliFor.Tasks.QuartzNet.utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;


/// <summary>
/// 
/// </summary>
namespace BiliFor.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BiLiBiLiLoginController : Controller
    {
        readonly IBiliAccountService _biliaccountservice;
        private readonly ILogger<BiLiBiLiLoginController> _logger;
        readonly IMangaDomainService _managadomainservice;
        readonly IBiliCookieServices _bilicookieservices;
        readonly IVipPrivilegeDomainService _vipService;
        readonly ICSDNSignServices _csdnsignservices;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vipService"></param>
        /// <param name="bilicookieservices"></param>
        /// <param name="biliaccountservice"></param>
        /// <param name="managadomainservice"></param>
        /// <param name="logger"></param>
        public BiLiBiLiLoginController(ICSDNSignServices csdnsignservices,IVipPrivilegeDomainService vipService, IBiliCookieServices bilicookieservices, IBiliAccountService biliaccountservice, IMangaDomainService managadomainservice, ILogger<BiLiBiLiLoginController> logger)
        {
            _csdnsignservices = csdnsignservices;
            _vipService = vipService;
            _bilicookieservices = bilicookieservices;
            _biliaccountservice = biliaccountservice;
            _managadomainservice = managadomainservice;
            _logger = logger;
        }


        /// <returns></returns>
        [HttpGet]
        public async Task<string> Get()
        {
            string BiLiLoginUrl = Appsettings.app("BiliBili", "LoginGet").ToString();

            string ResultJson = "";

            HttpHelper http = new HttpHelper();


            HttpItem item = new HttpItem()
            {
                URL = BiLiLoginUrl,//URL     必需项
                Method = "get",//URL     可选项 默认为Get
                Timeout = 100000,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = "",//字符串Cookie     可选项
                UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",//用户的浏览器类型，版本，操作系统     可选项有默认值
                Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
            };
            //item.Header.Add("测试Key1", "测试Value1");
            //item.Header.Add("测试Key2", "测试Value2");
            //得到HTML代码
            HttpResult result = http.GetHtml(item);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类
                ResultJson = result.Html;
            }
            return ResultJson;
        }





        /// <summary>
        /// 登录获取哔哩哔哩信息
        /// </summary>
        /// <param name="oauthKey"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(Policy = "Scope_BlogModule_Policy")]
        //[Authorize]
        public async Task<string> Post(string oauthKey)
        {

            string BiLiLoginForSure = Appsettings.app("BiliBili", "LoginForSure").ToString();

            string ResultJson = "";

            HttpHelper http = new HttpHelper();


            HttpItem item = new HttpItem()
            {
                URL = BiLiLoginForSure,//URL     必需项
                Method = "post",//URL     可选项 默认为Get
                Timeout = 100000,//连接超时时间     可选项默认为100000
                Accept = "application/x-www-form-urlencoded",//    可选项有默认值
                Postdata = "oauthKey=" + oauthKey,//Post数据     可选项GET时不需要写
            };
            HttpResult result = http.GetHtml(item);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类
                ResultJson = result.Html;
            }
            //取出返回的Cookie
            string cookie = result.Cookie;
            return ResultJson;
        }




        /// <summary>
        /// 登录获取哔哩哔哩信息
        /// </summary>
        /// <param name="oauthKey"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(Policy = "Scope_BlogModule_Policy")]
        //[Authorize]
        [Route("GetUserInfo")]
        public async Task<string> GetUserInfo()
        {
            string cookie = "CURRENT_FNVAL=80; _uuid=846FF8F2-C882-C1CD-808F-DEEA4719175585066infoc; blackside_state=1; rpdid=|(m)mJumk)R0J'uYuku|kmm|; buvid3=BB379260-46D0-407B-B31A-086227A133E1184999infoc; LIVE_BUVID=AUTO4416177595741756; fingerprint3=5bdd4f9e03267a2fa34c274964a20a2b; buvid_fp=BB379260-46D0-407B-B31A-086227A133E1184999infoc; bp_article_offset_4830365=510771353527463951; fingerprint_s=22fdbf4a2d532409728945361a3a8c15; bp_t_offset_4830365=514104505842011394; bp_video_offset_4830365=514160147147869440; PVID=1; bfe_id=1e33d9ad1cb29251013800c68af42315; fingerprint=bf201b8cab1033a1ec48ac66ca6093a4; buvid_fp_plain=3536A90E-676D-49F6-8195-3D36856858C4138393infoc; SESSDATA=524a2906,1634354147,94f35*41; bili_jct=884c71e8fcc5798e65461d34baaac988; DedeUserID=4830365; DedeUserID__ckMd5=3d3f94374f6dd343; sid=d3a6p9a7";



            BiliCookie cookieentity = _bilicookieservices.DescribeCookie(cookie);
            //获取登录用户信息
            HttpResponse<BiliUserInfo> biliuserinfo = _biliaccountservice.LoginByCookie(cookie);

            //获取每日任务
            HttpResponse<DailyTaskInfo> daily = _biliaccountservice.GetDailyTaskStatus(cookie);

            //领取大会员权益
            BiMessage viprecive = _vipService.ReceiveVipPrivilege(biliuserinfo.Data, cookieentity);

            //漫画签到
            BiMessage mangasign = _managadomainservice.MangaSign(cookie);


            DescibeHttpResponse describe = new DescibeHttpResponse();

            describe.DescibeResponseString(biliuserinfo.Message);
            describe.DescibeResponseString(daily.Message);
            describe.DescibeResponseString(viprecive.Message);
            describe.DescibeResponseString(mangasign.Message);


            string outstring = describe.outstring();


            string csdncookie = "uuid_tt_dd=10_36575270660-1607065853983-192011; UN=u010840685; p_uid=U010000; Hm_ct_6bcd52f51e9b3dce32bec4a3997715ac=6525*1*10_36575270660-1607065853983-192011!5744*1*u010840685; UserName=u010840685; UserInfo=2e1b0c3535df4361b38c567d8878a374; UserToken=2e1b0c3535df4361b38c567d8878a374; UserNick=%E6%AC%B2%E6%80%9D; AU=509; BT=1616394156533; Hm_up_6bcd52f51e9b3dce32bec4a3997715ac=%7B%22islogin%22%3A%7B%22value%22%3A%221%22%2C%22scope%22%3A1%7D%2C%22isonline%22%3A%7B%22value%22%3A%221%22%2C%22scope%22%3A1%7D%2C%22isvip%22%3A%7B%22value%22%3A%220%22%2C%22scope%22%3A1%7D%2C%22uid_%22%3A%7B%22value%22%3A%22u010840685%22%2C%22scope%22%3A1%7D%7D; ssxmod_itna=eqmx0D9Dc73iqxQq0dy7tigYY5APh0xqxqGXxpoDZDiqAPGhDC8ScxD5wEq4KCDy7+xpxCtGRGtW5=7+Tehtyr74GLDmKDy+W6eGGIxBYDQxAYDGDDpXD84DrAxYPG0DiKGRDlIFcDAf=Dbx=2DitSDDUF04G2D7tnzqL42wrDAd+yK7DnD0t5xBdPDcDniQnr=YiTeTNZDBQD7qNnDYo67eDHB2xTeO4f0O+YlPvY0hDG0xfbCY4PbIDei7vYQiOtD8DqQB+d9gkDG3PG2iD===; ssxmod_itna2=eqmx0D9Dc73iqxQq0dy7tigYY5APh0xqxA6b5P4D/iQCDFOYtpcDID5BIcxBM4ZmxwufCrm2qOUQLFIjHq7t+=qrXw0B=DDt0iYd+TIDgSeEyIrWYqYXiRlr0lD9YaW407KFx7=D+OGDD===; __gads=ID=0de619a196a7e516-22ce28af7ec700a4:T=1619073599:RT=1619073599:S=ALNI_MZMiIamo3GoRDRsFpW7RQfIW1wX5g; c_hasSub=true; dc_session_id=10_1619500073751.798823; dc_sid=01fedb8452acf1f00413c34dec3683bd; announcement-new=%7B%22isLogin%22%3Atrue%2C%22announcementUrl%22%3A%22https%3A%2F%2Fblog.csdn.net%2Fblogdevteam%2Farticle%2Fdetails%2F112280974%3Futm_source%3Dgonggao_0107%22%2C%22announcementCount%22%3A0%2C%22announcementExpire%22%3A3600000%7D; c_first_ref=github.com; c_first_page=https%3A//blog.csdn.net/; c_segment=11; Hm_lvt_6bcd52f51e9b3dce32bec4a3997715ac=1619428881,1619430562,1619430574,1619500082; c_ref=https%3A//mp.csdn.net/console/home%3Fspm%3D1001.2100.3001.4503; c_page_id=default; log_Id_click=94; c_pref=https%3A//mp.csdn.net/console/home%3Fspm%3D1001.2100.3001.4503; log_Id_view=739; Hm_lpvt_6bcd52f51e9b3dce32bec4a3997715ac=1619500342; dc_tos=qs7igo; log_Id_pv=427";
            _csdnsignservices.CSDNSign(csdncookie);

            return "";
        }
    }
}
