using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ReactJokes.Data
{
    public class JokeRepository
    {
        private string _connectionString;

        public JokeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        
        public IEnumerable<Joke> GetJokes()
        {
            using (var cxt = new JokeContext(_connectionString))
            {
                return cxt.Jokes
                    .Include(j=> j.UserLikedJokes)
                    .ToList();
            }
        }
        public Joke GetJokeWithLikes(int jokeId)
        {
            using (var cxt = new JokeContext(_connectionString))
            {
                return cxt.Jokes
                    .Include(j => j.UserLikedJokes)
                    .FirstOrDefault(j => j.Id == jokeId);  
            }
        }
        public bool UserLikedJoke(int jokeId, int userId)
        {
            using(var cxt = new JokeContext(_connectionString))
            { 
                var result = cxt.UserLikedJokes.FirstOrDefault(ulj => ulj.UserId == userId && ulj.JokeId == jokeId);
                if(result == null)
                {
                    return false;
                }
                return true;
            }
        }
        public void LikeJoke(Joke joke, int userId, bool liked)
        {
            using (var cxt = new JokeContext(_connectionString))
            {               
                var ulj = new UserLikedJokes();
                ulj.JokeId = joke.Id;
                ulj.UserId = userId;
                ulj.Liked = liked;
                ulj.DateLiked = DateTime.Now;
                cxt.UserLikedJokes.Add(ulj);
                if (!cxt.Jokes.Contains(joke))
                {
                    cxt.Jokes.Add(joke);
                }                
                cxt.SaveChanges();
            }
        }
        public void ChangeLikeStatusForJoke(Joke joke, int userId, bool liked)
        {
            using(var cxt = new JokeContext(_connectionString))
            {
                var result = cxt.Jokes.Where(j => j.Id == joke.Id).FirstOrDefault(j => j.UserId == userId);
                cxt.Database.ExecuteSqlCommand("UPDATE UserLikedJokes SET Liked = @liked WHERE JokeId = @jokeId And UserId = @userId",
                    new SqlParameter("@liked", liked),
                    new SqlParameter("@jokeId", result.Id),
                     new SqlParameter("@userId", result.UserId));
            }
        }
    }
}
