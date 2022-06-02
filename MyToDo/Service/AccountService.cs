using DryIoc;
using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public class AccountService:BaseService<UserDto>,IAccountService
    {
        private readonly HttpRestClient client;
        public AccountService(HttpRestClient client) : base(client, "Account")
        {
            this.client = client;
        }

        public async Task<ApiResponse<UserDto>> UploadFile(UploadFileParameter parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = "api/Account/UploadFile";
            request.Parameter = parameter;
            return await client.ExecuteAsync<UserDto>(request);
        }

        public async Task<ApiResponse<UserDto>> UpdateThemeColor(int id,string themeColor)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = "api/Account/UpdateThemeColor";
            request.Parameter = new UpdateThemeColorParameter() { id = id, themeColor = themeColor };
            return await client.ExecuteAsync<UserDto>(request);
        }
    }
}
