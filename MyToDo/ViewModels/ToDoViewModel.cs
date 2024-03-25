using Microsoft.Win32;
using MyToDo.Common;
using MyToDo.Extensions;
using MyToDo.Service;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class ToDoViewModel : NavigationViewModel
    {

        private readonly IDialogHostService dialogHost;

        public ToDoViewModel(IToDoService service, IContainerProvider provider)
            : base(provider)
        {
            ToDoDtos = new ObservableCollection<ToDoDto>(); // 客户端的使用的是Share下面的类
            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectedCommand = new DelegateCommand<ToDoDto>(Selected);
            DeleteCommand = new DelegateCommand<ToDoDto>(Delete);
            dialogHost = provider.Resolve<IDialogHostService>();    // 从Prism的Ioc容器中获取出来（IDialogService的父类，所以可以取出来）
            this.service = service;
        }

        private async void Delete(ToDoDto obj)
        {
            try
            {
                // 这里调用扩展方法
                // 删除需要确认
                var dialogResult = await dialogHost.Question("温馨提示", $"确认删除待办事项:{obj.Title}");

                if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;


                UpdateLoading(true);
                // 删除数据库的
                var deleteResult = await service.DeleteAsync(obj.Id);
                // 删除VM中的
                if (deleteResult.Status)
                {
                    var model = ToDoDtos.FirstOrDefault(t => t.Id.Equals(obj.Id));
                    if (model != null)
                        ToDoDtos.Remove(model);
                }
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增":
                    Add();
                    break;
                case "查询":
                    GetDataAsync();
                    break;
                case "保存": Save(); break;
            }
        }


        private int seleteIndex;
        /// <summary>
        /// 下拉列表选中状态=值
        /// </summary>
        public int SeleteIndex
        {
            get { return seleteIndex; }
            set { seleteIndex = value; RaisePropertyChanged(); }
        }




        private string search;
        /// <summary>
        /// 搜索条件
        /// </summary>
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }


        private bool isRightDrawerOpen;
        /// <summary>
        /// 右侧编辑窗口是否展开
        /// </summary>
        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        private ToDoDto currentDto;
        /// <summary>
        /// 编辑选择对象/新增时对象
        /// </summary>
        public ToDoDto CurrentDto
        {
            get { return currentDto; }
            set { currentDto = value; RaisePropertyChanged(); }
        }


        /// <summary>
        /// 添加待办
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void Add()
        {
            CurrentDto = new ToDoDto();
            IsRightDrawerOpen = true;
        }

        private async void Save()
        {
            if (string.IsNullOrWhiteSpace(CurrentDto.Title) || string.IsNullOrWhiteSpace(CurrentDto.Content))
                return;

            UpdateLoading(true);

            try
            {
                if (CurrentDto.Id > 0)
                {
                    var updateResult = await service.UpdateAsync(CurrentDto);
                    if (updateResult.Status)
                    {
                        var todo = ToDoDtos.FirstOrDefault(t => t.Id == CurrentDto.Id);
                        if (todo != null)
                        {
                            todo.Title = CurrentDto.Title;
                            todo.Content = CurrentDto.Content;
                            todo.Status = CurrentDto.Status;
                        }
                    }
                    IsRightDrawerOpen = false;
                }
                else
                {
                    var addResult = await service.AddAsync(CurrentDto);
                    if (addResult.Status)
                    {
                        toDoDtos.Add(addResult.Result);
                        IsRightDrawerOpen = false;
                    }
                }
            }
            catch (Exception ex)
            {


            }
            finally
            {
                UpdateLoading(false);
            }


        }

        // 选中了就打开右边窗口
        private async void Selected(ToDoDto obj)
        {
            try
            {
                UpdateLoading(true);
                var todoResult = await service.GetFirstOfDefaultAsync(obj.Id);

                if (todoResult.Status)
                {
                    CurrentDto = todoResult.Result;
                    IsRightDrawerOpen = true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                UpdateLoading(false);
            }

        }


        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<ToDoDto> SelectedCommand { get; private set; }
        public DelegateCommand<ToDoDto> DeleteCommand { get; private set; }

        private ObservableCollection<ToDoDto> toDoDtos;
        private readonly IToDoService service;

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; }
        }


        /// <summary>
        /// 获取数据
        /// </summary>
        async void GetDataAsync()
        {
            UpdateLoading(true);    // 打开等待窗口

            int? Status = SeleteIndex == 0 ? null : SeleteIndex == 2 ? 1 : 0;   // 这个参数是任务的状态

            var todoResult = await service.GetAllFilterAsync(new ToDoParameter()
            {
                PageIndex = 0,
                PageSize = 100,
                Search = Search,    // 这个是查询条件应该
                Status = Status,
            });

            if (todoResult.Status)
            {
                ToDoDtos.Clear();
                foreach (var item in todoResult.Result.Items)
                {
                    toDoDtos.Add(item);
                }
            }

            UpdateLoading(false);   // 数据加载完成后关闭动画
        }

        /// <summary>
        /// 这里重写作用就是导航过去就加载数据
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetDataAsync();
        }

    }
}
