using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class WebApiController : ApiController
    {
        List<UserProductModel> _products = new UserProductModel[]
        {
            new UserProductModel {Id = 1, ProductName = "Asus", Category = "Laptop", Price = 1200, Model = "NG-28-30"},
            new UserProductModel {Id = 2, ProductName = "Sony", Category = "Laptop", Price = 1000, Model = "MS-26-15"},
            new UserProductModel {Id = 3, ProductName = "HP", Category = "Desktop", Price = 800, Model = "LP-34-56"}
        }.ToList();

        [HttpGet]
        [Route("GetAll")]

        public IHttpActionResult GetAllProducts()
        {
            return Ok(_products);
        }

        [HttpGet]
        [Route("GetById")]

        public IHttpActionResult GetProduct([FromUri] int Id)
        {
            var product = _products.Find(x => x.Id == Id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


    }
}
