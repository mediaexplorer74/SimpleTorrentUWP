using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SimpleTorrentUWP.Torrent;
using Windows.Storage;

// namespace
namespace TorrentUWP
{
    
    // class
    public sealed partial class MainPage : Page
    {
        public static Client TorrentClient; // single torrentclient
        
        public MainPage()
        {
            this.InitializeComponent();
        }//

        // Alt . Debug
        void SuperWriteLine(string onestring)
        {
            ResponseBox.Items.Add(onestring);
        }//SuperWriteLine

        // Picker handler
        private async void ChooseButton_Click(object sender, RoutedEventArgs e)
        {

            // Open torrent file

            FileOpenPicker torrentFile = new FileOpenPicker();
            torrentFile.ViewMode = PickerViewMode.List;
            torrentFile.SuggestedStartLocation = PickerLocationId.Downloads;
            torrentFile.FileTypeFilter.Add(".torrent");

            StorageFile torrent = await torrentFile.PickSingleFileAsync();
            
            // check if torrent file picking ok or not
            if (torrent == null)
            {
                SuperWriteLine("Torrent file not choosed");
                TorrentFilePath.Text = "Torrent file not choosed";

                SuperWriteLine("Download folder not choosed");
                DownloadFolderPath.Text = "Download folder not choosed";

                return;
            }
            else
            {
                SuperWriteLine("Torrent file path: " + torrent.Path);
                TorrentFilePath.Text = torrent.Path;


                //Select download folder

                FolderPicker saveFolder = new FolderPicker();
                saveFolder.SuggestedStartLocation = PickerLocationId.Downloads;
                saveFolder.FileTypeFilter.Add(".");
                StorageFolder DownloadFolder = await saveFolder.PickSingleFolderAsync();

                // check if download folder picking ok or not
                if (DownloadFolder == null)
                {
                    SuperWriteLine("Download folder not choosed");
                    DownloadFolderPath.Text = "Download folder not choosed";

                    return;
                }
                else
                {                   
                    SuperWriteLine("Download Folder path: " + DownloadFolder.Path);
                    DownloadFolderPath.Text = DownloadFolder.Path;


                    // Start the client/download

                    SuperWriteLine("Create Torrent Client at port 6881...");
                    TorrentClient = new Client(6881, torrent.Path, DownloadFolder.Path);

                    SuperWriteLine("Load Torrent...");
                    await TorrentClient.loadTorrent(torrent, DownloadFolder.Path);

                    int peerscount = TorrentClient.Peers.Count;
                    SuperWriteLine("Peers Count: " + peerscount);

                    int seederscount = TorrentClient.Seeders.Count;
                    SuperWriteLine("Seeders Count: " + seederscount);

                    SuperWriteLine("Start...");
                    TorrentClient.Start();


                    peerscount = TorrentClient.Peers.Count;
                    SuperWriteLine("Peers Count: " + peerscount);

                    seederscount = TorrentClient.Seeders.Count;
                    SuperWriteLine("Seeders Count: " + seederscount);

                    // SuperWriteLine("Waiting?...");
                }
            }
        }//ChooseButton_Click


        // UpdatePeersAndSeedersInfo_Click
        private void UpdatePeersAndSeedersInfo_Click(object sender, RoutedEventArgs e)
        {
            if (TorrentClient != null)
            {
                int peerscount = TorrentClient.Peers.Count;
                SuperWriteLine("Peers Count: " + peerscount);

                int seederscount = TorrentClient.Seeders.Count;
                SuperWriteLine("Seeders Count: " + seederscount);
            }
            else
            {
                SuperWriteLine("Peek torrent file and download folder at first");
            }
        }//UpdatePeersAndSeedersInfo_Click

    }//class end

}//namespace end
