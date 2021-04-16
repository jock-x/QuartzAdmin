using BiliFor.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
