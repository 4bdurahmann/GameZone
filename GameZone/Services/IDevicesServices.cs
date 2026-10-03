namespace GameZone.Services;

public interface IDevicesServices {
    IEnumerable<SelectListItem> GetSelectedList();
}
