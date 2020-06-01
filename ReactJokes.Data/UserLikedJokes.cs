using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ReactJokes.Data
{
    public class UserLikedJokes
    {
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int JokeId { get; set; }
        [JsonIgnore]
        public Joke Joke { get; set; }
        public DateTime DateLiked { get; set; }
        public bool Liked { get; set; }
    }
}
