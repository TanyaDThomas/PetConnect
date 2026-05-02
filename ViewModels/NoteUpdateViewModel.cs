using Microsoft.AspNetCore.Mvc.Rendering;
using PetConnect.Domain.Enums;

namespace PetConnect.ViewModels
{
    public class NoteUpdateViewModel
    {
        public int Id { get; set; }
        public NoteCategory Category { get; set; }
        public string Content { get; set; } = "";
        public bool IsInternal { get; set; } = false;
        public DateTime UpdatedOn { get; set; }

        // Dropdown ENUM Category List
        public IEnumerable<SelectListItem> CategoryOptions =>
           Enum.GetValues<NoteCategory>()
               .Select(c => new SelectListItem
               {
                   Value = c.ToString(),
                   Text = c.ToString()
               });
    }
}
