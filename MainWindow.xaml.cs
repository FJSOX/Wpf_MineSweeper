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
using System.Threading;

namespace Wpf_MineSweeper
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //数据定义区
        //地雷list
        int MS_row;
        int NumberOfLandmine;//地雷数
        bool isGameOver_flag=false;
        int flagCount=0;
        ImageBrush imageFlag = new ImageBrush();
        ImageBrush imageUnknow = new ImageBrush();



        GameWindow gameWindow;

        //调试变量
        List<NormalButton> NBS=new List<NormalButton>();
        List<int> Landmines = new List<int>();


        List<CBItem> cBItems = new List<CBItem>() {
            new CBItem(){ShowItem="5×5", SelectItem=5},
            new CBItem(){ShowItem="6×6", SelectItem=6},
            new CBItem(){ShowItem="7×7", SelectItem=7},
            new CBItem(){ShowItem="8×8", SelectItem=8},
            new CBItem(){ShowItem="9×9", SelectItem=9},
            new CBItem(){ShowItem="10×10", SelectItem=10},
            new CBItem(){ShowItem="11×11", SelectItem=11},
            new CBItem(){ShowItem="12×12", SelectItem=12},
            new CBItem(){ShowItem="13×13", SelectItem=13},
            new CBItem(){ShowItem="14×14", SelectItem=14},
            new CBItem(){ShowItem="15×15", SelectItem=15},
            new CBItem(){ShowItem="16×16", SelectItem=16},
            new CBItem(){ShowItem="17×17", SelectItem=17},
            new CBItem(){ShowItem="18×18", SelectItem=18},
            new CBItem(){ShowItem="19×19", SelectItem=19},
            new CBItem(){ShowItem="20×20", SelectItem=20},
        };

        public MainWindow()
        {
            InitializeComponent();

            CB_MapSize.ItemsSource = cBItems;
            CB_MapSize.DisplayMemberPath = "ShowItem";
            CB_MapSize.SelectedValuePath = "SelectItem";

            imageFlag.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/flag.bmp", UriKind.Absolute));
            imageUnknow.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/unknow.bmp", UriKind.Absolute));
        }

        public void ShowMainWindow()
        {
            this.Show();
            //gameWindow.Close();
        }

        public void RemakeGame()
        {
            gameWindow.Close();
            NewGame();
        }

        public void QuitGame()
        {
            if (gameWindow.closeAll)
            {
                this.Close();
                Application.Current.Shutdown();
            }
            else
            {
                gameWindow.Close();
            }
        }

        void NewGame()
        {
            //判空
            if (CB_MapSize.SelectedItem == null)
            {
                MessageBox.Show("请选择地图大小！");
                return;
            }
            if (TBX_LandmineNumber.Text == null || Convert.ToInt32(TBX_LandmineNumber.Text) == 0)
            {
                Random random = new Random();
                NumberOfLandmine = random.Next() % (Convert.ToInt32(Math.Pow(((CBItem)CB_MapSize.SelectedItem).SelectItem, 2)) - 5) + 5;
            }
            else
            {
                NumberOfLandmine = Convert.ToInt32(TBX_LandmineNumber.Text);
            }

            //new GameWindow
            GameWindow.rowNumberOfLandmine = ((CBItem)CB_MapSize.SelectedItem).SelectItem;
            gameWindow = new GameWindow();

            //随机地雷表
            Random random1 = new Random();
            while (Landmines.Count < NumberOfLandmine)
            {
                int site = random1.Next() % (Convert.ToInt32(Math.Pow(((CBItem)CB_MapSize.SelectedItem).SelectItem, 2) - 1));
                if (!Landmines.Contains(site))
                {
                    Landmines.Add(site);
                }
            }

            //绘制窗体框架
            gameWindow.WP.Width = ((CBItem)CB_MapSize.SelectedItem).SelectItem * 30 + 2;
            gameWindow.WP.Height = ((CBItem)CB_MapSize.SelectedItem).SelectItem * 30 + 2;
            gameWindow.Width = ((CBItem)CB_MapSize.SelectedItem).SelectItem * 30 + 25;
            gameWindow.Height = ((CBItem)CB_MapSize.SelectedItem).SelectItem * 30 + 72;
            gameWindow.gameWindowDelegate += ShowMainWindow;
            gameWindow.gameWindowDelegate1 += RemakeGame;
            gameWindow.gameWindowDelegate2 += QuitGame;
            //gameWindow.MN.Width=
            for (int i = 0; i < ((CBItem)CB_MapSize.SelectedItem).SelectItem * ((CBItem)CB_MapSize.SelectedItem).SelectItem; i++)
            {
                NormalButton normalButton = new NormalButton() { Width = 30, Height = 30, listid = i, Background = Brushes.DarkGray, FontWeight = FontWeights.Black };
                if (Landmines.Contains(i))
                {
                    normalButton.islandmine_flag = true;
                }
                normalButton.clickhandle = NormalClick;//将NormalButton的clickhandle委托与函数绑定
                normalButton.rightClickHandle += NormalRightClick;
                gameWindow.WP.Children.Add(normalButton);
                NBS.Add(normalButton);
            }
            gameWindow.Show();

            foreach (int i in Landmines)
            {
                //为周围8个土块添加雷区标记  numberoflandminesaround++
                if (i - ((CBItem)CB_MapSize.SelectedItem).SelectItem >= 0 //判断土块序号是否存在
                && NBS[i - ((CBItem)CB_MapSize.SelectedItem).SelectItem].islandmine_flag == false) //判断是否为地雷
                {
                    NBS[i - ((CBItem)CB_MapSize.SelectedItem).SelectItem].numberoflandminesaround++;
                }
                if (i - ((CBItem)CB_MapSize.SelectedItem).SelectItem >= 0
                    && (i - ((CBItem)CB_MapSize.SelectedItem).SelectItem + 1) % ((CBItem)CB_MapSize.SelectedItem).SelectItem != 0 //判断土块序号是否存在
                    && NBS[i - ((CBItem)CB_MapSize.SelectedItem).SelectItem + 1].islandmine_flag == false) //判断是否为地雷
                {
                    NBS[i - ((CBItem)CB_MapSize.SelectedItem).SelectItem + 1].numberoflandminesaround++;
                }

                if (i + ((CBItem)CB_MapSize.SelectedItem).SelectItem < NBS.Count
                    && NBS[i + ((CBItem)CB_MapSize.SelectedItem).SelectItem].islandmine_flag == false)
                {
                    NBS[i + ((CBItem)CB_MapSize.SelectedItem).SelectItem].numberoflandminesaround++;
                }
                if (i + ((CBItem)CB_MapSize.SelectedItem).SelectItem < NBS.Count
                    && (i + ((CBItem)CB_MapSize.SelectedItem).SelectItem) % ((CBItem)CB_MapSize.SelectedItem).SelectItem != 0 //判断土块序号是否存在
                    && NBS[i + ((CBItem)CB_MapSize.SelectedItem).SelectItem - 1].islandmine_flag == false) //判断是否为地雷
                {
                    NBS[i + ((CBItem)CB_MapSize.SelectedItem).SelectItem - 1].numberoflandminesaround++;
                }

                if ((i) % ((CBItem)CB_MapSize.SelectedItem).SelectItem != 0
                   && NBS[i - 1].islandmine_flag == false)
                {
                    NBS[i - 1].numberoflandminesaround++;
                }
                if ((i) % ((CBItem)CB_MapSize.SelectedItem).SelectItem != 0
                    && i - ((CBItem)CB_MapSize.SelectedItem).SelectItem - 1 >= 0
                    && NBS[i - ((CBItem)CB_MapSize.SelectedItem).SelectItem - 1].islandmine_flag == false)
                {
                    NBS[i - ((CBItem)CB_MapSize.SelectedItem).SelectItem - 1].numberoflandminesaround++;
                }

                if ((i + 1) % ((CBItem)CB_MapSize.SelectedItem).SelectItem != 0
                   && NBS[i + 1].islandmine_flag == false)
                {
                    NBS[i + 1].numberoflandminesaround++;
                }
                if ((i + 1) % ((CBItem)CB_MapSize.SelectedItem).SelectItem != 0
                    && (i + 1) + ((CBItem)CB_MapSize.SelectedItem).SelectItem < NBS.Count
                    && NBS[(i + 1) + ((CBItem)CB_MapSize.SelectedItem).SelectItem].islandmine_flag == false)
                {
                    NBS[(i + 1) + ((CBItem)CB_MapSize.SelectedItem).SelectItem].numberoflandminesaround++;
                }//
            }

            this.Hide();
        }

        /// <summary>
        /// 确认按钮，点击新建地图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_OK_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        /// <summary>
        /// 游戏结束时调用此函数，使地图界面不可用；
        /// </summary>
        void GameOver()
        {
            VisualBrush vb = new VisualBrush(gameWindow.GD);
            Rectangle rectangle = new Rectangle();
            rectangle.Width = gameWindow.GD.ActualWidth;
            rectangle.Height = gameWindow.GD.ActualHeight;
            rectangle.Opacity = 0.75;
            rectangle.Fill = vb;
            gameWindow.Content = null;

            Grid grid = new Grid();
            grid.Background = Brushes.DarkGray;
            grid.Children.Add(rectangle);
            gameWindow.Content = grid;

            return;
        }

        /// <summary>
        /// NormalButton的Click响应，本质是一个委托
        /// </summary>
        /// <param name="listid"></param>
        public void NormalClick(int listid)
        {
            //if (isGameOver_flag)
            //{
            //    GameOver();

            //    return;
            //}

            //rightClickCount是否已标记
            if (NBS[listid].rightClickCount!=0)
            {
                return;
            }

            //是否已挖开
            if (NBS[listid].isdigged_flag==true)
            {
                return;
            }

            //是否为地雷
            if (Landmines.Contains(listid))
            {
                //NBS[listid].Background = Brushes.DarkRed;
                //NBS[listid].isdigged_flag = true;
                foreach(var i in Landmines)
                {
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource =new BitmapImage(new Uri("pack://application:,,,/images/boom.bmp", UriKind.Absolute));
                    NBS[i].Background=imageBrush;
                }

                //Thread.Sleep(100);
                //isGameOver_flag = true;                

                MessageBox.Show("你输了");

                //VisualBrush vb = new VisualBrush(gameWindow.GD);
                //Rectangle rectangle = new Rectangle();
                //rectangle.Width = gameWindow.GD.ActualWidth;
                //rectangle.Height = gameWindow.GD.ActualHeight;
                //rectangle.Opacity = 0.75;
                //rectangle.Fill = vb;
                //gameWindow.Content = null;

                //Grid grid = new Grid();
                //grid.Background = Brushes.DarkGray;
                //grid.Children.Add(rectangle);
                //gameWindow.Content = grid;
                GameOver();//必须放在1messagebox后面，原因未知

                //中止游戏
                return;
            }

            
            //扩散挖开
            List<int> normalButtons = new List<int>();//扩散表
            
            normalButtons.Add(listid);

            while (normalButtons.Count > 0)
            {
                int lid=normalButtons[0];

                //周围是否有地雷
                if (NBS[lid].numberoflandminesaround != 0)
                {
                    NBS[lid].Content = NBS[lid].numberoflandminesaround.ToString();//
                    //return;
                }

                else {
                    if (lid - ((CBItem)CB_MapSize.SelectedItem).SelectItem >= 0 //判断土块序号是否存在
                        && !normalButtons.Contains(lid - ((CBItem)CB_MapSize.SelectedItem).SelectItem) //判断是否在扩散表中
                        && NBS[lid - ((CBItem)CB_MapSize.SelectedItem).SelectItem].isdigged_flag == false //判断是否已挖开
                        && NBS[lid - ((CBItem)CB_MapSize.SelectedItem).SelectItem].islandmine_flag == false) //判断是否为地雷
                    {
                        normalButtons.Add(lid - ((CBItem)CB_MapSize.SelectedItem).SelectItem);
                    }
                    if (lid + ((CBItem)CB_MapSize.SelectedItem).SelectItem < NBS.Count
                        && !normalButtons.Contains(lid + ((CBItem)CB_MapSize.SelectedItem).SelectItem)
                        && NBS[lid + ((CBItem)CB_MapSize.SelectedItem).SelectItem].isdigged_flag == false
                        && NBS[lid + ((CBItem)CB_MapSize.SelectedItem).SelectItem].islandmine_flag == false)
                    {
                        normalButtons.Add(lid + ((CBItem)CB_MapSize.SelectedItem).SelectItem);
                    }
                    if ((lid) % ((CBItem)CB_MapSize.SelectedItem).SelectItem != 0 && !normalButtons.Contains(lid - 1)
                        && NBS[lid - 1].isdigged_flag == false && NBS[lid - 1].islandmine_flag == false)
                    {
                        normalButtons.Add(lid - 1);
                    }
                    if ((lid + 1) % ((CBItem)CB_MapSize.SelectedItem).SelectItem != 0 && !normalButtons.Contains(lid + 1)
                        && NBS[lid + 1].isdigged_flag == false && NBS[lid + 1].islandmine_flag == false)
                    {
                        normalButtons.Add(lid + 1);
                    }//
                }

                //挖开此块
                NormalButton normalButton = NBS[lid] as NormalButton;
                normalButton.isdigged_flag = true;

                normalButton.Background = Brushes.White;

                switch(normalButton.numberoflandminesaround)
                {
                    case 1:
                        normalButton.Foreground = Brushes.LightSteelBlue;
                        break;
                    case 2:
                        normalButton.Foreground = Brushes.Black;
                        break;
                    case 3:
                        normalButton.Foreground = Brushes.DarkGoldenrod;
                        break;
                    case 4:
                        normalButton.Foreground = Brushes.YellowGreen;
                        break;
                    case 5:
                        normalButton.Foreground = Brushes.Aqua;
                        break;
                    case 6:
                        normalButton.Foreground = Brushes.Fuchsia;
                        break;
                    case 7:
                        normalButton.Foreground = Brushes.Orange;
                        break;
                    case 8:
                        normalButton.Foreground = Brushes.Red;
                        break;
                }

                //normalButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));

                normalButtons.Remove(normalButtons[0]);
            }

        }

        /// <summary>
        /// 游戏胜利时调用
        /// </summary>
        void Win()
        {
            MessageBox.Show("你赢了");
            GameOver();
            return;
        }

        public void NormalRightClick(int listid)
        {
            ImageBrush imageBrush = new ImageBrush();
            switch (NBS[listid].rightClickCount)
            {
                
                case 0:
                    NBS[listid].Background=Brushes.DarkGray;
                    return;
                    //break;
                case 1:
                    
                    //imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/flag.bmp", UriKind.Absolute));
                    NBS[listid].Background = imageFlag;
                    flagCount++;
                    if (flagCount == Landmines.Count)
                    {
                        int cnt = 0;
                        foreach (var i in Landmines)
                        {
                            if (NBS[i].rightClickCount!=1)
                            {
                                break;
                            }
                            cnt++;
                        }
                        if (cnt==Landmines.Count)
                        {
                            Win();
                        }
                    }
                    break;
                case 2:
                    //ImageBrush imageBrush = new ImageBrush();
                    //imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/unknow.bmp", UriKind.Absolute));
                    NBS[listid].Background = imageUnknow;
                    flagCount--;
                    break;
            }
        }
    }

    /// <summary>
    /// 通常按钮
    /// </summary>
    public class NormalButton:Button
    {
        public int listid;//块id
        public bool islandmine_flag = false;//地雷标志
        public bool isdigged_flag=false;//是否被挖开，标志
        public int numberoflandminesaround = 0;//环绕地雷数
        public delegate void ClickHandle(int listid);
        public ClickHandle clickhandle;//click委托
        public ClickHandle rightClickHandle;//右击委托
        public int rightClickCount=0;//右击计数器
        //public bool isGameOver_flag;//

        protected override void OnClick()
        {
            base.OnClick();

            //处理列表,代理？
            clickhandle(this.listid);//调用

            //MessageBox.Show(this.Width.ToString());
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);

            if (rightClickCount<2)
            {
                rightClickCount++;
            }
            else
            {
                rightClickCount = 0;
            }


            rightClickHandle(this.listid);
        }
    }

    /// <summary>
    /// 地雷按钮
    /// </summary>
    public class LandmineButton : Button
    {
        protected override void OnClick()
        {
            base.OnClick();

            MessageBox.Show("你输了！");
        }
    }

    public class CBItem
    {
        public string ShowItem { get; set; }
        public int SelectItem { get; set; }
    }
}
