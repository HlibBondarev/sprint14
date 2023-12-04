﻿using System;
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
        public IActionResult Index(int? filterId, string filterName)
        {
            var filteredProducts = myProducts.ToList();
            if (filterId != null)
            {
                filteredProducts = myProducts
                    .Where(p => p.Id == filterId)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(filterName))
            {
                filteredProducts = myProducts
                    .Where(p => p.Name.Equals(filterName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
    
            return View(filteredProducts);
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
            var productToEdit = myProducts.Find(x => x.Id == id);
            if ( productToEdit == null)
                return RedirectToAction("Error", new ProductError(id, "Wrong Id input: "));

            return View(productToEdit);
        } 
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            int productIndex = myProducts.FindIndex(p => p.Id == product.Id);
            if (productIndex == -1)
            {
                return RedirectToAction("Error", new ProductError(product.Id, "Wrong product Id: "));
            }

            myProducts[productIndex] = product;
            
            return View("Index", myProducts);
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

        [Route("products/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var product = myProducts.Find(x => x.Id == id);
            if (product == null)
                return RedirectToAction("Error", new ProductError(id, "No product with this Id was found: "));

            myProducts.Remove(product);

            return RedirectToAction("Index");
        }


        [Route("~/product-error")]
        public IActionResult Error(ProductError error)
        {            
            return View(error);
        }
    }
}
