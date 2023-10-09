using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        XMLManager xmlManager = new XMLManager();
        JSONManager jsonManager = new JSONManager();

       
        DataClass data = new DataClass();
        Console.Write("Enter name: ");
        data.Name = Console.ReadLine();
        Console.Write("Enter your age: ");
        int age;
        while (!int.TryParse(Console.ReadLine(), out age))
        {
            Console.WriteLine("Incorrect age entered. Try again.");
            Console.Write("Enter your age: ");
        }
        data.Age = age;
        Console.Write("Enter Email: ");
        data.Email = Console.ReadLine();

       
        xmlManager.Save("data.xml", data);
        jsonManager.Save("data.json", data);

       
        DataClass xmlData = xmlManager.Load("data.xml");
        DataClass jsonData = jsonManager.Load("data.json");

        Console.WriteLine("Data з XML:");
        Console.WriteLine($"Name: {xmlData.Name}");
        Console.WriteLine($"Age: {xmlData.Age}");
        Console.WriteLine($"Email: {xmlData.Email}");

        Console.WriteLine("Data з JSON:");
        Console.WriteLine($"Name: {jsonData.Name}");
        Console.WriteLine($"Age: {jsonData.Age}");
        Console.WriteLine($"Email: {jsonData.Email}");
    }
}

class DataClass
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}

class XMLManager
{
    public void Save(string fileName, DataClass data)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataClass));
        using (TextWriter writer = new StreamWriter(fileName))
        {
            xmlSerializer.Serialize(writer, data);
        }
    }

    public DataClass Load(string fileName)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataClass));
        using (TextReader reader = new StreamReader(fileName))
        {
            return (DataClass)xmlSerializer.Deserialize(reader);
        }
    }
}

class JSONManager
{
    public void Save(string fileName, DataClass data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText(fileName, jsonData);
    }

    public DataClass Load(string fileName)
    {
        string jsonData = File.ReadAllText(fileName);
        return JsonConvert.DeserializeObject<DataClass>(jsonData);
    }
}
