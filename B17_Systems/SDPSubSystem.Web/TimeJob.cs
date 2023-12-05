using Quartz;
using SDPSubSystem.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDPSubSystem.Web
{
    public class TimeJob : IJob
    {

        public void Execute(IJobExecutionContext context)
        {
            //写具体的内容
            //System.IO.File.AppendAllText(@"c:\work\Quartz.txt", DateTime.Now + Environment.NewLine);

            //ESDAudit自动触发,发日报邮件
            //ESDAuditController.TestRecordDailyReport();

            //发失败未处理的邮件
            //ESDAuditController.TestRecordFailReport();

        }
    }
}