using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections;

namespace Liveticker_Client
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LiveTickerService liveTickerService = new LiveTickerService();

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
        private void addEvent(DateTime date, string title, string description, System.Windows.Media.Imaging.BitmapImage icon) //TODO: es gibt kein Image mehr... D: musst du dich bitte neu drum kümmern.
        {
            MemoryStream stream = new MemoryStream();
            //icon.Save(stream, ImageFormat.Jpeg);
            liveTickerService.addEvent(title, description, stream.ToArray(), date);
        }

        /// <summary>
        /// <para>Die Schnittstelle deleteEvent löscht eine Sportart.</para>
        /// </summary>
        /// <param name="id">ID des zu löschenden Events</param>
        private void deleteEvent(int id)
        {
            liveTickerService.deleteEvent(id);
        }

        /// <summary>
        /// <para>Die Schnittstelle deleteLiveticker löscht eine „LiveTicker” Nachricht.</para>
        /// </summary>
        /// <param name="id">ID des zu löschenden Livetickers</param>
        private void deleteLiveticker(int id)
        {
            liveTickerService.deleteTick(id);
        }

        /// <summary>
        /// <para>Die Schnittstelle publishLiveticker veröffentlicht eine „LiveTicker” Nachricht.</para>
        /// </summary>
        /// <param name="id">ID des zu veröffentlichenden Livetickers</param>
        private void publishLiveticker(int id)
        {
            liveTickerService.publishTick(id);
        }

        /// <summary>
        /// <para>Durch die Schnittstelle modifyEvent soll es möglich sein die Daten eines Events zu bearbeiten, dies beinhaltet Datum, Titel, Beschreibung und Icon.</para>
        /// </summary>
        private void modifyEvent(int id, string newTitle)
        {
            //liveTickerService.modifyEvent(id, newTitle);
        }


        private void EventBox_Loaded(object sender, RoutedEventArgs e)
        {
            EventBox.Items.Clear();
            //Event[] events = liveTickerService.getEvents();
            ArrayList events = new ArrayList(liveTickerService.getEvents()) ;
            //events.Sort();
//            LiveTickerService.
            foreach (Event ev in events)
            {
                EventBox.Items.Add(ev.text);
            }
            EventBox.SelectedIndex = 0;
            //EventBox.Sorted(true);
        }

        //private void tabControl1_Loaded(object sender, RoutedEventArgs e)
        //{
        //    tabControl1.Items.Clear();

        //    Event[] events = liveTickerService.getEvents();
        //    foreach (Event ev in events)
        //    {
        //        GridViewColumn gridviewcolumnId = new GridViewColumn();
        //        GridViewColumn gridviewcolumnEventId = new GridViewColumn();
        //        GridViewColumn gridviewcolumnIsPublished = new GridViewColumn();
        //        GridViewColumn gridviewcolumnReported = new GridViewColumn();
        //        GridViewColumn gridviewcolumnModified = new GridViewColumn();
        //        GridViewColumn gridviewcolumnAuthor = new GridViewColumn();
        //        GridViewColumn gridviewcolumnTitle = new GridViewColumn();
        //        GridViewColumn gridviewcolumnText = new GridViewColumn();
        //        GridView gridview = new GridView();
        //        ListView listview = new ListView();
        //        Grid grid = new Grid();
        //        TabItem tabitem = new TabItem();

        //        gridviewcolumnId.Header = "ID";
        //        gridviewcolumnId.DisplayMemberBinding = new Binding("Id");
        //        gridviewcolumnEventId.Header = "Event ID";
        //        gridviewcolumnEventId.DisplayMemberBinding = new Binding("EventId");

        //        gridviewcolumnIsPublished.Header = "Veröffentlicht?";
        //        DataTemplate dt_isPublished = new DataTemplate();
        //        FrameworkElementFactory factory = new FrameworkElementFactory(typeof(CheckBox));
        //        factory.SetValue(CheckBox.MarginProperty, new Thickness(40, 0, 40, 0));
        //        Binding binding = new Binding("IsPublished");
        //        binding.Mode = BindingMode.OneWay;
        //        factory.SetBinding(CheckBox.IsCheckedProperty, binding);
        //        dt_isPublished.VisualTree = factory;
        //        gridviewcolumnIsPublished.CellTemplate = dt_isPublished;

        //        gridviewcolumnReported.Header = "Zeit";
        //        gridviewcolumnReported.DisplayMemberBinding = new Binding("Reported");
        //        gridviewcolumnModified.Header = "Zuletzt geändert";
        //        gridviewcolumnModified.DisplayMemberBinding = new Binding("Modified");
        //        gridviewcolumnAuthor.Header = "Autor";
        //        gridviewcolumnAuthor.DisplayMemberBinding = new Binding("Author");
        //        gridviewcolumnTitle.Header = "Titel";
        //        gridviewcolumnTitle.DisplayMemberBinding = new Binding("Title");
        //        gridviewcolumnText.Header = "Beschreibung";
        //        gridviewcolumnText.DisplayMemberBinding = new Binding("Text");

        //        //gridview.Columns.Add(gridviewcolumnId);
        //        //gridview.Columns.Add(gridviewcolumnEventId);
        //        //gridview.Columns.Add(gridviewcolumnIsPublished);
        //        gridview.Columns.Add(gridviewcolumnReported);
        //        //gridview.Columns.Add(gridviewcolumnModified);
        //        //gridview.Columns.Add(gridviewcolumnAuthor);
        //        gridview.Columns.Add(gridviewcolumnTitle);
        //        gridview.Columns.Add(gridviewcolumnText);

        //        listview.View = gridview;

        //        grid.Children.Add(listview);

        //        TextBlock tabitemheader = new TextBlock();
        //        tabitemheader.Text = ev.text.ToUpper();
        //        tabitemheader.ToolTip = ev.description;

        //        tabitem.Header = tabitemheader;
        //        tabitem.Tag = ev;
        //        tabitem.Content = grid;
        //        tabitem.GotFocus += new RoutedEventHandler(tabitem_GotFocus);

        //        tabControl1.Items.Add(tabitem);
        //    }

        //    refreshListView();
        //}

        //private void refreshListView()
        //{
        //    TabItem currTabItem = (TabItem)tabControl1.SelectedItem;
        //    if (currTabItem == null)
        //    {
        //        return; // Nichts zum Aktualisieren vorhanden.
        //    }
        //    ListView currListView = (ListView)((Grid)currTabItem.Content).Children[0];

        //    currListView.Items.Clear();

        //    Tick[] ticks = liveTickerService.getAllTicksAdmin(((Event)(currTabItem.Tag)).id);
        //    foreach (Tick tick in ticks)
        //    {
        //        ListViewItem listViewItem = new ListViewItem();

        //        listViewItem.Content = new
        //        {
        //            Id = tick.id,
        //            EventId = tick.event_id,
        //            IsPublished = tick.is_published,
        //            Reported = tick.reported.ToString("HH:mm"),
        //            Modified = tick.modified.ToString("HH:mm"),
        //            Author = tick.author,
        //            Title = tick.title,
        //            Text = tick.message
        //        };
        //        listViewItem.Tag = tick;

        //        listViewItem.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(listViewItem_MouseDoubleClick);

        //        currListView.Items.Add(listViewItem);
        //    }
        //}

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Vielleicht besser this.tabControl1_Loaded(sender, e);
            //refreshListView();
        }

        private void btnTicketVerfassen_Click(object sender, RoutedEventArgs e)
        {
            TickErstellen te = new TickErstellen(EventBox.SelectedIndex);
            te.ShowDialog();

           // this.tabControl1_Loaded(sender, e);
        }

        private void listViewItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListViewItem selected = (ListViewItem)sender;
            Tick tick = (Tick)(selected.Tag);

            TickUpdaten tu = new TickUpdaten(ref tick);
            tu.ShowDialog();

           // this.tabControl1_Loaded(sender, e);
        }

        private void EventBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            EventHinzufuegen dialog = new EventHinzufuegen();
            dialog.Show();
        }

        

        //private void tabitem_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    #region Verhindere Bug

        //    TabItem currTabItem = (TabItem)tabControl1.SelectedItem;
        //    ListView currListView = (ListView)((Grid)currTabItem.Content).Children[0];

        //    //TODO: keine genaue Erklärung weshalb gecleared werden muss?!
        //    currListView.Items.Clear();

        //    #endregion Verhindere Bug

        //    refreshListView();
        //}
    }
}