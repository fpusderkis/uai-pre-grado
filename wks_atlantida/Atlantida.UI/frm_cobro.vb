﻿Imports Atlantida.Entidades.SIS.Entidades
Imports Atlantida.BLL.SIS.BLL

Public Class frm_cobro
    ''' <summary>
    ''' Creo obj tipo cuenta
    ''' </summary>
    ''' <remarks></remarks>
    Public cue As cuenta
    ''' <summary>
    '''  Creo obj tipo tarjeta
    ''' </summary>
    ''' <remarks></remarks>
    Public tar As tarjeta
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private idUsuario As String
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private idioma As String
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private numeroFactura As Integer
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private montoCobro As Integer
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private valorSeña As Integer
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private interfazCobro As NegCobro = New NegCobro
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private cobroTemporal As Cobro
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_validar.Click
        Dim bandera As Boolean = True
        If txt_cliente.Text = "" Then
            MsgBox("Ingrese un numero de dni")
            bandera = False
        End If
        'Por medio del nro dni voy a buscar todos las operaciones abiertas en estado pendiente de pago
        If bandera = True Then
            Dim _dni As Integer = Integer.Parse(txt_cliente.Text)
            dgw_resultDeuda.DataSource = interfazCobro.obtenerDeudaCliente(_dni)
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button4_Click(sender As Object, e As EventArgs)
        'Validar tipo de pago si es credito->tarjeta or transferencia->cuenta'
        'Creo popUps tarjeta y cuenta'
        Dim popUptarj As frm_tarjeta = New frm_tarjeta()
        Dim popUpcuen As frm_cuenta = New frm_cuenta()
        'Cargo tarjeta para el pago de la operacion'
        If popUptarj.ShowDialog() = DialogResult.OK Then
            Me.tar = popUptarj.tar
            If String.IsNullOrEmpty(txt_tarjeta.Text) Then
                Me.txt_tarjeta.Text = popUptarj.tar.banco + " " + popUptarj.tar.tipo + " " + popUptarj.tar.nrotarjeta + " " + popUptarj.tar.codsegu + " " + popUptarj.tar.fechvenc
            End If
        End If
        'Cargo cuenta para el pago de la operacion'
        If popUpcuen.ShowDialog() = DialogResult.OK Then
            Me.cue = popUpcuen.cue
            If String.IsNullOrEmpty(txt_cuenta.Text) Then
                Me.txt_cuenta.Text = popUpcuen.cue.des_tipocuenta + " " + popUpcuen.cue.des_banco + " " + popUpcuen.cue.des_numerocuenta + " " + popUpcuen.cue.des_nombre + " " + popUpcuen.cue.des_email
            End If
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_sena_Click(sender As Object, e As EventArgs) Handles btn_sena.Click
        txt_sena.Text = valorSeña
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub box_central_Enter(sender As Object, e As EventArgs) Handles box_central.Enter
        modificarIdiomaSegunPreferencias(UsuarioIdioma)

        Dim listadoFormasPago As New List(Of String)
        listadoFormasPago.Add("Efectivo")
        listadoFormasPago.Add("Debito")
        listadoFormasPago.Add("Credito")

        Dim listadoCantidad As New List(Of String)
        listadoCantidad.Add("1")
        listadoCantidad.Add("3")
        listadoCantidad.Add("6")
        listadoCantidad.Add("12")

        Dim enumerator_A As IEnumerator(Of String) = listadoFormasPago.GetEnumerator
        While enumerator_A.MoveNext
            cbx_formasPago.Items.Add(enumerator_A.Current)
        End While
        Dim enumerator_B As IEnumerator(Of String) = listadoCantidad.GetEnumerator
        While enumerator_B.MoveNext
            cbx_cantidad.Items.Add(enumerator_B.Current)
        End While

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idioma"></param>
    ''' <remarks></remarks>
    Private Sub modificarIdiomaSegunPreferencias(ByVal idioma As String)
        'Obtengo el listado de nombre de los componentes en ambos idiomas.
        Dim interfazMultiIdioma As INegMultiIdioma = New NegMultiIdioma
        Dim listadoMultiIdioma As New List(Of MultiIdioma)
        listadoMultiIdioma = interfazMultiIdioma.obtenerTablaMultiIdioma(idioma)

        Dim enu As IEnumerator(Of MultiIdioma) = listadoMultiIdioma.GetEnumerator
        While enu.MoveNext
            If enu.Current.componente = "box_cliente_deuda" Then
                Me.box_cliente_deuda.Text = enu.Current.value
            End If
            If enu.Current.componente = "box_formulario_pago" Then
                Me.box_formulario_pago.Text = enu.Current.value
            End If
            If enu.Current.componente = "box_resultado_deuda" Then
                Me.box_resultado_deuda.Text = enu.Current.value
            End If
            If enu.Current.componente = "btn_imprimir_voucher" Then
                Me.btn_imprimir_voucher.Text = enu.Current.value
            End If
            If enu.Current.componente = "btn_pagar" Then
                Me.btn_pagar.Text = enu.Current.value
            End If
            If enu.Current.componente = "btn_validar" Then
                Me.btn_validar.Text = enu.Current.value
            End If
            If enu.Current.componente = "lbl_cantidad" Then
                Me.lbl_cantidad.Text = enu.Current.value
            End If
            If enu.Current.componente = "lbl_dato_cuenta" Then
                Me.lbl_dato_cuenta.Text = enu.Current.value
            End If
            If enu.Current.componente = "lbl_datos_tarjeta" Then
                Me.lbl_datos_tarjeta.Text = enu.Current.value
            End If
            If enu.Current.componente = "lbl_dni" Then
                Me.lbl_dni.Text = enu.Current.value
            End If
            If enu.Current.componente = "lbl_forma_pago" Then
                Me.lbl_forma_pago.Text = enu.Current.value
            End If
            If enu.Current.componente = "lbl_importe" Then
                Me.lbl_importe.Text = enu.Current.value
            End If
            If enu.Current.componente = "rdio_cuota" Then
                Me.rdio_cuota.Text = enu.Current.value
            End If
            If enu.Current.componente = "btn_sena" Then
                Me.btn_sena.Text = enu.Current.value
            End If
            If enu.Current.componente = "box_sena" Then
                Me.box_sena.Text = enu.Current.value
            End If
            If enu.Current.componente = "btn_ConfirmarComprobantes" Then
                Me.btn_ConfirmarComprobantes.Text = enu.Current.value
            End If
            If enu.Current.componente = "ChkReserva" Then
                Me.ChkReserva.Text = enu.Current.value
            End If
            If enu.Current.componente = "ChkPagoToT" Then
                Me.ChkPagoToT.Text = enu.Current.value
            End If
            If enu.Current.componente = "frm_cobro" Then
                Me.Text = enu.Current.value
            End If
        End While
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgw_resultDeuda_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgw_resultDeuda.CellDoubleClick
        cobroTemporal = New Cobro
        Dim idx As Integer
        idx = e.RowIndex
        Dim selectedRow As DataGridViewRow
        selectedRow = dgw_resultDeuda.Rows(idx)

        cobroTemporal.numeroFactura = selectedRow.Cells(1).Value.ToString()
        numeroFactura = cobroTemporal.numeroFactura
        cobroTemporal.montos = selectedRow.Cells(4).Value.ToString()
        montoCobro = cobroTemporal.montos

        Dim seña As Integer
        seña = CInt(cobroTemporal.montos) * 0.3
        seña = CStr(seña)
        MsgBox("La seña es de: $" + seña.ToString)

        valorSeña = seña
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_pagar_Click(sender As Object, e As EventArgs) Handles btn_pagar.Click
        Dim bandera As String = True

        Dim selectedIndex As Integer
        selectedIndex = cbx_formasPago.SelectedIndex
        Dim selectedItem As Object
        selectedItem = cbx_formasPago.SelectedItem

        If selectedItem Is Nothing Then
            bandera = False
            MsgBox("Debe Seleccionar un Medio de Pago")
        End If

        'VALIDO SI ES EFECTIVO O SI ES TARJETA DEBITO/CREDITO
        If selectedItem = "Efectivo" Then
            If txt_importe.Text = "" Then
                MsgBox("El importe no puede ser vacio")
                bandera = False
            End If
        End If

        If selectedItem = "Credito" Or selectedItem = "Debito" Then
            'SI ES CREDITO/DEBITO LA CANT DE CUOTAS MIN ES 1 Y HAY QUE DEFINIR DATOS DE LA TARJETA
            If rdio_cuota.Checked = False Then
                MsgBox("La cantidad de cuotas minimas para debito y credito es de: 1")
                bandera = False
            End If
            If txt_tarjeta.Text = "" Or txt_cuenta.Text = "" Then
                MsgBox("Se debe ingresar el numero de tarjeta y de cuenta")
                bandera = False
            End If
        End If

        If bandera = True Then
            If ChkReserva.Checked = True Then
                generarReserva(numeroFactura, CInt(txt_sena.Text))
            End If

            If ChkPagoToT.Checked = True Then
                generarCobro(numeroFactura)
                'TO-DO:INCREMENTAR MI CAJA POR LA RESERVA O PAGO TOTAL

            End If
            MsgBox("Debe seleccionar un tipo de cobro ya sea RESERVA O PAGO TOTAL")
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idFactura"></param>
    ''' <param name="_montoReserva"></param>
    ''' <remarks></remarks>
    Private Sub generarReserva(ByVal idFactura As Integer, ByVal _montoReserva As Integer)
        Dim montoCReserva As Integer
        montoCReserva = montoCobro - _montoReserva
        interfazCobro.registrarReserva(idFactura, montoCReserva)
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idFactura"></param>
    ''' <remarks></remarks>
    Private Sub generarCobro(ByVal idFactura As String)
        interfazCobro.registrarCobro(idFactura)
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UsuarioId() As String
        Get
            Return idUsuario
        End Get
        Set(ByVal value As String)
            idUsuario = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UsuarioIdioma() As String
        Get
            Return idioma
        End Get
        Set(ByVal value As String)
            idioma = value
        End Set
    End Property
End Class