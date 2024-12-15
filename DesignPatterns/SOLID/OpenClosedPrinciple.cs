using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace DesignPatterns.SOLID;

/// <summary>
/// basicaly open closed principle tells you should make classes such that they can be extended
/// and they cant be modified e.g. using interface and abstract classes
/// </summary>
public class OpenClosedPrinciple
{
    private OpenClosedPrinciple()
    {
        
    } 
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Yuge
    }
    public class Product
    {
        public string Name;
        public Size Size;
        public Color Color;
        public Product(string name, Color color, Size size)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            Name = name;
            Color = color;
            Size = size;
        }
    }
    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            return products.Where(p => p.Size == size);
        }
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            return products.Where(p => p.Color == color);
        }
    }

    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T obj);
    }
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> collection, ISpecification<T> specification);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private readonly Color _color;

        public ColorSpecification(Color color)
        {
            _color = color;
        }
        public bool IsSatisfiedBy(Product obj)
        {
            return obj.Color == _color;
        }
    }
    
    public class SizeSpecification : ISpecification<Product>
    {
        private readonly Size _size;
        public SizeSpecification(Size size)
        {
            _size = size;
        }

        public bool IsSatisfiedBy(Product obj)
        {
            return obj.Size == _size;
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> collection, ISpecification<Product> specification)
        {
            // return collection.Where(p => specification.IsSatisfiedBy(p));
            return collection.Where(c => specification.IsSatisfiedBy(c));
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> _first, _second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            _first = first;
            _second = second;
        }

        public bool IsSatisfiedBy(T obj)
        {
            return _first.IsSatisfiedBy(obj) && _second.IsSatisfiedBy(obj);
        }
    }
    public class Demo
    {
        public static void Runner()
        {
            var apple = new Product("Apple", Color.Red, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Medium);
            var house = new Product("House", Color.Blue, Size.Large);
            Product[] products = {apple, tree, house};
            var pf = new ProductFilter();
            foreach (var p in pf.FilterByColor(products, Color.Blue))
            {
                WriteLine("Filter by Color: " + p.Name, Color.Blue);
            }

            var bf = new BetterFilter();
            WriteLine("Blue Products(new): ");
            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Blue)))
            {
                WriteLine("Blue Product: " + p.Name);
            };

            foreach (var p in bf.Filter(products, new AndSpecification<Product>(
                         new ColorSpecification(Color.Blue)
                         ,new SizeSpecification(Size.Large))))
            {
                WriteLine(p.Name+" is Big and Blue");
            }
        }
    }
}