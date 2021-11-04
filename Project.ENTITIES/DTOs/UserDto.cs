using Project.ENTITIES.Concrete;
using Project.SHARED.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.DTOs
{
    public class UserDto:DtoGetBase
    {
        public User User { get; set; }

    }
}
