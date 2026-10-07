namespace GameZone.Services {
    public interface IGameService {
        Task Create(CreateGameFormViewModel model);
    }
}
