using System;
using System.ComponentModel.DataAnnotations;

namespace Html.Models
{
    public class LogOnModel
    {
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ValidationRequired")]
        [DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ValidationRequired")]
        [DataType(DataType.Text)]
        public string Username
        {
            get;
            set;
        }
    }
}