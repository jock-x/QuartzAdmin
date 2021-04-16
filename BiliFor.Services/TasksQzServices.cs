using BiliFor.IRepository.Base;
using BiliFor.IServices;
using BiliFor.Model.Models;
using BiliFor.Services.BASE;

namespace BiliFor.Services
{
    public partial class TasksQzServices : BaseServices<TasksQz>, ITasksQzServices
    {
        IBaseRepository<TasksQz> _dal;
        public TasksQzServices(IBaseRepository<TasksQz> dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

    }
}
                    