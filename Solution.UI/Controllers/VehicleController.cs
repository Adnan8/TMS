using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solution.BAL.Models;
using Solution.BAL.Services.IServices;
[Authorize]
public class VehicleController : Controller
{
    private readonly IVehicleService _vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public async Task<IActionResult> GetAllVehicles(int draw, int start, int length, string searchValue = "")
    {
        try
        {
            var response = await _vehicleService.GetAllAsync();
            if (response != null)
            {
                var filteredData = string.IsNullOrEmpty(searchValue)
                    ? response.ToList()
                    : response.Where(c => (c.Make != null && c.Make.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                                       || (c.Model != null && c.Model.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                                       || (c.LicenseNumber != null && c.LicenseNumber.Contains(searchValue, StringComparison.OrdinalIgnoreCase))).ToList();

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
                return Json(new { draw = draw, recordsTotal = 0, recordsFiltered = 0, data = new List<Vehicle>() });
            }
        }
        catch (Exception ex)
        {
            return Json(new { draw = draw, recordsTotal = 0, recordsFiltered = 0, data = new List<Vehicle>() });
        }
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View(new Vehicle());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Vehicle vModel)
    {
        await _vehicleService.AddAsync(vModel);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var vehicle = await _vehicleService.GetByIdAsync(id);
        if (vehicle == null)
        {
            return NotFound();
        }
        return View(vehicle);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Vehicle vModel)
    {
        try
        {
            await _vehicleService.UpdateAsync(vModel);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "An error occurred while updating the vehicle.");
            return View(vModel);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _vehicleService.DeleteAsync(id);

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
