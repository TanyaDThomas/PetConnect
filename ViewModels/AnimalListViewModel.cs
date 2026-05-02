using PetConnect.Application.Services;
using PetConnect.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PetConnect.ViewModels
{
    public class AnimalListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string AnimalTypeName { get; set; } = "";
        public string Breed { get; set; } = "";
        public bool IsAdopted { get; set; }
        public string ShelterName { get; set; } = "";

       

    }
}
