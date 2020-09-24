using System;
using System.Collections.Generic;
using System.Text;

namespace Shop
{
    class Product : ICloneable
    {
        private static int _counter;
        public string Id { get; private set; }
        public string Name { get; set; }

        public Product()
        {
            Id = 'P' + (++_counter).ToString();
        }
        public Product(string name)
        {
            Id = 'P' + (++_counter).ToString();
            Name = name;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            Product product = obj as Product;
            if (product == null) return false;
            return product.Id == this.Id && product.Name == this.Name;
        }

        public object Clone()
        {
            return new Product {Id = this.Id, Name = this.Name};
        }
    }
}
