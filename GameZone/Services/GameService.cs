using System.Formats.Asn1;
using GameZone.Settings;

namespace GameZone.Services {
    public class GameServices : IGameService {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;

        public GameServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment) {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        }

        public IEnumerable<Game> GetAll() {
            return _context.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .ToList();
        }

        public async Task Create(CreateGameFormViewModel model) {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
            var path = Path.Combine(_imagesPath, coverName);

            using var stream = File.Create(path);
            await model.Cover.CopyToAsync(stream);

            Game game = new() {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
            };

            _context.Games.Add(game);
            _context.SaveChanges();
        }
    }
}
