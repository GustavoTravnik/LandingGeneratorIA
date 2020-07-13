using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LandingGenerator
{
    public class TextDownloader
    {
        private Main _main;

        List<string> Sections = new List<string>();

        private CustomSemaphore CustomSemaphore { get; set; } = new CustomSemaphore();

        private WebBrowser Browser = new WebBrowser();

        private volatile string TEXT_SEARCH_PARAMS = "&tbm=isch";
        const string TEXT_DOWNLOAD_FOLDER_NAME = "Texts";
        private volatile string TEXT_DOWNLOAD_FOLDER;     

        public TextDownloader(Main main, List<string> Sections)
        {
            _main = main;
           
            Browser.ScriptErrorsSuppressed = true;
            Browser.WebBrowserShortcutsEnabled = false;

            DownloadRelatedTexts().Wait();
        }

        private async Task DownloadRelatedTexts()
        {
            TEXT_DOWNLOAD_FOLDER = Path.Combine(Environment.CurrentDirectory, _main.SelectedTheme, TEXT_DOWNLOAD_FOLDER_NAME);
            
            await Task.CompletedTask;
        }

        private async Task StartLoadBrowserFromUrl(string url)
        {
            CustomSemaphore.Lock();
            Browser.DocumentCompleted += (object sender, WebBrowserDocumentCompletedEventArgs e) => CustomSemaphore.Unlock();
            Browser.Navigate(url);
            await Task.CompletedTask;
        }                
    }
}
