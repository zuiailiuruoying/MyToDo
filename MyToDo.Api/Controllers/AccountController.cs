using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context;
using MyToDo.Api.Service;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Controllers
{   
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController: ControllerBase
    {
        private readonly IAccountService service;

        public AccountController(IAccountService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await service.GetSingleAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameter param) => await service.GetAllAsync(param);

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] UserDto model) => await service.UpdateAsync(model);

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await service.DeleteAsync(id);

        [HttpPost]
        public async Task<ApiResponse> UploadFile(UploadFileParameter parameter) => await service.UploadFile(parameter);

        [HttpPost]
        public async Task<ApiResponse> UpdateThemeColor(UpdateThemeColorParameter parameter) => await service.UpdateThemeColor(parameter);
    }
}
