using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinProcessShot.Controller;

namespace WinProcessShot.Model
{
    public class Shot
    {
        public Dictionary<int, ProcessInfoObj> ProcessesByPid { get; set; }
        public DateTime TimeStamp { get; set; }

        public Shot() 
        { 
            TimeStamp = DateTime.Now;
            ProcessesByPid = ProcessesInfo.GetCurrentProcessesInfoByPid();
        }
    }
}
