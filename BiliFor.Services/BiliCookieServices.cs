using BiliFor.Common.Util;
using BiliFor.IRepository.Base;
using BiliFor.IServices;
using BiliFor.Model.Bili;
using BiliFor.Services.BASE;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BiliFor.Services
{
    public class BiliCookieServices : BaseServices<BiliCookie>, IBiliCookieServices
    {
        private readonly ILogger<BiliCookieServices> _logger;
        IBaseRepository<BiliCookie> _dal;


        public BiliCookieServices(IBaseRepository<BiliCookie> dal, ILogger<BiliCookieServices> logger)
        {
            this._logger = logger;
            this._dal = dal;
            base.BaseDal = dal;
        }

        public BiliCookie DescribeCookie(string cookie)
        {
            List<string> CookieStrList = new();
            string CookieStr = "";
            BiliCookie bilicookie = new();
            Dictionary<string, string> CookieDictionary = new Dictionary<string, string>();
           

            CookieStr = cookie ?? "";

            CookieStrList = CookieStr.Split(";")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();

            foreach (var item in CookieStrList)
            {
                var list = item.Split('=');
                CookieDictionary.TryAdd(list[0].Trim(), list[1].Trim());
            }

            bilicookie.CookieStr = cookie;
            if (CookieDictionary.TryGetValue("DedeUserID", out string userId))
            {
                bilicookie.UserId = userId;
            }
            if (CookieDictionary.TryGetValue("bili_jct", out string jct))
            {
                bilicookie.BiliJct = jct;
            }
            if (CookieDictionary.TryGetValue("SESSDATA", out string sess))
            {
                bilicookie.SessData = sess;
            }
            return bilicookie;
        }


        private string GetPropertyDescription(string propertyName)
        {
            return GetType().GetPropertyDescription(propertyName);
        }

     

    }
}
