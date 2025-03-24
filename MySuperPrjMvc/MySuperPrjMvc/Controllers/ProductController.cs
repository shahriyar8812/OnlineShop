using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySuperPrjMvc.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICategoryAttributeService _categoryAttributeService;

        public ProductController(IProductService productService, ICategoryService categoryService, ICategoryAttributeService categoryAttributeService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _categoryAttributeService = categoryAttributeService;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            var products = await _productService.GetAllProductsAsync() ?? new List<Product>();
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value).ToList();
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            var allCategoryAttributes = await _categoryAttributeService.GetAllAttributesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", categoryId);
            ViewBag.CategoryAttributes = allCategoryAttributes?.ToDictionary(ca => ca.Id, ca => ca.Name) ?? new Dictionary<int, string>();

            return View(products);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Product model)
        {
            ModelState.Remove("Category");
            if (model.ProductCategoryAttributes != null)
            {
                for (int i = 0; i < model.ProductCategoryAttributes.Count; i++)
                {
                    ModelState.Remove($"ProductCategoryAttributes[{i}].Product");
                    ModelState.Remove($"ProductCategoryAttributes[{i}].CategoryAttribute");
                }
            }

            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name", model.CategoryId);
                return View(model);
            }

            var product = new Product
            {
                Title = model.Title,
                Code = model.Code,
                CategoryId = model.CategoryId,
                Description = model.Description ?? "",
                ImageUrl = model.ImageUrl ?? "",
                ProductCategoryAttributes = model.ProductCategoryAttributes?.Select(a => new ProductCategoryAttribute
                {
                    CategoryAttributeId = a.CategoryAttributeId,
                    Value = a.Value,
                    ProductId = 0
                }).ToList() ?? new List<ProductCategoryAttribute>()
            };

            try
            {
                await _productService.AddProductAsync(product);

                if (product.Id > 0 && product.ProductCategoryAttributes != null)
                {
                    foreach (var attr in product.ProductCategoryAttributes)
                    {
                        attr.ProductId = product.Id;
                    }
                    await _productService.UpdateProductAsync(product);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "خطایی در ایجاد محصول رخ داد.");
                var categories = await _categoryService.GetAllCategoriesAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name", model.CategoryId);
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            var allCategoryAttributes = await _categoryAttributeService.GetAllAttributesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
            ViewBag.CategoryAttributes = allCategoryAttributes?.ToDictionary(ca => ca.Id, ca => ca.Name) ?? new Dictionary<int, string>();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Product model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            ModelState.Remove("Category");
            if (model.ProductCategoryAttributes != null)
            {
                for (int i = 0; i < model.ProductCategoryAttributes.Count; i++)
                {
                    ModelState.Remove($"ProductCategoryAttributes[{i}].Product");
                    ModelState.Remove($"ProductCategoryAttributes[{i}].CategoryAttribute");
                }
            }

            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                var allCategoryAttributes = await _categoryAttributeService.GetAllAttributesAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name", model.CategoryId);
                ViewBag.CategoryAttributes = allCategoryAttributes?.ToDictionary(ca => ca.Id, ca => ca.Name) ?? new Dictionary<int, string>();
                return View(model);
            }

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Title = model.Title;
            product.Code = model.Code;
            product.Description = model.Description ?? "";
            product.CategoryId = model.CategoryId;
            product.ImageUrl = model.ImageUrl ?? "";
            product.ProductCategoryAttributes = model.ProductCategoryAttributes?.Select(a => new ProductCategoryAttribute
            {
                Id = a.Id,
                ProductId = id,
                CategoryAttributeId = a.CategoryAttributeId,
                Value = a.Value
            }).ToList() ?? new List<ProductCategoryAttribute>();

            try
            {
                await _productService.UpdateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "خطایی در به‌روزرسانی محصول رخ داد.");
                var categories = await _categoryService.GetAllCategoriesAsync();
                var allCategoryAttributes = await _categoryAttributeService.GetAllAttributesAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name", model.CategoryId);
                ViewBag.CategoryAttributes = allCategoryAttributes?.ToDictionary(ca => ca.Id, ca => ca.Name) ?? new Dictionary<int, string>();
                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryAttributes(int categoryId)
        {
            var categoryAttributes = await _categoryAttributeService.GetAttributesByCategoryIdAsync(categoryId);
            return Json(categoryAttributes.Select(ca => new { id = ca.Id, name = ca.Name }));
        }
    }
}