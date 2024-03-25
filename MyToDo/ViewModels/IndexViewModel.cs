using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Service;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class IndexViewModel : NavigationViewModel
    {
        // 加上这两个是因为主页也要提供这个服务
        private readonly IToDoService toDoService;
        private readonly IMemoService memoService;
        private readonly IDialogHostService dialog; // 原本是用Prism带的IDialogService实现弹窗，但是原本的无法实现md样式，自己封装了一下

        public IndexViewModel(
            IContainerProvider provider,
            IDialogHostService dialog) : base(provider)    // 原本是注入Prism的接口，方便弹窗（这个是自己封装过的）
        {
            CreateTaskBars();
            ToDoDtos = new ObservableCollection<ToDoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.toDoService = provider.Resolve<IToDoService>();    // 构造函数获取Prism的Ioc，再获取出来
            this.memoService = provider.Resolve<IMemoService>();
            this.dialog = dialog;
        }

        public DelegateCommand<string> ExecuteCommand { get; private set; }

        #region 属性

        private ObservableCollection<TaskBar> taskBars;

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }


        private ObservableCollection<ToDoDto> toDoDtos;

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }


        private ObservableCollection<MemoDto> memoDtos;


        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }

        #endregion

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增待办": AddToDo(); break;
                case "新增备忘录": AddMemo(); break;
            }
        }


        /// <summary>
        /// 添加待办事项
        /// </summary>
        async void AddToDo()
        {
            // 调用自己的showDialog
            var dialogResult = await dialog.ShowDialog("AddToDoView", null);   // 直接就可以获取弹窗(这个弹窗需要在app.xaml中依赖注入) 弹窗的文件夹要放到Dialogs文件夹下面
            if (dialogResult.Result == ButtonResult.OK)
            {
                var todo = dialogResult.Parameters.GetValue<ToDoDto>("Value");  // 弹窗中获取的参数

                // 这个如果是0的话就是新增
                if (todo.Id > 0)   // 新增就只是添加标题和内容，BaseDto里面的id就没有修改过
                {

                }
                else
                {
                    var addResult = await toDoService.AddAsync(todo);
                    if (addResult.Status)
                    {
                        ToDoDtos.Add(addResult.Result);
                    }
                }
            }
        }

        /// <summary>
        /// 添加备忘录
        /// </summary>
        async void AddMemo()
        {
            // 调用自己的showDialog
            var dialogResult = await dialog.ShowDialog("AddMemoView", null);   // 直接就可以获取弹窗(这个弹窗需要在app.xaml中依赖注入) 弹窗的文件夹要放到Dialogs文件夹下面
            if (dialogResult.Result == ButtonResult.OK)
            {
                var memo = dialogResult.Parameters.GetValue<MemoDto>("Value");  // 弹窗中获取的参数

                if (memo.Id > 0)
                {

                }
                else
                {
                    var addResult = await memoService.AddAsync(memo);
                    if (addResult.Status)
                    {
                        MemoDtos.Add(addResult.Result);
                    }
                }
            }
        }


        void CreateTaskBars()
        {
            TaskBars = new ObservableCollection<TaskBar>();
            TaskBars.Add(new TaskBar() { Icon = "ClockFast", Title = "汇总", Content = "9", Color = "#FF0CA0FF", Target = "" });
            TaskBars.Add(new TaskBar() { Icon = "ClockCheckOutline", Title = "已完成", Content = "9", Color = "#FF1ECA3A", Target = "" });
            TaskBars.Add(new TaskBar() { Icon = "ChartLineVariant", Title = "完成比例", Content = "100%", Color = "#FF02C6DC", Target = "" });
            TaskBars.Add(new TaskBar() { Icon = "PlaylistStar", Title = "备忘录", Content = "19", Color = "#FFFFA000", Target = "" });
        }


    }
}
