using BiliFor.IServices.BASE;
using BiliFor.Model;
using BiliFor.Model.Models;
using System.Threading.Tasks;

namespace BiliFor.IServices
{
    public partial interface IGuestbookServices : IBaseServices<Guestbook>
    {
        Task<MessageModel<string>> TestTranInRepository();
        Task<bool> TestTranInRepositoryAOP();
    }
}
