﻿using Microsoft.EntityFrameworkCore;
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
    public class EfRoleRepository:EfEntityRepositoryBase<Role>, IRoleRepository
    {
        public EfRoleRepository(DbContext context):base(context)
        {

        }
    }
}