using BiliFor.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using System;

namespace BiliFor.Extensions
{
    /// <summary>
    /// 任务调度 启动服务
    /// </summary>
    public static class JobSetup
    {
        public static void AddJobSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //services.AddHostedService<Job1TimedService>();
            //services.AddHostedService<Job2TimedService>();

            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddTransient<Job_Blogs_Quartz>();//Job使用瞬时依赖注入
            services.AddTransient<Job_OperateLog_Quartz>();//Job使用瞬时依赖注入

            services.AddTransient<Job_Billi_Quartz>();//Job使用瞬时依赖注入

            services.AddTransient<Job_CSDN_Quartz>();//Job使用瞬时依赖注入
            services.AddSingleton<ISchedulerCenter, SchedulerCenterServer>();
        }
    }
}
