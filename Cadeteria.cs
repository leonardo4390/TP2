using System;
using System.Collections.Generic;

class Cadeteria
{
    private string nombre;
    private int telefono;
    public List<Cadete> Cadetes { get; private set; }
    public List<Pedido> PedidosPendientes { get; private set; } = new List<Pedido>();


    public string Nombre => nombre;
    public int Telefono => telefono;

    public Cadeteria(string nombre, int telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        Cadetes = new List<Cadete>();
    }

    public Cadeteria(string rutaCadeteria, string rutaCadetes)
    {
        var datosCadeteria = Csv.LeerLinea(rutaCadeteria);
        nombre = datosCadeteria[0];
        telefono = int.Parse(datosCadeteria[1]);

        Cadetes = new List<Cadete>();
        var datosCadetes = Csv.LeerArchivo(rutaCadetes);
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
        Console.WriteLine("\nDando de alta al Pedido:");
        int nro = PedidosPendientes.Count + 1;
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

        Cliente cliente = new Cliente(nombre, direccion, tel, referencia);
        Pedido pedido = new Pedido(nro, obs, cliente, "Pendiente");
        while (true)
        {
            Console.WriteLine("\nSeleccione comida:");
            foreach (var comida in Enum.GetValues(typeof(Producto.Comida)))
                Console.WriteLine($"- {comida}");

            string entrada = Console.ReadLine();
            if (Enum.TryParse(entrada, out Producto.Comida tipo))
                pedido.AgregarProducto(new Producto(tipo));
            else
                Console.WriteLine("Comida inválida.");

            Console.WriteLine("¿Agregar otro producto? (s/n):");
            if (Console.ReadLine().ToLower() != "s") break;
        }

        pedido.MostrarTicket();
        PedidosPendientes.Add(pedido);
        Console.WriteLine("Pedido registrado.");
    }

    public void AsignarPedidoACadete()
    {
        Console.WriteLine("\nAsignar Pedido:");
        Console.WriteLine("Pedidos pendientes:");
        foreach (var p in PedidosPendientes)
            Console.WriteLine($"Pedido #{p.Nro} - Cliente: {p.Datocliente.Nombre}");

        Console.Write("Ingrese número de pedido: ");
        int nro = Convert.ToInt32(Console.ReadLine());
        Pedido pedido = PedidosPendientes.FirstOrDefault(p => p.Nro == nro);

        if (pedido == null)
        {
            Console.WriteLine("Pedido no encontrado.");
            return;
        }

        Console.WriteLine("\nCadetes disponibles:");
        foreach (var c in Cadetes)
            Console.WriteLine($"ID: {c.Id} - {c.Nombre}");

        Console.Write("\nIngrese ID de cadete: ");
        int id = Convert.ToInt32(Console.ReadLine());
        Cadete cadete = Cadetes.FirstOrDefault(c => c.Id == id);

        if (cadete != null)
        {
            cadete.AgregarPedido(pedido);
            PedidosPendientes.Remove(pedido);
            Console.WriteLine($"\nPedido #{pedido.Nro} asignado a {cadete.Nombre}");
        }
        else
        {
            Console.WriteLine("Cadete no encontrado.");
        }
    }

    public void CambiarEstadoPedido()
    {
        Console.Write("\nIngrese número de pedido: ");
        int nro = Convert.ToInt32(Console.ReadLine());

        foreach (var cadete in Cadetes)
        {
            var pedido = cadete.Pedidos.FirstOrDefault(p => p.Nro == nro);
            if (pedido != null)
            {
                Console.WriteLine($"Estado actual: {pedido.Estado}");
                Console.Write("Nuevo estado (Pendiente/Entregado): ");
                pedido.Estado = Console.ReadLine();
                Console.WriteLine("Estado actualizado.");
                return;
            }
        }

        Console.WriteLine("Pedido no encontrado.");
    }

    public void ReasignarPedido()
    {
        Console.Write("\nIngrese número de pedido a reasignar: ");
        int nro = Convert.ToInt32(Console.ReadLine());

        Pedido pedido = null;
        Cadete origen = null;

        foreach (var cadete in Cadetes)
        {
            pedido = cadete.Pedidos.FirstOrDefault(p => p.Nro == nro);
            if (pedido != null)
            {
                origen = cadete;
                break;
            }
        }

        if (pedido == null)
        {
            Console.WriteLine("Pedido no encontrado.");
            return;
        }

        Console.WriteLine("\nCadetes disponibles:");
        foreach (var c in Cadetes)
            Console.WriteLine($"ID: {c.Id} - {c.Nombre}");

        Console.Write("Ingrese ID del nuevo cadete: ");
        int nuevoId = Convert.ToInt32(Console.ReadLine());
        Cadete destino = Cadetes.FirstOrDefault(c => c.Id == nuevoId);

        if (destino != null)
        {
            origen.EliminarPedido(nro);
            destino.AgregarPedido(pedido);
            Console.WriteLine($"Pedido #{nro} reasignado a {destino.Nombre}");
        }
        else
        {
            Console.WriteLine("Cadete no encontrado.");
        }
    }

    public void MostrarInforme()
    {
        Console.WriteLine($"\nInforme de actividad - {Nombre}");
        foreach (var cadete in Cadetes)
        {
            int entregados = cadete.Pedidos.Count(p => p.Estado.ToLower() == "entregado");
            int jornal = entregados * 500;
            Console.WriteLine($"\nCadete: {cadete.Nombre} - ID: {cadete.Id}");
            Console.WriteLine($"Pedidos Asignados: {cadete.Pedidos.Count}");
            Console.WriteLine($"Pedidos entregados: {entregados}");
            Console.WriteLine($"\nJornal: {jornal}");
        }
    }
}