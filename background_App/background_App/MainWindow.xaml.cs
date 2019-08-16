using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Diagnostics;
using System.Net;
using Nancy.Json;

namespace background_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string dirName = Directory.GetCurrentDirectory();
        public static int numOfImages = Directory.GetFiles(dirName + "\\images\\randomLoop").Length;
        public bool loopRun = false;

        public MainWindow()
        {
            InitializeComponent();
            string infoFromLocationFile = System.IO.File.ReadAllText(dirName + "/location.txt");
            locationBox.Text = infoFromLocationFile;
            getInfoFromOpenWeather(locationBox.Text);
        }

        public void runimage(string image, string dir)
        {
            Process myProcess = new Process();
            string imagePath = dir + image;

            if(File.Exists(imagePath + ".jpg") == false)
            {
                dir = Directory.GetCurrentDirectory();
                imagePath = dir + "\\images/Default.jpg";
            }

            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.FileName = @"cmd.exe";
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.RedirectStandardInput = true;
            myProcess.Start();
            StreamWriter myStreamWriter = myProcess.StandardInput;

            myStreamWriter.WriteLine("powershell -ExecutionPolicy ByPass -File powershellBackground.ps1 -filename " + imagePath);
            myStreamWriter.WriteLine("exit");
            myStreamWriter.Close();
        }

        public void rerunWeather(object sender, RoutedEventArgs e)
        {
            string textInfo = locationBox.Text;
            System.IO.File.WriteAllText(dirName + "/location.txt", textInfo);
            getInfoFromOpenWeather(locationBox.Text);
        }
        public void getInfoFromOpenWeather(string cityName)
        {
            try
            {
                string weather = "default";
                string APIkey = "b2bcf15f01b56ec4cb4f15b67b60c655";
                string URL = "https://api.openweathermap.org/data/2.5/weather?q=" + cityName + "&appid=" + APIkey;
                WebClient cheekyClient = new WebClient();
                string webString = cheekyClient.DownloadString(URL);

                JavaScriptSerializer js = new JavaScriptSerializer();
                myWeatherInfo weatherInfo = js.Deserialize<myWeatherInfo>(webString);

                weather = weatherInfo.weather[0].main;

                runimage(weather, dirName + "\\images\\weather/");
                runimage(weather, dirName + "\\images\\weather/");
                runimage(weather, dirName + "\\images\\weather/");
                runimage(weather, dirName + "\\images\\weather/");
                runimage(weather, dirName + "\\images\\weather/");
            }
            catch
            {
                runimage("Default", dirName + "\\images/");
                runimage("Default", dirName + "\\images/");
                runimage("Default", dirName + "\\images/");
                runimage("Default", dirName + "\\images/");
                runimage("Default", dirName + "\\images/");
            }
        }
        public class myWeatherInfo
        {
            public List<Weather> weather { get; set; }
            public string name { get; set; }
            public Sys sys { get; set; }
        }
        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }
        public class Sys
        {
            public int type { get; set; }
            public int id { get; set; }
            public double message { get; set; }
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }



        private void randomLoop(object sender, RoutedEventArgs e)
        {
            if(loopRun == false)
            {
                Thread.Sleep(1000);
                loopRun = true;
                int updateTimer = Convert.ToInt32(updateTime.Text);

                System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += (senderT, eventArgs) =>
                {
                    if (loopRun == true)
                    {
                        randomise(dirName + "\\images\\randomLoop/", numOfImages);
                    }
                    else
                    {
                        dispatcherTimer.Stop();
                    }
                };
                dispatcherTimer.Interval = new TimeSpan(0, updateTimer, 0);
                dispatcherTimer.Start();
            }
        }
        public void randomise(string dirName, int numOfImages)
        {
            int imgNum = returnRandom(1, numOfImages);

            runimage(imgNum.ToString(), dirName);
            runimage(imgNum.ToString(), dirName);
            runimage(imgNum.ToString(), dirName);
            runimage(imgNum.ToString(), dirName);
            runimage(imgNum.ToString(), dirName);

            Console.WriteLine("image = " + imgNum);
        }
        public static int returnRandom(int min, int folderSize)
        {
            Random myRand = new Random();
            int output = myRand.Next(min, folderSize);
            return output;
        }
        private void stopLoop(object sender, RoutedEventArgs e)
        {
            loopRun = false;
        }
    }


}
