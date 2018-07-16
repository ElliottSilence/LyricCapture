using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LyricCapture
{
    class QQMusicParser : BaseParser
    {
        public QQMusicParser()
        {
            this.SetParserType(ParserType.QQ音乐);
        }

        public override bool CheckSong(string id)
        {
            String detail_url = "https://c.y.qq.com/v8/fcg-bin/fcg_play_single_song.fcg?songmid=" + id + "&tpl=yqq_song_detail&format=jsonp&callback=getOneSongInfoCallback&g_tk=5382&jsonpCallback=getOneSongInfoCallback&loginUin=0&hostUin=0&format=jsonp&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0";
            String detail_resp = GetQQMusicDetail(detail_url, id);
            try
            {
                if (detail_resp.StartsWith("getOneSongInfoCallback"))
                {
                    String jsonStr = detail_resp.Replace("getOneSongInfoCallback(", "");
                    jsonStr = jsonStr.Remove(jsonStr.Length - 1);
                    JObject jObject = JObject.Parse(jsonStr);
                    Object o = jObject["data"];
                    if (!"[]".Equals(o.ToString()))
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

        public override Song GetSong(string id)
        {
            DateTime dtFrom = new DateTime(1970, 1, 1, 8, 0, 0, 0, DateTimeKind.Local);
            long currentMillis = (DateTime.Now.ToLocalTime().Ticks - dtFrom.Ticks) / 10000;
            
            String detail_url = "https://c.y.qq.com/v8/fcg-bin/fcg_play_single_song.fcg?songmid=" + id + "&tpl=yqq_song_detail&format=jsonp&callback=getOneSongInfoCallback&g_tk=5381&jsonpCallback=getOneSongInfoCallback&loginUin=0&hostUin=0&format=jsonp&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0";
            //此处的id是数字id，可从detail中获取；refer是歌曲详情页（不是播放器）；获取到的歌词没有加base64编码。
            //不过从url名字上判断，这个url似乎要被下面的new取代，所以没有使用
            //String lyric_url = "https://c.y.qq.com/lyric/fcgi-bin/fcg_query_lyric.fcg?nobase64=1&musicid=" + id +  "&callback=jsonp1&g_tk=5381&jsonpCallback=jsonp1&loginUin=0&hostUin=0&format=jsonp&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0";
            String lyric_url = "https://c.y.qq.com/lyric/fcgi-bin/fcg_query_lyric_new.fcg?callback=MusicJsonCallback_lrc&pcachetime=" + currentMillis + "&songmid=" + id + "&g_tk=5381&jsonpCallback=MusicJsonCallback_lrc&loginUin=0&hostUin=0&format=jsonp&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0";
            String detail_resp = GetQQMusicDetail(detail_url, id);
            String lyric_resp = GetQQMusicLyric(lyric_url);

            Song song = new Song();
            song.Name = ParseSongName(detail_resp);
            song.Singer = ParseSinger(detail_resp);
            song.Lyric = ParseLyric(lyric_resp);
            if (song.Lyric.Contains("[00:00:00]此歌曲为没有填词的纯音乐，请您欣赏"))
            {
                song.Lyric = "此歌曲为没有填词的纯音乐，请您欣赏";
                song.NoLyric = true;
            }
            song.Tlyric = ParseTlyric(lyric_resp);
            return song;
        }

        private string GetQQMusicDetail(string detail_url, String songmid)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(detail_url);
            request.Timeout = 2000;
            request.Method = "GET";
            //设置请求header
            request.Accept = "*/*";
            request.Referer = "https://y.qq.com/n/yqq/song/" + songmid + ".html";
            request.Headers.Set("accept-encoding", "gzip, deflate, br");
            request.Headers.Set("accept-language", "zh-CN,zh;q=0.9");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

            try
            {
                String retString = "";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                //gzip解压
                if ("gzip".Equals(response.Headers.Get("content-encoding")))
                {
                    GZipStream gZipStream = new GZipStream(myResponseStream, CompressionMode.Decompress);
                    StreamReader myStreamReader = new StreamReader(gZipStream, Encoding.UTF8);
                    retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    gZipStream.Close();
                }
                else
                {
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                    retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                }
                myResponseStream.Close();

                Console.WriteLine(retString);

                return retString;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private string GetQQMusicLyric(string lyric_url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(lyric_url);
            request.Timeout = 2000;
            request.Method = "GET";
            //设置请求header
            request.Accept = "*/*";
            request.Referer = "https://y.qq.com/portal/player.html";
            request.Headers.Set("accept-encoding", "gzip, deflate, br");
            request.Headers.Set("accept-language", "zh-CN,zh;q=0.9");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

            try
            {
                String retString = "";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                //gzip解压
                if ("gzip".Equals(response.Headers.Get("content-encoding")))
                {
                    GZipStream gZipStream = new GZipStream(myResponseStream, CompressionMode.Decompress);
                    StreamReader myStreamReader = new StreamReader(gZipStream, Encoding.UTF8);
                    retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    gZipStream.Close();
                }
                else
                {
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                    retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                }
                myResponseStream.Close();

                Console.WriteLine(retString);

                return retString;
            }
            catch
            {
                return "";
            }
            
        }

        private string ParseSongName(string detail_resp)
        {
            try
            {
                if (detail_resp.StartsWith("getOneSongInfoCallback"))
                {
                    String jsonStr = detail_resp.Replace("getOneSongInfoCallback(", "");
                    jsonStr = jsonStr.Remove(jsonStr.Length - 1);
                    JObject jObject = JObject.Parse(jsonStr);
                    Object o = jObject["data"][0]["name"];
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

        private string ParseSinger(string detail_resp)
        {
            try
            {
                if (detail_resp.StartsWith("getOneSongInfoCallback"))
                {
                    String jsonStr = detail_resp.Replace("getOneSongInfoCallback(", "");
                    jsonStr = jsonStr.Remove(jsonStr.Length - 1);
                    JObject jObject = JObject.Parse(jsonStr);
                    Object o = jObject["data"][0]["singer"][0]["name"];
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

        private string ParseLyric(string lyric_resp)
        {
            try
            {
                if (lyric_resp.StartsWith("MusicJsonCallback_lrc"))
                {
                    String jsonStr = lyric_resp.Replace("MusicJsonCallback_lrc(", "");
                    jsonStr = jsonStr.Remove(jsonStr.Length - 1);
                    JObject jObject = JObject.Parse(jsonStr);
                    Object o = jObject["lyric"];
                    //base64解码
                    String lyric = Encoding.UTF8.GetString(Convert.FromBase64String(o.ToString()));
                    lyric = Regex.Replace(lyric, "\n", "\r\n");
                    return lyric;
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

        private string ParseTlyric(string lyric_resp)
        {
            try
            {
                if (lyric_resp.StartsWith("MusicJsonCallback_lrc"))
                {
                    String jsonStr = lyric_resp.Replace("MusicJsonCallback_lrc(", "");
                    jsonStr = jsonStr.Remove(jsonStr.Length - 1);
                    JObject jObject = JObject.Parse(jsonStr);
                    Object o = jObject["trans"];
                    //base64解码
                    String lyric = Encoding.UTF8.GetString(Convert.FromBase64String(o.ToString()));
                    lyric = Regex.Replace(lyric, "\n", "\r\n");
                    return lyric;
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

        public override bool GetSongMp3(string id, string filename)
        {
            try
            {
                String url = "";
                String guid = GetGUID();
                String vkey = GetVKEY(id, guid, out string realFileName);
                if (String.IsNullOrEmpty(vkey))
                {
                    return false;
                }
                if (!String.IsNullOrEmpty(realFileName))
                {
                    url = "http://dl.stream.qqmusic.qq.com/" + realFileName + "?vkey=" + vkey + "&guid=" + guid + "&uin=0&fromtag=66";
                    filename += realFileName.Substring(realFileName.LastIndexOf("."));
                }
                else
                {
                    url = "http://dl.stream.qqmusic.qq.com/C400" + id + ".m4a?vkey=" + vkey + "&guid=" + guid + "&uin=0&fromtag=66";
                }

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 2000;
                request.Method = "GET";
                //设置请求header
                //request.Accept = "*/*";
                //request.Referer = "https://y.qq.com/portal/player.html";
                //request.Headers.Set("accept-language", "zh-CN,zh;q=0.9");
                request.Headers.Set("accept-encoding", "identity;q=1, *;q=0");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
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
            catch
            {
                Console.WriteLine("GetSongMp3() Exception!");
                return false;
            }
        }

        private string GetGUID()
        {
            String guid = "";
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                guid += Convert.ToString(r.Next(10));
            }
            return guid;            
        }

        private string GetVKEY(string id, string guid, out string filename)
        {
            String url = "https://c.y.qq.com/base/fcgi-bin/fcg_music_express_mobile3.fcg?g_tk=5381&loginUin=0&hostUin=0&format=json&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0&cid=205361747&uin=0&songmid=" + id + "&filename=C400" + id + ".m4a&guid=" + guid;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 2000;
            request.Method = "GET";
            //设置请求header
            request.Accept = "*/*";
            request.Referer = "https://y.qq.com/portal/player.html";
            request.Headers.Set("accept-encoding", "gzip, deflate, br");
            request.Headers.Set("accept-language", "zh-CN,zh;q=0.9");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

            String retString = "";
            filename = "";
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                //gzip解压
                if ("gzip".Equals(response.Headers.Get("content-encoding")))
                {
                    GZipStream gZipStream = new GZipStream(myResponseStream, CompressionMode.Decompress);
                    StreamReader myStreamReader = new StreamReader(gZipStream, Encoding.UTF8);
                    retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    gZipStream.Close();
                }
                else
                {
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                    retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                }
                myResponseStream.Close();

                Console.WriteLine(retString);

                if (retString.StartsWith("MusicJsonCallback"))
                {
                    String jsonStr = retString.Remove(0, retString.IndexOf("(") + 1);
                    jsonStr = jsonStr.Remove(jsonStr.Length - 1);

                    JObject jObject = JObject.Parse(jsonStr);
                    String vkey = jObject["data"]["items"][0]["vkey"].ToString();
                    filename = jObject["data"]["items"][0]["filename"].ToString();
                    return vkey;
                }
                else if (retString.StartsWith("{") && retString.EndsWith("}"))
                {
                    JObject jObject = JObject.Parse(retString);
                    String vkey = jObject["data"]["items"][0]["vkey"].ToString();
                    filename = jObject["data"]["items"][0]["filename"].ToString();
                    return vkey;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }
    }
}
