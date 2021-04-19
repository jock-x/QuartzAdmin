using BiliFor.Common;
using BiliFor.Common.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        private readonly ILogger<BiLiBiLiLoginController> _logger;
        /// <summary>
        /// 构造函数
        /// </summary>

        /// <param name="logger"></param>
        public BiLiBiLiLoginController(ILogger<BiLiBiLiLoginController> logger)
        {
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

            string BiLiLoginForSure = "http://api.bilibili.com/x/web-interface/nav";

            
            HttpHelper http = new HttpHelper();

            string cookie = "CURRENT_FNVAL=80; _uuid=846FF8F2-C882-C1CD-808F-DEEA4719175585066infoc; blackside_state=1; rpdid=|(m)mJumk)R0J'uYuku|kmm|; buvid3=BB379260-46D0-407B-B31A-086227A133E1184999infoc; LIVE_BUVID=AUTO4416177595741756; fingerprint3=5bdd4f9e03267a2fa34c274964a20a2b; buvid_fp=BB379260-46D0-407B-B31A-086227A133E1184999infoc; bp_article_offset_4830365=510771353527463951; fingerprint_s=22fdbf4a2d532409728945361a3a8c15; bp_t_offset_4830365=514104505842011394; bp_video_offset_4830365=514160147147869440; PVID=1; bfe_id=1e33d9ad1cb29251013800c68af42315; fingerprint=bf201b8cab1033a1ec48ac66ca6093a4; buvid_fp_plain=3536A90E-676D-49F6-8195-3D36856858C4138393infoc; SESSDATA=524a2906,1634354147,94f35*41; bili_jct=884c71e8fcc5798e65461d34baaac988; DedeUserID=4830365; DedeUserID__ckMd5=3d3f94374f6dd343; sid=d3a6p9a7";


            HttpItem item = new HttpItem()
            {
                URL = BiLiLoginForSure,//URL 必需项
                Method = "get",//URL     可选项 默认为Get
                Timeout = 100000,//连接超时时间     可选项默认为100000
                Accept = "application/x-www-form-urlencoded",//可选项有默认值
                Cookie = cookie
            };
            HttpResult result = http.GetHtml(item);
            string cookietest = result.Cookie;
            return "";
        }
    }
}
