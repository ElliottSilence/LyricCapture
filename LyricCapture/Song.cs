using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricCapture
{
    public class Song
    {
        String name;

        String singer;

        String lyric;

        String tlyric;

        Boolean noLyric;

        public Song()
        {
            NoLyric = false;
        }

        public string Name { get => name; set => name = value; }
        public string Singer { get => singer; set => singer = value; }
        public string Lyric { get => lyric; set => lyric = value; }
        public string Tlyric { get => tlyric; set => tlyric = value; }
        public bool NoLyric { get => noLyric; set => noLyric = value; }
    }
}
