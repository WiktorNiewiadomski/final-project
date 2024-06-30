using Application.Attributes.TypeAuthorize;
using Application.Interfaces.Services;
using Application.Mappers;
using Application.Models.Group;
using Application.Models.Training;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TrainingController : ControllerBase
	{
        private ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpPost]
        [TypeAuthorize(new[] { MemberType.Owner, MemberType.Coach })]
        public TrainingDto CreateTraining(
            [FromBody] CreateTrainingDto dto
            )
        {
            return TrainingMapper.FromEntity(_trainingService.Create(dto));
        }

        [HttpPatch]
        [Route("{id}")]
        [TypeAuthorize(new[] { MemberType.Owner, MemberType.Coach })]
        public TrainingDto UpdateTraining(
            int id,
            [FromBody] UpdateTrainingDto dto
            )
        {
            return TrainingMapper.FromEntity(_trainingService.Update(id, dto));
        }

        [HttpDelete]
        [Route("{id}")]
        [TypeAuthorize(new[] { MemberType.Owner, MemberType.Coach })]
        public ActionResult DeleteTraining(int id)
        {
            _trainingService.DeleteById(id);
            return Ok();
        }

        [HttpGet]
        public List<TrainingDto> GetAllTrainings()
        {
            return _trainingService.GetAll().Select(TrainingMapper.FromEntity).ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public TrainingDto GetTraining(int id)
        {
            return TrainingMapper.FromEntity(_trainingService.GetById(id));
        }
    }
}
