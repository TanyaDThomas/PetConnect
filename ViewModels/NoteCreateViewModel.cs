using Microsoft.AspNetCore.Mvc.Rendering;
using PetConnect.Domain.Enums;

namespace PetConnect.ViewModels
{
    public class NoteCreateViewModel
    {
        public NoteEntityType EntityType { get; set; }

        // ID of associated animal or adopter
        public int EntityId { get; set; }

        public string? EntityDisplayName { get; set; }

        public NoteCategory Category { get; set; }

        public string Content { get; set; } = "";

        public bool IsInternal { get; set; } = false;

        public string? ReturnUrl { get; set; }

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
