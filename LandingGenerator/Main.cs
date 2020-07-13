using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LandingGenerator
{
    public partial class Main : Form
    {
        private object _lock = new object();
        public const int IA_MAX_VARIATIONS = 8;

        public enum TimerStates { Unknow, Image, Video, Text }
        public volatile TimerStates CurrentTimerState;

        public volatile string MAIN_URL = "https://www.google.com/search?q=";

        public volatile string VIDEO_SEARCH_PARAMS = "&tbm=vid";

        public string SelectedTheme { get; set; }


        public Main()
        {
            InitializeComponent();
            ThreadPool.SetMaxThreads(4, 4);
        }

        [MTAThread]
        private async void btnStartSync_Click(object sender, EventArgs e)
        {
            SelectedTheme = txtTheme.Text;
            new ImageDownloader(this);
            new TextDownloader(this, listSections.Items.Cast<string>().ToList());
            await Task.CompletedTask;
        }        

        public string GetExtentionFromUrl(string url)
        {
            var extention = url.Substring(url.LastIndexOf("."), url.Length - url.LastIndexOf("."));
            if (extention.All(k => !Path.GetInvalidFileNameChars().Contains(k)))
            {
                return url.Substring(url.LastIndexOf("."), url.Length - url.LastIndexOf("."));
            }
            else
            {
                return ".jpg";
            }            
        }        

        protected int GetRandomNumber(int min, int max)
        {
            lock(_lock)
            {
                return new Random().Next(min, max);
            }
        }

        private void AddSection_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtSectionName.Text) && !listSections.Items.Contains(txtSectionName.Text))
            {
                listSections.Items.Add(txtSectionName.Text);
                txtSectionName.Text = string.Empty;
            }
        }

        private void RemoveSection_Click(object sender, EventArgs e)
        {
            if (listSections.SelectedIndex != -1)
            {
                listSections.Items.Remove(listSections.SelectedItem);
            }
        }
    }
}
