using System.Linq;
using AutoMapper;
using DatingApp.api.DTO;
using DatingApp.api.Models;

namespace DatingApp.api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap< User, UserForListDto>()
            .ForMember(desc => desc.PhotoUrl ,opt =>
            opt.MapFrom(src =>src.Photos.FirstOrDefault(p =>p.isMain==true).Url))
            .ForMember(desc =>desc.Age ,opt =>opt.MapFrom(src =>src.DateOfBirth.GetAge()));

            CreateMap<User, UserDetailedDto>().ForMember(desc =>desc.PhotoUrl,
            opt =>opt.MapFrom(src =>src.Photos
            .FirstOrDefault(p =>p.isMain==true).Url))
            .ForMember(desc =>desc.Age ,opt =>opt.MapFrom(src =>src.DateOfBirth.GetAge()));;
            CreateMap<Photo,PhotoForDatailedDto>();
            CreateMap<UpdateForUserDto,User>();
            CreateMap<Photo,PhotoReturenDto>();
            CreateMap<PhotoCreationeDto,Photo>();
            CreateMap<UserForRegister,User>();
            CreateMap<CrateMessageDto,Message>().ReverseMap();
            CreateMap<Message,MessageToReturenDto>().
            ForMember(op =>op.RecipientPhotoUrl,opt =>opt.MapFrom(src =>src.Recipient.Photos.FirstOrDefault(p =>p.isMain).Url))
            .ForMember(op =>op.SenderPhotoUrl, opt =>opt.MapFrom(src =>src.Sender.Photos.FirstOrDefault(p =>p.isMain).Url));
        }
    }
}