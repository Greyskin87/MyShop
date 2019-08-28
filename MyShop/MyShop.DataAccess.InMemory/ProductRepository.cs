using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching; //to use the cache
using MyShop.Core.Models; //to access my models

namespace MyShop.DataAccess.InMemory
{
    class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default; //Initialize the cache
        List<Product> products; //Declare my internal collection


        /// <summary>
        /// Constructor: get or create the internal collection
        /// </summary>
        public ProductRepository()
        {
            products = cache["products"] as List<Product>; //Get the products from the inmemory cache
            if (products == null) 
            {
                products = new List<Product>(); //If I don't have product cached, initialize the collection
            }
        }

        /// <summary>
        /// Overwrite the cache using the internal collection
        /// </summary>
        public void Commit()
        {
            cache["products"] = products;
        }


        /// <summary>
        /// Takes care of inserting an object in the cache
        /// </summary>
        /// <param name="p">The product I want to insert</param>
        public void Insert(Product p)
        {
            products.Add(p);
        }

        /// <summary>
        /// Takes care of updating the specified object
        /// </summary>
        /// <param name="product">The product I want to update</param>
        public void Update(Product product)
        {
            //I want to update the product with the same ID as my input
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        /// <summary>
        /// Find a product given the Id
        /// </summary>
        /// <param name="Id">The Id of the product I want</param>
        /// <returns>The product I was searching for</returns>
        public Product Find (string Id)
        {
            Product product = products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        /// <summary>
        /// Returns the entire collection from the cache
        /// </summary>
        /// <returns>The collection as a IQueryable</returns>
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        /// <summary>
        /// Delete a product from memory using its Id
        /// </summary>
        /// <param name="Id">The Id of the product I want to delete</param>
        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
