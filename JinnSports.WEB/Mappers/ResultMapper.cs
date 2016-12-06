using JinnSports.Entities.Entities;
using JinnSports.WEB.Views.ViewModels;

namespace JinnSports.WEB.Mappers
{
    internal static class ResultMapper
    {
        public static ResultViewModel MapToResultViewModel(this Result result)
        {
            return new ResultViewModel
            {
                Id = result.Id,
                Score = result.Score
            };
        }
    }
}