﻿Imports System.IO
Imports Atlantida.IO.SIS.IO
Imports Atlantida.Entidades.SIS.Entidades
Imports Atlantida.DAL.SIS.DAL

Namespace SIS.BLL
    Public Class NegPago
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Private unUsuarioField As Usuario
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Dim interfazNegocioBitacora As INegBitacora = New NegBitacora
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="_pago"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function registrarPago(ByVal _pago As Pago)
            Dim oDalPago As New DALPago
            Try
                oDalPago.registrarPago(_pago)
            Catch ex As Exception
                interfazNegocioBitacora.registrarEnBitacora_BLL(unUsuario.idUsuario, ex)
            End Try
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="idPres"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function actualizarEstadoPago(ByVal idPres As Integer, ByVal estado As String)
            Dim oDalPago As New DALPago
            Try
                oDalPago.actualizarEstadoPago(idPres, estado)
            Catch ex As Exception
                interfazNegocioBitacora.registrarEnBitacora_BLL(unUsuario.idUsuario, ex)
            End Try
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="idPres"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function registrarPresupuestoPago(ByVal idPres As Integer)
            Dim oDalPago As New DALPago
            Try
                oDalPago.insertarPresupuestoPago(idPres)
            Catch ex As Exception
                interfazNegocioBitacora.registrarEnBitacora_BLL(unUsuario.idUsuario, ex)
            End Try
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function obtenerPagos() As DataTable
            Dim listadoPago As DataTable
            listadoPago = Nothing
            Dim oDalPago As New DALPago
            Try
                listadoPago = oDalPago.obtenerPagosActivos()
            Catch ex As Exception
                interfazNegocioBitacora.registrarEnBitacora_BLL(unUsuario.idUsuario, ex)
            End Try

            Return listadoPago
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="_idx"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function cancelarPago(ByVal _idx As Integer)
            Dim oDalPago As New DALPago
            Try
                oDalPago.cancelarPago(_idx)
            Catch ex As Exception
                interfazNegocioBitacora.registrarEnBitacora_BLL(unUsuario.idUsuario, ex)
            End Try
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property unUsuario() As Usuario
            Get
                Return unUsuarioField
            End Get
            Set(ByVal value As Usuario)
                unUsuarioField = value
            End Set
        End Property
    End Class
End Namespace

