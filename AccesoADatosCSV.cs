using System;

class AccesoADatosCSV : IAccesoADatos
{
    public string[] LeerCadeteria(string ruta)
    {
        using var leer = new StreamReader(ruta);
        leer.ReadLine();
        return leer.ReadLine()?.Split(',');
    }

    public List<string[]> LeerCadetes(string ruta)
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