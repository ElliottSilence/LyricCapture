using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LyricCapture
{
    public partial class Main_Form : Form
    {
        String id;

        BaseParser parser;

        Song song;

        public Main_Form()
        {
            InitializeComponent();
        }


        private void button_search_Click(object sender, EventArgs e)
        {
            id = textBox_id.Text;
            parser = CreateParser();
            if (parser == null)
            {
                MessageBox.Show(this, "暂不支持该来源的歌词获取", "提示");
            }
            else
            {
                if (parser.CheckSong(id))
                {
                    song = parser.GetSong(id);
                    ShowLyric();
                    ActiveTextBox_lyric();
                }
                else
                {
                    MessageBox.Show(this, "该歌曲不存在，请输入正确的" + parser.ToString() + "id！", "歌曲id错误");
                    ActiveTextBox_id();
                }
            }
        }

        private void label_name_DoubleClick(object sender, EventArgs e)
        {
            String current_id = textBox_id.Text;
            if (parser != null && song != null && current_id == id)
            {
                SaveMp3(current_id);
            }
            else
            {
                parser = CreateParser();
                if (parser == null)
                {
                    MessageBox.Show(this, "暂不支持该来源的试听下载", "提示");
                }
                else
                {
                    if (parser.CheckSong(current_id))
                    {
                        song = parser.GetSong(current_id);
                        ShowLyric();
                        SaveMp3(current_id);
                    }
                    else
                    {
                        MessageBox.Show(this, "该歌曲不存在，请输入正确的" + parser.ToString() + "id！", "歌曲id错误");
                        ActiveTextBox_id();
                    }
                }
            }
        }

        private void SaveMp3(String id)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存试听";
            //设置初始路径是桌面
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //设置初始文件名
            saveFileDialog.FileName = song.Singer + " - " + song.Name;
            //设置文件类型列表
            //saveFileDialog.Filter = "mp3文件(*.mp3)|*.mp3|m4a文件(*.m4a)|*.m4a";
            saveFileDialog.Filter = "所有文件(*.*)|*.*";

            //if (parser.GetParserType() == ParserType.CloudMusic)
            //{
            //    saveFileDialog.FilterIndex = 1;
            //}
            //else if (parser.GetParserType() == ParserType.QQMusic)
            //{
            //    saveFileDialog.FilterIndex = 2;
            //}

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (parser.GetSongMp3(id, saveFileDialog.FileName))
                {
                    MessageBox.Show(this, "试听保存成功！", "提示");
                }
                else
                {
                    MessageBox.Show(this, "该歌曲不支持试听，保存失败！", "提示");
                }
            }
        }

        private void ShowLyric()
        {
            textBox_name.Text = song.Name;
            textBox_singer.Text = song.Singer;
            if (!String.IsNullOrEmpty(song.Lyric))
            {
                textBox_lyric.Text = constructLyric();
            }
            else
            {
                textBox_lyric.Text = "该歌曲尚未上传歌词";
            }
        }

        private string constructLyric()
        {
            int transMode = comboBox_transmode.SelectedIndex;
            bool displayTimestamp = checkBox_timestamp.Checked;

            Regex regex = new Regex("\\[\\d{2}:\\d{2}.\\d{2,3}\\]");
            String lyric = "";
            //如果有翻译歌词，设置显示模式
            if (!String.IsNullOrEmpty(song.Tlyric))
            {
                if (transMode == 0 || transMode == 1)
                {
                    String[] lyrics = Regex.Split(song.Lyric, "\r\n");
                    String[] tlyrics = Regex.Split(song.Tlyric, "\r\n");
                    
                    foreach (String line in lyrics)
                    {
                        if (!regex.IsMatch(line))
                        {
                            lyric += line + "\r\n";
                        }
                        else
                        {
                            string timestamp = regex.Match(line).Value;
                            string trans = SearchTransFromTlyric(timestamp);
                            //如果未找到相同时间戳歌词，或原文和译文相同，则使用原文
                            if (trans == "" || line.Trim() == trans.Trim())
                            {
                                lyric += line + "\r\n";
                            }
                            else
                            {
                                if (transMode == 0)
                                {
                                    lyric += line + "\r\n";
                                    lyric += trans + "\r\n";
                                }
                                else
                                {
                                    lyric += line + " " + trans.Replace(timestamp, "").Trim() + "\r\n";
                                }
                            }
                        }
                    }
                }
                else
                {
                    lyric = song.Lyric + song.Tlyric;
                }
            }
            else
            {
                lyric = song.Lyric;
            }
            //是否显示时间戳
            if (displayTimestamp)
            {
                return lyric;
            }
            else
            {
                return regex.Replace(lyric, "");
            }
            
        }

        //查找并返回具有相同时间戳的歌词
        private string SearchTransFromTlyric(string timestamp)
        {
            String[] tlyrics = Regex.Split(song.Tlyric, "\r\n");
            foreach (string tlyric in tlyrics)
            {
                if (tlyric.Contains(timestamp))
                {
                    return tlyric;
                }
            }
            return "";
        }

        private BaseParser CreateParser()
        {
            
            ParserType pt = (ParserType)comboBox_from.SelectedIndex;
            switch (pt)
            {
                case ParserType.网易云:
                    return new CloudMusicParser();
                case ParserType.QQ音乐:
                    return new QQMusicParser();
            }
            return null;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (song != null && song.Lyric != "" && !song.NoLyric)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "保存歌词";
                //设置初始路径是桌面
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                //设置初始文件名
                saveFileDialog.FileName = song.Singer + " - " + song.Name;
                //设置文件类型列表
                saveFileDialog.Filter = "lrc文件(*.lrc)|*.lrc|txt文件(*.txt)|*.txt";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = File.CreateText(saveFileDialog.FileName);
                    sw.Write(textBox_lyric.Text);
                    sw.Flush();
                    sw.Close();
                    MessageBox.Show(this, "保存成功！", "提示");
                }
            }
            else if (song != null && song.NoLyric){
                MessageBox.Show(this, "纯音乐，无歌词！", "无需保存");
            }
            else
            {
                MessageBox.Show(this, "请查找到歌词后再选择保存！", "未找到歌词");
                ActiveTextBox_id();
            }
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            comboBox_from.Items.AddRange(Enum.GetNames(typeof(ParserType)));
            //来源默认显示“网易云”
            comboBox_from.SelectedIndex = 0;
            //翻译歌词显示模式默认“模式1”
            comboBox_transmode.SelectedIndex = 0;
            //网易云id
            textBox_id.Text = "550469718";
            //QQ音乐id
            //textBox_id.Text = "000dMwsh3E6OWe";
        }

        private void comboBox_from_Changed(object sender, EventArgs e)
        {
            ActiveTextBox_id();
        }

        private void comboBox_transmode_Changed(object sender, EventArgs e)
        {
            if (song != null && song.Lyric != "" && song.Tlyric != "")
            {
                textBox_lyric.Text = constructLyric();
            }
            ActiveTextBox_lyric();
        }

        private void checkBox_timestamp_Changed(object sender, EventArgs e)
        {
            if (song != null && song.Lyric != "")
            {
                textBox_lyric.Text = constructLyric();
            }
            ActiveTextBox_lyric();
        }

        private void ActiveTextBox_lyric()
        {
            textBox_lyric.Focus();
            textBox_lyric.Select(0, 0);
        }

        private void ActiveTextBox_id()
        {
            textBox_id.Focus();
            textBox_id.SelectAll();
        }
    }
}
