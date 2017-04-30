using System.Collections.Generic;

namespace FProj.Api
{
    public class ResponsePage<T>
    {
        public List<T> Data { get; set; }
        public int Count { get; set; }
    }
}
