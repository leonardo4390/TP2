using System;

class Cliente
{
    public string Nombre { get; private set; }
    public string Direccion { get; private set; }
    public int Telefono { get; private set; }
    public string ReferenciaDireccion { get; private set; }

    public Cliente(string nombre, string direccion, int telefono, string referencia)
    {
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        ReferenciaDireccion = referencia;
    }
}