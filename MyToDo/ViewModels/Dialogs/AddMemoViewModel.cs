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
    public class AddMemoViewModel : BindableBase, IDialogHostAware    // 由原本的DialogAware变为自己封装的就是为了能实现弹窗也能有md效果
    {
        public AddMemoViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }


        private MemoDto model;

        public MemoDto Model
        {
            get { return model; }
            set { model = value; RaisePropertyChanged(); }
        }



        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName)) // 这个Dialog调用的是md库里面的
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
        }

        private void Save()
        {
            // 验证一下
            if (string.IsNullOrWhiteSpace(Model.Title) ||
                string.IsNullOrWhiteSpace(model.Content)) return;

            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                // 点击确认要将参数返回
                DialogParameters param = new DialogParameters();
                param.Add("Value", model);  // 用Value获取弹窗参数
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, param));
            }
        }

        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        // 打开这个弹窗，有数据就填上，没有就创建对象
        public void OnDialogOpend(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))    // 判断有没有给这个弹窗提供参数
            {
                Model = parameters.GetValue<MemoDto>("Value");
            }
            else
            {
                Model = new MemoDto();
            }
        }
    }
}
