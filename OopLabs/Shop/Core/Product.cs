using System;

namespace Shop
{
    class Product
    {
        private static int _counter;
        public string Id { get; }
        public string Name { get; }

        public Product()
            : this("")
        {}
        public Product(string name)
        {
            Id = 'P' + (++_counter).ToString();
            Name = name;
        }

        private Product(string id, string name)
        {
            Id = id;
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

        public Product CopyWith(string id, string name) => new Product(id, name);
    }
}
