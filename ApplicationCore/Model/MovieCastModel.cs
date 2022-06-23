using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Model
{
    public class MovieCastModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? PosterUrl { get; set; }
        public decimal? Rating { get; set; }
        public string? Overview { get; set; }
        public string? Tagline { get; set; }
        public decimal? Revenue { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? RunTime { get; set; }
        public string? Character { get; set; }

    }
}
