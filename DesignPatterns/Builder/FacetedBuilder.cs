namespace DesignPatterns.Builder;

public class FacetedBuilder
{
    public class Person
    {
        public string StreetAddress, Postcode, City;
        
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{StreetAddress}, {Postcode}, {City}, {CompanyName}, {Position}, {AnnualIncome}";
        }
    }

    public class PersonBuilder
    {
        protected Person person = new Person();
        
        public PersonJobBuilder Works => new PersonJobBuilder(person);
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earnings(int annualIncome)
        {
            person.AnnualIncome = annualIncome;
            return this;
        }
    }

    public class Demo()
    {
        public static void Runner()
        {
            var pb = new PersonBuilder();
            var person = pb
                .Works.At("Valve")
                .AsA("Software Engineer")
                .Earnings(200000);
        }
    }
}