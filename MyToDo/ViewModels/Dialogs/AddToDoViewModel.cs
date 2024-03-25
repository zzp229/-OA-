using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels.Dialogs
{
    public class AddToDoViewModel : BindableBase, IDialogHostAware    // 由原本的DialogAware变为自己封装的就是为了能实现弹窗也能有md效果
    {
        public AddToDoViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }


        private ToDoDto model;
        /// <summary>
        /// 新增或编辑的实体
        /// </summary>
        public ToDoDto Model
        {
            get { return model; }
            set { model = value; RaisePropertyChanged(); }
        }


        /// <summary>
        /// 取消
        /// </summary>
        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName)) // 这个Dialog调用的是md库里面的
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));    //操作结束
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void Save()
        {
            // 验证一下
            if (string.IsNullOrWhiteSpace(Model.Title) ||
                string.IsNullOrWhiteSpace(model.Content)) return;

            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                // 点击确认要将参数返回
                DialogParameters param = new DialogParameters();
                param.Add("Value", model);  // 填上窗体数据
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, param));
            }
        }

        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public void OnDialogOpend(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))    // 判断有没有给这个弹窗提供参数
            {
                Model = parameters.GetValue<ToDoDto>("Value");  // 有值就将值赋给VM中的属性
            }
            else
            {
                Model = new ToDoDto();
            }
        }
    }
}
