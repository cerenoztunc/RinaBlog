﻿using Project.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.DTOs
{
    public class CommentListDto
    {
        public IList<Comment> Comments { get; set; }
    }
}
