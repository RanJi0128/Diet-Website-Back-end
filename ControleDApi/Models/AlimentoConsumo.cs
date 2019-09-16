namespace ControleDApi.Models
{
    public class AlimentoConsumo
    {
        public int Id { get; set; }
        public decimal QtdAlimento { get; set; }
        public decimal QtdCarboidrato { get; set; }
        public decimal QtdInsulina { get; set; }
        public virtual Alimento Alimento { get; set; }
        public virtual Refeicao Refeicao { get; set; }
    }
}