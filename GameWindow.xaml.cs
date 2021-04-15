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
        public static int widthOfWP;
        //public static int heightOfWP;
        //public static int widthOfGameWindow;
        //public static int heightOfGameWindow;
        public static int rowNumberOfLandmine;
        public static int numberOfLandmine;

        public Random randomLandmine;
        public List<int> listLandmine;
        public List<int> digged;

        public GameWindow()
        {
            InitializeComponent();
        }
    }
}
