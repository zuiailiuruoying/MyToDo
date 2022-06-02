using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDo.Api.Service
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public AccountService(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }

        public Task<ApiResponse> AddAsync(UserDto model)
        {
            return Task.FromResult(new ApiResponse(""));
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var repository = work.GetRepository<User>();
                var user = await repository.GetFirstOrDefaultAsync(predicate: x=> x.Id.Equals(id));
                if (user == null)
                {
                    return new ApiResponse("账户无效！");
                }

                repository.Delete(id);
                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, "");
                return new ApiResponse("删除账户失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                var repository = work.GetRepository<User>();
                var user = await repository.GetPagedListAsync(predicate:
                   x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.UserName.Contains(parameter.Search),
                   pageIndex: parameter.PageIndex,
                   pageSize: parameter.PageSize,
                   orderBy: source => source.OrderByDescending(t => t.CreateDate));
                return new ApiResponse(true, user);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = work.GetRepository<User>();
                var user = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return new ApiResponse(true, user);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(UserDto model)
        {
            try
            {
                var dbUser = mapper.Map<User>(model);
                var repository = work.GetRepository<User>();
                var user = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(dbUser.Id));

                user.UserName = dbUser.UserName;
                user.PictureUrl = dbUser.PictureUrl;
                user.PassWord = dbUser.PassWord;
                user.UpdateDate = DateTime.Now;

                repository.Update(user);

                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, user);
                return new ApiResponse("更新数据异常！");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateThemeColor(UpdateThemeColorParameter parameter)
        {
            try
            {
                var repository = work.GetRepository<User>();
                var user = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(parameter.id));

                user.ThemeColor = parameter.themeColor;
                user.UpdateDate = DateTime.Now;

                repository.Update(user);

                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, user);
                return new ApiResponse("更新数据异常！");
            }
            catch (Exception ex)
            {

                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UploadFile(UploadFileParameter parameter)
        {
            try
            {
                var dbUser = mapper.Map<User>(parameter.user);
                var bytes = parameter.bytes;
                var repository = work.GetRepository<User>();
                var user = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(dbUser.Id));

                if (user == null)
                {
                    return new ApiResponse("用户验证失败，无法上传文件！");
                }

                string Path = System.AppDomain.CurrentDomain.BaseDirectory + @"Images\";

                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }

                string fileName = user.Id.ToString()+"_"+ Guid.NewGuid().ToString("N")+".jpg";

                for (int i = 0; i < bytes.Length; i++)
                {
                    Path = Path + fileName;
                    FileInfo fi = new FileInfo(Path);
                    FileStream fs;

                    fs = fi.OpenWrite();

                    fs.Write(bytes[i], 0, bytes[i].Length);

                    fs.Close();
                }

                user.PictureUrl = Path;
                user.UpdateDate = DateTime.Now;
                repository.Update(user);

                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, user);

                return new ApiResponse("更新数据异常！");

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}
