using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace Liveticker_Client
{
    /// <summary>
    /// Interaktionslogik für EventHinzufuegen.xaml
    /// </summary>
    public partial class EventHinzufuegen : Window
    {
        private LiveTickerService liveTickerService = new LiveTickerService();
        private Event[] events;

        public EventHinzufuegen()
        {
            InitializeComponent();
        }

        private void btnAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            events = liveTickerService.getEvents();
            foreach (Event ev in events)
            {
                if (this.tbxEvent.Text.ToLower() == ev.text.ToLower())
                {
                    System.Windows.MessageBox.Show("Sportart schon vorhanden!");
                    return;
                }
            }

            
            byte[] barray = new byte[1];
       
            if(icon.Source != null)
            { 
                System.Windows.Media.Imaging.JpegBitmapEncoder encoder = new System.Windows.Media.Imaging.JpegBitmapEncoder();
                encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bitmapImage));
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    encoder.Save(ms);
                    barray = ms.ToArray();
                }
            }

            liveTickerService.addEvent(this.tbxEvent.Text, "Sportart Beschreibung", barray, DateTime.UtcNow);
            this.Close();
        }

        private void iconButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog= new OpenFileDialog();
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            System.IO.Stream myStream = null;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = dialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            bitmapImage = new System.Windows.Media.Imaging.BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                            bitmapImage.StreamSource = myStream;
                            bitmapImage.EndInit();
                            icon.Source = bitmapImage;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
        private System.Windows.Media.Imaging.BitmapImage bitmapImage;
    }
}