namespace LyricCapture
{
    partial class Main_Form
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
            this.label_id = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.textBox_id = new System.Windows.Forms.TextBox();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.textBox_singer = new System.Windows.Forms.TextBox();
            this.label_singer = new System.Windows.Forms.Label();
            this.button_search = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.textBox_lyric = new System.Windows.Forms.TextBox();
            this.label_from = new System.Windows.Forms.Label();
            this.comboBox_from = new System.Windows.Forms.ComboBox();
            this.checkBox_timestamp = new System.Windows.Forms.CheckBox();
            this.label_transmode = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboBox_transmode = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label_id
            // 
            this.label_id.AutoSize = true;
            this.label_id.Font = new System.Drawing.Font("宋体", 10F);
            this.label_id.Location = new System.Drawing.Point(13, 14);
            this.label_id.Name = "label_id";
            this.label_id.Size = new System.Drawing.Size(63, 14);
            this.label_id.TabIndex = 0;
            this.label_id.Text = "歌曲id：";
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Font = new System.Drawing.Font("宋体", 10F);
            this.label_name.Location = new System.Drawing.Point(13, 41);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(49, 14);
            this.label_name.TabIndex = 1;
            this.label_name.Text = "歌名：";
            this.toolTip1.SetToolTip(this.label_name, "双击此处下载歌曲试听");
            this.label_name.DoubleClick += new System.EventHandler(this.label_name_DoubleClick);
            // 
            // textBox_id
            // 
            this.textBox_id.Location = new System.Drawing.Point(82, 12);
            this.textBox_id.Name = "textBox_id";
            this.textBox_id.Size = new System.Drawing.Size(193, 21);
            this.textBox_id.TabIndex = 2;
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(68, 39);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.ReadOnly = true;
            this.textBox_name.Size = new System.Drawing.Size(248, 21);
            this.textBox_name.TabIndex = 3;
            this.textBox_name.DoubleClick += new System.EventHandler(this.label_name_DoubleClick);
            // 
            // textBox_singer
            // 
            this.textBox_singer.Location = new System.Drawing.Point(68, 66);
            this.textBox_singer.Name = "textBox_singer";
            this.textBox_singer.ReadOnly = true;
            this.textBox_singer.Size = new System.Drawing.Size(140, 21);
            this.textBox_singer.TabIndex = 4;
            // 
            // label_singer
            // 
            this.label_singer.AutoSize = true;
            this.label_singer.Font = new System.Drawing.Font("宋体", 10F);
            this.label_singer.Location = new System.Drawing.Point(13, 68);
            this.label_singer.Name = "label_singer";
            this.label_singer.Size = new System.Drawing.Size(49, 14);
            this.label_singer.TabIndex = 5;
            this.label_singer.Text = "歌手：";
            // 
            // button_search
            // 
            this.button_search.Font = new System.Drawing.Font("宋体", 10F);
            this.button_search.Location = new System.Drawing.Point(403, 14);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(73, 73);
            this.button_search.TabIndex = 0;
            this.button_search.Text = "查找";
            this.button_search.UseVisualStyleBackColor = true;
            this.button_search.Click += new System.EventHandler(this.button_search_Click);
            // 
            // button_save
            // 
            this.button_save.Font = new System.Drawing.Font("宋体", 10F);
            this.button_save.Location = new System.Drawing.Point(482, 14);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(73, 73);
            this.button_save.TabIndex = 7;
            this.button_save.Text = "保存";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // textBox_lyric
            // 
            this.textBox_lyric.Location = new System.Drawing.Point(16, 93);
            this.textBox_lyric.Multiline = true;
            this.textBox_lyric.Name = "textBox_lyric";
            this.textBox_lyric.ReadOnly = true;
            this.textBox_lyric.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_lyric.Size = new System.Drawing.Size(539, 313);
            this.textBox_lyric.TabIndex = 8;
            this.textBox_lyric.WordWrap = false;
            // 
            // label_from
            // 
            this.label_from.AutoSize = true;
            this.label_from.Font = new System.Drawing.Font("宋体", 10F);
            this.label_from.Location = new System.Drawing.Point(281, 18);
            this.label_from.Name = "label_from";
            this.label_from.Size = new System.Drawing.Size(35, 14);
            this.label_from.TabIndex = 9;
            this.label_from.Text = "来源";
            // 
            // comboBox_from
            // 
            this.comboBox_from.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_from.FormattingEnabled = true;
            this.comboBox_from.Location = new System.Drawing.Point(322, 14);
            this.comboBox_from.Name = "comboBox_from";
            this.comboBox_from.Size = new System.Drawing.Size(75, 20);
            this.comboBox_from.TabIndex = 10;
            this.comboBox_from.SelectedIndexChanged += new System.EventHandler(this.comboBox_from_Changed);
            // 
            // checkBox_timestamp
            // 
            this.checkBox_timestamp.AutoSize = true;
            this.checkBox_timestamp.Checked = true;
            this.checkBox_timestamp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_timestamp.Font = new System.Drawing.Font("宋体", 10F);
            this.checkBox_timestamp.Location = new System.Drawing.Point(322, 41);
            this.checkBox_timestamp.Name = "checkBox_timestamp";
            this.checkBox_timestamp.Size = new System.Drawing.Size(68, 18);
            this.checkBox_timestamp.TabIndex = 11;
            this.checkBox_timestamp.Text = "时间戳";
            this.toolTip1.SetToolTip(this.checkBox_timestamp, "勾选后每行歌词前显示时间戳");
            this.checkBox_timestamp.UseVisualStyleBackColor = true;
            this.checkBox_timestamp.CheckedChanged += new System.EventHandler(this.checkBox_timestamp_Changed);
            // 
            // label_transmode
            // 
            this.label_transmode.AutoSize = true;
            this.label_transmode.Font = new System.Drawing.Font("宋体", 10F);
            this.label_transmode.Location = new System.Drawing.Point(214, 68);
            this.label_transmode.Name = "label_transmode";
            this.label_transmode.Size = new System.Drawing.Size(119, 14);
            this.label_transmode.TabIndex = 12;
            this.label_transmode.Text = "翻译歌词显示模式";
            this.toolTip1.SetToolTip(this.label_transmode, "*当有翻译歌词时此选项生效\r\n模式1：\r\n    翻译显示在每行歌词原文下方\r\n模式2：\r\n    翻译和歌词原文在同一行\r\n模式3：\r\n    整段歌词原文结束" +
        "后显示整段翻译");
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 200;
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 40;
            // 
            // comboBox_transmode
            // 
            this.comboBox_transmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_transmode.Font = new System.Drawing.Font("宋体", 10F);
            this.comboBox_transmode.FormattingEnabled = true;
            this.comboBox_transmode.Items.AddRange(new object[] {
            "模式1",
            "模式2",
            "模式3"});
            this.comboBox_transmode.Location = new System.Drawing.Point(339, 65);
            this.comboBox_transmode.Name = "comboBox_transmode";
            this.comboBox_transmode.Size = new System.Drawing.Size(58, 21);
            this.comboBox_transmode.TabIndex = 13;
            this.comboBox_transmode.SelectedIndexChanged += new System.EventHandler(this.comboBox_transmode_Changed);
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 421);
            this.Controls.Add(this.comboBox_transmode);
            this.Controls.Add(this.label_transmode);
            this.Controls.Add(this.checkBox_timestamp);
            this.Controls.Add(this.comboBox_from);
            this.Controls.Add(this.label_from);
            this.Controls.Add(this.textBox_lyric);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.button_search);
            this.Controls.Add(this.label_singer);
            this.Controls.Add(this.textBox_singer);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.textBox_id);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.label_id);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main_Form";
            this.Text = "查找歌词";
            this.Load += new System.EventHandler(this.Main_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_id;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.TextBox textBox_id;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.TextBox textBox_singer;
        private System.Windows.Forms.Label label_singer;
        private System.Windows.Forms.Button button_search;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.TextBox textBox_lyric;
        private System.Windows.Forms.Label label_from;
        private System.Windows.Forms.ComboBox comboBox_from;
        private System.Windows.Forms.CheckBox checkBox_timestamp;
        private System.Windows.Forms.Label label_transmode;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox comboBox_transmode;
    }
}

