namespace DesignPatterns.SOLID;
//<summary>
//This states that you should always be able to upcast to your base type
//e.g. the square should still behave like a square even if you are upcasting to rectangle
//
//</summary>
using System;
public class LiskovSubstitutionPrinciple
{
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {
            
        }
        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"Width: {Width}, Height: {Height}";
        }
        
    }

    public class Square : Rectangle
    {
        //public new int Width
        //{
        //  set { base.Width = base.Height = value; }
        //}

        //public new int Height
        //{ 
        //  set { base.Width = base.Height = value; }
        //}

        public override int Width // nasty side effects
        {
            set { base.Width = base.Height = value; }
        }

        public override int Height
        { 
            set { base.Width = base.Height = value; }
        }
    }
    public class Demo
    {
        private static int Area(Rectangle r)=> r.Width * r.Height;

        public static void Runner()
        {
            Rectangle r = new Rectangle(2,3);
            Console.WriteLine($"{r} has area {Area(r)}");
            Rectangle square = new Square();
            square.Width = 4;
            Console.WriteLine($"{square} has area {Area(square)}");
        }
    }
}

