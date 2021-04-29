using BiliFor.IServices;
using Quartz;
using System;
using System.Threading.Tasks;

namespace BiliFor.Tasks
{
    public class Job_CSDN_Quartz : JobBase, IJob
    {
        readonly ICSDNSignServices _csdnsignservices;
        private readonly ITasksQzServices _tasksQzServices;

        public Job_CSDN_Quartz(ICSDNSignServices csdnsignservices, ITasksQzServices tasksQzServices)
        {
            _csdnsignservices = csdnsignservices;
            _tasksQzServices = tasksQzServices;
        }
        public async Task Execute(IJobExecutionContext context)
        {

            //var param = context.MergedJobDataMap;
            // 可以直接获取 JobDetail 的值
            var jobKey = context.JobDetail.Key;
            var jobId = jobKey.Name;

            var executeLog = await ExecuteJob(context, async () => await Run(context, jobId.ObjToInt()));

            // 也可以通过数据库配置，获取传递过来的参数
            JobDataMap data = context.JobDetail.JobDataMap;
            //int jobId = data.GetInt("JobParam");

            //var model = await _tasksQzServices.QueryById(jobId);
            //if (model != null)
            //{
            //    model.RunTimes += 1;
            //    model.Remark += $"{executeLog}<br />";
            //    await _tasksQzServices.Update(model);
            //}

        }
        public async Task Run(IJobExecutionContext context, int jobid)
        {
            string csdncookie = "uuid_tt_dd=10_36575270660-1607065853983-192011; UN=u010840685; p_uid=U010000; Hm_ct_6bcd52f51e9b3dce32bec4a3997715ac=6525*1*10_36575270660-1607065853983-192011!5744*1*u010840685; UserName=u010840685; UserInfo=2e1b0c3535df4361b38c567d8878a374; UserToken=2e1b0c3535df4361b38c567d8878a374; UserNick=%E6%AC%B2%E6%80%9D; AU=509; BT=1616394156533; Hm_up_6bcd52f51e9b3dce32bec4a3997715ac=%7B%22islogin%22%3A%7B%22value%22%3A%221%22%2C%22scope%22%3A1%7D%2C%22isonline%22%3A%7B%22value%22%3A%221%22%2C%22scope%22%3A1%7D%2C%22isvip%22%3A%7B%22value%22%3A%220%22%2C%22scope%22%3A1%7D%2C%22uid_%22%3A%7B%22value%22%3A%22u010840685%22%2C%22scope%22%3A1%7D%7D; ssxmod_itna=eqmx0D9Dc73iqxQq0dy7tigYY5APh0xqxqGXxpoDZDiqAPGhDC8ScxD5wEq4KCDy7+xpxCtGRGtW5=7+Tehtyr74GLDmKDy+W6eGGIxBYDQxAYDGDDpXD84DrAxYPG0DiKGRDlIFcDAf=Dbx=2DitSDDUF04G2D7tnzqL42wrDAd+yK7DnD0t5xBdPDcDniQnr=YiTeTNZDBQD7qNnDYo67eDHB2xTeO4f0O+YlPvY0hDG0xfbCY4PbIDei7vYQiOtD8DqQB+d9gkDG3PG2iD===; ssxmod_itna2=eqmx0D9Dc73iqxQq0dy7tigYY5APh0xqxA6b5P4D/iQCDFOYtpcDID5BIcxBM4ZmxwufCrm2qOUQLFIjHq7t+=qrXw0B=DDt0iYd+TIDgSeEyIrWYqYXiRlr0lD9YaW407KFx7=D+OGDD===; __gads=ID=0de619a196a7e516-22ce28af7ec700a4:T=1619073599:RT=1619073599:S=ALNI_MZMiIamo3GoRDRsFpW7RQfIW1wX5g; c_hasSub=true; dc_session_id=10_1619500073751.798823; dc_sid=01fedb8452acf1f00413c34dec3683bd; announcement-new=%7B%22isLogin%22%3Atrue%2C%22announcementUrl%22%3A%22https%3A%2F%2Fblog.csdn.net%2Fblogdevteam%2Farticle%2Fdetails%2F112280974%3Futm_source%3Dgonggao_0107%22%2C%22announcementCount%22%3A0%2C%22announcementExpire%22%3A3600000%7D; c_first_ref=github.com; c_first_page=https%3A//blog.csdn.net/; c_segment=11; Hm_lvt_6bcd52f51e9b3dce32bec4a3997715ac=1619428881,1619430562,1619430574,1619500082; c_ref=https%3A//mp.csdn.net/console/home%3Fspm%3D1001.2100.3001.4503; c_page_id=default; log_Id_click=94; c_pref=https%3A//mp.csdn.net/console/home%3Fspm%3D1001.2100.3001.4503; log_Id_view=739; Hm_lpvt_6bcd52f51e9b3dce32bec4a3997715ac=1619500342; dc_tos=qs7igo; log_Id_pv=427";
            string csdnsign=_csdnsignservices.CSDNSign(csdncookie);

            if (jobid > 0)
            {
                var model = await _tasksQzServices.QueryById(jobid);
                if (model != null)
                {
                    model.RunTimes += 1;
                    var separator = "<br>";
                    model.Remark =
                        $"【{DateTime.Now}】执行任务【Id：{context.JobDetail.Key.Name}，组别：{context.JobDetail.Key.Group}】【执行成功】:{csdnsign}{separator}";

                    await _tasksQzServices.Update(model);
                }
            }

            //await Console.Out.WriteLineAsync("博客总数量" + list.Count.ToString());
        }
    }
}
