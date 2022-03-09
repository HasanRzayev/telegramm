
using AIMLbot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
using System.Windows.Threading;

namespace Telegram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   
    public partial class MainWindow : Window
    {
        public class Human
        {

            public Human(string name, string time, List<string> data, string imageurl)
            {
                this.name = name;
                this.time = time;
                this.data = data;
                this.imageurl = imageurl;
            }

            public string name { get; set; }
            public string time { get; set; }
            public List<string> data { get; set; }
            public string imageurl { get; set; }

        }
        List<string> emojiss = new List<string>();
        List<Human> contact = new List<Human>();
        List<string> lazimli = new List<string> { "hello", "hi" };
        string copyimage2;
        DateTime date = new DateTime();

      

        public MainWindow()
        {
            InitializeComponent();
            string value = date.ToShortTimeString();
            if (File.Exists("contacc.json") == false)
            {

                contact.Add(new Human("Admistration bot", value, lazimli, "https://www.internetandtechnologylaw.com/files/2019/06/iStock-872962368-chat-bots.jpg"));
                contact.Add(new Human("Messi", value, lazimli, "https://img.a.transfermarkt.technology/portrait/big/28003-1631171950.jpg?lm=1"));
                contact.Add(new Human("Angelina jolie", value, lazimli, "https://m.media-amazon.com/images/M/MV5BODg3MzYwMjE4N15BMl5BanBnXkFtZTcwMjU5NzAzNw@@._V1_.jpg"));
            }
            else
            {
                var dialog = File.ReadAllText($"contacc.json");
                contact = JsonConvert.DeserializeObject<List<Human>>(dialog);



            }

            emojiss.Add("😀");
            emojiss.Add("😂");
            emojiss.Add("😍");
            emojiss.Add("😘");
            emojiss.Add("🤔");
            emojiss.Add("🤐");
            emojiss.Add("😴");
            emojiss.Add("😷");  
            emojiss.Add("🤠");
            emojiss.Add("😎");
         

            baza.ItemsSource = emojiss;
            humans.ItemsSource = contact;

            DataContext = this;
        }
      
        List<string> zapas = new List<string>();
        string gelenmesaj;
        string qarsiinsan;
        SoundPlayer playSound = new SoundPlayer(Properties.Resources.iphone_sms_original__online_audio_converter_com_);
        DispatcherTimer timer = new DispatcherTimer();
       
        private void gonder_Click(object sender, RoutedEventArgs e)
        {
             
        
            Label gonderiren=new Label();
            
            Color c = Colors.Cyan;
            


            gonderiren.Background=new SolidColorBrush(c);
            gonderiren.Content = $"You:{mesaj.Text}";
            zapas.Add($"You:{mesaj.Text}");
            Chat.HorizontalAlignment = HorizontalAlignment.Stretch;
            gonderiren.HorizontalContentAlignment= HorizontalAlignment.Right;

            Chat.Items.Add(gonderiren);

            playSound.Play();
    
            timer.Start();
            //////////////////////////////////////////////////////////////////
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += timer_Tick;
            mesaj.IsEnabled = false;

           
      
            ///////////////////////////////////////////////////////////////////////////////////
            foreach (Human human in contact)
            {
                if ((human is Human a ))
                {
                    if(a.name == qarsiinsan && Chat.Items != null)
                    {
                     
                        foreach (var item in zapas)
                        {
                           
                            a.data.Add(item);


                        }
                    }
                  
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////////
            var contactjson = JsonConvert.SerializeObject(contact, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("contacc.json", contactjson);

        }
        void timer_Tick(object sender, EventArgs e)
        {
            Color c = Colors.Cyan;
            Bot AI = new Bot();

 
            AI.loadSettings();
            AI.loadAIMLFromFiles();

            AI.isAcceptingUserInput = false;

            User myUser = new User("Username", AI);

            AI.isAcceptingUserInput = true;



            Request r = new Request(mesaj.Text, myUser, AI);

            Result res = AI.Chat(r);

            gelenmesaj =$"{qarsiinsan}: {res.Output}" ;


            Label GELEN = new Label();
            GELEN.Content = gelenmesaj;

            GELEN.HorizontalContentAlignment = HorizontalAlignment.Left;
            
            zapas.Add($"{gelenmesaj}");
            
            GELEN.Background = new SolidColorBrush(c);
            playSound.Play();
            Chat.Items.Add(GELEN);
         
            timer.Stop();

            mesaj.Text = null;
            mesaj.IsEnabled = true;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            send.IsEnabled = true;
            string value = date.ToShortTimeString();
            
            basliq.Content = (sender as Button).Tag.ToString();
            Chat.Items.Clear();
            qarsiinsan = (sender as Button).Tag.ToString();
            foreach (Human human in contact)
            {
                copyimage2 = (human as Human).imageurl;
                if ((human as Human).name == qarsiinsan  && human.data!=null)
                {
                    (human as Human).time = value;
                   

                    foreach (var item in human.data)
                    {
                        Label gonderiren = new Label();
                        Color c = Colors.Cyan;


                        gonderiren.Background = new SolidColorBrush(c);
                        gonderiren.Content= item;
                        Chat.Items.Add(gonderiren);

                    }
                    break;
                }
                
            }

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(copyimage2, UriKind.Absolute);
            bitmap.EndInit();

            



            copyimage.Source = bitmap;
   



        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            send.IsEnabled = false;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
      
            emojis.Visibility= Visibility.Visible;
            

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mesaj.Text += (sender as Button).Content;
        }

        private void exitemoji_Click(object sender, RoutedEventArgs e)
        {
            emojis.Visibility = Visibility.Hidden;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Chat.Items.Clear();
            foreach (Human human in contact)
            {
                if ((human is Human a))
                {
                    if (a.name == qarsiinsan )
                    {


                        a.data = null ;


                        
                    }

                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////////
            var contactjson = JsonConvert.SerializeObject(contact, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("contacc.json", contactjson);
        }

      
    }
}
