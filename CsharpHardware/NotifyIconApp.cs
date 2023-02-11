using CsharpHardware.Properties;
using System.Diagnostics;

namespace CsharpHardware
{
    internal class NotifyIconApp
    {
        /// <summary>
        /// 声明通知静态实体
        /// </summary>
        private static NotifyIcon NotifyIcon = new NotifyIcon();

        /// <summary>
        /// 初始化通知栏图标。
        /// </summary>
        public static void InitNotifyIcon()
        {
            List<string> menuItems = new()
            {
                    MenuItems.OPEN_TEST_MENU,
                    MenuItems.ABOUT_MENU,
                    MenuItems.EXIT_MENU
                };
            ContextMenuStrip notifyIconContextMenu = new();
            //生成菜单项并添加事件
            foreach (ToolStripMenuItem menuItem in menuItems.Select(item => new ToolStripMenuItem(item)))
            {
                menuItem.Click += MenuItem_Click!;
                notifyIconContextMenu.Items.Add(menuItem);
            }
            NotifyIcon.Text = "CsharpMaster";
            NotifyIcon.Icon = Resources.favicon32;
            NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            NotifyIcon.BalloonTipText = "CsharpMaster程序运行中……";
            NotifyIcon.BalloonTipTitle = "CsharpMaster程序";
            NotifyIcon.ShowBalloonTip(3000);
            NotifyIcon.ContextMenuStrip = notifyIconContextMenu;
            NotifyIcon.Visible = true;
        }

        /// <summary>
        /// 通知栏图标的菜单功能方法。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            switch (menuItem!.Text)
            {
                case MenuItems.ABOUT_MENU:
                    menuItem.Checked = true;
                    ShowAboutForm();
                    menuItem.Checked = false;
                    break;

                case MenuItems.OPEN_TEST_MENU:
                    menuItem.Checked = true;
                    Process.Start("explorer", "http://localhost:8896");
                    menuItem.Checked = false;
                    break;

                case MenuItems.EXIT_MENU:
                    Exit();
                    break;

                default:
                    break;
            }
        }

        private static void ShowAboutForm()
        {
            FormAbout form = new();
            form.ShowDialog();
            form.Dispose();
        }

        /// <summary>
        /// 退出程序
        /// </summary>
        private static void Exit()
        {
            if (NotifyIcon != null)
            {
                NotifyIcon.Visible = false;
                NotifyIcon.Dispose();
            }
            Thread.Sleep(500);
            Environment.Exit(0);
        }
    }

    /// <summary>
    /// 常量类
    /// </summary>
    public static class MenuItems
    {
        #region 通告图标菜单项

        public const string OPEN_TEST_MENU = "打开硬件测试页";
        public const string ABOUT_MENU = "关于CsharpMaster";
        public const string EXIT_MENU = "退出";

        #endregion 通告图标菜单项
    }
}