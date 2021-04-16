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
    [Authorize]
    public class BiLiBiLiLoginController : Controller
    {

      
        private readonly ILogger<BiLiBiLiLoginController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
      
        /// <param name="logger"></param>
        public BiLiBiLiLoginController( ILogger<BiLiBiLiLoginController> logger)
        {
        
            _logger = logger;
        }


    }
}
