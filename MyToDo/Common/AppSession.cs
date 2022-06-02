using MyToDo.Shared.Dtos;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common
{
    public static class AppSession
    {
        public static UserDto CurrentUser{get;set;}

    }
}
