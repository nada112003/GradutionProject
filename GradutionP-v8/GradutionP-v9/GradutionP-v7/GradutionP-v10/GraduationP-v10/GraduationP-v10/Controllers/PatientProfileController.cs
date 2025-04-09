using GradutionP.Interface;
using Microsoft.AspNetCore.Mvc;
using GradutionP.Models;

namespace GradutionP.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        public class PatientProfileController : ControllerBase
        {
            private readonly IPatientProfileService _profileService;

            public PatientProfileController(IPatientProfileService profileService)
            {
                _profileService = profileService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var profiles = await _profileService.GetAllProfiles();
                return Ok(profiles);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var profile = await _profileService.GetProfileById(id);
                if (profile == null)
                {
                    return Ok(new { message = "Patient is not found" });
                }
                return Ok(profile);
            }
            [HttpPost]
            public async Task<IActionResult> Create([FromBody] Profile profile)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var createdProfile = await _profileService.AddProfile(profile);
                return CreatedAtAction(nameof(GetById), new { id = createdProfile.ProfileId }, createdProfile);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromBody] Profile profile)
            {
                var existingProfile = await _profileService.GetProfileById(id);
                if (existingProfile == null)
                {
                    return Ok(new { message = "Patient is not found، Cannot be modified" });
                }

                var updatedProfile = await _profileService.UpdateProfile(id, profile);
                return Ok(updatedProfile);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var existingProfile = await _profileService.GetProfileById(id);
                if (existingProfile == null)
                {
                    return Ok(new { message = "Patient is not found، Cannot be deleted" });
                }

                var result = await _profileService.DeleteProfile(id);
                return Ok(new { message = "Patient successfully deleted" });
            }

        }
    }

