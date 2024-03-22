namespace MyToDo.Api.Service
{
    /// <summary>
    /// 应该是api返回内容的一个暂存
    /// </summary>
    public class ApiResponse
    {
        public ApiResponse(string message, bool status = false)
        {
            Message = message;
            Status = status;
        }
        public ApiResponse(bool status, object result)
        {
            Status = status;
            Result = result;
        }

        public string Message { get; set; }
        public bool Status { get; set; }
        public object Result { get; set; }
    }
}
