﻿using DryIoc;
using MyToDo.Common;
using MyToDo.Extensions;
using Prism.Events;
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

namespace MyToDo.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        private readonly IDialogHostService dialogHostService;

        public MainView(IEventAggregator aggregator, IDialogHostService dialogHostService) // 构造函数注入了聚合器和弹窗服务
        {
            InitializeComponent();

            // 使用拓展方法注册消息（其实就是一个委托方便调用该方法将消息加到消息队列）
            // 注册的行为就是将消息添加到消息队列
            aggregator.ResgiterMessage(arg =>
            {
                Snackbar.MessageQueue.Enqueue(arg);
            });

            // 这个聚合器是调整加载的
            // 注册等待消息窗口
            // 注册了这个消息窗口，需要用的时候就在全局都可以调用它了，更改它的IsOpen属性就可以调控要不要显示了
            aggregator.Resgiter(arg =>
            {
                DialogHost.IsOpen = arg.IsOpen; // 这个应该是侧边栏

                if (DialogHost.IsOpen)
                    DialogHost.DialogContent = new ProgressView();  // 加载动画用户控件 (就是注册了md库中的对话框，用来显示动图而已)
            });

            // 点击进入页面了就关闭左侧小弹窗
            menuBar.SelectionChanged += (s, e) =>
            {
                drawerHost.IsLeftDrawerOpen = false;
            };


            // 对窗口进行调整
            btnMin.Click += (s, e) =>
            {
                this.WindowState = WindowState.Minimized;
            };
            btnMax.Click += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Maximized;
            };
            btnClose.Click += async (s, e) =>
            {
                // 弹窗是自己的拓展方法
                var dialogResult = await dialogHostService.Question("温馨提示", "确认退出系统?"); // 退出应用的时候的弹窗提示
                if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;  // 取消按钮

                this.Close();
            };

            // ColorZone是窗体最上边那一条
            ColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };

            ColorZone.MouseDoubleClick += (s, e) =>
            {
                if (this.WindowState == WindowState.Normal)
                    this.WindowState = WindowState.Maximized;
                else
                    this.WindowState = WindowState.Normal;
            };
            this.dialogHostService = dialogHostService;
        }
    }
}
