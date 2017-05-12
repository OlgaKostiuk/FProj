using System;
using System.Collections.Generic;

namespace FProj.Api
{
    public class FilmApi: IId
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public DateTime PremiereDate { get; set; }
        public DateTime DateCreated { get; set; }
        public double Rate { get; set; }
        public double Duration { get; set; }
        
        public UserApi User { get; set; }
        public ImageApi Poster { get; set; }
        public List<ImageApi> Pictures { get; set; }
        public List<GenreApi> Genres { get; set; }
    }
}