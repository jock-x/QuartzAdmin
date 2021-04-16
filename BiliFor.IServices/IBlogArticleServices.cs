using BiliFor.IServices.BASE;
using BiliFor.Model.Models;
using BiliFor.Model.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiliFor.IServices
{
    public interface IBlogArticleServices :IBaseServices<BlogArticle>
    {
        Task<List<BlogArticle>> GetBlogs();
        Task<BlogViewModels> GetBlogDetails(int id);

    }

}
