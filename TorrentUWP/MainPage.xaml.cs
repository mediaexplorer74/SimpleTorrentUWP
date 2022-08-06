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
        public static Client TorrentClient;

        // constructor
        public MainPage()
        {
            this.InitializeComponent();
        }//

        // Alt . Debug

        void SuperWriteLine(string onestring)
        {

            ResponseBox.Items.Add(onestring);
        }

        // Open Picker demo
        private async void openButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.CommitButtonText = "Choose XAP file";
            openPicker.FileTypeFilter.Add(".xap");
            var file = await openPicker.PickSingleFileAsync();

            // check the file choosed or not
            if (file != null)
            {
                // fulfill filePath
                XapPathBox.Text = file.Path;
            }
            */


            // Open torrent file
            FileOpenPicker torrentFile = new FileOpenPicker();
            torrentFile.ViewMode = PickerViewMode.List;
            torrentFile.SuggestedStartLocation = PickerLocationId.Downloads;
            torrentFile.FileTypeFilter.Add(".torrent");

            StorageFile torrent = await torrentFile.PickSingleFileAsync();
            
            if (torrent == null)
            {
                return;
            }
            else
            {
                //Select download folder
                FolderPicker saveFolder = new FolderPicker();
                saveFolder.SuggestedStartLocation = PickerLocationId.Downloads;
                saveFolder.FileTypeFilter.Add(".");
                StorageFolder DownloadFolder = await saveFolder.PickSingleFolderAsync();
                
                if (DownloadFolder == null)
                {
                    SuperWriteLine("Torrent file not choosed");
                    return;
                }
                else
                {
                    // Start the client/download

                    SuperWriteLine("Port: 6881");
                    SuperWriteLine("Torrent path: " + torrent.Path);
                    SuperWriteLine("Download Folder path: " + DownloadFolder.Path);

                    SuperWriteLine("Create Torrent Client...");
                    TorrentClient = new Client(6881, torrent.Path, DownloadFolder.Path);

                    SuperWriteLine("Load Torrent...");
                    await TorrentClient.loadTorrent(torrent, DownloadFolder.Path);

                    SuperWriteLine("Start...");
                    TorrentClient.Start();

                    SuperWriteLine("Stop?...");
                }
            }
        }//openButton_Click


        // Win10 <-> WM10 Path sync (NOT READY YET!)
        private void PathSync(string src)
        {
            // TODO
        }

        // Try to Start XAP Delpoy =)
        private async void StartDeploy_Click(object sender, RoutedEventArgs e)
        {
            /*
            openButton.IsEnabled = false;
            saveButton.IsEnabled = false;

            StringBuilder sb = new StringBuilder();
            string Parameter1 = XapPathBox.Text;
            string Parameter2;

            string ipaddress = TelnetIP.Text;

            int port = 23;

            // TO DO
            // IP address checking

            try
            {
                port = Int32.Parse(TelnetPort.Text);
            }
            catch (Exception E2)
            {
                SuperWriteLine("Error! Bad port: " + E2.Message);
                openButton.IsEnabled = true;
                saveButton.IsEnabled = true;
                return;
            }


            // -- 1 -- 
            //create TcpClient object 
            telnetClient = new TcpClient();



            //await telnetClient.ConnectAsync("192.168.1.33", 23);
            try
            {
                SuperWriteLine("Trying to connect to  " + ipaddress + ":" + port);

                // ERROR IF TRY FROM WM !!!
                await telnetClient.ConnectAsync(ipaddress, port);
            }
            catch (Exception e1)
            {
                // DEBUG INFO
                SuperWriteLine("Telnet connect error!\r\n Exception:  " + e1.Message);
                openButton.IsEnabled = true;
                saveButton.IsEnabled = true;
                return;
            }

            try
            {
                var r = GetAnswer(telnetClient);
                // DEBUG: get telnet echo
                SuperWriteLine(r);
            }
            catch (Exception e2)
            {
                // DEBUG INFO
                SuperWriteLine("Telnet data transfer error!\r\n Exception: " + e2.Message);
                openButton.IsEnabled = true;
                saveButton.IsEnabled = true;
                return;
            }



            // --2 --  
            // Parse parameters

            if (toggleSwitch1.IsOn)
            {
                Parameter2 = "1";
            }
            else
            {
                Parameter2 = "0";
            }

            // send loginname
            //SendCmd(telnetClient, "Sirepuser");

            // DEBUG: get telnet echo
            //SuperWriteLine(GetAnswer(telnetClient));


            // send password
            //SendCmd(telnetClient, "1234");

            // DEBUG: get telnet echo
            //SuperWriteLine(GetAnswer(telnetClient));

            // -- 3 --
            // try to install xap
            SendCmd(telnetClient, "xapinst " + Parameter1 + " " + Parameter2 + "\r\n");
            SuperWriteLine("xapinst " + Parameter1 + " " + Parameter2);

            // DEBUG: get telnet echo
            SuperWriteLine(GetAnswer(telnetClient));


            //SuperWriteLine("bb.xap must be installed (by xapinst.bat)");

            //RnD
            //telnetClient.Dispose();
            openButton.IsEnabled = true;
            saveButton.IsEnabled = true;
            */

        }// StartDelpoy_Click end

    }
}
