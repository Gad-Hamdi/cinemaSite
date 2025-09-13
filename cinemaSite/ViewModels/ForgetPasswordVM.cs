using System.ComponentModel.DataAnnotations;

namespace cinemaSite.ViewModels
{
    public class ForgetPasswordVM
    {
        public int Id { get; set; }
        [Required]
        public string EmailORUserName { get; set; } = string.Empty;
    }
}
