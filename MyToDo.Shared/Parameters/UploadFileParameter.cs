using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Parameters
{
    public class UploadFileParameter
    {
        public UserDto user { get; set; }
        public byte[][] bytes { get; set; }
    }
}
