using MyToDo.Shared.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dtos
{
    public class UserDto : BaseDto
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }
         
        private string account;

        public string Account
        {
            get { return account; }
            set { account = value; OnPropertyChanged(); }
        }
         
        private string passWord;

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; OnPropertyChanged(); }
        }

        private string pictureUrl;

        public string PictureUrl
        {
            get { return pictureUrl; }
            set { pictureUrl = value; OnPropertyChanged(); }
        }

        private string themeColor;

        public string ThemeColor
        {
            get { return themeColor; }
            set { themeColor = value; }
        }


        public static explicit operator UserDto(ApiResponse<UserDto> v)
        {
            throw new NotImplementedException();
        }
    }
}
