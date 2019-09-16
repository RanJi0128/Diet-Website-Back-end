using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ControleDApi.DAL;
using ControleDApi.Models;
using System.Web.Http.Cors;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ControleDApi.Util;
using System.Net.Mail;
using Microsoft.AspNet.Identity.EntityFramework;
using ControleDApi.Models.Auth;
using ControleDApi.ViewModel;

namespace ControleDApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        private Context db = new Context();

        // GET: api/Usuario
        [HttpGet]
        [Route("Get")]
        [Route("")]
        public IQueryable<Usuario> GetUsuario()
        {

            //db.UsuarioRole.Where(x => x.RoleId == EnumRole.Paciente.GetHashCode());

            //int usuarioLogado = User.Identity.GetUserId<int>();

            // var medico = db.Users.Include(m => m.Usuarios).Where(x => x.Id == usuarioLogado).FirstOrDefault();

            // var pacientes = medico.Usuarios;
            var x = db.Users;

            return x;
        }

        [HttpGet]
        [Route("GetWithSenhaTemporaria")]
        public List<Usuario> GetWithSenhaTemporaria()
        {
            return db.Users.Where(x => x.senhaTemporaria == true).AsNoTracking().ToList();
        }

        [HttpGet]
        [Route("GetUsuariosByUserLogado")]
        [Authorize(Roles = "Administrador,Medico")]
        public List<Usuario> GetUsuariosByUserLogado()
        {
            var id = User.Identity.GetUserId<int>();
            var lista = db.Users.Include(x => x.Usuarios).Where(x => x.Usuarios.Select(u => u.Id).Contains(id)).AsNoTracking().ToList();
            //lista = lista.ToList().SelectMany(x => x.Usuarios.Select(c => c)).ToList();
            //lista = db.Users.Include(x => x.Usuarios).ToList();
            return lista;
        }

        // GET: api/Usuario/5
        [ResponseType(typeof(Usuario))]
        [HttpGet]
        [Route("GetUsuario")]
        [Route("")]
        public IHttpActionResult GetUsuario(int id)
        {
            Usuario pessoa = db.Users.Find(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return Ok(pessoa);
        }

        // PUT: api/Usuario/5
        [ResponseType(typeof(void))]
        [Route("")]
        [Route("PutPessoa")]
        [Authorize(Roles = "Administrador,Medico")]
        public IHttpActionResult PutPessoa(int id, Usuario pessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            db.Entry(pessoa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id.ToString()))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        [Route("DesassociarPaciente")]
        [Authorize(Roles = "Administrador,Medico")]
        [HttpPut]
        public IHttpActionResult DesassociarPaciente(int id, Usuario pessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            pessoa = db.Users.Include(x => x.Usuarios).Where(x => x.Id == pessoa.Id).FirstOrDefault();

            var idMedico = User.Identity.GetUserId<int>();

            var medico = pessoa.Usuarios.Where(x => x.Id == idMedico).FirstOrDefault();

            if(medico == null)
                throw new Exception("Esse paciente não está associado a você.");

            pessoa.Usuarios.Remove(medico);

            db.Entry(pessoa).State = EntityState.Modified;

            try
            {
               bool sucesso =  db.SaveChanges() > 0;
                if (sucesso)
                {
                    return StatusCode(HttpStatusCode.OK);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id.ToString()))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("CadastrarNovaSenha")]
        public HttpResponseMessage CadastrarNovaSenha(CadastroNovaSenhaData novaSenhaData)
        {
            try
            {

                var pessoa = db.Users.Where(x => x.Email == novaSenhaData.Email).FirstOrDefault();

                if (pessoa == null)
                    throw new Exception("Email não encontrado!");

                if (!(bool)pessoa.senhaTemporaria)
                    throw new Exception("A senha nova já foi cadastrada anteriormente!");

                if (pessoa.Senha != novaSenhaData.SenhaAnterior)
                    throw new Exception("Senha inválida! Favor colocar a senha temporária que foi enviada por e-mail!");


                pessoa.senhaTemporaria = false;

                db.Entry(pessoa).State = EntityState.Modified;

                bool sucesso = db.SaveChanges() > 0;

                if (sucesso)
                {
                    var userManager = Request.GetOwinContext().GetUserManager<AppUserManager>();

                    var alterou = userManager.ChangePassword(pessoa.Id, novaSenhaData.SenhaAnterior, novaSenhaData.NovaSenha);

                    if (alterou.Succeeded)
                        return Request.CreateResponse(HttpStatusCode.OK, "Nova senha cadastrada com sucesso!");
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, string.Join("; ",alterou.Errors));
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, e);
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro ao cadastrar nova senha!");

        }

        // POST: api/Usuario
        [ResponseType(typeof(Usuario))]
        [HttpPost]
        [Route("PostPessoa")]
        public HttpResponseMessage PostPessoa(Usuario usuario)
        {

            HttpResponseMessage response;
            string resultado = string.Empty;

            try
            {

                var listPerfisValidos = new Context().Roles.Where(x => !x.Name.ToUpper().Contains("ADMINISTRADOR")).Select(x => x.Id).ToList();

                if (!listPerfisValidos.Contains(usuario.Roles.First().RoleId))
                {
                    throw new Exception("Perfil Inválido!!");
                }

                if (usuario.Roles.First().RoleId == EnumRole.Medico.GetHashCode())
                {
                    if (string.IsNullOrEmpty(Convert.ToString(usuario.Crm)))
                        throw new Exception("Crm Obrigatório!");
                }
                if (usuario.Roles.First().RoleId == EnumRole.Paciente.GetHashCode())
                {
                    if (string.IsNullOrEmpty(Convert.ToString(usuario.Cpf)) || usuario.Cpf == 0)
                        throw new Exception("Cpf Obrigatório!");
                }

                usuario.UserName = usuario.Email;

                var userManager = Request.GetOwinContext().GetUserManager<AppUserManager>();

                usuario.UserName = usuario.Email;
                usuario.PasswordHash = usuario.Senha;

                IdentityResult result = userManager.Create(usuario, usuario.Senha);

                if (result.Succeeded)
                {
                    resultado = "Usuario Criado com sucesso";
                }
                else
                {
                    resultado = string.Join(",", result.Errors);
                    throw new Exception(resultado);
                }

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);
                return response;

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        [ResponseType(typeof(Usuario))]
        //[Authorize(Roles = "Administrador")]
        [Authorize(Roles = "Medico,Administrador")]
        [HttpPost]
        [Route("CadastrarPaciente")]
        public HttpResponseMessage CadastrarPaciente(Usuario usuario)
        {

            HttpResponseMessage response;
            string resultado = string.Empty;

            try
            {
                usuario.UserName = usuario.Email;

                var userManager = Request.GetOwinContext().GetUserManager<AppUserManager>();

                usuario.UserName = usuario.Email;

                usuario.Senha = Guid.NewGuid().ToString().Substring(0, 6).Replace("-", "1");
                usuario.senhaTemporaria = true;

                var listPerfisValidos = new Context().Roles.Where(x => x.Name.ToUpper().Contains("PACIENTE")).AsNoTracking().Select(x => x.Id).ToList();

                var usuarioRole = new CustomUserRole();

                usuarioRole.RoleId = listPerfisValidos.FirstOrDefault();
                //usuarioRole.

                usuario.Roles.Add(usuarioRole);

                var idUsuarioLogado = Convert.ToInt32(User.Identity.GetUserId());

                var userLogged = userManager.FindById(idUsuarioLogado);

                usuario.Usuarios.Add(userLogged);

                //usuario.Usuarios.Add(usuarioMedico);

                IdentityResult result = userManager.Create(usuario, usuario.Senha);

                if (result.Succeeded)
                    resultado = "Paciente Criado com sucesso";
                else
                {
                    resultado = string.Join(",", result.Errors);
                    throw new Exception(resultado);
                }

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                //var teste =  db.Users.Include(x => x.Usuarios).Where(x => x.Id == usuario.Id);

                return response;

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        private static void EnviarEmail(Usuario usuario)
        {
            MailMessage mail = new MailMessage();

            //define os endereços
            mail.From = new MailAddress("thiagoacao@gmail.com");
            mail.To.Add(usuario.Email);

            //define o conteúdo
            mail.Subject = "Controle D - Senha temporária.";
            mail.Body = " Olá! Parece que o(a) Dr(a) cadastrou você no Controle D!";
            mail.Body += " Seja bem vindo! Para acessar o site entre na seguinte Url: www.ControleD.com.br";
            mail.Body += " Para fazer login, utilize o e-mail: " + usuario.Email + " e a senha temporária: " + usuario.Senha;
            mail.Body += " No seu primeiro acesso sera você podera cadastrar uma nova senha. Obrigado!";

            //envia a mensagem
            SmtpClient smtp = new SmtpClient("smtp.live.com", 465);
            //   smtp.DeliveryMethod = SmtpDeliveryMethod.Network; 465
            //   smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("thiagonetorj@hotmail.com", "united18juvebr@");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        // DELETE: api/Usuario/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeletePessoa(int id)
        {
            Usuario pessoa = db.Users.Find(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            db.Users.Remove(pessoa);
            db.SaveChanges();

            return Ok(pessoa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PessoaExists(string id)
        {
            return db.Users.Count(e => e.Id.ToString() == id) > 0;
        }
    }
}