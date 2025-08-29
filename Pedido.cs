using System;
using System.Collections.Generic;

class Pedido
{
    public int Nro { get; private set; }
    public string Obs { get; private set; }
    public Cliente Datocliente { get; private set; }
    public string Estado { get; set; }
    public List<Producto> Productos { get; private set; }

    public Pedido(int nro, string obs, Cliente cliente, string estado)
    {
        Nro = nro;
        Obs = obs;
        Datocliente = cliente;
        Estado = estado;
        Productos = new List<Producto>();
    }

    public void AgregarProducto(Producto producto)
    {
        Productos.Add(producto);
    }

    public void VerDireccionCliente()
    {
        Console.WriteLine($"Dirección: {Datocliente.Direccion}");
        Console.WriteLine($"Referencia: {Datocliente.ReferenciaDireccion}");
    }

    public void VerInformacionCliente()
    {
        Console.WriteLine($"Cliente: {Datocliente.Nombre}");
        Console.WriteLine($"Teléfono: {Datocliente.Telefono}");
    }

    public void MostrarTicket()
    {
        Console.WriteLine("\nTICKET DEL PEDIDO");
        Console.WriteLine($"Pedido N°: {Nro}");
        VerInformacionCliente();
        VerDireccionCliente();
        Console.WriteLine($"Observación: {Obs}");
        Console.WriteLine($"Productos:");
        foreach (var producto in Productos)
        {
            Console.WriteLine($"- {producto}");
        }
        Console.WriteLine($"Estado: {Estado}");
        Console.WriteLine("-----------------------------\n");
    }

    public bool EstaEntregado()
    {
        return Estado.ToLower() == "entregado";
    }

}