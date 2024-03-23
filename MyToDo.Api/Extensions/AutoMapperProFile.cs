using AutoMapper.Configuration;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;

namespace MyToDo.Api.Extensions
{
    /// <summary>
    /// 这个文件夹和类是管理AutoMappper的
    /// </summary>
    public class AutoMapperProFile : MapperConfigurationExpression
    {
        public AutoMapperProFile()
        {
            CreateMap<ToDo, ToDoDto>().ReverseMap();    // 添加上这个就可以相互转换了
            CreateMap<Memo, MemoDto>().ReverseMap();
        }
    }
}
