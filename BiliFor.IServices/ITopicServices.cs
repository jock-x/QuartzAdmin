using BiliFor.IServices.BASE;
using BiliFor.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiliFor.IServices
{
    public interface ITopicServices : IBaseServices<Topic>
    {
        Task<List<Topic>> GetTopics();
    }
}
