using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LandingGenerator
{
    public class ImageDownloader
    {
        private Main _main;

        private WebBrowser Browser = new WebBrowser();

        private volatile string imageAltCheck;
        private volatile string IMAGE_SEARCH_PARAMS = "&tbm=isch";

        const string IMAGE_DOWNLOAD_FOLDER_NAME = "Images";
        private volatile string IMAGE_DOWNLOAD_FOLDER;

        List<string> imageUrlsToDownload = new List<string>();

        System.Windows.Forms.Timer mainTimer;

        public ImageDownloader(Main main)
        {
            _main = main;
            mainTimer = new System.Windows.Forms.Timer()
            {
                Interval = 1000
            };
            mainTimer.Tick += TimerImages_Tick;

            Browser.ScriptErrorsSuppressed = true;
            Browser.WebBrowserShortcutsEnabled = false;

            DownloadRelatedImages().Wait();
        }

        private async Task DownloadRelatedImages()
        {
            IMAGE_DOWNLOAD_FOLDER = Path.Combine(Environment.CurrentDirectory, _main.SelectedTheme, IMAGE_DOWNLOAD_FOLDER_NAME);
            await StartLoadBrowserFromUrl(_main.MAIN_URL + _main.SelectedTheme + IMAGE_SEARCH_PARAMS);
            System.Windows.Forms.Timer SyncThreadTimer = new System.Windows.Forms.Timer()
            {
                Interval = 1000,
                Enabled = false
            };
            SyncThreadTimer.Tick += async (object sender, EventArgs e) =>
            {
                if (CustomSemaphore.IsFree)
                {
                    SyncThreadTimer.Enabled = false;

                    var imgDivs = Browser.Document.GetElementsByTagName("div")
                        .Cast<HtmlElement>()
                        .Where(k => k.GetAttribute("className").Contains("isv-r"))
                        .Take(Main.IA_MAX_VARIATIONS)
                        .ToList();
                    var imgAlts = imgDivs
                        .Select(k => k.FirstChild.FirstChild.FirstChild.GetAttribute("alt"))
                        .ToList();

                    CustomSemaphore.Lock();
                    _main.CurrentTimerState = Main.TimerStates.Image;
                    mainTimer.Start();

                    ThreadPool.QueueUserWorkItem(new WaitCallback(async (object o) =>
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
                        _main.CurrentTimerState = Main.TimerStates.Unknow;
                        mainTimer.Enabled = false;
                        mainTimer.Stop();
                        await DownloadImages();
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
            Browser.DocumentCompleted += (object sender, WebBrowserDocumentCompletedEventArgs e) => CustomSemaphore.Unlock();
            Browser.Navigate(url);
            await Task.CompletedTask;
        }

        private async Task DownloadImages()
        {
            Directory.CreateDirectory(IMAGE_DOWNLOAD_FOLDER);
            WebClient wc = new WebClient();
            foreach (var imageUrl in imageUrlsToDownload)
            {
                await wc.DownloadFileTaskAsync(imageUrl, Path.Combine(IMAGE_DOWNLOAD_FOLDER, Guid.NewGuid().ToString() + _main.GetExtentionFromUrl(imageUrl)));
            }
        }

        private void TimerImages_Tick(object sender, EventArgs e)
        {
            if (_main.CurrentTimerState == Main.TimerStates.Image && CustomSemaphore.IsFree)
            {
                var imageTags = Browser.Document.GetElementsByTagName("img")
                    .Cast<HtmlElement>()
                    .Where(j => j.GetAttribute("className").Contains("n3VNCb"))
                    .Where(k => k.GetAttribute("alt").Equals(imageAltCheck));

                var imageUrl = imageTags.First().GetAttribute("src");

                if (!imageUrlsToDownload.Contains(imageUrl) && !imageUrl.Contains("usqp=CAU"))
                {
                    imageUrlsToDownload.Add(imageUrl);
                }

                CustomSemaphore.Lock();
            }
        }
    }
}
