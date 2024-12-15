namespace DesignPatterns.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

public class Builder
{
  class HtmlElement
  {
    public string Name, Text;
    public List<HtmlElement> Elements = new ();
    private const int indentSize = 2;

    public HtmlElement()
    {
      
    }

    public HtmlElement(string name, string text)
    {
      Name = name;
      Text = text;
    }

    private string ToStringImpl(int indent)
    {
      var sb = new StringBuilder();
      var i = new string(' ', indentSize * indent);
      sb.Append($"{i}<{Name}>\n");

      if (!string.IsNullOrWhiteSpace(Text))
      {
        
        sb.Append(Text);
        sb.Append("\n");
      }

      foreach (var e in Elements)
        sb.Append(e.ToStringImpl(indent + 1));

      sb.Append($"{i}</{Name}>\n");
      return sb.ToString();
    }

    public override string ToString()
    {
      return ToStringImpl(0);
    }
  }

  class HtmlBuilder
  {
    private readonly string _rootName;

    public HtmlBuilder(string rootName)
    {
      this._rootName = rootName;
      root.Name = rootName;
    }

    // not fluent
    public HtmlBuilder AddChild(string childName, string childText)
    {
      var e = new HtmlElement(childName, childText);
      root.Elements.Add(e);
      return this;
    }

    public HtmlBuilder AddChildFluent(string childName, string childText)
    {
      var e = new HtmlElement(childName, childText);
      root.Elements.Add(e);
      return this;
    }

    public override string ToString()
    {
      return root.ToString();
    }

    public void Clear()
    {
      root = new HtmlElement{Name = _rootName};
    }

    HtmlElement root = new HtmlElement();
  }

  public class Demo
  {
    public static void Runner(string[] args)
    {
      // if you want to build a simple HTML paragraph using StringBuilder
      var hello = "hello";
      var sb = new StringBuilder();
      sb.Append("<p>");
      sb.Append(hello);
      sb.Append("</p>");
      WriteLine(sb);

      // now I want an HTML list with 2 words in it
      var words = new[] {"hello", "world"};
      sb.Clear();
      sb.Append("<ul>");
      foreach (var word in words)
      {
        sb.AppendFormat("<li>{0}</li>", word);
      }
      sb.Append("</ul>");
      WriteLine(sb);

      // ordinary non-fluent builder
      var builder = new HtmlBuilder("ul");
      builder.AddChild("li", "hello").AddChild("li", "world");
      WriteLine(builder.ToString());

      // fluent builder
      sb.Clear();
      builder.Clear(); // disengage builder from the object it's building, then...
      builder.AddChildFluent("li", "hello").AddChildFluent("li", "world");
      WriteLine(builder);
    }
  }
}

