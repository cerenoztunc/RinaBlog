using Microsoft.EntityFrameworkCore;
using Project.DAL.Abstract;
using Project.DAL.Concrete.EntityFramework.Context;
using Project.ENTITIES.Concrete;
using Project.SHARED.Data.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Concrete.EntityFramework.Repositories
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
    {
        public EfCategoryRepository(DbContext context) : base(context)
        {

        }
        public async Task<Category> GetById(int categoryID)
        {
           return await RinaBlogContext.Categories.SingleOrDefaultAsync(c => c.ID == categoryID);
        }
        private RinaBlogContext RinaBlogContext
        {
            get
            {
                return _context as RinaBlogContext;
            }
        }


    }
}
