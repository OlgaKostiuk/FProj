using System;
using FProj.Repository.Base;
using FProj.Data;
using System.Collections.Generic;
using System.Linq;
using FProj.Api;

namespace FProj.Repository
{
    public class FilmRepository : BaseRepository, IFetch<FilmApi>, ICrud<FilmApi>
    {
        public FilmRepository(FProjContext context) : base(context)
        {
        }

        public FilmApi Default()
        {
            return new FilmApi();
        }

        public FilmApi Create(FilmApi model)
        {
            model.User = DataToApi.UserToApi(_dbContext.User.FirstOrDefault());
            var filmData = ApiToData.FilmApiToData(model);
            try
            {
                _dbContext.Film.Add(filmData);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return DataToApi.FilmToApi(filmData);
        }

        public void Delete(int Id)
        {
            var film = _dbContext.Film.FirstOrDefault(x => x.Id == Id);

            if (film == null) return;

            film.IsDeleted = true;
            _dbContext.SaveChanges();
        }

        public List<FilmApi> GetAll(bool IsDeleted = false)
        {
            var filmData = _dbContext.Film.Where(x => x.IsDeleted == IsDeleted).ToList();
            var filmApi = filmData.Select(x => DataToApi.FilmToApi(x));
            return filmApi.ToList();
        }

        public FilmApi GetById(int Id)
        {
            var filmData = _dbContext.Film.FirstOrDefault(x => x.Id == Id);

            if (filmData == null) return null;

            return DataToApi.FilmToApi(filmData);
        }

        public FilmApi Update(FilmApi model)
        {
            var original = _dbContext.Film.FirstOrDefault(x => x.Id == model.Id);
            if (original == null) return null;

            var newData = ApiToData.FilmApiToData(model);

            _dbContext.Entry(original).CurrentValues.SetValues(newData);
            _dbContext.SaveChanges();

            return DataToApi.FilmToApi(newData);
        }

        public ResponsePage<FilmApi> GetPage(int page, int size = 5)
        {
            var filmApi = _dbContext.Film.OrderByDescending(x => x.DateCreated)
                .Where(x => !x.IsDeleted)
                .Skip((page - 1) * size)
                .Take(size)
                .ToList()
                .Select(DataToApi.FilmToApi)
                .ToList();

            int count = _dbContext.Film.Count(x => !x.IsDeleted) % size == 0
                ? _dbContext.Film.Count(x => !x.IsDeleted) / size
                : _dbContext.Film.Count(x => !x.IsDeleted) / size + 1;

            return new ResponsePage<FilmApi>()
            {
                Data = filmApi,
                Count = count
            };
        }
    }
}
