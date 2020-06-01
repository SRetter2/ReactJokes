﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ReactJokes.Data
{
    public class User
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public List<Joke> Jokes { get; set; }
        public List<UserLikedJokes> UserLikedJokes { get; set; }

    }
}
