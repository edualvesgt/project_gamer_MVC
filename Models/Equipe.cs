using System.ComponentModel.DataAnnotations;

namespace Projeto_gamer_Back.Models
{
    public class Equipe
    {
        [Key] // DATA ANNOTATION - IDEQUIPE
        public int IdEquipe { get; set; }

        [Required]
        public string Nome { get; set; }
        public string Imagem { get; set; }

        //REFERENCIA A CLASSE EQUIPE PORTANTO A CLASSE EQUIPE TERA ACESSO A CLASSE "JOGADOR"
        public ICollection<Jogador> Jogador { get; set; }

    }
}