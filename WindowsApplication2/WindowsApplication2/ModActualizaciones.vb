Module ModActualizaciones

    Public Event InventarioActualizado()
    Public Event VentasActualizadas()
    Public Event PedidosActualizados()

    Public Sub NotificarInventarioActualizado()
        RaiseEvent InventarioActualizado()
    End Sub

    Public Sub NotificarVentasActualizadas()
        RaiseEvent VentasActualizadas()
    End Sub

    Public Sub NotificarPedidosActualizados()
        RaiseEvent PedidosActualizados()
    End Sub

End Module
