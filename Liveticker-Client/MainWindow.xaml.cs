using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml;

namespace Liveticker_Client
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public struct Event
        {
            public int Id;
            public string Text;
        }

        public struct Tick
        {
            public string Author;
            public int EventId;
            public int Id;
            public bool IsPublished;
            public DateTime Modified;
            public DateTime Reported;
            public string Text;
            public string Title;
        }

        private LiveTickerService liveTickerService = new LiveTickerService();
        private XmlDocument xmlDoc = new XmlDocument();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// <para>Durch die Schnittstelle addEvent soll es möglich sein eine Sportart hinzuzufügen.</para>
        /// </summary>
        /// <param name="date">...</param>
        /// <param name="title">...</param>
        /// <param name="description">...</param>
        /// <param name="icon">...</param>
        private void addEvent(DateTime date, string title, string description, Image icon)
        {
        }

        /// <summary>
        /// <para>Die Schnittstelle deleteEvent löscht eine Sportart.</para>
        /// </summary>
        /// <param name="id">ID des zu löschenden Events</param>
        private void deleteEvent(int id)
        {
        }

        /// <summary>
        /// <para>Die Schnittstelle deleteLiveticker löscht eine „LiveTicker” Nachricht.</para>
        /// </summary>
        /// <param name="id">ID des zu löschenden Livetickers</param>
        private void deleteLiveticker(int id)
        {
        }

        /// <summary>
        /// <para>Die Schnittstelle publishLiveticker veröffentlicht eine „LiveTicker” Nachricht.</para>
        /// </summary>
        /// <param name="id">ID des zu veröffentlichenden Livetickers</param>
        private void publishLiveticker(int id)
        {
        }

        /// <summary>
        /// <para>Durch die Schnittstelle modifyEvent soll es möglich sein die Daten eines Events zu bearbeiten, dies beinhaltet Datum, Titel, Beschreibung und Icon.</para>
        /// </summary>
        private void modifyEvent()
        {
        }

        private void tabControl1_Loaded(object sender, RoutedEventArgs e)
        {
            xmlDoc.LoadXml(liveTickerService.getEvents());
            //MessageBox.Show(liveTickerService.getEvents()); // Debug
            XmlNodeList entryNodeList = xmlDoc.GetElementsByTagName("Entry");
            foreach (XmlNode entryNode in entryNodeList)
            {
                //entryNode.ChildNodes.Item(0).InnerText // <Id> Node
                //entryNode.ChildNodes.Item(1).InnerText // <Text> Node

                Event ev = new Event();
                ev.Id = int.Parse(entryNode.ChildNodes.Item(0).InnerText);
                ev.Text = entryNode.ChildNodes.Item(1).InnerText;

                GridViewColumn gridviewcolumnId = new GridViewColumn();
                GridViewColumn gridviewcolumnEventId = new GridViewColumn();
                GridViewColumn gridviewcolumnIsPublished = new GridViewColumn();
                GridViewColumn gridviewcolumnReported = new GridViewColumn();
                GridViewColumn gridviewcolumnModified = new GridViewColumn();
                GridViewColumn gridviewcolumnAuthor = new GridViewColumn();
                GridViewColumn gridviewcolumnTitle = new GridViewColumn();
                GridViewColumn gridviewcolumnText = new GridViewColumn();
                GridView gridview = new GridView();
                ListView listview = new ListView();
                Grid grid = new Grid();
                TabItem tabitem = new TabItem();

                gridviewcolumnId.Header = "ID";
                gridviewcolumnId.DisplayMemberBinding = new Binding("Id");
                gridviewcolumnEventId.Header = "Event ID";
                gridviewcolumnEventId.DisplayMemberBinding = new Binding("EventId");
                gridviewcolumnIsPublished.Header = "Veröffentlicht?";
                gridviewcolumnIsPublished.DisplayMemberBinding = new Binding("IsPublished");
                gridviewcolumnReported.Header = "Zeit";
                gridviewcolumnReported.DisplayMemberBinding = new Binding("Reported");
                gridviewcolumnModified.Header = "Zuletzt geändert";
                gridviewcolumnModified.DisplayMemberBinding = new Binding("Modified");
                gridviewcolumnAuthor.Header = "Autor";
                gridviewcolumnAuthor.DisplayMemberBinding = new Binding("Author");
                gridviewcolumnTitle.Header = "Titel";
                gridviewcolumnTitle.DisplayMemberBinding = new Binding("Title");
                gridviewcolumnText.Header = "Beschreibung";
                gridviewcolumnText.DisplayMemberBinding = new Binding("Text");

                //gridview.Columns.Add(gridviewcolumnId);
                //gridview.Columns.Add(gridviewcolumnEventId);
                //gridview.Columns.Add(gridviewcolumnIsPublished);
                gridview.Columns.Add(gridviewcolumnReported);
                //gridview.Columns.Add(gridviewcolumnModified);
                //gridview.Columns.Add(gridviewcolumnAuthor);
                gridview.Columns.Add(gridviewcolumnTitle);
                gridview.Columns.Add(gridviewcolumnText);

                listview.View = gridview;

                grid.Children.Add(listview);

                tabitem.Header = ev.Text.ToUpper();
                tabitem.Tag = ev;
                tabitem.Content = grid;
                tabitem.GotFocus += new RoutedEventHandler(tabitem_GotFocus);

                tabControl1.Items.Add(tabitem);
            }

            refreshListView();
        }

        private void refreshListView()
        {
            TabItem currTabItem = (TabItem)tabControl1.SelectedItem;
            ListView currListView = (ListView)((Grid)currTabItem.Content).Children[0];

            currListView.Items.Clear();

            xmlDoc.LoadXml(liveTickerService.getAllTicksAdmin(((Event)(currTabItem.Tag)).Id));
            //MessageBox.Show(liveTickerService.getAllTicksAdmin(((Event)(currTabItem.Tag)).Id)); // Debug
            XmlNodeList tickNodeList = xmlDoc.GetElementsByTagName("Tick");
            foreach (XmlNode tickNode in tickNodeList)
            {
                //tickNode.ChildNodes.Item(0).InnerText; // <Id> Node
                //tickNode.ChildNodes.Item(1).InnerText; // <EventId> Node
                //tickNode.ChildNodes.Item(2).InnerText; // <IsPublished> Node
                //tickNode.ChildNodes.Item(3).InnerText; // <Reported> Node
                //tickNode.ChildNodes.Item(4).InnerText; // <Modified> Node
                //tickNode.ChildNodes.Item(5).InnerText; // <Author> Node
                //tickNode.ChildNodes.Item(6).InnerText; // <Title> Node
                //tickNode.ChildNodes.Item(7).InnerText; // <Text> Node

                Tick tick = new Tick();
                tick.Id = int.Parse(tickNode.ChildNodes.Item(0).InnerText);
                tick.EventId = int.Parse(tickNode.ChildNodes.Item(1).InnerText);
                tick.IsPublished = bool.Parse(tickNode.ChildNodes.Item(2).InnerText);
                tick.Reported = DateTime.Parse(tickNode.ChildNodes.Item(3).InnerText);
                tick.Modified = DateTime.Parse(tickNode.ChildNodes.Item(4).InnerText);
                tick.Author = tickNode.ChildNodes.Item(5).InnerText;
                tick.Title = tickNode.ChildNodes.Item(6).InnerText;
                tick.Text = tickNode.ChildNodes.Item(7).InnerText;

                ListViewItem listViewItem = new ListViewItem();

                listViewItem.Content = new
                {
                    Id = tick.Id,
                    EventId = tick.EventId,
                    IsPublished = tick.IsPublished,
                    Reported = tick.Reported.ToString("HH:mm"),
                    Modified = tick.Modified.ToString("HH:mm"),
                    Author = tick.Author,
                    Title = tick.Title,
                    Text = tick.Text
                };
                listViewItem.Tag = tick;

                listViewItem.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(listViewItem_MouseDoubleClick);

                currListView.Items.Add(listViewItem);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            refreshListView();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTicketVerfassen_Click(object sender, RoutedEventArgs e)
        {
            TickErstellen te = new TickErstellen();
            te.ShowDialog(); // Öffnen so das das MainWindow nicht mehr anklickbar ist.
        }
        private void listViewItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListViewItem selected = (ListViewItem)sender;
            Tick tick = (Tick)(selected.Tag);

            MessageBox.Show("Ticketinformationen:\n"
                + "ID: " + tick.Id + "\n"
                + "EventId: " + tick.EventId + "\n"
                + "IsPublished: " + tick.IsPublished + "\n"
                + "Reported: " + tick.Reported + "\n"
                + "Modified: " + tick.Modified + "\n"
                + "Author: " + tick.Author + "\n"
                + "Title: " + tick.Title + "\n"
                + "Text: " + tick.Text
                );
        }
        private void tabitem_GotFocus(object sender, RoutedEventArgs e)
        {
            #region Verhindere Bug

            TabItem currTabItem = (TabItem)tabControl1.SelectedItem;
            ListView currListView = (ListView)((Grid)currTabItem.Content).Children[0];

            //TODO: keine genaue Erklärung weshalb gecleared werden muss?!
            currListView.Items.Clear();

            #endregion Verhindere Bug

            refreshListView();
        }

        /// <summary>
        /// <para>Handled eingaben die an das gesamte Fenster gerichtet sind.</para>
        /// <para>Wird benötigt um das Fenster zu schließen.</para>
        /// </summary>
        /// <param name="sender">...</param>
        /// <param name="e">...</param>
        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Escape:
                    this.Close();
                    break;

                default:
                    break;
            }
        }
    }
}