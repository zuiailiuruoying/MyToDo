using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Service
{
    public interface IAccountService:IBaseService<UserDto>
    {
        Task<ApiResponse> UploadFile(UploadFileParameter parameter);
        Task<ApiResponse> UpdateThemeColor(UpdateThemeColorParameter parameter);
    }
}
