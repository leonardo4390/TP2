using System;

interface IAccesoADatos
{
    string[] LeerCadeteria(string ruta);
    List<string[]> LeerCadetes(string ruta);
}