using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ProductsController : ApiController
    {
        //TODO : Get real data from database and get it when needed, not globally
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
            //TODO : Get real data from database here
            return Ok(_products);
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult GetProduct([FromUri] int Id)
        {
            //TODO : Get data from database (GetById)
            var product = _products.Find(x => x.Id == Id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("GetByPrice")]
        public IHttpActionResult GetPrice([FromUri] int Price)
        {
            //TODO : Get data from database (GetByPrice)
            var price = _products.Find(x => x.Price == Price);
            if(price == null)
            {
                return NotFound();
            }
            return Ok(price);
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create([FromBody] UserProductModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int maxId = _products.Max(x => x.Id);
            product.Id = (maxId + 1);
            _products.ToList().Add(product);
            return Content(HttpStatusCode.Created, product.Id);
        }

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update([FromBody] UserProductModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //TODO GetbyId
            if (!_products.ToList().Exists(x => x.Id == product.Id))
            {
                return NotFound();
            }

            var productToUpdate = _products.ToList().Find(x => x.Id == product.Id);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.Price = product.Price;
            productToUpdate.Category = product.Category;
            productToUpdate.Model = product.Model;
            _products.ToList().RemoveAll(x => x.Id == product.Id);
            _products.ToList().Add(product);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            if (!_products.ToList().Exists(x => x.Id == id))
            {
                return NotFound();
            }
            _products.ToList().RemoveAll(x => x.Id == id);
            return Content(HttpStatusCode.NoContent, "deleted");
        }

        [HttpGet]
        [Route("GetDashboard")]
        public Charts GetDashboard()
        {
            var lst = Helpers.ChartsHelper.GetRandomSurvey();
            var charts = new Charts()
            {
                lstCharts1 = Helpers.ChartsHelper.SetDataForChart1(lst),
                lstCharts2 = Helpers.ChartsHelper.SetDataForChart2(lst),
                lstCharts3 = Helpers.ChartsHelper.SetDataForChart3(lst)
            };
            return charts;
        }

    }
}
