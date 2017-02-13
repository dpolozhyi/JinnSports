using JinnSports.BLL.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace JinnSports.BLL.Interfaces
{
    public interface IConformityService
    {
        List<ConformityDto> GetConformities();
        ConformityViewModel GetConformityViewModel(int id);
        void Save(ConformityViewModel model);
    }
}
