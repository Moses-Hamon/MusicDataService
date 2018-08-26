using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicDataService.Model
{
    public class Music
    {
        public long Id { get; set; }
        public string Track { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string Duration { get; set; }
    }
}