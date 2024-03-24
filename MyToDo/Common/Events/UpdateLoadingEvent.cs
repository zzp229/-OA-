using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Events
{

    /// <summary>
    /// 这个类用来实现加载动画
    /// </summary>
    public class UpdateLoadingEvent : PubSubEvent<UpdateModel>
    {
    }
}
