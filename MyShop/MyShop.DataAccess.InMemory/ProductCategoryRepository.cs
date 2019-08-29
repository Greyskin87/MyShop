using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching; //to use the cache
using MyShop.Core.Models; //to access my models

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default; //Initialize the cache
        List<ProductCategory> productCategories; //Declare my internal collection


        /// <summary>
        /// Constructor: get or create the internal collection
        /// </summary>
        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>; //Get the products from the inmemory cache
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>(); //If I don't have product cached, initialize the collection
            }
        }

        /// <summary>
        /// Overwrite the cache using the internal collection
        /// </summary>
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }


        /// <summary>
        /// Takes care of inserting an object in the cache
        /// </summary>
        /// <param name="p">The product I want to insert</param>
        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        /// <summary>
        /// Takes care of updating the specified object
        /// </summary>
        /// <param name="productCategory">The product I want to update</param>
        public void Update(ProductCategory productCategory)
        {
            //I want to update the product with the same ID as my input
            ProductCategory CategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);

            if (CategoryToUpdate != null)
            {
                CategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product category not found");
            }
        }

        /// <summary>
        /// Find a product given the Id
        /// </summary>
        /// <param name="Id">The Id of the product I want</param>
        /// <returns>The product I was searching for</returns>
        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == Id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product category not found");
            }
        }

        /// <summary>
        /// Returns the entire collection from the cache
        /// </summary>
        /// <returns>The collection as a IQueryable</returns>
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        /// <summary>
        /// Delete a product from memory using its Id
        /// </summary>
        /// <param name="Id">The Id of the product I want to delete</param>
        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id);

            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product category not found");
            }
        }
    }
}
