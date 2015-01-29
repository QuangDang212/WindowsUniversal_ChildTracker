using System;
using System.Collections.Generic;
using System.Text;

namespace ChildTracker.Models
{
    public class SignUpModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
