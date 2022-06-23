using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Model
{
    public class CastDetailsModel
    {
        public CastDetailsModel()
        {
            movies = new List<MovieCastModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfilePath { get; set; }
        public string Gender { get; set; }

        public List<MovieCastModel> movies { get; set; }

    }
}
