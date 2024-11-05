using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solution.BAL.Models;
using Solution.BAL.Services.IServices;

[Authorize]
public class MaintenanceTypeController : Controller
{
    private readonly IMaintenanceTypeService _maintenanceTypeService;

    public MaintenanceTypeController(IMaintenanceTypeService maintenanceTypeService)
    {
        _maintenanceTypeService = maintenanceTypeService;
    }

    public async Task<IActionResult> GetAllMaintenanceTypes(int draw, int start, int length, string searchValue = "")
    {
        try
        {
            var response = await _maintenanceTypeService.GetAllAsync();
            if (response != null)
            {
                var filteredData = string.IsNullOrEmpty(searchValue)
                    ? response.ToList()
                    : response.Where(mt => mt.TypeName != null && mt.TypeName.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();

                var paginatedData = filteredData.Skip(start).Take(length).ToList();

                var result = new
                {
                    draw = draw,
                    recordsTotal = response.Count(),
                    recordsFiltered = filteredData.Count,
                    data = paginatedData
                };

                return Json(result);
            }
            else
            {
                return Json(new { draw = draw, recordsTotal = 0, recordsFiltered = 0, data = new List<MaintenanceType>() });
            }
        }
        catch (Exception ex)
        {
            return Json(new { draw = draw, recordsTotal = 0, recordsFiltered = 0, data = new List<MaintenanceType>() });
        }
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View(new MaintenanceType());
    }

    [HttpPost]
    public async Task<IActionResult> Create(MaintenanceType model)
    {
        await _maintenanceTypeService.AddAsync(model);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var maintenanceType = await _maintenanceTypeService.GetByIdAsync(id);
            if (maintenanceType == null)
            {
                return NotFound();
            }
            return View(maintenanceType);
        }
        catch (Exception ex)
        {
            return RedirectToAction(nameof(Index));
        }
    }
    [HttpPost]
    public async Task<IActionResult> Edit(MaintenanceType model)
    {
        try
        {
            await _maintenanceTypeService.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "An error occurred while updating the maintenance type.");
            return View(model);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _maintenanceTypeService.DeleteAsync(id);

        if (success)
        {
            return Json(new { success = true, message = "Deletion successful." });
        }
        else
        {
            return Json(new { success = false, message = "Deletion failed. Maintenance Type not found." });
        }
    }


}
