using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClinicForAnimal.Models
{
    public class LogIn
    {
        public int Id { get; set; }
        [Required(ErrorMessage="UserName required.",AllowEmptyStrings = false)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password required.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        
    }
}