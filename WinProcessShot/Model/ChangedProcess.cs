using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinProcessShot.Model
{
    public class ChangedProcess
    {
        public ProcessInfoObj? Before { get; set; }
        public ProcessInfoObj? After { get; set; }
    }
}
