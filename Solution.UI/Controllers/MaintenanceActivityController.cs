using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Solution.BAL.DTO;
using Solution.BAL.Models;
using Solution.BAL.Services.IServices;

[Authorize]
public class MaintenanceActivityController : Controller
{
    private readonly IMaintenanceActivityService _maintenanceActivityService;
    private readonly IVehicleService _vehicleService;
    private readonly IMaintenanceTypeService _MaintenanceTypeService;

    public MaintenanceActivityController(IMaintenanceActivityService maintenanceActivityService, IVehicleService vehicleService, IMaintenanceTypeService maintenanceTypeService)
    {
        _maintenanceActivityService = maintenanceActivityService;
        _vehicleService = vehicleService;
        _MaintenanceTypeService = maintenanceTypeService;
    }

    public async Task<IActionResult> GetAllMaintenanceActivities(int draw, int start, int length, string searchValue = "")
    {
        try
        {
            var response = await _maintenanceActivityService.GetAllAsync();

            var data = response.Select(ma => new MaintenanceActivityDto
            {
                MaintenanceID = ma.MaintenanceID,
                Description = ma.Description,
                MaintenanceTypeName = ma.MaintenanceType?.TypeName,
                VehicleLicenseNumber = ma.Vehicle?.LicenseNumber,
                Date = ma.Date.ToString("yyyy-MM-dd")
            }).ToList();

            var filteredData = data
                .Where(ma => string.IsNullOrEmpty(searchValue)
                            || ma.Description.Contains(searchValue, StringComparison.OrdinalIgnoreCase)
                            || ma.MaintenanceTypeName.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var paginatedData = filteredData.Skip(start).Take(length).ToList();

            var result = new
            {
                draw = draw,
                recordsTotal = data.Count,
                recordsFiltered = filteredData.Count,
                data = paginatedData
            };

            return Json(result);
        }
        catch (Exception ex)
        {
            return Json(new { draw, recordsTotal = 0, recordsFiltered = 0, data = new List<MaintenanceActivityDto>(), error = ex.Message });
        }
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Vehicles = new SelectList(await _vehicleService.GetAllAsync(), "VehicleID", "LicenseNumber");
        ViewBag.MaintenanceTypes = new SelectList(await _MaintenanceTypeService.GetAllAsync(), "MaintenanceTypeID", "TypeName");
        return View(new MaintenanceActivity());
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MaintenanceActivity model)
    {
        await _maintenanceActivityService.AddAsync(model);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var maintenanceActivity = await _maintenanceActivityService.GetByIdAsync(id);
        if (maintenanceActivity == null)
        {
            return NotFound();
        }

        ViewBag.Vehicles = new SelectList(await _vehicleService.GetAllAsync(), "VehicleID", "LicenseNumber", maintenanceActivity.VehicleID);
        ViewBag.MaintenanceTypes = new SelectList(await _MaintenanceTypeService.GetAllAsync(), "MaintenanceTypeID", "TypeName", maintenanceActivity.MaintenanceTypeID);

        return View(maintenanceActivity);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(MaintenanceActivity model)
    {
        await _maintenanceActivityService.UpdateAsync(model);
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _maintenanceActivityService.DeleteAsync(id);

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
