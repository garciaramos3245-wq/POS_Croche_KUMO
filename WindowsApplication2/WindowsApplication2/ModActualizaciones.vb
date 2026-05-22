' Publica eventos compartidos para que las pantallas se refresquen cuando cambian inventario, ventas o pedidos.

Module ModActualizaciones

    ' Eventos que comunican cambios entre formularios sin acoplarlos directamente.

    ' Evento que se dispara cuando cambia el inventario.
    Public Event InventarioActualizado()
    ' Evento que se dispara cuando se registra o cancela una venta.
    Public Event VentasActualizadas()
    ' Evento que se dispara cuando cambia la lista de pedidos.
    Public Event PedidosActualizados()

    ' Avisa a las pantallas abiertas que deben recargar inventario.
    Public Sub NotificarInventarioActualizado()
        RaiseEvent InventarioActualizado()
    End Sub

    ' Avisa a las pantallas abiertas que deben recargar ventas o reportes.
    Public Sub NotificarVentasActualizadas()
        RaiseEvent VentasActualizadas()
    End Sub

    ' Avisa a las pantallas abiertas que deben recargar pedidos.
    Public Sub NotificarPedidosActualizados()
        RaiseEvent PedidosActualizados()
    End Sub

End Module
