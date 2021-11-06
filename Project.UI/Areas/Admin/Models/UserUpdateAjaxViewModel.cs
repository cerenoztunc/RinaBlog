using Project.ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Areas.Admin.Models
{
    public class UserUpdateAjaxViewModel
    {
        public UserUpdateDto UserUpdateDto { get; set; }
        public string UserUpdatePartial { get; set; } //UserUpdateDto ile gidecek bir property.. Post işlemi yaptığımızda ajax ile bu post işlemi sonucunda bize UserUpdatePartial geliyor olacak..Yani modelin son durumu...
        public UserDto UserDto { get; set; }
    }
}
