//------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.KinectExplorer
{
    using System.Threading;
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Input;
    using Microsoft.Kinect;
    using System.IO;
    using Microsoft.Speech.AudioFormat;
    using Microsoft.Speech.Recognition;
    using System.Net;
    using System.IO.Ports;
    using System.IO;
    using System.Speech.Synthesis;
    //to get back, remove the below line. Uncomment the above two, re add Microsoft.speech
    //as a reference, remove system.speech as a reference and remove the 
    //using System.Speech.Recognition;
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        static ArduinoHandler ardy;
        static MusicHandler music;
        static VoiceHandler voice;
        SpeechSynthesizer reader;
        private readonly KinectSensorItemCollection sensorItems;
        private readonly ObservableCollection<KinectStatusItem> statusItems;
        Boolean lights = false;
        SpeechRecognitionEngine recognizer;
        /// <summary>
        /// Initializes a new instance of the MainWindow class, which displays a list of sensors and their status.
        /// </summary>
        public MainWindow()
        {

            InitializeComponent();
            //ardy = new ArduinoHandler();
            music = new MusicHandler("D:\\Music");
            //"C:\\Users\\Public\\Music\\Sample Music"
            music.setLabel(currentSong);
            music.setPlayer(myPlayer);
            music.bindConsole(consoleBox);
            HttpHandler server = new HttpHandler(this);
            //this.sensorItems = new KinectSensorItemCollection();
            //this.statusItems = new ObservableCollection<KinectStatusItem>();
            //removed these labels
            //this.kinectSensors.ItemsSource = this.sensorItems;
            //this.kinectStatus.ItemsSource = this.statusItems;
            setupSpeech();
            reader = new SpeechSynthesizer();
        }
        void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string info = DateTime.Now.ToString("HH:mm tt")+": Recognized text: " + e.Result.Text + "| Confidence: " + e.Result.Confidence;
            consoleBox.Items.Add(info);
            if (e.Result.Confidence < .5) return;
            switch (e.Result.Semantics.Value.ToString())
            {
                case "NEXT":
                    music.nextSong();
                    break;

                case "PAUSE":
                    music.PausePlay();
                    break;
                case "LIGHTS":
                    if (ardy.available)
                    {
                        ardy.lights(!lights);
                        lights = !lights;
                    }
                    reader.Speak("Lights");
                    break;
                case "EXIT":
                    Environment.Exit(0);
                    break;
            }
        }
        void setupSpeech()
        {
            RecognizerInfo ri = GetKinectRecognizer();
            recognizer = new SpeechRecognitionEngine(ri.Id);
            var directions = new Choices();
            directions.Add(new SemanticResultValue("next song", "NEXT"));
            directions.Add(new SemanticResultValue("pause", "PAUSE"));
            directions.Add(new SemanticResultValue("play", "PAUSE"));
            directions.Add(new SemanticResultValue("kinect lights", "LIGHTS"));
            directions.Add(new SemanticResultValue("exit app", "EXIT"));

            var gb = new GrammarBuilder { Culture = recognizer.RecognizerInfo.Culture };
            gb.Append(directions);

            var g = new Grammar(gb);

            recognizer.LoadGrammar(g);
            KinectSensor x = KinectSensor.KinectSensors[0];
            try
            {
                // Start the sensor!
                x.Start();
            }
            catch (Exception)
            {
            }

            recognizer.SetInputToAudioStream(
                x.AudioSource.Start(), new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
            recognizer.SpeechRecognized +=recognizer_SpeechRecognized;
            consoleBox.Items.Add("Speech Set up!");

        }

        class HttpHandler
        {
            HttpListener listener;
            Window debug;
            public HttpHandler(Window lol)
            {
                debug = lol;
                listener = new HttpListener();
                listener.Prefixes.Add("http://*:2999/");
                listener.Start();
                Console.WriteLine("Listening...");
                IAsyncResult result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
            }
            public void ListenerCallback(IAsyncResult result)
            {
                string responseString = "Success";
                HttpListener listener = (HttpListener)result.AsyncState;
                // Call EndGetContext to complete the asynchronous operation.
                HttpListenerContext context = listener.EndGetContext(result);
                HttpListenerRequest request = context.Request;
                // Obtain a response object.
                //Break point here doesnt even get stopped
                //Console.WriteLine(request.RawUrl) doesn't show anything either
                string control = request.QueryString["control"];
                string[] command = request.QueryString["command"].Split('_');
                HttpListenerResponse response = context.Response;
                response.AppendHeader("Access-Control-Allow-Origin", "*");

                if (control == "music")
                {
                    music.nextSong();
                }
                else if ((control == "servo") && (ardy.available))
                {
                    ardy.changeServo(Convert.ToInt32(command[0]));
                }
                else if ((control == "port") && (ardy.available))
                {

                    ardy.setPort(Convert.ToInt32(command[0]), Convert.ToInt32(command[1]));
                }
                else
                {
                    responseString = "Not recognized";
                }

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
                listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
            }
        }
        void OnMouseDownPlayPauseMedia(object sender, RoutedEventArgs e)
        {

            // The Pause method pauses the media if it is currently running.
            // The Play method can be used to resume.
            music.PausePlay();

        }

        // Stop the media.
        void OnMouseDownNextMedia(object sender, RoutedEventArgs e)
        {
            music.nextSong();

        }
        void OnMouseDownPrevMedia(object sender, RoutedEventArgs e)
        {

        }
        void OnSpeechButton(object sender, RoutedEventArgs e)
        {
            RecognizerInfo ri = GetKinectRecognizer();
            recognizer = new SpeechRecognitionEngine(ri.Id);
            var directions = new Choices();
            directions.Add(new SemanticResultValue("next song", "NEXT"));
            directions.Add(new SemanticResultValue("pause", "PAUSE"));
            directions.Add(new SemanticResultValue("play", "PAUSE"));
            directions.Add(new SemanticResultValue("kinect lights", "LIGHTS"));
            directions.Add(new SemanticResultValue("exit app", "EXIT"));

            var gb = new GrammarBuilder { Culture = recognizer.RecognizerInfo.Culture };
            gb.Append(directions);

            var g = new Grammar(gb);

            recognizer.LoadGrammar(g);
            recognizer.SetInputToAudioStream(
                KinectSensor.KinectSensors[0].AudioSource.Start(), new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
            recognizer.SpeechRecognized +=recognizer_SpeechRecognized;

           
            reader.SpeakAsync("Setup!");
        }
        private void WindowLoaded(object sender, EventArgs e)
        {
            /*
            // listen to any status change for Kinects.
            KinectSensor.KinectSensors.StatusChanged += this.KinectsStatusChanged;

            // show status for each sensor that is found now.
            foreach (KinectSensor kinect in KinectSensor.KinectSensors)
            {
                this.ShowStatus(kinect, kinect.Status);
            }
             * */
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            /*
            foreach (KinectSensorItem sensorItem in this.sensorItems)
            {
                sensorItem.CloseWindow();
            }

            this.sensorItems.Clear();
            */
        }

        private void ShowStatus(KinectSensor kinectSensor, KinectStatus kinectStatus)
        {
            this.statusItems.Add(new KinectStatusItem 
            { 
                Id = (null == kinectSensor) ? null : kinectSensor.DeviceConnectionId,
                Status = kinectStatus,
                DateTime = DateTime.Now
            });

            KinectSensorItem sensorItem;
            this.sensorItems.SensorLookup.TryGetValue(kinectSensor, out sensorItem);

            if (KinectStatus.Disconnected == kinectStatus)
            {
                if (sensorItem != null)
                {
                    this.sensorItems.Remove(sensorItem);
                    sensorItem.CloseWindow();
                }
            }
            else
            {
                if (sensorItem == null)
                {
                    sensorItem = new KinectSensorItem(kinectSensor, kinectSensor.DeviceConnectionId);
                    sensorItem.Status = kinectStatus;

                    this.sensorItems.Add(sensorItem);
                }
                else
                {
                    sensorItem.Status = kinectStatus;
                }

                if (KinectStatus.Connected == kinectStatus)
                {
                    // show a window
                    sensorItem.ShowWindow();
                }
                else
                {
                    sensorItem.CloseWindow();
                }
            }
        }
        private void KinectsStatusChanged(object sender, StatusChangedEventArgs e)
        {
            this.ShowStatus(e.Sensor, e.Status);
        }
        private void ShowMoreInfo(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = e.OriginalSource as Hyperlink;
            if (hyperlink != null)
            {
                // Careful - ensure that this NavigateUri comes from a trusted source, as in this sample, before launching a process using it.
                Process.Start(new ProcessStartInfo(hyperlink.NavigateUri.ToString()));
            }

            e.Handled = true;
        }
        private void Sensor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;

            if (null == element)
            {
                return;
            }

            var sensorItem = element.DataContext as KinectSensorItem;

            if (null == sensorItem)
            {
                return;
            }

            sensorItem.ActivateWindow();
        }
        private static RecognizerInfo GetKinectRecognizer()
        {
            foreach (RecognizerInfo recognizer in SpeechRecognitionEngine.InstalledRecognizers())
            {
                string value;
                recognizer.AdditionalInfo.TryGetValue("Kinect", out value);
                if ("True".Equals(value, StringComparison.OrdinalIgnoreCase) && "en-US".Equals(recognizer.Culture.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return recognizer;
                }
            }

            return null;
        }
    }

}
