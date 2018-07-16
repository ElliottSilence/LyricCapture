using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricCapture
{
    interface IBaseParser
    {
        Boolean CheckSong(String id);

        Song GetSong(String id);

        Boolean GetSongMp3(String id, String filename);
    }
}
