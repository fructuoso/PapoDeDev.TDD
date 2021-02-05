using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PapoDeDev.TDD.Domain.Core.Entity;
using PapoDeDev.TDD.Domain.Core.Interfaces.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PapoDeDev.TDD.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public abstract class GenericControllerCrud<TKey, TEntity, TModel> : ControllerBase where TKey : struct where TEntity : BaseEntity<TKey>
    {
        private readonly IServiceCrud<TKey, TEntity> _Service;
        protected readonly IMapper _Mapper;

        protected GenericControllerCrud(
            IServiceCrud<TKey, TEntity> service,
            IMapper mapper)
        {
            _Service = service;
            _Mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TModel>> Get()
        {
            IEnumerable<TEntity> entities = _Service.GetAll();
            IEnumerable<TModel> models = _Mapper.Map<IEnumerable<TModel>>(entities);
            return Ok(models);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TModel>> Get(TKey id)
        {
            TEntity entity = await _Service.GetAsync(id);

            if (entity == null) return NotFound();

            TModel model = _Mapper.Map<TModel>(entity);
            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] TModel model)
        {
            TEntity entity = await _Service.AddAsync(_Mapper.Map<TEntity>(model));

            return CreatedAtAction(nameof(Get), new { id = entity.Id }, _Mapper.Map<TModel>(entity));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(TKey id, [FromBody] TModel model)
        {
            TEntity input = _Mapper.Map<TEntity>(model);
            input.Id = id;
            TEntity entity = await _Service.UpdateAsync(input);
            return entity == null ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(TKey id)
        {
            TEntity entity = await _Service.DeleteAsync(id);
            return entity == null ? NotFound() : NoContent();
        }
    }
}