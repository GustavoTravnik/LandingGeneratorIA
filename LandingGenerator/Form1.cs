using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LandingGenerator
{
    public partial class Form1 : Form
    {
        private object _lock = new object();
        const int IA_MAX_VARIATIONS = 5;

        protected enum TimerStates { Unknow, Image, Video, Text }
        private volatile TimerStates CurrentTimerState;

        private volatile string imageAltCheck;

        private volatile string MAIN_URL = "https://www.google.com/search?q=";
        private volatile string IMAGE_SEARCH_PARAMS = "&tbm=isch";
        private volatile string VIDEO_SEARCH_PARAMS = "&tbm=vid";

        List<string> imageUrlsToDownload = new List<string>();


        public Form1()
        {
            InitializeComponent();
            ThreadPool.SetMaxThreads(4, 4);
            ConfigureWebBrowser();
        }

        private void ConfigureWebBrowser()
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.WebBrowserShortcutsEnabled = false;
        }

        [MTAThread]
        private async void btnStartSync_Click(object sender, EventArgs e)
        {
            await DownloadRelatedImages();
            await Task.CompletedTask;
        }

        private async Task DownloadRelatedImages()
        {
            await StartLoadBrowserFromUrl(MAIN_URL + txtTheme.Text + IMAGE_SEARCH_PARAMS);
            System.Windows.Forms.Timer SyncThreadTimer = new System.Windows.Forms.Timer()
            {
                Interval = 1000,
                Enabled = false               
            };
            SyncThreadTimer.Tick += async(object sender, EventArgs e) =>
            {
                if (CustomSemaphore.IsFree)
                {
                    SyncThreadTimer.Enabled = false;

                    var imgDivs = webBrowser1.Document.GetElementsByTagName("div")
                        .Cast<HtmlElement>()
                        .Where(k => k.GetAttribute("className").Contains("isv-r"))
                        .Take(IA_MAX_VARIATIONS)
                        .ToList();
                    var imgAlts = imgDivs
                        .Select(k => k.FirstChild.FirstChild.FirstChild.GetAttribute("alt"))
                        .ToList();

                    CustomSemaphore.Lock();
                    CurrentTimerState = TimerStates.Image;
                    TimerImages.Start();

                    ThreadPool.QueueUserWorkItem(new WaitCallback((object o) =>
                    {
                        int altIndex = 0;
                        imgDivs
                        .Select(k => k.FirstChild)
                        .ToList()
                        .ForEach(k =>
                        {
                            k.InvokeMember("click");                            
                            imageAltCheck = imgAlts[altIndex];
                            Thread.Sleep(3000);
                            CustomSemaphore.Unlock();                            
                            Thread.Sleep(2000);
                            CustomSemaphore.Lock();
                            k.InvokeMember("click");
                            Thread.Sleep(3000);
                            altIndex++;
                        });
                        CurrentTimerState = TimerStates.Unknow;
                        TimerImages.Enabled = false;
                        TimerImages.Stop();
                    }));                    

                    await Task.CompletedTask;
                }                
            };
            SyncThreadTimer.Start();

            await Task.CompletedTask;
        }

        private async Task StartLoadBrowserFromUrl(string url)
        {
            CustomSemaphore.Lock();
            webBrowser1.DocumentCompleted += (object sender, WebBrowserDocumentCompletedEventArgs e) => CustomSemaphore.Unlock();
            webBrowser1.Navigate(url);
            await Task.CompletedTask;
        }

        protected int GetRandomNumber(int min, int max)
        {
            lock(_lock)
            {
                return new Random().Next(min, max);
            }
        }

        private void TimerImages_Tick(object sender, EventArgs e)
        {
            if (CurrentTimerState == TimerStates.Image && CustomSemaphore.IsFree)
            {
                var imageTags = webBrowser1.Document.GetElementsByTagName("img")
                    .Cast<HtmlElement>()
                    .Where(j => j.GetAttribute("className").Contains("n3VNCb"))
                    .Where(k => k.GetAttribute("alt").Equals(imageAltCheck));

                imageUrlsToDownload.Add(imageTags.First().GetAttribute("src"));
                CustomSemaphore.Lock();
            }
        }
    }
}
