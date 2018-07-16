using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LyricCapture
{
    class CloudMusicParser: BaseParser
    {
        public CloudMusicParser()
        {
            this.SetParserType(ParserType.网易云);
        }

        public override Boolean CheckSong(String id)
        {
            String detail_url = "http://music.163.com/api/song/detail/?id=" + id + "&ids=[" + id + "]";
            String detail_jsonstr = HttpRequestGet(detail_url);
            try
            {
                JObject jObject = JObject.Parse(detail_jsonstr);
                if (Convert.ToInt32(jObject["code"]) == 200)
                {
                    if (!"[]".Equals(jObject["songs"].ToString()))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override Song GetSong(String id)
        {
            String detail_url = "http://music.163.com/api/song/detail/?id=" + id + "&ids=[" + id + "]";
            String lyric_url = "http://music.163.com/api/song/lyric?os=pc&id=" + id + "&lv=-1&kv=-1&tv=-1";
            String detail_jsonstr = HttpRequestGet(detail_url);
            String lyric_jsonstr = HttpRequestGet(lyric_url);

            Song song = new Song();
            song.Name = ParseSongName(detail_jsonstr);
            song.Singer = ParseSinger(detail_jsonstr);
            if (lyric_jsonstr.Contains("\"nolyric\""))
            {
                song.Lyric = "纯音乐，无歌词";
                song.NoLyric = true;
            }
            else
            {
                song.Lyric = ParseLyric(lyric_jsonstr);
                song.Tlyric = ParseTlyric(lyric_jsonstr);
            }
            
            return song;
        }

        public override Boolean GetSongMp3(String id, String filename)
        {
            String url = "http://music.163.com/song/media/outer/url?id=" + id + ".mp3";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 2000;
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(response.Headers.ToString());
            if (String.IsNullOrEmpty(response.GetResponseHeader("Content-Disposition")))
            {
                return false;
            }
            else
            {
                if (response.Headers.Get("Content-Type").Contains("audio/mpeg"))
                {
                    filename += ".mp3";
                }
                Stream myResponseStream = response.GetResponseStream();
                BufferedStream bs = new BufferedStream(myResponseStream);
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                FileStream fs = new FileStream(filename, FileMode.Create);
                int c = 0;
                byte[] b = new byte[4096];
                while (true)
                {
                    c = bs.Read(b, 0, b.Length);
                    if (c == 0)
                    {
                        break;
                    }
                    else
                    {
                        fs.Write(b, 0, c);
                    }
                }
                fs.Close();
                bs.Close();
                myResponseStream.Close();

                return true;
            }
            
        }

        private string ParseSongName(string detail_jsonstr)
        {
            try
            {
                JObject jObject = JObject.Parse(detail_jsonstr);
                Object o = jObject["songs"][0]["name"];
                if (o != null)
                {
                    return o.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        private string ParseSinger(string detail_jsonstr)
        {
            try
            {
                JObject jObject = JObject.Parse(detail_jsonstr);
                Object o = jObject["songs"][0]["artists"][0]["name"];
                if (o != null)
                {
                    return o.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        private string ParseLyric(string lyric_jsonstr)
        {
            if (!lyric_jsonstr.Contains("\"uncollected\""))
            {
                try
                {
                    JObject jObject = JObject.Parse(lyric_jsonstr);
                    Object o = jObject["lrc"]["lyric"];
                    if (o != null)
                    {
                        if (!"".Equals(o.ToString()))
                        {
                            String[] lyric_lines = Regex.Split(o.ToString(), "\\n");
                            return string.Join("\r\n", lyric_lines);
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
                catch (Exception)
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        private string ParseTlyric(string lyric_jsonstr)
        {
            if (!lyric_jsonstr.Contains("\"uncollected\""))
            {
                try
                {
                    JObject jObject = JObject.Parse(lyric_jsonstr);
                    Object o = jObject["tlyric"]["lyric"];
                    if (o != null)
                    {
                        if (!"".Equals(o.ToString()))
                        {
                            String[] lyric_lines = Regex.Split(o.ToString(), "\\n");
                            return string.Join("\r\n", lyric_lines);
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
                catch (Exception)
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}
