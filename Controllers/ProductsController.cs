using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

		[Route("{controller}/{action}")]
		[HttpPost]
		public IActionResult Create(Product product)
		{
			// Server side validation if client side javascript is disabled
			if (ModelState.IsValid)
			{
				myProducts.Add(product);
				return RedirectToAction("Index");
			}

			string errorMessages = "The created product has not been validated.";
			foreach (var item in ModelState)
			{
				if (item.Value.ValidationState == ModelValidationState.Invalid)
				{
					errorMessages = $"{errorMessages}\nExceptiion in property {item.Key}:\n";
					foreach (var error in item.Value.Errors)
					{
						errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
					}
				}
			}
			return RedirectToAction("Error", new ProductError(product.Id, errorMessages));
		}

		[Route("{controller}/{new}")]
		public IActionResult Create()
		{
			return View(new Product() { Id = myProducts.MaxBy(p => p.Id).Id + 1 });
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
