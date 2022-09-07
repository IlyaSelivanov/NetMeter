using Microsoft.EntityFrameworkCore;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data
{
    public class ProfileRepo : IProfileRepo
    {
        private readonly AppDbContext _context;

        public ProfileRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePlanProfileAsync(int planId, Profile profile)
        {
            if(profile == null)
                throw new ArgumentNullException($"{typeof(Profile)} {nameof(profile)} is null");
            
            var plan = _context.Plans
                .Include(p => p.Profile)
                .FirstOrDefault(p => p.Id == planId);

            if(plan != null && plan.Profile == null)
            {
                profile.PlanId = planId;
                _context.Profiles.Add(profile);

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task DeletePlanProfileAsync(int planId)
        {
            var profile = await _context.Profiles
                .FirstOrDefaultAsync(p => p.PlanId == planId);

            if(profile != null)
            {
                _context.Profiles.Remove(profile);
                await _context.SaveChangesAsync();
            }
                
        }

        public async Task<Profile> GetPlanProfileAsync(int planId)
        {
            var profile = await _context.Profiles
                .FirstOrDefaultAsync(p => p.PlanId == planId);

            return profile;

        }

        public async Task<bool> UpdatePlanProfileAsync(int planId, Profile profile)
        {
            if(profile == null)
                throw new ArgumentNullException($"{typeof(Profile)} {nameof(profile)} is null");
            
            if(await _context.Profiles.ContainsAsync(profile))
            {
                _context.Entry(profile).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}