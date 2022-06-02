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
    public interface IAccountService : IBaseService<UserDto>
    {
        Task<ApiResponse<UserDto>> UploadFile(UploadFileParameter parameter);
        Task<ApiResponse<UserDto>> UpdateThemeColor(int id, string themeColor);
    }
}
