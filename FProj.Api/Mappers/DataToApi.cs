using FProj.Data;
using System.Linq;
using System.Web.Configuration;

namespace FProj.Api
{
    public static class DataToApi
    {
        public static FilmApi FilmToApi(Film film)
        {
            return new FilmApi()
            {
                Description = film.Description,
                Director = film.Director,
                Duration = film.Duration ?? 0,
                Id = film.Id,
                PremiereDate = film.PremiereDate,
                DateCreated = film.DateCreated,
                Title = film.Title,
                Rate = film.Rate,
                User = UserToApi(film.UserCreator),
                Poster = ImageToApi(film.Images.FirstOrDefault(x => !x.IsDeleted && x.IsPoster)),
                Pictures = film.Images?.Where(x => !x.IsPoster && !x.IsDeleted).Select(x => ImageToApi(x)).ToList(),
                Genres = film.Genres?.Select(x => GenreToApi(x)).ToList()
            };
        }

        public static UserApi UserToApi(User user)
        {
            return new UserApi()
            {
                Email = user.UserAccount.Email,
                FirstName = user.FirtsName,
                Id = user.Id,
                LastName = user.LastName,
                Login = user.Login
            };
        }

        public static ImageApi ImageToApi(Image image)
        {
            if (image == null) return null;

            return new ImageApi()
            {
                Id = image.Id,
                Path = WebConfigurationManager.AppSettings["ImageFolder"] + image.Name
            };
        }

        public static ActorApi ActorToApi(Actor actor)
        {
            return new ActorApi()
            {
                Id = actor.Id,
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                Films = actor.Films.Select(x => FilmToApi(x))
            };
        }

        public static CommentApi CommentToApi(Comment comment)
        {
            return new CommentApi()
            {
                DateCreated = comment.DateCreated,
                Id = comment.Id,
                Text = comment.Text,
                User = UserToApi(comment.User)
            };
        }

        public static GenreApi GenreToApi(Genre genre)
        {
            return new GenreApi()
            {
                Description = genre.Description,
                Id = genre.Id,
                Title = genre.Title
            };
        }
    }
}