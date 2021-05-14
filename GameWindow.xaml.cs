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
using System.Windows.Shapes;

namespace Wpf_MineSweeper
{
    /// <summary>
    /// GameWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GameWindow : Window
    {
        //定义区
        public static int widthOfWP;//
        //public static int heightOfWP;
        //public static int widthOfGameWindow;
        //public static int heightOfGameWindow;
        public static int rowNumberOfLandmine;//行数
        public static int numberOfLandmine;//地雷数

        public bool closeAll=true;

        public Random randomLandmine;
        public List<int> listLandmine;//地雷列表
        public List<int> digged;//已挖开土块列表

        public GameWindow()
        {
            InitializeComponent();
        }

        public delegate void GameWindowDelegate();
        public GameWindowDelegate gameWindowDelegate;
        public GameWindowDelegate gameWindowDelegate1;
        public GameWindowDelegate gameWindowDelegate2;

        /// <summary>
        /// 返回主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //this.Hide();//用于进入if (gameWindow.IsVisible)分支
            closeAll = false;
            gameWindowDelegate();
            this.Close();
        }

        /// <summary>
        /// 重新开始游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            closeAll = false;
            gameWindowDelegate1();                    
        }

        /// <summary>
        /// 退出任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            closeAll = true;
            gameWindowDelegate2();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            gameWindowDelegate2();
        }
    }
}
