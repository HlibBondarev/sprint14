using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsWithRouting.Models;
using ProductsWithRouting.Services;

namespace ProductsWithRouting.Controllers
{
	public class ProductsController : Controller
    {
        private List<Product> myProducts;

        public ProductsController(Data data)
        {
            myProducts = data.Products;
        }

		[Route("{controller}/{action}")]
		[Route("{items}/{action}")]
		[Route("{controller}")]
		[Route("{items}")]
        public IActionResult Index(int filterId, string filtername)
        {
            return View(myProducts);
        }

        public IActionResult View(int id)
        {
            if (myProducts.Find(x => x.Id == id) == null)
                return RedirectToAction("Error", new ProductError(id, "Wrong product Id input: "));

            //Please, add your implementation of the method
            return View(/*TODO: pass corresponding product here*/);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (myProducts.Find(x => x.Id == id) == null)
                return RedirectToAction("Error", new ProductError(id, "Wrong Id input: "));

            return View(/*TODO: pass corresponding product here*/);
        } 
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            //Please, add your implementation of the method
            return View(/*TODO: pass corresponding product here*/);
        } 
        
        [HttpPost]
        public IActionResult Create(Product product)
        {
            //Please, add your implementation of the method
            return View(/*TODO: pass corresponding product here*/);
        }

        public IActionResult Create()
        {
            //Please, add your implementation of the method
            return View(/*TODO: pass corresponding product here*/);
        }

        public IActionResult Delete(int id)
        {
            if (myProducts.Find(x => x.Id == id) == null)
                return RedirectToAction("Error", new ProductError(id, "Wrong Id input: "));

            //Please, add your implementation of the method
            return View("Index"/*TODO: pass corresponding product here*/);
        }


        [Route("~/product-error")]
        public IActionResult Error(ProductError error)
        {            
            return View(error);
        }
    }
}
