﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduate.Core.View.Model
{
   public class ViewBase
    {
        public int id { get; set; }
        public int fid { get; set; }
        public String label { get; set; }
        public String credits { get; set; }
        public String status { get; set; }
        public String statusLong { get; set; }
        public String percentGrade { get; set; }
        public String gpaGrade { get; set; }
        public String letterGrade { get; set; }
        public String parentLabel { get; set; }

        public ViewBase() { }
    }
}
