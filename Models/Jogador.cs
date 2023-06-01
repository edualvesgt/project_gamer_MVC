using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_gamer_Back.Models
{
    public class Jogador
    {
        [Key] // DATA ANNOTATION - IDJOGADOR
        public int IdJogador { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        [ForeignKey ("Equipe")] // DATA ANNOTATION
        public int IdEquipe { get; set; }

        public Equipe Equipe { get; set; }
        
    }
}