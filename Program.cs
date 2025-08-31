using System;

class Program
{
    public static void Main(String[] args)
    {
        var acceso = InicializarAccesoADatos(out string rutaCadeteria, out string rutaCadetes);
        Cadeteria cadeteria = new Cadeteria(acceso,rutaCadeteria, rutaCadetes);
        GestioPedidos(cadeteria);

    }

    public static IAccesoADatos InicializarAccesoADatos(out string rutaCadeteria, out string rutaCadetes)
    {
        Console.WriteLine("\nSeleccione Fuente de Datos:");
        Console.WriteLine("1 - CSV");
        Console.WriteLine("2 - JSON");
        Console.WriteLine("Opcion: ");
        string opcion = Console.ReadLine();

        rutaCadeteria = "";
        rutaCadetes = "";

        switch (opcion)
        {
            case "1":
                rutaCadeteria = "cadeteria.csv";
                rutaCadetes = "cadetes.csv";
                return new AccesoADatosCSV();

            case "2":
                rutaCadeteria = "cadeteria.csv";
                rutaCadetes = "cadetes.csv";
                return new AccesoADatosJson();

            default:
                Console.WriteLine("Opcion Invalida, Finalizando Programa.");
                return null;
        }
    }

    public static void GestioPedidos(Cadeteria cadeteria)
    {
        while (true)
        {
            Console.WriteLine("\n MENU DE GESTION DE PEDIDOS");
            Console.WriteLine("1. Dar de alta pedido");
            Console.WriteLine("2. Asignar pedido a cadete");
            Console.WriteLine("3. Cambiar estado de pedido");
            Console.WriteLine("4. Mostrar Pedido");
            Console.WriteLine("5. Calcular Jornal Cadete");
            Console.WriteLine("6. Salir");

            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    cadeteria.DarDeAltaPedido();
                    break;
                case "2":
                    AsignarCadete(cadeteria);
                    break;
                case "3":
                    cadeteria.CambiarEstadoPedido();
                    break;
                case "4":
                    cadeteria.MostrarInforme();
                    break;
                case "5":
                    CalcularJornal(cadeteria);
                    break;
                case "6":
                    Console.WriteLine("Cerrando sistema...");
                    Console.WriteLine("Programa finalizado.");
                    return;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }

    public static void AsignarCadete(Cadeteria cadeteria)
    {
        Console.WriteLine("\nPedidos Disponibles:");
        foreach (var p in cadeteria.Pedidos)
        {
            string estadoCadete = p.CadeteAsignado != null ? $"asignado a {p.CadeteAsignado.Nombre}" : "Sin asignar";
            Console.WriteLine($"Pedido: {p.Nro} - Cliente: {p.Datocliente.Nombre}");
        }

        Console.WriteLine("Ingrese el numero de pedido: ");
        if (!int.TryParse(Console.ReadLine(), out int nroPedido))
        {
            Console.WriteLine("Numero no Valido.");
            return;
        }

        Console.WriteLine("\nCadetes Disponibles:");
        foreach (var c in cadeteria.Cadetes)
        {
            Console.WriteLine($"Id: {c.Id} - {c.Nombre}");
        }

        Console.WriteLine("Ingrese Id de Cadete: ");
        if (!int.TryParse(Console.ReadLine(), out int idCadete))
        {
            Console.WriteLine("Id no Valido.");
            return;
        }

        cadeteria.AsignarCadeteAPedido(idCadete, nroPedido);
    }

    public static void CalcularJornal(Cadeteria cadeteria)
    {
        Console.WriteLine("\nIngrese el Id del cadete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Id No Valido.");
            return;
        }

        int jornal = cadeteria.JornalACobrar(id);
        Console.WriteLine($"Jornal a Cobrar: {jornal}");
    }
}
