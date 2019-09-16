using System.Collections.Generic;

namespace ControleDApi.Models
{
    public class Insulina
    {
        public Insulina()
        {
            this.Refeicoes = new List<Refeicao>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual List<Refeicao> Refeicoes { get; set; }
    }
}