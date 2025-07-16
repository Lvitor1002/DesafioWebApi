using System.ComponentModel.DataAnnotations;

namespace GruposPokemonApi
{
    public record Objects(
        [Required(ErrorMessage="Para criar o time é obrigatório ter um usuário!")] string User,
        [MinLength(1, ErrorMessage= "A equipe precisa de no mínimo 1 membro!")] List<string> Team
    );
}
