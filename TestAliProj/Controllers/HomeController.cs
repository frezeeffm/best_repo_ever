using System;
using System.Threading.Tasks;
using DAL.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace TestAliProj.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbHelper _dbHelper;

        public HomeController(IConfiguration configuration)
        {
            _dbHelper = new DbHelper(configuration);
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Add(Category cat)
        {
            try
            {
                await _dbHelper.Create(cat);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("update")]
        [HttpPost]
        public async Task<IActionResult> Update(Category cat)
        {
            try
            {
                await _dbHelper.UpdateCategory(cat);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("get_tree/{id?}")]
        [HttpGet]
        public async Task<IActionResult> GetTree(int? id)
        {
            try
            {
                var res = await _dbHelper.GetCategoryTree(id);
                return new JsonResult(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}