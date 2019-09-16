using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDApi.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public DateTime Hora { get; set; }
        public virtual Usuario Paciente { get; set; }
        public virtual Insulina Insulina { get; set; }
    }
}