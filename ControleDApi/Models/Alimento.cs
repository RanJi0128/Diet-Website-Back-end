using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ControleDApi.Models
{
    public class Alimento
    {
        public Alimento()
        {
            this.AlimentosConsumo = new List<AlimentoConsumo>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Descrição é obrigatório!")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Qtd. Carboidrato é obrigatório!")]
       // public decimal QtdCarboidrato { get; set; }
        public int Classificacao { get; set; }
        public string Umidade { get; set; }
        public Energia Energia { get; set; }
        public string Proteina { get; set; }
        public string Lipideos { get; set; }
        public string Colesterol { get; set; }
        public string Carboidrato { get; set; }
        public string FibraAlimentar { get; set; }
        public string Cinzas { get; set; }
        public string Calcio { get; set; }
        public string Magnesio { get; set; }
        public string Manganes { get; set; }
        public string Fosforo { get; set; }
        public string Ferro { get; set; }
        public string Sodio { get; set; }
        public string Potassio { get; set; }
        public string Cobre { get; set; }
        public string Zinco { get; set; }
        public string Retinol { get; set; }
        public string Re { get; set; }
        public string Rae { get; set; }
        public string Tiamina { get; set; }
        public string Riboflavina { get; set; }
        public string Piridoxina { get; set; }
        public string Niacina { get; set; }
        public string VitaminaC { get; set; }
        public virtual List<AlimentoConsumo> AlimentosConsumo { get; set; }
    }
}