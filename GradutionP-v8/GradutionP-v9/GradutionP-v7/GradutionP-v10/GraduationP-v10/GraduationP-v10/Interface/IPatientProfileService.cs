using GradutionP.Models;
namespace GradutionP.Interface
{
    public interface IPatientProfileService
    {
        Task<IEnumerable<Profile>> GetAllProfiles();//doctor get patient profile
        Task<Profile> GetProfileById(int id);
        Task<Profile> AddProfile(Profile profile);
        Task<Profile> UpdateProfile(int id, Profile profile);
        Task<bool> DeleteProfile(int id);
    }
}
