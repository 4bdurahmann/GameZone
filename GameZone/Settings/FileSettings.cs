namespace GameZone.Settings;

public static class FileSettings {
	public const string ImagePath = "/assets/images/games";
	public const string AllowedExtension = ".jpg,.jpeg,.png";
	public const int MaXFileSizeInMB = 1;
	public const int MaxFileSizeInByte = MaXFileSizeInMB * 1024 * 1024;
}