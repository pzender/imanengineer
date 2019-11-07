using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_App.Models;
using TV_App.DataTransferObjects;

namespace TV_App.Services
{
    public class ProgrammeService
    {
        readonly string[] IGNORED_TITLES = { "Zakonczenie programu" };
        private readonly TvAppContext db = new TvAppContext();
        public IEnumerable<ProgrammeDTO> GetFilteredProgrammes(Filter filter = null, string user = null)
        {
            var programmes = db.Programmes
                .Include(prog => prog.ProgrammesFeatures)
                .AsNoTracking()
                .ToList();
            var emissions = db.Emissions
                .Include(em => em.ChannelEmitted)
                .AsNoTracking()
                .ToList();
            var features = db.Features
                .Include(f => f.RelType)
                .AsNoTracking()
                .ToList();
            var ratings = db.Ratings
                .Where(r => r.UserLogin == user)
                .AsNoTracking()
                .ToList();
            
            var filtered_emissions = emissions.Where(em => filter.Apply(em)).ToList();

            var result = from programme in programmes
                         join emission in filtered_emissions on programme.Id equals emission.ProgrammeId into prog_emissions
                         where prog_emissions.Any() && !IGNORED_TITLES.Contains(programme.Title)
                         select new ProgrammeDTO()
                         {
                             Id = programme.Id,
                             Title = programme.Title,
                             IconUrl = programme.IconUrl,
                             Rating = ratings.FirstOrDefault(r => r.ProgrammeId == programme.Id)?.RatingValue,
                             Emissions = prog_emissions.Select(em => new EmissionDTO(em)),
                             Features = features.Where(f => programme.ProgrammesFeatures.Select(pf => pf.FeatureId).Contains(f.Id)).Select(f => new FeatureDTO(f))
                         };
            Console.WriteLine($"[{DateTime.Now}] Filtered - {result.Count()} programmes after filter; {programmes.Count} before filter");
            return result;
        }

        public ProgrammeDTO GetById(long id)
        {
            Programme target = db.Programmes
                .Include(prog => prog.Descriptions)
                .Include(prog => prog.Emissions)
                .ThenInclude(em => em.ChannelEmitted)
                .Include(prog => prog.ProgrammesFeatures)
                .ThenInclude(pf => pf.RelFeature)
                .ThenInclude(f => f.RelType)
                .Single(prog => prog.Id == id);
            return (from prog in db.Programmes where prog.Id == id select new ProgrammeDTO(prog)).Single();
        }

        public IEnumerable<ProgrammeDTO> GetRatedBy(string username)
        {
            var programmes = db.Programmes
                .Include(prog => prog.ProgrammesFeatures)
                .AsNoTracking()
                .ToList();
            var features = db.Features
                .Include(f => f.RelType)
                .AsNoTracking()
                .ToList();
            var ratings = db.Ratings
                .Where(r => r.UserLogin == username)
                .AsNoTracking()
                .ToList();

            var result = from programme in programmes
                         select new ProgrammeDTO()
                         {
                             Id = programme.Id,
                             Title = programme.Title,
                             IconUrl = programme.IconUrl,
                             Rating = ratings.FirstOrDefault(r => r.ProgrammeId == programme.Id)?.RatingValue,
                             Features = features.Where(f => programme.ProgrammesFeatures.Select(pf => pf.FeatureId).Contains(f.Id)).Select(f => new FeatureDTO(f))
                         };
            Console.WriteLine($"[{DateTime.Now}] Ratings - {result.Count()} programmes after filter; {programmes.Count} before filter");
            return result;
        }
        public IEnumerable<ProgrammeDTO> GetNotificationsFor(string username)
        {
            var programmes = db.Programmes
                .Include(prog => prog.ProgrammesFeatures)
                .AsNoTracking()
                .ToList();
            var features = db.Features
                .Include(f => f.RelType)
                .AsNoTracking()
                .ToList();
            var notifications = db.Notifications
                .Include(n => n.RelEmission)
                    .ThenInclude(em => em.ChannelEmitted)
                .Where(r => r.UserLogin == username)
                .AsNoTracking()
                .ToList();

            var result = from notification in notifications
                         join programme in programmes on notification.RelEmission.ProgrammeId equals programme.Id
                         select new ProgrammeDTO()
                         {
                             Id = programme.Id,
                             Title = programme.Title,
                             IconUrl = programme.IconUrl,
                             Features = features.Where(f => programme.ProgrammesFeatures.Select(pf => pf.FeatureId).Contains(f.Id)).Select(f => new FeatureDTO(f)),
                             Emissions = new List<EmissionDTO>()
                             {
                                 new EmissionDTO(notification.RelEmission)
                             }
                         };
            Console.WriteLine($"[{DateTime.Now}] Notifications - {result.Count()} programmes after filter; {programmes.Count} before filter");
            return result;
        }

        public IEnumerable<ProgrammeDTO> GetWithFeature(Feature feature, Filter filter = null, string user = null)
        {
            var programmes = db.Programmes
                .Include(prog => prog.ProgrammesFeatures)
                .Where(prog => prog.ProgrammesFeatures.Any(pf => pf.FeatureId == feature.Id))
                .AsNoTracking()
                .ToList();
            var emissions = db.Emissions
                .Include(em => em.ChannelEmitted)
                .AsNoTracking()
                .ToList();
            var features = db.Features
                .Include(f => f.RelType)
                .AsNoTracking()
                .ToList();
            var ratings = db.Ratings
                .Where(r => r.UserLogin == user)
                .AsNoTracking()
                .ToList();

            var filtered_emissions = emissions.Where(em => filter.Apply(em)).ToList();

            var result = from programme in programmes
                         join emission in filtered_emissions on programme.Id equals emission.ProgrammeId into prog_emissions
                         where prog_emissions.Any() && !IGNORED_TITLES.Contains(programme.Title)
                         select new ProgrammeDTO()
                         {
                             Id = programme.Id,
                             Title = programme.Title,
                             IconUrl = programme.IconUrl,
                             Rating = ratings.FirstOrDefault(r => r.ProgrammeId == programme.Id)?.RatingValue,
                             Emissions = prog_emissions.Select(em => new EmissionDTO(em)),
                             Features = features.Where(f => programme.ProgrammesFeatures.Select(pf => pf.FeatureId).Contains(f.Id)).Select(f => new FeatureDTO(f))
                         };

            return result;

        }

        public IEnumerable<ProgrammeDTO> GetBySearchTerm(string term, Filter filter = null, string user = null)
        {
            var programmes = db.Programmes
                .Include(prog => prog.ProgrammesFeatures)
                .Where(prog => prog.Title.ToLower().Contains(term.ToLower()))
                .AsNoTracking()
                .ToList();
            var emissions = db.Emissions
                .Include(em => em.ChannelEmitted)
                .AsNoTracking()
                .ToList();
            var features = db.Features
                .Include(f => f.RelType)
                .AsNoTracking()
                .ToList();
            var ratings = db.Ratings
                .Where(r => r.UserLogin == user)
                .AsNoTracking()
                .ToList();

            var filtered_emissions = emissions.Where(em => filter.Apply(em)).ToList();

            var result = from programme in programmes
                         join emission in filtered_emissions on programme.Id equals emission.ProgrammeId into prog_emissions
                         where prog_emissions.Any() && !IGNORED_TITLES.Contains(programme.Title)
                         select new ProgrammeDTO()
                         {
                             Id = programme.Id,
                             Title = programme.Title,
                             IconUrl = programme.IconUrl,
                             Rating = ratings.FirstOrDefault(r => r.ProgrammeId == programme.Id)?.RatingValue,
                             Emissions = prog_emissions.Select(em => new EmissionDTO(em)),
                             Features = features.Where(f => programme.ProgrammesFeatures.Select(pf => pf.FeatureId).Contains(f.Id)).Select(f => new FeatureDTO(f))
                         };
            
            Console.WriteLine($"[{DateTime.Now}] Search for {term} - {result.Count()} programmes after filter; {programmes.Count} before filter");

            return result;

        }
    }
}
