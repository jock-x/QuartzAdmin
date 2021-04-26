using BiliFor.IServices;
using BiliFor.Model.Bili;
using Quartz;
using System;

using System.Linq;
using System.Threading.Tasks;

namespace BiliFor.Tasks.QuartzNet.Jobs
{
    class Job_Billi_Quartz : JobBase, IJob
    {
        private readonly IBiliAccountService _biliaccountservice;
        private readonly ITasksQzServices _tasksQzServices;

        public Job_Billi_Quartz(IBiliAccountService biliaccountservice, ITasksQzServices tasksQzServices)
        {
            _biliaccountservice = biliaccountservice;
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

           
            string cookie = "CURRENT_FNVAL=80; _uuid=846FF8F2-C882-C1CD-808F-DEEA4719175585066infoc; blackside_state=1; rpdid=|(m)mJumk)R0J'uYuku|kmm|; buvid3=BB379260-46D0-407B-B31A-086227A133E1184999infoc; LIVE_BUVID=AUTO4416177595741756; fingerprint3=5bdd4f9e03267a2fa34c274964a20a2b; buvid_fp=BB379260-46D0-407B-B31A-086227A133E1184999infoc; bp_article_offset_4830365=510771353527463951; fingerprint_s=22fdbf4a2d532409728945361a3a8c15; bp_t_offset_4830365=514104505842011394; bp_video_offset_4830365=514160147147869440; PVID=1; bfe_id=1e33d9ad1cb29251013800c68af42315; fingerprint=bf201b8cab1033a1ec48ac66ca6093a4; buvid_fp_plain=3536A90E-676D-49F6-8195-3D36856858C4138393infoc; SESSDATA=524a2906,1634354147,94f35*41; bili_jct=884c71e8fcc5798e65461d34baaac988; DedeUserID=4830365; DedeUserID__ckMd5=3d3f94374f6dd343; sid=d3a6p9a7";
             BiliUserInfo apiResponse = _biliaccountservice.LoginByCookie(cookie);

            ////if (jobid > 0)
            ////{
            ////    var model = await _tasksQzServices.QueryById(jobid);
            ////    if (model != null)
            ////    {
            ////        model.RunTimes += 1;
            ////        var separator = "<br>";
            ////        model.Remark =
            ////            $"【{DateTime.Now}】执行任务【Id：{context.JobDetail.Key.Name}，组别：{context.JobDetail.Key.Group}】【执行成功】:博客数{list.Count}{separator}"
            ////            + string.Join(separator, StringHelper.GetTopDataBySeparator(model.Remark, separator, 9));

            ////        await _tasksQzServices.Update(model);
            ////    }
            ////}

            //await Console.Out.WriteLineAsync("博客总数量" + list.Count.ToString());
        }
    }
}
