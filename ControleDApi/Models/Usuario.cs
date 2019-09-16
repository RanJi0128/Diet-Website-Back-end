using ControleDApi.Models.Auth;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDApi.Models
{
    public class Usuario : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public Usuario()
        {
            this.Agendamentos = new List<Agendamento>();
            this.Refeicoes = new List<Refeicao>();
            this.Usuarios = new List<Usuario>();
        }

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Senha { get; set; }
        public decimal? QtdInsulinaPorGramaCarbo { get; set; }
        public long Cpf { get; set; }
        public int? Crm { get; set; }
        public virtual List<Agendamento> Agendamentos { get; set; }
        public virtual List<Refeicao> Refeicoes { get; set; }
        public bool? senhaTemporaria { get; set; }
        public virtual List<Usuario> Usuarios { get; set; }
    }
}