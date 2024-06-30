using Application.Interfaces.Services;
using Application.Mappers;
using Application.Models.Group;
using Application.Models.Training;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainingController : ControllerBase
	{
        private ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpPost]
        public TrainingDto CreateMember(
            [FromBody] CreateTrainingDto dto
            )
        {
            return TrainingMapper.FromEntity(_trainingService.Create(dto));
        }

        [HttpPatch]
        [Route("{id}")]
        public TrainingDto UpdateMember(
            int id,
            [FromBody] UpdateTrainingDto dto
            )
        {
            return TrainingMapper.FromEntity(_trainingService.Update(id, dto));
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteMember(int id)
        {
            _trainingService.DeleteById(id);
            return Ok();
        }

        [HttpGet]
        public List<TrainingDto> GetAllMembers()
        {
            return _trainingService.GetAll().Select(TrainingMapper.FromEntity).ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public TrainingDto GetMember(int id)
        {
            return TrainingMapper.FromEntity(_trainingService.GetById(id));
        }
    }
}
