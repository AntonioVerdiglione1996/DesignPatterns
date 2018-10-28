using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns
{

    //Abstract Factory Design Pattern:

    //MainApp startup class for Structural
    class MainApp
    {
        public static void Main(string[] args)
        {
            AbstractFactory factory = new ConcreteFactory();
            Client client = new Client(factory);
            client.Run();

            Console.ReadKey();
        }
    }
    abstract class AbstractFactory
    {
        public abstract AbstractProductA CreateProductA();
        public abstract AbstractProductB CreateProductB();
    }
    class ConcreteFactory : AbstractFactory
    {
        public override AbstractProductA CreateProductA()
        {
            return new ProductA1();
        }
        public override AbstractProductB CreateProductB()
        {
            return new ProductB1();
        }
    }
    abstract class AbstractProductA { }
    abstract class AbstractProductB { public abstract void Interact(AbstractProductA a); }
    class ProductA1 : AbstractProductA { }
    class ProductB1 : AbstractProductB
    {
        public override void Interact(AbstractProductA a)
        {
            Console.WriteLine(this.GetType().Name +
              " interacts with " + a.GetType().Name);
        }
    }
    class Client
    {
        private AbstractProductA _abstractProductA;
        private AbstractProductB _abstractProductB;
        public Client(AbstractFactory factory)
        {
            _abstractProductA = factory.CreateProductA();
            _abstractProductB = factory.CreateProductB();
        }
        public void Run()
        {
            _abstractProductB.Interact(_abstractProductA);
        }
    }
}
