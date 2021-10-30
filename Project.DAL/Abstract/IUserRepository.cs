using Project.ENTITIES.Concrete;
using Project.SHARED.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Abstract
{
    public interface IUserRepository:IEntityRepository<User>
    {

    }
}
