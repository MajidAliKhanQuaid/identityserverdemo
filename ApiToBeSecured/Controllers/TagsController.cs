using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiToBeSecured.ViewModels;
using ApiToBeSecured.Services;
using ApiToBeSecured.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApiToBeSecured.Controllers
{
    // [Authorize(Policy = nameof(Constants.AdministratorRole))]
    [Route("api/[controller]")]
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;
        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var isSuccessResult = await _tagService.GetAllTag();
            if(isSuccessResult == null) return BadRequest();
            return Json(isSuccessResult);
        }

        [HttpGet("{id}", Name = "TagGet")]
        public async Task<IActionResult> Get(Guid id)
        {
            var isSuccessResult = await _tagService.GetTagById(id);
            if(isSuccessResult == null) return BadRequest();
            return Json(isSuccessResult);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TagDto model)
        {
            bool containsId = Guid.TryParse(model.Id, out var guid) && guid != Guid.Empty;
            // if there's no id, then add new
            if (!containsId)
            {
                var isSuccessResult = await _tagService.AddTag(model);

                if (isSuccessResult == "Unsucessfull")
                    return BadRequest();
                else
                {
                    var NewUri = Url.Link("TagGet", new { id = new Guid(isSuccessResult) });
                    return Created(NewUri, model);
                }
            }
            else
            {
                var isSuccessResult = await _tagService.EditTagById(guid, model);

                if (isSuccessResult == "Unsucessfull")
                    return BadRequest();
                else
                {
                    var NewUri = Url.Link("TagGet", new { id = new Guid(isSuccessResult) });
                    return Created(NewUri, model);
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id , [FromBody]TagDto model)
        {
            var isSuccessResult = await _tagService.EditTagById(id,model);

            if(isSuccessResult == "Unsucessfull")
                return BadRequest();
            else 
            {
                var NewUri = Url.Link("TagGet",new{id = new Guid(isSuccessResult)});
                return Created(NewUri,model);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isSuccessResult = await _tagService.DeleteTagById(id);

            if(!isSuccessResult)
                return BadRequest();
            else 
            {
                return Ok();
            }
        }
    }
}