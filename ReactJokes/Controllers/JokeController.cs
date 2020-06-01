using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReactJokes.Data;
using ReactJokes.ViewModels;

namespace ReactJokes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JokeController : ControllerBase
    {
        private string _connectionString;

        public JokeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [HttpGet]
        [Route("getjoke")]
        public Joke GetJoke()
        {
            var joke = JokeApi.GetJoke();
            var repo2 = new UserRepository(_connectionString);
            var user =  repo2.GetByEmail(User.Identity.Name);
            var repo = new JokeRepository(_connectionString);
            var wasliked = repo.UserLikedJoke(joke.Id, user.Id);
            if (!wasliked)
            {
                return joke;
            }
            return repo.GetJokeWithLikes(joke.Id);
        }
        [HttpGet]
        [Route("getalljokes")]
        public IEnumerable<Joke> GetAllJokes()
        {
            var repo = new JokeRepository(_connectionString);
            return repo.GetJokes();
        }
        [HttpPost]
        [Route("likejoke")]
        public void LikeJoke(LikeJokeViewModel vm)
        {
            var repo = new JokeRepository(_connectionString);
            var repo2 = new UserRepository(_connectionString);
            var user = repo2.GetByEmail(User.Identity.Name);
            vm.Joke.UserId = user.Id;
            var wasLiked = repo.UserLikedJoke(vm.Joke.Id, user.Id);
            if (!wasLiked)
            {
                repo.LikeJoke(vm.Joke,user.Id, vm.Liked);
                return;
            }
            repo.ChangeLikeStatusForJoke(vm.Joke, user.Id, vm.Liked);
            return;
        }
    }
}