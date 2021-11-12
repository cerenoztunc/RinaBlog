using AutoMapper;
using Project.ENTITIES.Concrete;
using Project.ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.AutoMapper.Profiles
{
    public class ArticleProfile:Profile
    {
        public ArticleProfile()
        {
            // Source --> Target

            CreateMap<ArticleAddDto, Article>().ForMember(dest => dest.CreatedDate, opt=>opt.MapFrom(x=>DateTime.Now));
            CreateMap<ArticleUpdateDto, Article>().ForMember(dest=>dest.ModifiedDate, opt=>opt.MapFrom(x=>DateTime.Now));
            CreateMap<Article, ArticleUpdateDto>();

        }
    }
}
