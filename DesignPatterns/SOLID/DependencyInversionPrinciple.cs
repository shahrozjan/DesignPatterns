using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.SOLID;

//This code demonstrates the Dependency Inversion Principle (DIP) from SOLID design principles
//using a simple family relationship example. The principle promotes the idea that high-level
//modules (like Research) should not depend on low-level modules (like Relationships) directly.
//Instead, both should depend on abstractions (like IRelationshipBrowser).
public class DependencyInversionPrinciple
{
    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name;
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }
    public class Relationships : IRelationshipBrowser
    {
        private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }
        public List<(Person, Relationship, Person)> Relations => relations;


        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations.Where(
                x=> x.Item1.Name == name && x.Item2 == Relationship.Parent).Select(r => r.Item3);
        }
    }

    public class Research
    {
        public Research(IRelationshipBrowser browser)
        {
            foreach (var relationship in browser.FindAllChildrenOf("Michael"))
            {
                Console.WriteLine(relationship.Name);
            }
        }
        public static void Runner()
        {
            var parent = new Person {Name = "Michael"};
            var child1 = new Person{Name = "Michael 2"};
            var child2 = new Person { Name = "Michael Jr" };
            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);
            new Research(relationships);
        }

        
    }
}