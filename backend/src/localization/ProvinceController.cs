using backend.localization;
namespace backend.database;

public class ProvinceController : BaseCrudController<Province, ProvinceCreationDto>
{
  public ProvinceController(ProvinceService crudService) : base(crudService) { }
}