using System.ComponentModel.DataAnnotations;

namespace NSE.WebApp.MVC.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} a {1} caracteres.", MinimumLength = 5)]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} a {1} caracteres.", MinimumLength = 5)]
        public string PasswordConfirm { get; set; }
    }

    public class UserLogin
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} a {1} caracteres.", MinimumLength = 5)]
        public string Password { get; set; }
    }
}
