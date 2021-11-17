using Project.ENTITIES.Concrete;
using Project.SHARED.Entities.Abstract;
using Project.SHARED.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.DTOs
{
    public class ArticleListDto: DtoGetBase
    {
        public IList<Article> Articles { get; set; }
        public int? CategoryID { get; set; }


    }
}
