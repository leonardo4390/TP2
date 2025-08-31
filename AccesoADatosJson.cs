using System;
using System.Text.Json;


class AccesoADatosJson : IAccesoADatos
{
    public string[] LeerCadeteria(string ruta)
    {
        var json = File.ReadAllText(ruta);
        var datos = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        return new[] { datos["nombre"], datos["telefono"] };
    }

    public List<string[]> LeerCadetes(string ruta)
    {
        var json = File.ReadAllText(ruta);
        var cadetes = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);
        var resultado = new List<string[]>();

        foreach (var cadete in cadetes)
        {
            resultado.Add(new[] {
                cadete["nombre"],
                cadete["direccion"],
                cadete["telefono"]
            });
        }

        return resultado;
    }

}