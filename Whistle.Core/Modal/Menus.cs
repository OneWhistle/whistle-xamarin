﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistle.Core.Modal
{
    public class Menus
    {
        public int Badges { get; set; }
        public string Title { get; set; }
        public int MenuIcon { get; set; }
    }
    public class USettings : Menus
    {
        public string Description { get; set; }
    }
}
