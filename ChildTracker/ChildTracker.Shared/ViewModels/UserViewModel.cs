﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChildTracker.ViewModels
{
    public class UserViewModel:ViewModelBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
