using System;
using System.Collections.Generic;

class Cadeteria
{
    private string nombre;
    private int telefono;
    public List<Cadete> Cadetes { get; private set; }
    public List<Pedido> Pedidos { get; private set; } = new List<Pedido>();


    public string Nombre => nombre;
    public int Telefono => telefono;

    public Cadeteria(IAccesoADatos acceso, string rutaCadeteria, string rutaCadetes)
    {
        var datosCadeteria = acceso.LeerCadeteria(rutaCadeteria);
        nombre = datosCadeteria[0];
        telefono = int.Parse(datosCadeteria[1]);

        Cadetes = new List<Cadete>();
        var datosCadetes = acceso.LeerCadetes(rutaCadetes);
        foreach (var fila in datosCadetes)
        {
            string nombre = fila[0];
            string direccion = fila[1];
            int tel = int.Parse(fila[2]);
            Cadetes.Add(new Cadete(nombre, direccion, tel));
        }
    }

    public void DarDeAltaPedido()
    {
        int nro = Pedidos.Count + 1;
        Console.WriteLine("\nDando de alta al Pedido:");
        Console.WriteLine("Observación:");
        string obs = Console.ReadLine();
        Console.WriteLine("Nombre del cliente:");
        string nombre = Console.ReadLine();
        Console.WriteLine("Dirección:");
        string direccion = Console.ReadLine();
        Console.WriteLine("Teléfono:");
        int tel = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Referencia:");
        string referencia = Console.ReadLine();

        var cliente = new Cliente(nombre, direccion, tel, referencia);
        var pedido = new Pedido(nro, obs, cliente, "Pendiente");
        while (true)
        {
            Console.WriteLine("\nSeleccione comida:");
            foreach (var comida in Enum.GetValues(typeof(Producto.Comida)))
                Console.WriteLine($"- {comida}");

            string entrada = Console.ReadLine();
            if (Enum.TryParse(entrada, out Producto.Comida tipo))
                pedido.AgregarProducto(new Producto(tipo));
            else
                Console.WriteLine("Comida invalida.");

            Console.WriteLine("¿Agregar otro producto? (s/n):");
            if (Console.ReadLine().ToLower() != "s") break;
        }

        pedido.MostrarTicket();
        Pedidos.Add(pedido);
        Console.WriteLine("Pedido registrado.");
    }

    public void AsignarCadeteAPedido(int idCadete, int nroPedido)
    {
        var cadete = Cadetes.FirstOrDefault(c => c.Id == idCadete);
        var pedido = Pedidos.FirstOrDefault(p => p.Nro == nroPedido);

        if (cadete != null && pedido != null)
        {
            pedido.AsignarCadete(cadete);
            Console.WriteLine($"Pedido: #{pedido.Nro} asignado a {cadete.Nombre}");
        }
        else
        {
            Console.WriteLine($"Cadete o Pedido no encontrado.");
        }
    }

    public void CambiarEstadoPedido()
    {
        Console.Write("\nIngrese número de pedido: ");
        int nro = Convert.ToInt32(Console.ReadLine());

        var pedido = Pedidos.FirstOrDefault(p => p.Nro == nro);
        if (pedido != null)
        {
            Console.WriteLine($"Estado actual: {pedido.Estado}");
            Console.Write("Nuevo estado (Pendiente/Entregado): ");
            pedido.Estado = Console.ReadLine();
            Console.WriteLine("Estado actualizado.");
            return;
        }
        else
        {
            Console.WriteLine("Pedido no encontrado.");
        }
    }
    public void MostrarInforme()
    {
        Console.WriteLine($"\nInforme de actividad Cadeteria - {Nombre}");
        foreach (var cadete in Cadetes)
        {
            int entregados = Pedidos.Count(p => p.CadeteAsignado?.Id == cadete.Id && p.EstaEntregado());
            int total = Pedidos.Count(p => p.CadeteAsignado?.Id == cadete.Id);
            Console.WriteLine($"\nCadete: {cadete.Nombre} - ID: {cadete.Id}");
            Console.WriteLine($"Pedidos Asignados: {total}");
            Console.WriteLine($"Pedidos entregados: {entregados}");
            Console.WriteLine($"\nJornal: {entregados * 500}");
            Console.WriteLine("-----------------------------------");
        }

        var sinAsignar = Pedidos.Where(p => p.CadeteAsignado == null).ToList();
        Console.WriteLine($"\nPedidos sin asignar: {sinAsignar.Count}");
    }

    public int JornalACobrar(int idCadete)
    {
        return Pedidos
            .Where(p => p.CadeteAsignado?.Id == idCadete && p.EstaEntregado())
            .Count() * 500;
    }
}