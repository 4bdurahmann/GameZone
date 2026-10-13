namespace GameZone.ViewModels;

public class CreateGameFormViewModel : GameFormViewModel {
	[AllowedExtensions(FileSettings.AllowedExtension), MaxFileSize(FileSettings.MaxFileSizeInByte)]
	public IFormFile Cover { get; set; } = default!;
}