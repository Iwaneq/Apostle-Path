using System.Globalization;

namespace ApostlePath.Converter
{
    public class DisciplineTitleToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string? title = value?.ToString();
            if(string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("Discipline Title");
            }

            var dictionary = Application.Current?.Resources.MergedDictionaries.Where(x => x.Source.ToString() == "Resources/Styles/Colors.xaml;assembly=ApostlePath").First();

            if(dictionary == null)
            {
                throw new FileNotFoundException("Could not find Colors.xaml resource", "Colors.xaml");
            }

            switch(title)
            {
                case "Adept":
                    return dictionary["TitleGreen"];
                case "Initiate":
                    return dictionary["TitleBlue"];
                case "Warrior":
                    return dictionary["TitleRed"];
                case "Monk":
                    return dictionary["TitleLightBlue"];
                case "Paladin":
                    return dictionary["TitlePurple"];
                case "Apostle":
                    return dictionary["TitleYellow"];
                default:
                    return Colors.White;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
