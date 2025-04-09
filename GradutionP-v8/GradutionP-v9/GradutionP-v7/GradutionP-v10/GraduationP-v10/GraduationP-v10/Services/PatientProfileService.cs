using GradutionP.Interface;
using GradutionP.Data;
using GradutionP.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationP.Services
{
 
        public class PatientProfileService : IPatientProfileService
        {
            private readonly AppDbContext _context;

            public PatientProfileService(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Profile>> GetAllProfiles()
            {
                return await _context.Profiles.ToListAsync();
            }

            public async Task<Profile> GetProfileById(int id)
            {
                return await _context.Profiles.FindAsync(id);
            }

            public async Task<Profile> AddProfile(Profile profile)
            {
                _context.Profiles.Add(profile);
                await _context.SaveChangesAsync();
                return profile;
            }

            public async Task<Profile> UpdateProfile(int id, Profile profile)
            {
                var existingProfile = await _context.Profiles.FindAsync(id);
                if (existingProfile == null) return null;

                existingProfile.ProfileId = profile.ProfileId;
                existingProfile.FullName = profile.FullName;
                existingProfile.ProfilePicture = profile.ProfilePicture;
                existingProfile.DateOfBirth = profile.DateOfBirth;
                existingProfile.Gender = profile.Gender;
                existingProfile.Age = profile.Age;
                existingProfile.NationalID = profile.NationalID;
                existingProfile.BloodType = profile.BloodType;
                existingProfile.ChronicDiseases = profile.ChronicDiseases;
                existingProfile.Allergies = profile.Allergies;
                existingProfile.CurrentMedications = profile.CurrentMedications;
                existingProfile.InsuranceProvider = profile.InsuranceProvider;
                existingProfile.PatientId = profile.PatientId;



                await _context.SaveChangesAsync();
                return existingProfile;
            }

            public async Task<bool> DeleteProfile(int id)
            {
                var profile = await _context.Profiles.FindAsync(id);
                if (profile == null) return false;

                _context.Profiles.Remove(profile);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }

