internal class Program
{
  public static void Main (string[] args)
  {
    var byteOrderMarkUtf8
        = Encoding.UTF8.GetString (Encoding.UTF8.GetPreamble ());
    const string url = "https://www.cbr.ru/scripts/XML_daily_eng.asp";
    var xmlDoc = new XmlDocument ();
    var currencyType = CurrencyType.JPY;

    using (var client = new WebClient ())
    {
      // Download the XML string
      string xmlContent = client.DownloadString (url);

      if (xmlContent.StartsWith (byteOrderMarkUtf8, StringComparison.Ordinal))
        {
          xmlContent = xmlContent.Remove (0, byteOrderMarkUtf8.Length);
        }

      xmlDoc.LoadXml (xmlContent);
      xmlDoc.CreateXmlDeclaration ("1.0", "UTF-8", null);
    }

    XmlNode valCursNode = xmlDoc.SelectSingleNode ("//ValCurs");
    string date = valCursNode.Attributes["Date"].Value;
    string name = valCursNode.Attributes["name"].Value;

    Console.WriteLine ($"Date: {date}");
    Console.WriteLine ($"Market Name: {name}");

    XmlNodeList valuteNodes = valCursNode.SelectNodes ("Valute");

    foreach (XmlNode valuteNode in valuteNodes)
      {
        string id = valuteNode.Attributes["ID"].Value;
        string numCode = valuteNode["NumCode"].InnerText;
        string charCode = valuteNode["CharCode"].InnerText;
        string nominal = valuteNode["Nominal"].InnerText;
        string currencyName = valuteNode["Name"].InnerText;
        string value = valuteNode["Value"].InnerText;
        string valueRate = valuteNode["VunitRate"].InnerText;

        // Replace comma with dot for parsing the value as a decimal
        value = value.Replace (',', '.');
        valueRate = valueRate.Replace (',', '.');

        // Parse the string to decimal using InvariantCulture
        decimal parsedValue
            = decimal.Parse (value, CultureInfo.InvariantCulture);
        decimal parsedValueRate
            = decimal.Parse (valueRate, CultureInfo.InvariantCulture);

        if (charCode == currencyType.ToString ())
          {
            Console.WriteLine ($"ID: {id}");
            Console.WriteLine ($"NumCode: {numCode}");
            Console.WriteLine ($"CharCode: {charCode}");
            Console.WriteLine ($"Nominal: {nominal}");
            Console.WriteLine ($"Currency Name: {currencyName}");
            Console.WriteLine ($"Value: {parsedValue}");
            Console.WriteLine ($"Unit Rate: {parsedValueRate}");
            Console.WriteLine ();
            break;
          }
      }
  }
}

public class CurrencyType : Enumeration
{
  public CurrencyType (int id, string name) : base (id, name) {}

  public static CurrencyType USD => new(1, "USD");
  public static CurrencyType EUR => new(2, "EUR");
  public static CurrencyType JPY => new(3, "JPY");
}

public abstract class Enumeration : IComparable
{
  public string Name { get; private set; }

  public int Id { get; private set; }

  protected Enumeration (int id, string name) => (Id, Name) = (id, name);

  public override string ToString () => Name;

  public static IEnumerable<T>
  GetAll<T> ()
      where T : Enumeration => typeof (T).GetFields (BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly).Select (f => f.GetValue (null)).Cast<T> ();

                public override bool Equals (object obj)
  {
    if (obj is not Enumeration otherValue)
      {
        return false;
      }

    var typeMatches = GetType ().Equals (obj.GetType ());
    var valueMatches = Id.Equals (otherValue.Id);

    return typeMatches && valueMatches;
  }

  public int CompareTo (object other) => Id.CompareTo (((Enumeration)other).Id);
}
