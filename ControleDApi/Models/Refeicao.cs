using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDApi.Models
{
    public class Refeicao
    {
        public Refeicao()
        {
            this.AlimentosConsumo = new List<AlimentoConsumo>();
        }
        public int Id { get; set; }
        public decimal QtdCarboidrato { get; set; }
        public decimal QtdInsulina { get; set; }
        public virtual Insulina Insulina { get; set; }
        public virtual Usuario Pessoa { get; set; }
        public virtual List<AlimentoConsumo> AlimentosConsumo { get; set; }
        
    }
}