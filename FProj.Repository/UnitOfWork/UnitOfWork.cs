using FProj.Data;

namespace FProj.Repository
{
    public class UnitOfWork
    {
        private readonly FProjContext _context;

        private static UnitOfWork _instance;
        public static UnitOfWork Instance => _instance ?? (_instance = new UnitOfWork());

        private UnitOfWork()
        {
            _context = new FProjContext();
        }

        private FilmRepository _filmRepository;
        public FilmRepository FilmRepository => _filmRepository ?? (_filmRepository = new FilmRepository(_context));

        private ImageRepository _imageRepository;
        public ImageRepository ImageRepository => _imageRepository ?? (_imageRepository = new ImageRepository(_context));

        private UserRepository _userRepository;
        public UserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_context));

        private GenreRepository _genreRepository;
        public GenreRepository GenreRepository => _genreRepository ?? (_genreRepository = new GenreRepository(_context));
    }
}
