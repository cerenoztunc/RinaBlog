using Project.ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Areas.Admin.Models
{
    public class UserRoleAssignAjaxViewModel
    {
        public UserRoleAssignDto UserRoleAssignDto { get; set; }
        public string RoleAssignPartial { get; set; } //hata alırsak dönüelecek
        public UserDto UserDto { get; set; } //işlem başarılı olduğunda dönülecek mesaj için
    }
}
