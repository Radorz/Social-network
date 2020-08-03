using AutoMapper;
using Database.Models;
using dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ViewModels;

namespace Repository.Repository
{
    public class PublicacionesRepository : RepositoryBase<Publicaciones, itlatwitterContext>
    {
        private readonly IMapper _mapper;

        public PublicacionesRepository(itlatwitterContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;

        }

        public async Task<HomeViewModel> GetAllpubbyuser(IdentityUser user)
        {

            var Usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == user.Id);


            HomeViewModel vms = new HomeViewModel();
            vms.idusuario = user.Id;
            vms.Foto = Usuario.Foto;
            vms.publicacion = await _context.Publicaciones.Where(b => (b.Idusuario == vms.idusuario)).OrderByDescending(a => a.Fecha).ToListAsync();
            var listOfListcom = new List<List<ComentariosViewModel>>();
            var listOfListreplies = new List<List<RepliesViewModel>>();
            var lastreplies = new List<RepliesViewModel>();

            foreach (Publicaciones idp in vms.publicacion)
            {

                var coments = _context.Comentario.Where(b => (b.Idpublicacion == idp.Id)).ToList();

                List<ComentariosViewModel> Firtslist = new List<ComentariosViewModel>();
                List<RepliesViewModel> secondlistreplies = new List<RepliesViewModel>();

                foreach (var Comento in coments)
                {

                    var sabercomentarista = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == user.Id);
                    var cvms = _mapper.Map<ComentariosViewModel>(Comento);
                    cvms.Comentarista = sabercomentarista.Usuario;
                    cvms.Foto = sabercomentarista.Foto;

                    Firtslist.Add(cvms);
                    var replies = await _context.RespuestasCom.Where(m => m.Idcomentario == Comento.Id).ToListAsync();
                    if (replies.Count() > 0)
                    {
                        var last = replies.Last(list => list.Idcomentario == Comento.Id);
                        var maplastreplies = _mapper.Map<RepliesViewModel>(last);
                        var úserlastreply = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == last.Idusuario);
                        maplastreplies.Nameuser = úserlastreply.Usuario.Trim();
                        lastreplies.Add(maplastreplies);
                    }
                    if (replies != null)
                    {
                        foreach (var rep in replies)
                        {
                            var mapreplies = _mapper.Map<RepliesViewModel>(rep);
                            var úserwhoreply = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == rep.Idusuario);
                            mapreplies.Nameuser = úserwhoreply.Usuario.Trim();
                            if (úserwhoreply.Foto != null) {
                                mapreplies.Photouser = úserwhoreply.Foto.Trim();

                            }

                            secondlistreplies.Add(mapreplies);
                        }
                    }

                }

                listOfListcom.Add(Firtslist);
                listOfListreplies.Add(secondlistreplies);




            }
            vms.replies = listOfListreplies;
            vms.Last = lastreplies;
            vms.comentario = listOfListcom;
            vms.NombreUsuario = Usuario.Nombre + " " + Usuario.Apellido;
            vms.Usuario = Usuario.Usuario;
            return vms;
        }

        public async Task<AmigosViewModel> GetAllpubFirends(IdentityUser user)
        {

            var amigos = await _context.Amigos.Where(m => m.Idenvia == user.Id).ToListAsync();
            var listOfListCom = new List<List<ComentariosViewModel>>();
            var Listdatoamigos = new List<Usuarios>();
            var listOfListreplies = new List<List<RepliesViewModel>>();
            var lastreplies = new List<RepliesViewModel>();

            foreach (Amigos idp in amigos)
            {
                var datoamigos = _context.Usuarios.FirstOrDefault(m => m.Id == idp.Idrecibe);
                var publicacionesdeltipo = await _context.Publicaciones.Where(m => m.Idusuario == idp.Idrecibe).OrderByDescending(a => a.Fecha).ToListAsync();
                foreach (Publicaciones pub in publicacionesdeltipo)
                {
                    var comentarios = _context.Comentario.Where(b => (b.Idpublicacion == pub.Id)).ToList();
                    List<ComentariosViewModel> cometariosview = new List<ComentariosViewModel>();
                    List<RepliesViewModel> secondlistreplies = new List<RepliesViewModel>();

                    foreach (var Comento in comentarios)
                    {

                        var sabercomentarista = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == Comento.Idusuario);
                        var cvms = _mapper.Map<ComentariosViewModel>(Comento);
                        cvms.Comentarista = sabercomentarista.Usuario;
                        cvms.Foto = sabercomentarista.Foto;

                        cometariosview.Add(cvms);
                        var replies = await _context.RespuestasCom.Where(m => m.Idcomentario == Comento.Id).ToListAsync();
                        if (replies.Count() > 0)
                        {
                            var last = replies.Last(list => list.Idcomentario == Comento.Id);
                            var maplastreplies = _mapper.Map<RepliesViewModel>(last);
                            var úserlastreply = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == last.Idusuario);
                            maplastreplies.Nameuser = úserlastreply.Usuario.Trim();
                            lastreplies.Add(maplastreplies);
                        }
                        if (replies != null)
                        {
                            foreach (var rep in replies)
                            {
                                var mapreplies = _mapper.Map<RepliesViewModel>(rep);
                                var úserwhoreply = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == rep.Idusuario);
                                mapreplies.Nameuser = úserwhoreply.Usuario.Trim();
                                if (úserwhoreply.Foto != null) { 
                                mapreplies.Photouser = úserwhoreply.Foto.Trim();
                                }
                                secondlistreplies.Add(mapreplies);
                            }
                        }

                    

                }
                    listOfListCom.Add(cometariosview);
                    listOfListreplies.Add(secondlistreplies);


                }

                Listdatoamigos.Add(datoamigos);

            }

            var pubs = _context.Publicaciones2.FromSqlRaw("SELECT I.id,I.contenido,I.idusuario,I.Imagen, Nombre, Apellido, Usuario,Foto,Fecha FROM Usuarios O JOIN Publicaciones I ON O.id = I.idusuario JOIN Amigos P ON P.idrecibe = I.idusuario where P.idenvia ={0} ORDER BY i.Fecha desc", user.Id).ToList();

            AmigosViewModel vms = new AmigosViewModel();
            vms.replies = listOfListreplies;
            vms.Last = lastreplies;
            vms.idusuario = user.Id;
            vms.Amigos = Listdatoamigos;
            vms.publicacion = pubs;
            vms.comentario = listOfListCom;
            vms.idusuario.Max();
            return vms;

        }
            public async Task<bool> Tweet(HomeViewModel vms, string folderpath)
        {
            string uniqueName = null;
            if (vms.photo != null)
            {

                uniqueName = Guid.NewGuid().ToString() + "_" + vms.photo.FileName;
                var folderPath = Path.Combine(folderpath, "img/pub");
                var filepath = Path.Combine(folderPath, uniqueName);

                if (filepath != null)
                {

                    vms.photo.CopyTo(new FileStream(filepath, mode: FileMode.Create));
                }
            }
            vms.nuevapub.Imagen = uniqueName;
            vms.nuevapub.Fecha = DateTime.Now;
            _context.Publicaciones.Add(vms.nuevapub);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<PublicacionesDTO>> GetAllpubbyuserApi(IdentityUser user)
        {

            var Usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == user.Id);


            List<PublicacionesDTO> vms = new List<PublicacionesDTO>();
            var publicacion = await _context.Publicaciones.Where(b => (b.Idusuario == Usuario.Id)).OrderByDescending(a => a.Fecha).ToListAsync();

            foreach (Publicaciones idp in publicacion)
            {
                var Pub = _mapper.Map<PublicacionesDTO>(idp);
                Pub.Contenido = Pub.Contenido.Trim();

                var coments = _context.Comentario.Where(b => (b.Idpublicacion == idp.Id)).ToList();

                List<ComentariosDTO> Coments = new List<ComentariosDTO>();

                foreach (var Comento in coments)
                {
                    var cvms = _mapper.Map<ComentariosDTO>(Comento);
                    cvms.Contenido = cvms.Contenido.Trim();
                    List<RepliesDTO> Replies = new List<RepliesDTO>();

                    var replies = await _context.RespuestasCom.Where(m => m.Idcomentario == Comento.Id).ToListAsync();
                    if (replies != null)
                    {
                        foreach (var rep in replies)
                        {
                            var mapreplies = _mapper.Map<RepliesDTO>(rep);
                            mapreplies.Contenido = mapreplies.Contenido.Trim();
                            Replies.Add(mapreplies);
                        }
                        cvms.Replies = Replies;
                        Coments.Add(cvms);

                    }

                }
                Pub.Comentarios = Coments;
                vms.Add(Pub);




            }
            return vms;
        }
            public async Task<List<userinfoDTO>> GetAllFriendsuserApi(IdentityUser user)
            {



            var amigos = await _context.Amigos.Where(m => m.Idenvia == user.Id).ToListAsync();
            
            var listfriends = new List<userinfoDTO>();

            foreach (Amigos idp in amigos) { 
            
                var datoamigos = _context.Usuarios.FirstOrDefault(m => m.Id == idp.Idrecibe);
                var  mapsfriend = _mapper.Map<userinfoDTO>(datoamigos);
                listfriends.Add(mapsfriend);
            }
            return listfriends;
        }

        public async Task<bool> TweetApi(NewPubDTO vms)
        {
            var pub = new Publicaciones();
            pub.Imagen = vms.Imagen;
            pub.Contenido = vms.Contenido;
            pub.Idusuario = vms.iduser;
            pub.Fecha = DateTime.Now;
            _context.Publicaciones.Add(pub);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<PublicacionesDTO> PopularPub(IdentityUser user)
        {
             var Usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == user.Id);


            var findpublicacion =   _context.Popularpub.FromSqlRaw("select idpublicacion, COUNT(idpublicacion) as comentarios from comentario a join Publicaciones b on b.id=a.idpublicacion where b.idusuario={0} GROUP BY idpublicacion",user.Id);
            var popular = await findpublicacion.MaxAsync(i => i.comentarios);
            var pib = findpublicacion.FirstOrDefault(i => i.comentarios == popular);
            var publicacion = await _context.Publicaciones.FirstOrDefaultAsync(i => i.Id == pib.idpublicacion);
            var Pub = _mapper.Map<PublicacionesDTO>(publicacion);
            Pub.Contenido = Pub.Contenido.Trim();
            var coments = _context.Comentario.Where(b => (b.Idpublicacion == Pub.Id)).ToList();

            List<ComentariosDTO> Coments = new List<ComentariosDTO>();

                foreach (var Comento in coments)
                {
                    var cvms = _mapper.Map<ComentariosDTO>(Comento);
                    cvms.Contenido = cvms.Contenido.Trim();
                    List<RepliesDTO> Replies = new List<RepliesDTO>();

                    var replies = await _context.RespuestasCom.Where(m => m.Idcomentario == Comento.Id).ToListAsync();
                    if (replies != null)
                    {
                        foreach (var rep in replies)
                        {
                            var mapreplies = _mapper.Map<RepliesDTO>(rep);
                            mapreplies.Contenido = mapreplies.Contenido.Trim();
                            Replies.Add(mapreplies);
                        }
                        cvms.Replies = Replies;
                        Coments.Add(cvms);

                    }

                }
                Pub.Comentarios = Coments;

            return Pub;
        }

    }
}
