using System.Text;

namespace DesignPatterns.Factories;

public class ObjectTrackingAndBulkReplacement
{
    public interface ITheme
    {
        string TextColor { get; }
        string BgrColor { get; }
    }

    class LightTheme : ITheme
    {
        public string TextColor => "black";
        public string BgrColor => "white";
    }

    class DarkTheme : ITheme
    {
        public string TextColor => "white";
        public string BgrColor => "dark gray";
    }

    public class TrackingThemeFactory
    {
        private readonly List<WeakReference<ITheme>> _themes = new List<WeakReference<ITheme>>();
        public ITheme CreateTheme(bool isDark)
        {
            ITheme theme = isDark ? new DarkTheme() : new LightTheme();
            _themes.Add(new WeakReference<ITheme>(theme));
            return theme;
        }

        public string Info
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var reference in _themes)
                {
                    if (reference.TryGetTarget(out ITheme theme))
                    {
                        bool dark = theme is DarkTheme;
                        sb.AppendLine(dark ? "Dark" : "Light").AppendLine(" theme");
                    }
                }
                return sb.ToString();
            }
        }
    }

    public class ReplaceableThemeFactory
    {
        private readonly List<WeakReference<TRef<ITheme>>> _themes = new List<WeakReference<TRef<ITheme>>>();

        private ITheme createThemeImpl(bool dark)
        {
            return dark ? new DarkTheme() : new LightTheme();
        }

        public TRef<ITheme> CreateTheme(bool isDark)
        {
            var r = new TRef<ITheme>(createThemeImpl(isDark));
            _themes.Add(new(r));
            return r;
        }

        public void ReplaceTheme(bool dark)
        {
            foreach (var wvar in _themes)
            {
                if (wvar.TryGetTarget(out var reference))
                {
                    reference.Value = createThemeImpl(dark);
                }
            }
        }
    }
    public class TRef<T> where T : class
    {
        public T Value;

        public TRef(T value)
        {
            Value = value;
        }
    }
    public class Demo
    {
        public static void Run()
        {
            var factory = new TrackingThemeFactory();
            var theme1 = factory.CreateTheme(true);
            var theme2 = factory.CreateTheme(false);

            var factory2 = new ReplaceableThemeFactory();
            var magic = factory2.CreateTheme(true);
            Console.WriteLine(magic.Value.BgrColor);
            factory2.ReplaceTheme(false);
            Console.WriteLine(magic.Value.BgrColor);
        }
    }
}