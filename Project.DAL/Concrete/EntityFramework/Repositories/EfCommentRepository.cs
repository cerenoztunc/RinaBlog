using Microsoft.EntityFrameworkCore;
using Project.DAL.Abstract;
using Project.ENTITIES.Concrete;
using Project.SHARED.Data.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Concrete.EntityFramework.Repositories
{
    public class EfCommentRepository : EfEntityRepositoryBase<Comment>, ICommentRepository
    {
        public EfCommentRepository(DbContext context) : base(context)
        {

        }
    }
}
