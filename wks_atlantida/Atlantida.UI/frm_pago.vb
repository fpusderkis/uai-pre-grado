﻿Imports Atlantida.BLL.SIS.BLL
Imports Atlantida.Entidades.SIS.Entidades

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class frm_pago
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
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_pago_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        modificarIdiomaSegunPreferencias(UsuarioIdioma)
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
            If enu.Current.componente = "box_acciones" Then
                Me.box_acciones.Text = enu.Current.value
            End If
            If enu.Current.componente = "btn_cotizar" Then
                Me.btn_cotizar.Text = enu.Current.value
            End If
            If enu.Current.componente = "btn_terminar_op" Then
                Me.btn_terminar_op.Text = enu.Current.value
            End If
            If enu.Current.componente = "btn_transferencia" Then
                Me.btn_transferencia.Text = enu.Current.value
            End If
            If enu.Current.componente = "box_pagos_pend" Then
                Me.btn_transferencia.Text = enu.Current.value
            End If
            If enu.Current.componente = "btn_confirmar" Then
                Me.btn_confirmar.Text = enu.Current.value
            End If
            If enu.Current.componente = "lbl_idPres" Then
                Me.lbl_idPres.Text = enu.Current.value
            End If
            If enu.Current.componente = "btn_solicitarPax" Then
                Me.btn_solicitarPax.Text = enu.Current.value
            End If
            If enu.Current.componente = "frm_pago" Then
                Me.Text = enu.Current.value
            End If
        End While
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