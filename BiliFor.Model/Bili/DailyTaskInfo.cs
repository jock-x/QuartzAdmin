using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliFor.Model.Bili
{
    public class DailyTaskInfo
    {
        public bool Login { get; set; }

        public bool Watch { get; set; }

        public long Coins { get; set; }

        public bool Share { get; set; }

        public bool Email { get; set; }

        public bool Tel { get; set; }

        public bool Safe_question { get; set; }

        public bool Identify_card { get; set; }
    }
}
