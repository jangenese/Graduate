﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graduate.Core.Data.Models;

namespace Graduate.Core.View.Model
{
   public class ClassView : ViewBase
    {
        public IList<Activity> children;
        public ClassView() { }

    }
}
