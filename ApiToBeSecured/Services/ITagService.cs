using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiToBeSecured.ViewModels;
using ApiToBeSecured.Models;

namespace ApiToBeSecured.Services
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllTag();
        Task<Tag> GetTagById(Guid id);
        Task<string> AddTag(TagDto tag);
        Task<string> EditTagById(Guid id, TagDto tag);
        Task<bool> DeleteTagById(Guid id);
    }
}