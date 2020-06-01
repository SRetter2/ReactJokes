using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactJokes.Data;

namespace ReactJokes.ViewModels
{
    public class SignupViewModel : User
    {
        public string Password { get; set; }
    }
}
