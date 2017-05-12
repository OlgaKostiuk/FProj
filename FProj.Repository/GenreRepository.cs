using FProj.Api;
using FProj.Data;
using FProj.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FProj.Repository
{
    public class GenreRepository : BaseRepository, IFetch<GenreApi>, ICrud<GenreApi>
    {
        public GenreRepository(FProjContext context): base(context)
        {
        }

        public GenreApi Default()
        {
            return new GenreApi();
        }
        public GenreApi Create(GenreApi model)
        {
            Genre genreData = ApiToData.GenreApiToData(model);
            try
            {
                _dbContext.Genre.Add(genreData);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return DataToApi.GenreToApi(genreData);
        }

        //public void AddFilmToGenre(GenreApi genre, FilmApi film)
        //{
        //    Genre genreData = _dbContext.Genre.FirstOrDefault(x => x.Id == genre.Id);
        //    Film filmData = _dbContext.Film.FirstOrDefault(x => x.Id == film.Id);
        //    if (genreData == null || filmData == null) return;
        //    genreData.Films.Add(filmData);
        //    _dbContext.SaveChanges();
        //}

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<GenreApi> GetAll(bool IsDeleted = false)
        {
            return _dbContext.Genre.ToList().Select(x => DataToApi.GenreToApi(x)).ToList();
        }

        public GenreApi GetById(int Id)
        {
            Genre genreData = _dbContext.Genre.FirstOrDefault(x => x.Id == Id);
            if (genreData == null) return null;
            return DataToApi.GenreToApi(genreData);
        }

        public GenreApi Update(GenreApi model)
        {
            Genre original = _dbContext.Genre.FirstOrDefault(x => x.Id == model.Id);
            if (original == null) return null;
            Genre newData = ApiToData.GenreApiToData(model);
            _dbContext.Entry(original).CurrentValues.SetValues(newData);
            _dbContext.SaveChanges();
            return DataToApi.GenreToApi(original);
        }
    }
}
