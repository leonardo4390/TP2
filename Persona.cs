using System;


abstract class Persona
{
    public string Nombre { get; protected set; }
    public string Direccion { get; protected set; }
    public int Telefono { get; protected set; }

    public Persona(string nombre, string direccion, int telefono)
    {
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
    }
}