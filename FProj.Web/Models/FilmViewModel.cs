using FProj.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FProj.Web
{
    public class FilmViewModel
    {
        public FilmApi Film { get; set; }
        public List<ActorApi> Actors { get; set; }
        public List<GenreApi> Genres { get; set; }
    }
}
