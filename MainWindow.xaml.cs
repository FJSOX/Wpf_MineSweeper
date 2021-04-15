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
        int NumberOfLandMine;


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
        }

        private void BTN_OK_Click(object sender, RoutedEventArgs e)
        {
            //new GameWindow
            GameWindow.rowNumberOfLandmine = ((CBItem)CB_MapSize.SelectedItem).SelectItem;
            GameWindow gameWindow = new GameWindow();
            
            //gameWindow.WP.Width = ((CBItem)CB_MapSize.SelectedItem).SelectItem * 31;
            //gameWindow.WP.Height = ((CBItem)CB_MapSize.SelectedItem).SelectItem * 31;
            //gameWindow.Width = ((CBItem)CB_MapSize.SelectedItem).SelectItem * 31+6;
            //gameWindow.Height = ((CBItem)CB_MapSize.SelectedItem).SelectItem * 31+30;
            for (int i = 0; i < ((CBItem)CB_MapSize.SelectedItem).SelectItem* ((CBItem)CB_MapSize.SelectedItem).SelectItem; i++)
            {
                gameWindow.WP.Children.Add(new NormalButton() { Width = 30, Height = 30 });
            }
            gameWindow.Show();

            this.Hide();
        }
    }

    /// <summary>
    /// 通常按钮
    /// </summary>
    public class NormalButton:Button
    {
        protected override void OnClick()
        {
            base.OnClick();

            MessageBox.Show(this.Width.ToString());
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
