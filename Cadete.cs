using System;
using System.Collections.Generic;

class Cadete: Persona
{
    private static int idCadete = 0;

    public int Id { get; private set; }
    public Cadete(string nombre, string direccion, int telefono): base(nombre, direccion, telefono)
    {
        Id = ++idCadete;
    }
}