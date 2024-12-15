using System;
namespace DesignPatterns.Builder;

public class StepwiseBuilder
{
    public enum CarType
    {
        Sedan, Crossover
    }

    public class Car
    {
        public CarType Type;
        public int WheelSize;
    }
    
    public interface ISpecifyCarType
    {
        ISpecifyWheelSize OfType(CarType carType);
    }

    public interface ISpecifyWheelSize
    {
        IBuildCar WithWheelSize(int wheelSize);
    }

    public interface IBuildCar
    {
        public Car Build();
    }

    public class CarBuilder()
    {
        private class Impl : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
        {
            private Car _car = new Car();
            public ISpecifyWheelSize OfType(CarType carType)
            {
                _car.Type = carType;
                return this;
            }

            public IBuildCar WithWheelSize(int wheelSize)
            {
                switch (_car.Type)  
                {
                    case CarType.Crossover when wheelSize < 17 || wheelSize > 20:
                    case CarType.Sedan when wheelSize < 15 && wheelSize > 17:
                        throw new Exception($"Cannot build car with wheel size {wheelSize}.");
                }
                _car.WheelSize = wheelSize;
                return this;
            }

            public Car Build()
            {
                return _car;
            }
        }
        public static ISpecifyCarType Create()
        {
            return new Impl();
        }
    }

    public class Demo
    {
        public static void Run()
        {
            var car = CarBuilder.Create()
                .OfType(CarType.Crossover)
                .WithWheelSize(18)
                .Build();
            
            var car2 = CarBuilder.Create().OfType(CarType.Sedan).WithWheelSize(14).Build();
        }
    }
}