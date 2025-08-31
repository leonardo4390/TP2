using System;
using System.Collections.Generic;

class Pedido
{
    public int Nro { get; private set; }
    public string Obs { get; private set; }
    public Cliente Datocliente { get; private set; }
    public string Estado { get; set; }
    public List<Producto> Productos { get; private set; }
    public Cadete CadeteAsignado{ get; private set; }

    public Pedido(int nro, string obs, Cliente cliente, string estado)
    {
        Nro = nro;
        Obs = obs;
        Datocliente = cliente;
        Estado = estado;
        Productos = new List<Producto>();
    }

    public void AgregarCadete(Cadete cadete) => CadeteAsignado = cadete;

    public void AgregarProducto(Producto producto) => Productos.Add(producto);

    public bool EstaEntregado() => Estado.ToLower() == "entregado";

    public void MostrarTicket()
    {
        Console.WriteLine("\nTICKET DEL PEDIDO");
        Console.WriteLine($"Pedido N°: {Nro}");
        Console.WriteLine($"Cliente: {Datocliente.Nombre} - Tel: {Datocliente.Telefono}");
        Console.WriteLine($"Direccion: {Datocliente.Direccion} - Ref: {Datocliente.ReferenciaDireccion}");
        Console.WriteLine($"Observación: {Obs}");
        Console.WriteLine($"Productos:");
        foreach (var producto in Productos)
        {
            Console.WriteLine($"- {producto}");
        }
        Console.WriteLine($"Estado: {Estado}");
        Console.WriteLine($"Cadete Asignado: {(CadeteAsignado != null? CadeteAsignado.Nombre : "Sin Asignaar")}");
        Console.WriteLine("-----------------------------\n");
    }

}