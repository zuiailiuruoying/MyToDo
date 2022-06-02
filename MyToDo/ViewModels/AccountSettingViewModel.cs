using Microsoft.Win32;
using MyToDo.Common;
using MyToDo.Service;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MyToDo.ViewModels
{
    public class AccountSettingViewModel:BindableBase
    {
        public DelegateCommand ChangePictCommand { get; private set; }
        private UserDto currentUser;
        private bool isCanUpdate;
        private readonly IAccountService service;

        public bool IsCanUpdate
        {
            get { return isCanUpdate; }
            set { isCanUpdate = value; RaisePropertyChanged(); }
        }


        public UserDto CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; RaisePropertyChanged(); }
        }

        public AccountSettingViewModel(IAccountService service)
        {
            CurrentUser = AppSession.CurrentUser;
            IsCanUpdate = false;
            ChangePictCommand = new DelegateCommand(ChangePicture);
            this.service = service;
        }


        private async void ChangePicture()
        {
            try
            {
                Stream photo = null;
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "JPEG文件|*.jpg";
                dialog.Title = "选择头像";

                if ((bool)dialog.ShowDialog())
                {
                    //currentUser.PictureUrl = dialog.FileName;
                    //await service.UpdateAsync(currentUser);
                    List<byte[]> byteList = new List<byte[]>();

                    if ((photo = dialog.OpenFile()) != null)
                    {

                        int length = (int)photo.Length;

                        byte[] bytes = new byte[length];

                        photo.Read(bytes, 0, length);
                        byteList.Add(bytes);

                        byte[][] bytepicture = new byte[byteList.Count][];
                        for (int i = 0; i < byteList.Count; i++)
                        {
                            bytepicture[i] = byteList[i];
                        }
                        
                        var response = await service.UploadFile(new Shared.Parameters.UploadFileParameter
                        {
                            user = currentUser,
                            bytes = bytepicture
                        });

                        if (response.Status)
                        {
                            this.currentUser = response.Result;
                            AppSession.CurrentUser = response.Result;
                            MessageBox.Show("修改头像成功！", "系统提醒",MessageBoxButton.OK, MessageBoxImage.Information); 
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
