using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.shared
{
    public class ActionResult
    {
        public Status Status { get; set; }
        public string Msg { get; set; }
        public object Value { get; set; }
    }

    public enum Status
    {
        Success,
        Fail
    }
}
