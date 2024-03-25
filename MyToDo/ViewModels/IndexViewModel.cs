using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Service;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
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
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.toDoService = provider.Resolve<IToDoService>();    // 构造函数获取Prism的Ioc，再获取出来
            this.memoService = provider.Resolve<IMemoService>();
            this.dialog = dialog;
            EditMemoCommand = new DelegateCommand<MemoDto>(AddMemo);
            EditToDoCommand = new DelegateCommand<ToDoDto>(AddToDo);
            ToDoCompeletedCommand = new DelegateCommand<ToDoDto>(Completed);
        }

        private async void Completed(ToDoDto obj)
        {
            var updateResult = await toDoService.UpdateAsync(obj);
            if (updateResult.Status)
            {
                var todo = summary.TodoList.FirstOrDefault(t => t.Id.Equals(obj.Id));
                if (todo != null)
                {
                    summary.TodoList.Remove(todo);
                }
            }
        }

        // 暂时的作用好像就是更新状态
        public DelegateCommand<ToDoDto> ToDoCompeletedCommand { get; private set; }
        public DelegateCommand<ToDoDto> EditToDoCommand { get; private set; }
        public DelegateCommand<MemoDto> EditMemoCommand { get; private set; }
        public DelegateCommand<string> ExecuteCommand { get; private set; }

        #region 属性

        private ObservableCollection<TaskBar> taskBars;

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }


        private SummaryDto summary;
        /// <summary>
        /// 首页统计
        /// </summary>
        public SummaryDto Summary
        {
            get { return summary; }
            set { summary = value; RaisePropertyChanged(); }
        }

        #endregion

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增待办": AddToDo(null); break;
                case "新增备忘录": AddMemo(null); break;
            }
        }


        /// <summary>
        /// 添加待办事项
        /// </summary>
        async void AddToDo(ToDoDto model)
        {
            DialogParameters param = new DialogParameters();
            if (model != null)
                param.Add("Value", model);  // 非空就添加到窗体参数，后面就是调出

            // 调用自己的showDialog
            var dialogResult = await dialog.ShowDialog("AddToDoView", param);   // 直接就可以获取弹窗(这个弹窗需要在app.xaml中依赖注入) 弹窗的文件夹要放到Dialogs文件夹下面
            if (dialogResult.Result == ButtonResult.OK)
            {
                var todo = dialogResult.Parameters.GetValue<ToDoDto>("Value");  // 弹窗中获取的参数

                // 修改id做标记，这个是修改
                if (todo.Id > 0)
                {
                    var updateResult = await toDoService.UpdateAsync(todo);
                    if (updateResult.Status)
                    {
                        var todoModel = summary.TodoList.FirstOrDefault(t => t.Id.Equals(todo.Id)); // 找出来再更新
                        if (todoModel != null)
                        {
                            todoModel.Title = todo.Title;
                            todoModel.Content = todo.Content;
                        }
                    }
                }
                else // 新增
                {
                    var addResult = await toDoService.AddAsync(todo);   // 这个返回的多少有点问题，id是0，是我的api有问题
                    if (addResult.Status)
                    {
                        summary.TodoList.Add(addResult.Result);
                    }

                    // 测试一下这个id
                    //var addResult = await toDoService.AddAsync(new ToDoDto { Content = "123", Id = 55, Status = 1, Title = "test" });   // 这个返回的多少有点问题，id是0
                    //if (addResult.Status)
                    //{

                    //    ToDoDtos.Add(addResult.Result);
                    //}
                }
            }
        }

        /// <summary>
        /// 添加备忘录
        /// </summary>
        async void AddMemo(MemoDto model)
        {
            DialogParameters param = new DialogParameters();
            if (model != null)
                param.Add("Value", model);  // 不是空就添加到窗体参数

            // 调用自己的showDialog
            var dialogResult = await dialog.ShowDialog("AddMemoView", param);   // 直接就可以获取弹窗(这个弹窗需要在app.xaml中依赖注入) 弹窗的文件夹要放到Dialogs文件夹下面
            if (dialogResult.Result == ButtonResult.OK)
            {
                var memo = dialogResult.Parameters.GetValue<MemoDto>("Value");  // 弹窗中获取的参数

                if (memo.Id > 0)
                {
                    var updateResult = await memoService.UpdateAsync(memo);
                    if (updateResult.Status)
                    {
                        var todoModel = summary.MemoList.FirstOrDefault(t => t.Id.Equals(memo.Id));
                        if (todoModel != null)
                        {
                            todoModel.Title = memo.Title;
                            todoModel.Content = memo.Content;
                        }
                    }
                }
                else
                {
                    var addResult = await memoService.AddAsync(memo);
                    if (addResult.Status)
                    {
                        summary.MemoList.Add(addResult.Result);
                    }
                }
            }
        }


        void CreateTaskBars()
        {
            TaskBars = new ObservableCollection<TaskBar>();
            TaskBars.Add(new TaskBar() { Icon = "ClockFast", Title = "汇总", Color = "#FF0CA0FF", Target = "" });
            TaskBars.Add(new TaskBar() { Icon = "ClockCheckOutline", Title = "已完成", Color = "#FF1ECA3A", Target = "" });
            TaskBars.Add(new TaskBar() { Icon = "ChartLineVariant", Title = "完成比例", Color = "#FF02C6DC", Target = "" });
            TaskBars.Add(new TaskBar() { Icon = "PlaylistStar", Title = "备忘录", Color = "#FFFFA000", Target = "" });
        }


        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 先调用首页统计显示主页上边的数据
            var summaryResult = await toDoService.SummaryAsync();

            if (summaryResult.Status)
            {
                Summary = summaryResult.Result;
                Refresh();
            }

            base.OnNavigatedTo(navigationContext);
        }

        void Refresh()
        {
            TaskBars[0].Content = summary.Sum.ToString();
            TaskBars[1].Content = summary.CompeletedCount.ToString();
            TaskBars[2].Content = summary.CompletedRatio;
            TaskBars[3].Content = summary.MemoCount.ToString();
        }


    }
}
