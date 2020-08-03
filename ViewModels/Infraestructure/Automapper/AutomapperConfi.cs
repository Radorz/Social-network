using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.ViewModels;
using Database.Models;
using dto;

namespace ViewModels.Infraestructure.Automapper
{
    public class AutomapperConfinew : Profile
    {

        public AutomapperConfinew()
        {

            Configureregister();
            ConfigureUsuarios();
            ConfigureComents();
            Configurereplies();
            ConfigurePublicacionDTO();
            ConfigureReplydto();
            ConfigureComentsdto();
            userinfo();
            }

        private void Configureregister()
        {
            CreateMap<RegisterViewModel, Usuarios>().ReverseMap().ForMember(dest => dest.indicetel, opt => opt.Ignore()).ForMember
                (dest => dest.RepeatContraseña, opt => opt.Ignore()).ForMember
                (dest => dest.photo, opt => opt.Ignore());
            ;

        }
        private void Configurereplies()
        {
            CreateMap<RepliesViewModel,RespuestasCom>().ReverseMap().ForMember(dest => dest.Nameuser, opt => opt.Ignore()).ForMember
                (dest => dest.Photouser, opt => opt.Ignore());
            ;

        }
        private void ConfigureUsuarios()
        {
            CreateMap<Usuarios, RegisterViewModel>().ReverseMap();
            ;

        }
        private void ConfigureComents()
        {
            CreateMap<ComentariosViewModel, Comentario>().ReverseMap().ForMember(dest => dest.Comentarista, opt => opt.Ignore());
            ;

        }

        private void ConfigurePublicacionDTO()
        {
            CreateMap<PublicacionesDTO, Publicaciones>().ReverseMap().ForMember(dest => dest.Comentarios, opt => opt.Ignore())
            ;

        }
        private void ConfigureComentsdto()
        {
            CreateMap<ComentariosDTO, Comentario>().ReverseMap().ForMember
                (dest => dest.Replies, opt => opt.Ignore()); ;
            ;

        }
        private void ConfigureReplydto()
        {
            CreateMap<RepliesDTO, RespuestasCom>().ReverseMap();
            ;

        }

        private void userinfo()
        {
            CreateMap<Usuarios, userinfoDTO>().ReverseMap().ForMember(dest => dest.Telefono, opt => opt.Ignore()).ForMember
                (dest => dest.Contraseña, opt => opt.Ignore()).ForMember
                (dest => dest.Estado, opt => opt.Ignore()); ;
            ;

        }
    }
}
