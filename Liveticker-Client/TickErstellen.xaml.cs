using System.Windows;
using System.Xml;

namespace Liveticker_Client
{
    /// <summary>
    /// Interaktionslogik für TickErstellen.xaml
    /// </summary>
    public partial class TickErstellen : Window
    {
        private LiveTickerService liveTickerService = new LiveTickerService();
        private XmlDocument xmlDoc = new XmlDocument();

        public TickErstellen()
        {
            InitializeComponent();
        }
        private void cbxSportart_Loaded(object sender, RoutedEventArgs e)
        {
            xmlDoc.LoadXml(liveTickerService.getEvents());
            //MessageBox.Show(liveTickerService.getEvents()); // Debug
            XmlNodeList entryNodeList = xmlDoc.GetElementsByTagName("Entry");
            foreach (XmlNode entryNode in entryNodeList)
            {
                //entryNode.ChildNodes.Item(0).InnerText // <Id> Node
                //entryNode.ChildNodes.Item(1).InnerText // <Text> Node

                cbxSportart.Items.Add(entryNode.ChildNodes.Item(1).InnerText);
            }

            cbxSportart.SelectedIndex = 0;
        }

        private void btnTickErstellen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}