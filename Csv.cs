using System;
using System.Collections.Generic;
using System.IO;

//esto se hizo con poco de ayuda ya que no recordaba leer archivos.
static class Csv
{
    public static string[] LeerLinea(string ruta)
    {
        using var leer = new StreamReader(ruta);
        leer.ReadLine();
        return leer.ReadLine()?.Split(',');
    }

    public static List<string[]> LeerArchivo(string ruta)
    {
        var datos = new List<string[]>();
        using var leer = new StreamReader(ruta);
        leer.ReadLine();
        while (!leer.EndOfStream)
        {
            var linea = leer.ReadLine();
            if (!string.IsNullOrWhiteSpace(linea))
                datos.Add(linea.Split(','));
        }
        return datos;
    }
}