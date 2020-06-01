using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ReactJokes.Data
{
    public class Joke
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }        
        public string Type { get; set; }
        public string Setup { get; set; }
        public string Punchline{get;set;}

        public int UserId { get; set; }
        public List<UserLikedJokes> UserLikedJokes { get; set; }
    }
}
