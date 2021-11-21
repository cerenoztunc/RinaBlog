using AutoMapper;
using Project.ENTITIES.ComplexTypes;
using Project.ENTITIES.Concrete;
using Project.ENTITIES.DTOs;
using Project.UI.Helpers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.AutoMapper.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile(IImageHelper imageHelper)
        {
            CreateMap<UserAddDto, User>().ForMember(dest=>dest.Picture,opt=>opt.MapFrom(x=>imageHelper.Upload(x.UserName,x.PictureFile,PictureTypes.User,null)));
            CreateMap<User,UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();

        }
    }
}
