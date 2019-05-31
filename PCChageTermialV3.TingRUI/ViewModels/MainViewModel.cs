using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using ServiceStack;
using System;
using System.Windows;
using System.Windows.Media;
using TingRUI.Data.Models.DataTemplate;
using TingRUI.Data.JustEnum;
using System.Collections.Generic;

namespace PCChageTermialV3.TingRUI.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    internal class MainViewModel : ViewModelBase
    {
        public string AppInfo { get; set; } = string.Format("1.{0} App开发始于{1}",
            App.AppDescription,
            App.StartAt.ToLongDateString()
        );

        public string TodaysBackImage { get; } = AutoImageSelector();

        public ObservableCollection<ModulizedBtn> AcceptModuels { get; set; }
            = new ObservableCollection<ModulizedBtn>();

        /* 动态配置主界面左边系 【统菜单栏】 */
        public ObservableCollection<ModualizedMenu> AcceptMenus { get; set; }
            = new ObservableCollection<ModualizedMenu>();

        /// <summary>
        /// 初始化一个 VM实体 继承自 ViewModelBase MvvmLight框架基类
        /// </summary>
        public MainViewModel()
        {
            // 一次性初始化所有UI模块
            InitialAllFuckingModules();

            FuncModuleCMD = new RelayCommand(() =>
            {
                MessageBox.Show("测试【事件转命令】成功...");
            });

            ChangeBgColorCMD = new RelayCommand<Object>( Idx => {
                App.Current.MainWindow.Background = Brushes.Yellow;
            });
        }

        private static string AutoImageSelector()
        {
            var JpgFile = string.Empty;
            DayOfWeek NowDay = DateTime.Now.DayOfWeek;
            switch (NowDay)
            {
                case DayOfWeek.Friday:
                case DayOfWeek.Monday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                    JpgFile = "MainBg3-Programer.Jpg";
                    break;
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                default:
                    JpgFile = "MainBg2-TonyStark.JPG";
                    break;
            }
            var Resource_Dir_File = Path.Combine("~/".MapProjectPath(),$"/Resources/{JpgFile}");
            return Resource_Dir_File;
        }

        /* 添加左边侧边栏下拉子菜单: 添加2级子菜单绑定到子类Title或者SubTitle字段 */
        private void InitialMenuSubNodes()
        {
            // LINQ: 内存分页添加
            ModualizedMenu DockerLast = this.AcceptMenus.LastOrDefault();
            if (DockerLast == null) return;
            var range2Add = AcceptModuels
                    .Where(o => o.gType == DockerLast.gTypeL1)
                    .ToList();
            DockerLast.MenuSublines.AddRange(range2Add);
        }

        /// <summary>
        /// 一次性初始化所有UI模块
        /// </summary>
        void InitialAllFuckingModules()
        {
            IEnumerable<ModulizedBtn> data = ModulizedBtn.FakeData();
            foreach (var item in data)
            {
                /* 添加顶部【***所有基础功能***】菜单栏 */
                AcceptModuels.Add(item);
            }
            
            var menus = ModualizedMenu.FakeData();
            for (int i = 0; i < menus.Count; i++)
            {
                // 先把菜单添加到 UI
                AcceptMenus.Add(menus[i]);
                // 再添加2级子节点 SubNodes
                InitialMenuSubNodes();
            }
        }

        #region  WPF事件转命令
        public RelayCommand FuncModuleCMD { get; set; }
        public RelayCommand<Object> ChangeBgColorCMD { get; set; }
        #endregion
    }
}