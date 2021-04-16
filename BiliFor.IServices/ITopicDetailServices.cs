using BiliFor.IServices.BASE;
using BiliFor.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiliFor.IServices
{
    public interface ITopicDetailServices : IBaseServices<TopicDetail>
    {
        Task<List<TopicDetail>> GetTopicDetails();
    }
}
