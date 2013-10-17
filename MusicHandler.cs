using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;

namespace Microsoft.Samples.Kinect.KinectExplorer
{
    class MusicHandler
    {
        string[] allSongs;
        string[] randomSongs;
        Boolean available;
        Boolean playing;
        string currentSong;
        System.Windows.Controls.MediaElement player;
        System.Windows.Controls.Label label;
        System.Windows.Controls.ListBox console;
        public MusicHandler(string path)
        {
            allSongs = Directory.GetFiles(path,"*.mp3",SearchOption.AllDirectories);
            available = (allSongs.Length>0);
        }
        public Boolean bindConsole(System.Windows.Controls.ListBox x)
        {
            console = x;
            return true;
        }
        public Boolean setPlayer(System.Windows.Controls.MediaElement x)
        {
            player = x;
            return true;
        }
        public Boolean setLabel(System.Windows.Controls.Label x)
        {
            label = x;
            return true;
        }
        public String nextSong()
        {
            string song = allSongs.RandomElement();
            string songTitle = song.Replace("D:\\Music\\", "");
            if (!label.Dispatcher.CheckAccess())
            {
                label.Dispatcher.Invoke(
                  System.Windows.Threading.DispatcherPriority.Normal,
                  new Action(
                    delegate()
                    {
                        label.Content = songTitle;
                    }
                ));
            }
            else
            {
                label.Content = songTitle;
            }
            if (!player.Dispatcher.CheckAccess())
            {
                player.Dispatcher.Invoke(
                  System.Windows.Threading.DispatcherPriority.Normal,
                  new Action(
                    delegate()
                    {
                        player.Source = new Uri(song, UriKind.RelativeOrAbsolute);
                        player.Play();
                    }
                ));
            }
            else
            {
                player.Source = new Uri(song, UriKind.RelativeOrAbsolute);
                player.Play();
            }
            playing = true;
            currentSong = song;
            console.Items.Add("Next Song: "+songTitle);
            return song;
        }

        public void PausePlay()
        {
            if (playing)
            {
                player.Pause();
                playing = false;
            }
            else
            {
                player.Play();
                playing = true;
            }
        }

    }
}
