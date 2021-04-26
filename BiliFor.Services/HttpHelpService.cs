using BiliFor.Common.Helper;
using BiliFor.IRepository.Base;
using BiliFor.IServices;
using BiliFor.Services.BASE;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliFor.Services
{
    public class HttpHelpService : BaseServices<HttpResult>, IHttpHelpService
    {

        private readonly ILogger<HttpHelpService> _logger;
        IBaseRepository<HttpResult> _dal;
        HttpHelper http = new HttpHelper();


        public HttpHelpService(IBaseRepository<HttpResult> dal, ILogger<HttpHelpService> logger)
        {
            this._logger = logger;
            this._dal = dal;
            base.BaseDal = dal;
        }

        public HttpResult ToPost(string url,string cookie,string postdata)
        {
            HttpItem item = new HttpItem()
            {
                URL = url,//URL 必需项
                Method = "post",//URL     可选项 默认为Get
                Timeout = 100000,//连接超时时间     可选项默认为100000
                Accept = "application/x-www-form-urlencoded",//可选项有默认值
                Cookie = cookie,
                Postdata = postdata
            };
            HttpResult result = http.GetHtml(item);
            return result;
        }

        public HttpResult ToGet(string url,string cookie)
        {
            HttpItem item = new HttpItem()
            {
                URL = url,//URL 必需项
                Method = "get",//URL     可选项 默认为Get
                Timeout = 100000,//连接超时时间     可选项默认为100000
                Accept = "application/x-www-form-urlencoded",//可选项有默认值
                Cookie = cookie
            };
            HttpResult result = http.GetHtml(item);
            return result;
        }
    }
}
