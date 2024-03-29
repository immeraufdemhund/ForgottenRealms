﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ForgottenRealms.Engine.Classes;

public class ItemLibrary
{
    private static string libraryPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ForgottenRealms");

    private static string libraryFile = Path.Combine(libraryPath, "ItemLibrary.dat");

    private static List<Item> library = new();

    public static void Add(Item item)
    {
        var i = item.ShallowClone();
        i.readied = false;
        i.hidden_names_flag = 0;
        i.name = i.GenerateName(0);
        if (library.Contains(i) == false)
        {
            library.Add(i);
            Write();
        }
    }

    public static void Read()
    {
        if (System.IO.File.Exists(libraryFile))
        {
            var fs = new FileStream(libraryFile, FileMode.Open);

            if (fs.Length == 0)
            {
                library = new List<Item>();
                return;
            }

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            var formatter = new BinaryFormatter();
            try
            {
                library = (List<Item>)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                //Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
    }

    public static void Write()
    {
        Directory.CreateDirectory(libraryPath);
        var fs = new FileStream(libraryFile, FileMode.Create);

        // Construct a BinaryFormatter and use it to serialize the data to the stream.
        var formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(fs, library);
        }
        catch (SerializationException e)
        {
            //Console.WriteLine("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }
}
