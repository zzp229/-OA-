using MyToDo.Shared;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    /// <summary>
    /// 通用的api请求类
    /// </summary>
    public class HttpRestClient
    {
        private readonly string apiUrl;
        protected readonly RestClient client;

        public HttpRestClient(string apiUrl)
        {
            this.apiUrl = apiUrl;
            client = new RestClient(apiUrl);
        }

        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Method);  // 旧版中才有这个方法
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
            {
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);
            }

            client.BaseUrl = new Uri(apiUrl + baseRequest.Route);

            var response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<ApiResponse>(response.Content);
        }


        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Method);  // 旧版中才有这个方法
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
            {
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);
            }

            //http://localhost:46147/api/ToDo/GetAll?PageIndex=0&pageSize=100&search=
            client.BaseUrl = new Uri(apiUrl + baseRequest.Route);

            var response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
        }
    }
}
