
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace TwitterBot.Core.ViewModel
{
    public class UserVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [ValidateNever]
        public string Role { get; set; }
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        [ValidateNever]

        public IEnumerable<SelectListItem>? RoleList { get; set; }

    }
}
