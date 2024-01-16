using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace xml
{
	public static class StreamReaderSequence
	{
		public static IEnumerable<string> Lines(this StreamReader source)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			string line;
			while ((line = source.ReadLine()) != null)
			{
				yield return line;
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var sr = new StreamReader("names.txt");
			var wr = new StreamWriter("result.xml", false);
		
			var xmlTree = new XDocument(
			new XDeclaration("1.0", "utf-16", "yes"),
			new XDocumentType("Root", null, "student.dtd", null),
			new XElement("Root",
			from line in sr.Lines()
			where !line.StartsWith("#")
			let items = line.Split()

			select new XElement("Телефон",
			new XElement("Название", items[0]),
			new XElement("Изготовитель", items[1]),
			new XElement("Цвет", items[2]),
			new XElement("Тип_экрана", items[3]),
			new XElement("Размер_корпуса", items[4]),
			new XElement("Год_Изготовления", items[5]),
			new XElement("Время_зарядки", items[6]),
			new XElement("Время_работы", items[7]),
			new XElement("Чехол", items[8])
			)
			)
			);
			xmlTree.Save(wr);
			sr.Close();
			wr.WriteLine("<?xml-stylesheet type=\"text/css\" href=\"Style.css\"?>");
			wr.Close();


			using (StreamWriter sw = new StreamWriter("Style.css"))
			{
				sw.WriteLine("Root{color:red;background-color:#FFDAF7}");
				sw.Close();
			}
			
		}
	}
}