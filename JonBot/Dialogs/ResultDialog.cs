using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JonBot.Dialogs
{
    public class ResultDialog
    {
        public object Obj { get; set; }

        public bool Wait { get; set; }

        public Activity Activity { get; set; }
    }
}