using System;
using System.Linq;
using System.Xml.Linq;

public class XMLLinq
{
   static int DisplayMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1.Add_User");
        Console.WriteLine("2.Delete_User");
        Console.WriteLine("3.Update_User");
        Console.WriteLine("4.Get_All_Users");
        Console.WriteLine("5.Report");
        string result = Console.ReadLine();

        return Convert.ToInt32(result);
    }

    static void Main()
    {

        int userInput = 1;
        do
        {
            userInput = DisplayMenu();


            switch (userInput)
            {

                case 1:
                    Console.WriteLine("Enter Name:\n");
                    string input_name = Console.ReadLine();
                    Console.WriteLine("Enter ADDRESS:\n");
                    string input_address = Console.ReadLine();
                    Console.WriteLine("Enter EMAIL:\n");
                    string input_email = Console.ReadLine();


                    XDocument xdoc = XDocument.Load(@"c://temp/APP_EM/data.xml");

                    var count = xdoc.Descendants("USER").Count();
                    //var maxID = xdoc.Root.Elements().DefaultIfEmpty().Max(p => (int)p.Element("ID"));

                    xdoc.Root.Add(new XElement("USER",
                                              new XElement("ID", count + 1),
                                              new XElement("Name", input_name),
                                              new XElement("ADDRESS", input_address),
                                              new XElement("EMAIL", input_email)
                                              ));

                    xdoc.Save(@"c://temp/APP_EM/data.xml");
                    break;


                case 2:
                    
                    Console.WriteLine("Enter ID of User you want to Delete:\n");
                    string input_delete = Console.ReadLine();
                   // int input_int = Int32.Parse(input_delete);

                    XDocument xdoc3 = XDocument.Load(@"c://temp/APP_EM/data.xml");

                    {
                        XElement emp = xdoc3.Descendants("USER").FirstOrDefault(p => p.Element("ID").Value == input_delete);
                        if (emp != null)
                        {
                            emp.Remove();
                            xdoc3.Save(@"c://temp/APP_EM/data.xml");
                        }
                    }
                    break;


                case 3:

                    Console.WriteLine("Enter ID of User you want to Update:");
                           string input_update_id = Console.ReadLine();
                    Console.WriteLine("Enter field you want to Update:");
                           string input_update_field = Console.ReadLine();
                    Console.WriteLine("New Value:");
                           string input_update_to = Console.ReadLine();

                    XDocument xdoc4 = XDocument.Load(@"c://temp/APP_EM/data.xml");


                    var items = from item in xdoc4.Descendants("USER")
                                                     where item.Element("ID").Value == input_update_id
                                                        select item;

                                         foreach (XElement itemElement in items)
                    {
                        itemElement.SetElementValue(input_update_field, input_update_to);
                    }
                    xdoc4.Save(@"c://temp/APP_EM/data.xml");
                    break;


                case 4:

                    XDocument xdoc2 = XDocument.Load(@"c://temp/APP_EM/data.xml");

                    var getem = from p in xdoc2.Descendants("USER")
                                select new
                                {
                                    Id = p.Element("ID").Value,
                                    Name = p.Element("Name").Value,
                                    Address = p.Element("ADDRESS").Value,
                                    Email = p.Element("EMAIL").Value
                                };
                    foreach (var p in getem)
                        Console.WriteLine(String.Join(Environment.NewLine, "", "ID: " + p.Id, "NAME: " + p.Name, "ADDRESS: " + p.Address, "EMAIL: " + p.Email, ""));
                    //Console.WriteLine(p.Name + p.Address + p.Email);
                    Console.ReadLine();
                    break;


                    case 5:

                    XDocument xdoc5 = XDocument.Load(@"c://temp/APP_EM/data.xml");

                    var missing_add = from p in xdoc5.Descendants("USER")
                                  where p.Element("ADDRESS").Value == ""
                                select new
                                {
                                    Id = p.Element("ID").Value,
                                    Name = p.Element("Name").Value,
                                    Address = p.Element("ADDRESS").Value,
                                    Email = p.Element("EMAIL").Value
                                };
                    foreach (var p in missing_add)
                        Console.WriteLine(String.Join(Environment.NewLine, "", "ID: " + p.Id, "NAME: " + p.Name, "ADDRESS: " + p.Address, "EMAIL: " + p.Email, ""));
                    var missing_name = from p in xdoc5.Descendants("USER")
                                      where p.Element("Name").Value == ""
                                      select new
                                      {
                                          Id = p.Element("ID").Value,
                                          Name = p.Element("Name").Value,
                                          Address = p.Element("ADDRESS").Value,
                                          Email = p.Element("EMAIL").Value
                                      };
                    foreach (var p in missing_name)
                        Console.WriteLine(String.Join(Environment.NewLine, "", "ID: " + p.Id, "NAME: " + p.Name, "ADDRESS: " + p.Address, "EMAIL: " + p.Email, ""));
                    var missing_email = from p in xdoc5.Descendants("USER")
                                       where p.Element("EMAIL").Value == ""
                                       select new
                                       {
                                           Id = p.Element("ID").Value,
                                           Name = p.Element("Name").Value,
                                           Address = p.Element("ADDRESS").Value,
                                           Email = p.Element("EMAIL").Value
                                       };
                    foreach (var p in missing_email)
                        Console.WriteLine(String.Join(Environment.NewLine, "", "ID: " + p.Id, "NAME: " + p.Name, "ADDRESS: " + p.Address, "EMAIL: " + p.Email, ""));
                    //Console.WriteLine(p.Name + p.Address + p.Email);
                    Console.ReadLine();

                    break;
            }

        } while (userInput != 0);
    }
}


