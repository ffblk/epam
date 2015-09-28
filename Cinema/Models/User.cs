#region Assembly EntityFramework.dll, v5.0.0.0
// C:\Users\ffblk\Documents\Visual Studio 2012\Projects\MvcApplication1\packages\EntityFramework.5.0.0\lib\net45\EntityFramework.dll
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Cinema.Models
{
    public enum RolesUser { user, administrator, courier, guest};

    public class User
    {
        public int Id { set; get; }
        string h = @Resources.Resource.Films;        
        [Required(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "NoField")]
        [StringLength(15, MinimumLength = 5, ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "SetName")]        
        public string name { set; get; }
        [Required(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "NoField")]
        [DataType(DataType.Password)]        
        public string pass { set; get; }
        [Required(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "NoField")]
        [Compare("pass", ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "SetPass")]
        [DataType(DataType.Password)]        
        public string passConfirm { set; get; }
        [Required(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "NoField")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "SetMail")]    
        public string mail { set; get; }
        [Required(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "NoField")]
        [RegularExpression(@"[0-9]{6,12}", ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "SetPhone")]        
        public string phone { set; get; }
        public User(string name, string pass, string mail, string tel)
        {
            this.name = name;
            this.pass = pass;
            this.mail = mail;
            this.phone = phone;
        }
        public User() { }
    }
}