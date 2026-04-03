using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SCP_Speedrun_Tool
{
    /// <summary>
    /// Interaction logic for MapPreview.xaml
    /// </summary>
    public partial class MapPreview : Window
    {
        string mapdata = @"Assets\serial.json";
        mapobject Map = new mapobject();

        MainWindow main = new MainWindow();

        string tunnelinfo = "";

        string forestinfo = "";

        Grid select = new Grid { Background = Brushes.White, Opacity = 0.3 };

        public MapPreview(MainWindow win)
        {
            main = win;

            Closing += Cleanse;

            KeyDown += AltCheck;

            //Loaded += MapLoaded;

            InitializeComponent();
        }

        private async void AltCheck(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System && e.SystemKey == Key.F4)
            {
                e.Handled = true;
                Hide();
            }
        }

        public void StartGeneration()
        {
            PlotRooms();
            LoadSeed();
            MakeTunnels();
            MakeForest();
            //CloseLast();
            main.loaded = true;
            SetWindow(main);
        }

        public void Cleanse(object? sender, CancelEventArgs e)
        {
            main.mapopen = false;
            //main.map = null;
            //main = null;
            Map = null;
            //Loaded -= MapLoaded;
            //Closing -= Cleanse;

            int ix = 0;

            int iy = 0;

            // fuckass memory leak fix

            for (int i = 0; i < 18 * 18; i++)
            {

                if (ix > 16)
                {
                    iy++;
                    ix = 0;
                }
                else
                    ix++;

                try
                {
                    Image g = ((Grid)container.Children[((18 * iy) + (18 - ix)) - 5]).Children[0] as Image;
                    g.Source = null;
                    try
                    {
                        Image g1 = ((Grid)container.Children[((18 * iy) + (18 - ix)) - 5]).Children[1] as Image;
                        g1.Source = null;
                    }
                    catch { }
                    try
                    {
                        Image g1 = ((Grid)container.Children[((18 * iy) + (18 - ix)) - 5]).Children[2] as Image;
                        g1.Source = null;
                    }
                    catch { }
                }
                catch { }

                container.Children.Clear();
            }

            tunnels.Children.Clear();
            forest.Children.Clear();

            UpdateLayout();

            if (e != null)
            {
                e.Cancel = true;
                Hide();
            }

            GC.Collect();
        }

        private void MapLoaded(object sender, RoutedEventArgs e)
        {
            main.loaded = true;
        }

        private void CloseLast()
        {
            if (main.map != null)
            {
                main.map.Close();
                main.map = null;
            }
        }

        public void SetWindow(MainWindow win)
        {
            if ((main.Left + (2 * main.Width)) > (SystemParameters.FullPrimaryScreenWidth - main.Width))
            {
                Left = (main.Left - (2 * main.Width));
            }
            else
                Left = main.Left + main.Width;

            Top = main.Top;
        }

        public void LoadSeed()
        {
            sint.Text = $"Seed: {Map.seedValue}";
            sstr.Text = $"Vanilla Seed: {Map.seedString}";
            sls.Text = $"Loading Screen: {Map.loadingScreen}";
            s106.Text = $"106 State: {Map.state106}";
            sa.Text = $"Initial Angle: {Map.angle}";
        }

        public async void PlotRooms()
        {
            Map = JsonSerializer.Deserialize<mapobject>(File.ReadAllText(mapdata));

            Title = $"Map Preview: {Map.seedValue}";

            container.Children.Remove(select);

            container.Children.Add(select);

            //initialize coordinate system for map checking

            int cx = 0;

            int cy = 0;

            for (int i = 0; i < (18 * 18) + 16; i++)
            {
                Grid g = new Grid();
                if (cx > 16)
                {
                    cx = 0;
                    cy++;
                }
                else
                {
                    cx++;
                }
                Grid.SetColumn(g, cx);
                Grid.SetRow(g, cy);

                container.Children.Add(g);
                //g.Background = Brushes.Black;
                //await Task.Delay(10);
            }

            // plot rooms

            BitmapImage room2 = new BitmapImage();
            room2.BeginInit();
            room2.UriSource = new Uri("/roomd/room2.png", UriKind.Relative);
            room2.EndInit();

            BitmapImage room2c = new BitmapImage();
            room2c.BeginInit();
            room2c.UriSource = new Uri("/roomd/room2c.png", UriKind.Relative);
            room2c.EndInit();

            BitmapImage room3 = new BitmapImage();
            room3.BeginInit();
            room3.UriSource = new Uri("/roomd/room3.png", UriKind.Relative);
            room3.EndInit();

            BitmapImage room4 = new BitmapImage();
            room4.BeginInit();
            room4.UriSource = new Uri("/roomd/room4.png", UriKind.Relative);
            room4.EndInit();

            BitmapImage room1 = new BitmapImage();
            room1.BeginInit();
            room1.UriSource = new Uri("/roomd/room1.png", UriKind.Relative);
            room1.EndInit();

            for (int i = 0; i < Map.rooms.Count; i++)
            {
                Image rimg = new Image();

                string type = "";

                switch (Map.rooms[i].name)
                {
                    case "start":
                        type = "room1";
                        break;
                    case "exit1":
                        type = "room1";
                        break;
                    case "gateaentrance":
                        type = "room1";
                        break;
                    case "lockroom":
                        type = "room2c";
                        break;
                    case "room1123":
                        type = "room2";
                        break;
                    case "endroom":
                        type = "room1";
                        break;
                    case "room012":
                        type = "room2";
                        break;
                    case "room205":
                        type = "room1";
                        break;
                    case "room2closets":
                        type = "room2";
                        break;
                    case "checkpoint2":
                        type = "room2";
                        break;
                    case "checkpoint1":
                        type = "room2";
                        break;
                    case "room1162":
                        type = "room2c";
                        break;
                    case "room2cafeteria":
                        type = "room2";
                        break;
                    case "tunnel":
                        type = "room2";
                        break;
                    case "tunnel2":
                        type = "room2";
                        break;
                    case "room860":
                        forestinfo = Map.rooms[i].info;
                        type = "room2";
                        break;
                    case "roompj":
                        type = "room1";
                        break;
                    case "lockroom3":
                        type = "room2c";
                        break;
                    case "room035":
                        type = "room1";
                        break;
                    case "room049":
                        type = "room2";
                        break;
                    case "room106":
                        type = "room1";
                        break;
                    case "room513":
                        type = "room3";
                        break;
                    case "room966":
                        type = "room3";
                        break;
                    case "coffin":
                        type = "room1";
                        break;
                    case "endroom2":
                        type = "room1";
                        break;
                    case "room079":
                        type = "room1";
                        break;
                    case "medibay":
                        type = "room2";
                        break;
                    case "testroom":
                        type = "room2";
                        break;
                    case "lockroom2":
                        type = "room2c";
                        break;
                    case "room2tunnel":
                        tunnelinfo = Map.rooms[i].info;
                        type = "room2";
                        break;
                    default:
                        try
                        {
                            if (Map.rooms[i].name.Substring(0, 6).ToLower() == "room2c")
                            {
                                type = "room2c";
                                break;
                            }
                        }
                        catch { }

                        try
                        {
                            type = Map.rooms[i].name.Substring(0, 5);
                        }
                        catch
                        {
                            type = "room1";
                        }
                        break;
                }



                if (type == "room2")
                {
                    rimg = new Image() { Source = room2 };
                }
                else if (type == "room3")
                {
                    rimg = new Image() { Source = room3 };
                }
                else if (type == "room1")
                {
                    rimg = new Image() { Source = room1 };
                }
                else if (type == "room2c")
                {
                    rimg = new Image() { Source = room2c };
                }
                else if (type == "room4")
                {
                    rimg = new Image() { Source = room4 };
                }

                Grid g = ((Grid)container.Children[((18 * Map.rooms[i].y) + (18 - Map.rooms[i].x)) - 1]);
                //g.Uid = $"{Map.rooms[i].name},{type},{Map.rooms[i].x},{Map.rooms[i].y},{Map.rooms[i].angle},{Map.rooms[i].en}";
                //g.MouseEnter += ReadInfo;

                //Grid.SetColumn(rimg, 17 - Map.rooms[i].x);
                //Grid.SetRow(rimg, Map.rooms[i].y);
                RotateTransform rot = new RotateTransform();
                if (type == "room3" || type == "room1")
                    rot = new RotateTransform((180 - Map.rooms[i].angle));
                else if (type == "room2c")
                    rot = new RotateTransform((90 - Map.rooms[i].angle));
                else
                    rot = new RotateTransform(Map.rooms[i].angle);
                rimg.Uid = $"{Map.rooms[i].name},{type},{Map.rooms[i].x},{Map.rooms[i].y},{Map.rooms[i].angle},{Map.rooms[i].en},{Map.rooms[i].dh},{Map.rooms[i].dv},{Map.rooms[i].ek}";
                rimg.RenderTransformOrigin = new Point(0.5, 0.5);
                rimg.RenderTransform = rot;
                rimg.MouseEnter += ReadInfo;
                if (Map.rooms[i].name == "room2tunnel")
                {
                    rimg.MouseEnter += ShowTunnels;
                    rimg.MouseMove += OpenTunnels;
                    rimg.MouseLeave += CloseTunnels;
                    tunnelmap.Visibility = Visibility.Hidden;
                }
                if (Map.rooms[i].name == "room860")
                {
                    rimg.MouseEnter += ShowForest;
                    rimg.MouseMove += OpenForest;
                    rimg.MouseLeave += CloseForest;
                    forestmap.Visibility = Visibility.Hidden;
                }

                g.Children.Add(rimg);
            }

            ScanRooms();

            UpdateLayout();
        }

        public async void ScanRooms()
        {
            int ix = 1;

            int iy = 11;
            // scan hcz and find first tunnel room for PD exit location
            for (int i = 0; i < 18 * 5; i++)
            {
                if (ix > 16)
                {
                    ix = 1;
                    iy--;
                }
                else
                    ix++;

                try
                {
                    Image g = ((Grid)container.Children[((18 * iy) + (18 - ix)) - 1]).Children[0] as Image;

                    string[] rinfo = g.Uid.Split(',');

                    if (rinfo[0] == "tunnel")
                    {
                        TextBlock t = new TextBlock() { Text = "x", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, IsHitTestVisible = false, Foreground = Brushes.White };
                        ((Grid)g.Parent).Children.Add(t);
                        break;
                    }
                }
                catch { }
            }

            ix = 0;

            iy = 0;

            // text placement

            for (int i = 0; i < 18 * 18; i++)
            {
                if (ix > 16)
                {
                    iy++;
                    ix = 0;
                }
                else
                    ix++;

                try
                {
                    Image g = ((Grid)container.Children[((18 * iy) + (18 - ix)) - 1]).Children[0] as Image;

                    if (g == null) { }

                    string[] rinfo = g.Uid.Split(',');

                    string add = "";

                    switch (rinfo[0])
                    {
                        case "914":
                            add = "914";
                            break;
                        case "room2testroom2":
                            add = "K2";
                            break;
                        case "room2closets":
                            add = "K1";
                            break;
                        case "room2storage":
                            add = "K1";
                            break;
                        case "room2scps2":
                            add = "14\n99";
                            break;
                        case "room2sl":
                            add = "CAM";
                            break;
                        case "room1162":
                            add = "11\n62";
                            break;
                        case "room1123":
                            add = "11\n23";
                            break;
                        case "roompj":
                            add = "372";
                            break;
                        case "008":
                            add = "008";
                            break;
                        case "room049":
                            add = "049";
                            break;
                        case "room106":
                            add = "106";
                            break;
                        case "coffin":
                            add = "895";
                            break;
                        case "room966":
                            add = "966";
                            break;
                        case "room079":
                            add = "079";
                            break;
                        case "room035":
                            add = "035";
                            break;
                        case "room860":
                            add = "860";
                            break;
                        case "room2tunnel":
                            add = "500";
                            break;
                        case "room2shaft":
                            add = "K3";
                            break;
                        case "room2servers":
                            add = "096";
                            break;
                        case "room2ccont":
                            add = "EC";
                            break;
                        case "exit1":
                            add = "B";
                            break;
                        case "gateaentrance":
                            add = "A";
                            break;
                        case "room2sroom":
                            add = "K5";
                            break;
                        case "room2cafeteria":
                            add = "294";
                            break;
                        case "room2toilets":
                            add = "WC";
                            break;
                        case "room2nuke":
                            add = "B2";
                            break;
                    }

                    int ax = 0;
                    int ay = 0;

                    HorizontalAlignment ha = HorizontalAlignment.Center;
                    VerticalAlignment va = VerticalAlignment.Center;

                    for (int q = 0; q < 4; q++)
                    {
                        va = VerticalAlignment.Center;
                        ha = HorizontalAlignment.Center;
                        switch (q)
                        {
                            case 0:
                                ax = 0;
                                ay = -1;
                                va = VerticalAlignment.Bottom;
                                break;
                            case 1:
                                ax = 0;
                                ay = 1;
                                va = VerticalAlignment.Top;
                                break;
                            case 2:
                                ax = -1;
                                ay = 0;
                                ha = HorizontalAlignment.Left;
                                break;
                            case 3:
                                ax = 1;
                                ay = 0;
                                ha = HorizontalAlignment.Right;
                                break;
                        }
                        if (((Grid)container.Children[((18 * (iy + ay)) + (18 - (ix + ax))) - 1]).Children.Count == 0)
                        {
                            if (add != "")
                            {
                                TextBlock b = new TextBlock { Text = add, Foreground = Brushes.White, HorizontalAlignment = ha, VerticalAlignment = va, FontSize = 10, TextWrapping = TextWrapping.Wrap, TextAlignment = TextAlignment.Center };
                                ((Grid)container.Children[((18 * (iy + ay)) + (18 - (ix + ax))) - 1]).Children.Add(b);
                            }
                            break;
                        }
                    }
                }
                catch { }
            }

            ix = 0;

            iy = 0;

            //scan all rooms for symbol adding
            for (int i = 0; i < 18 * 18; i++)
            {
                if (ix > 16)
                {
                    iy++;
                    ix = 0;
                }
                else
                    ix++;

                try
                {
                    Image g = ((Grid)container.Children[((18 * iy) + (18 - ix)) - 1]).Children[0] as Image;

                    if (g == null) { }

                    string[] rinfo = g.Uid.Split(',');

                    RotateTransform r = null;

                    switch (rinfo[0])
                    {
                        case "room049":
                            g.Tag = "b2d";
                            break;
                        case "room2tunnel":
                            g.Tag = "b2d";
                            break;
                        case "room2servers":
                            g.Tag = "block";
                            break;
                        case "testroom":
                            g.Tag = "block";
                            break;
                        case "checkpoint1":
                            g.Tag = "block";
                            break;
                        case "checkpoint2":
                            g.Tag = "block";
                            break;
                        case "room2gw":
                            g.Tag = "block";
                            break;
                        case "room860":
                            g.Tag = "block";
                            break;
                        case "room2doors":
                            g.Tag = "block";
                            break;
                        case "room2tesla_lcz":
                            g.Tag = "zap";
                            break;
                        case "room2tesla_hcz":
                            g.Tag = "zap";
                            break;
                        case "room2tesla":
                            g.Tag = "zap";
                            break;
                        case "room3storage":
                            g.Tag = "3block";
                            r = new RotateTransform(int.Parse(rinfo[4]));
                            break;
                        case "room966":
                            g.Tag = "3block";
                            break;
                        case "room513":
                            g.Tag = "3block";
                            break;
                        case "room3servers2":
                            g.Tag = "3block";
                            break;
                        case "room3gw":
                            g.Tag = "3block";
                            break;
                        case "room2pit":
                            g.Tag = "pit";
                            r = new RotateTransform(180 - int.Parse(rinfo[4]));
                            break;
                        case "room2_4":
                            g.Tag = "pit";
                            break;
                        case "room2storage":
                            g.Tag = "2";
                            break;
                        case "room2scps":
                            g.Tag = "2";
                            break;
                        case "room2scps2":
                            g.Tag = "1tf";
                            r = new RotateTransform(180 - int.Parse(rinfo[4]));
                            break;
                        case "room1123":
                            g.Tag = "1t";
                            r = new RotateTransform(int.Parse(rinfo[4]));
                            break;
                        case "room2testroom2":
                            g.Tag = "1t";
                            r = new RotateTransform(int.Parse(rinfo[4]));
                            break;
                        case "room2nuke":
                            g.Tag = "1";
                            break;
                        case "room2toilets":
                            g.Tag = "1";
                            r = new RotateTransform(int.Parse(rinfo[4]) - 180);
                            break;
                        case "room2cafeteria":
                            g.Tag = "1t";
                            r = new RotateTransform(180 - int.Parse(rinfo[4]));
                            break;
                        case "room2sroom":
                            g.Tag = "1";
                            r = new RotateTransform(int.Parse(rinfo[4]) - 180);
                            break;
                        case "room2poffices":
                            g.Tag = "12d";
                            r = new RotateTransform(180 - int.Parse(rinfo[4]));
                            break;
                        case "medibay":
                            g.Tag = "1t";
                            break;
                        case "room2closets":
                            g.Tag = "closet";
                            break;
                        case "room1archive":
                            g.Tag = "arch";
                            break;
                        case "room012":
                            g.Tag = "1t";
                            break;
                        case "room2sl":
                            g.Tag = "1t";
                            r = new RotateTransform(int.Parse(rinfo[4]));
                            break;
                        case "room2shaft":
                            g.Tag = "1tf";
                            r = new RotateTransform(180 - int.Parse(rinfo[4]));
                            break;
                        default:
                            g.Tag = null;
                            break;
                    }

                    switch (rinfo[5])
                    {
                        case "106sinkhole":
                            g.Tag = "sink";
                            break;
                        case "endroom106":
                            g.Tag = "sink";
                            break;
                        case "coffin106":
                            g.Tag = "sink";
                            break;
                    }

                    switch (rinfo[8])
                    {
                        case "106sinkhole":
                            g.Tag = "sink";
                            break;
                        case "endroom106":
                            g.Tag = "sink";
                            break;
                        case "coffin106":
                            g.Tag = "sink";
                            break;
                    }

                    if (g.Tag != null)
                    {
                        if (r == null)
                        {
                            switch (g.Tag)
                            {
                                case "pit":
                                    r = new RotateTransform(180 - int.Parse(rinfo[4]));
                                    break;
                                case "1t":
                                    r = new RotateTransform(180 - int.Parse(rinfo[4]));
                                    break;
                                case "3block":
                                    r = new RotateTransform(int.Parse(rinfo[4]) + 90);
                                    break;
                                default:
                                    r = new RotateTransform(int.Parse(rinfo[4]));
                                    break;
                            }
                        }
                        else
                        {

                        }

                        Image s = new Image() { Source = new BitmapImage(new Uri($@"roomd/{g.Tag}.png", UriKind.Relative)), IsHitTestVisible = false, RenderTransformOrigin = new Point(0.5, 0.5), RenderTransform = r };
                        ((Grid)g.Parent).Children.Add(s);
                    }
                }
                catch { }
            }

            ix = 0;

            iy = 0;

            // place open / auto close images
            for (int i = 0; i < 18 * 18; i++)
            {
                if (ix > 16)
                {
                    iy++;
                    ix = 0;
                }
                else
                    ix++;
                try
                {
                    string st = ""; //door state

                    Image g = ((Grid)container.Children[((18 * iy) + (18 - ix)) - 1]).Children[0] as Image;

                    string[] rinfo = g.Uid.Split(',');

                    switch (rinfo[6])
                    {
                        case "1":
                            st = "dho";
                            break;
                        case "2":
                            st = "dhoa";
                            break;
                    }

                    switch (rinfo[7])
                    {
                        case "1":
                            st = "dvo";
                            break;
                        case "2":
                            st = "dvoa";
                            break;
                    }

                    Image s = new Image() { Source = new BitmapImage(new Uri($@"roomd/{st}.png", UriKind.Relative)), IsHitTestVisible = false };
                    ((Grid)g.Parent).Children.Add(s);
                }
                catch { }
            }
        }

        public async void MakeTunnels()
        {
            string t = tunnelinfo.Substring(8);
            char[] rooms = tunnelinfo.ToCharArray();

            int x = 19;
            int y = 0;

            //TextBlock te = new TextBlock();
            //Grid.SetRowSpan(te, 6);
            //Grid.SetColumnSpan(te, 6);
            //tunnels.Children.Add(te);

            foreach (char r in rooms)
            {
                x--;

                Grid g = new Grid { Background = Brushes.White };

                switch (r)
                {
                    case '|':
                        y++;
                        x = 19;
                        break;
                    case 'X':
                        Grid.SetColumn(g, x);
                        Grid.SetRow(g, y);
                        tunnels.Children.Add(g);
                        break;
                    case 'E':
                        Grid.SetColumn(g, x);
                        Grid.SetRow(g, y);
                        g.Background = Brushes.Yellow;
                        tunnels.Children.Add(g);
                        break;
                    case 'H':
                        Grid.SetColumn(g, x);
                        Grid.SetRow(g, y);
                        g.Background = Brushes.PaleVioletRed;
                        tunnels.Children.Add(g);
                        break;
                }
            }
        }

        public async void MakeForest()
        {
            string f = forestinfo.Substring(7);
            char[] rooms = f.ToCharArray();

            int x = -1;
            int y = 0;

            foreach (char r in rooms)
            {
                x++;

                Grid g = new Grid { Background = Brushes.White };

                switch (r)
                {
                    case '|':
                        y++;
                        x = -1;
                        break;
                    case 'X':
                        Grid.SetColumn(g, x);
                        Grid.SetRow(g, y);
                        forest.Children.Add(g);
                        break;
                    case 'H':
                        Grid.SetColumn(g, x);
                        Grid.SetRow(g, y);
                        g.Background = Brushes.PaleVioletRed;
                        forest.Children.Add(g);
                        break;
                }
            }
        }

        private void OpenTunnels(object sender, MouseEventArgs e)
        {
            Point mp = e.GetPosition(this);
            tunnelmap.RenderTransform = new TranslateTransform { X = mp.X - (114 / 2), Y = mp.Y - 120 };
        }

        private void CloseTunnels(object sender, MouseEventArgs e)
        {
            tunnelmap.Visibility = Visibility.Hidden;
        }

        private void ShowTunnels(object sender, MouseEventArgs e)
        {
            tunnelmap.Visibility = Visibility.Visible;
        }

        private void OpenForest(object sender, MouseEventArgs e)
        {
            Point mp = e.GetPosition(this);
            forestmap.RenderTransform = new TranslateTransform { X = mp.X, Y = mp.Y + 24 };
        }

        private void ShowForest(object sender, MouseEventArgs e)
        {
            forestmap.Visibility = Visibility.Visible;
        }

        private void CloseForest(object sender, MouseEventArgs e)
        {
            forestmap.Visibility = Visibility.Hidden;
        }

        private void ReadInfo(object sender, MouseEventArgs e)
        {
            UIElement rimg = sender as UIElement;

            string[] data = rimg.Uid.Split(',');

            rname.Text = $"Name: {data[0]}";
            rshp.Text = $"Shape: {data[1]}";
            rx.Text = $"X Coor.: {data[2]}";
            ry.Text = $"Y Coor.: {data[3]}";
            ra.Text = $"Angle: {data[4]}";
            ren.Text = $"Events: {data[5]} / {data[8]}";

            Grid.SetColumn(select, 17 - int.Parse(data[2]));
            Grid.SetRow(select, int.Parse(data[3]));
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseProgram(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
