using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.ViewModels;
using Database.Models;

namespace ItlaTwitter.Infraestructure.Automapper
{
    public class AutomapperConfi : Profile
    {

        public AutomapperConfi ()
        {

            Configureregister();
            ConfigureUsuarios();
            ConfigureComents();
            Configurereplies();


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
    }
}
