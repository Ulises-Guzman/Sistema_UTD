<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompAsocInstrumentos.aspx.cs" Inherits="Sistema_UTD.GestionInstrumentos.GestionCompAsocInst" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
        <div class="col-xl-10 mt-3 mb-4">
            <div class="p-3 bg-white shadow-sm rounded" data-bs-theme="ligth">
                <h4>
                    <i class="bi bi-wrench-adjustable me-1"></i>
                    Gestión de componentes asociados a instrumentos
                </h4>
                <small class="mb-1 text-secondary">Agregue, modifique o desactive nuevos destinos, secciones, estados de instrumentos, tiempos de habilitación</small>
            </div>
        </div>
    </div>

    <div class="row justify-content-center text-center">
        <div class="col-xl-10 mb-2">
            <asp:LinkButton ID="lnkAgregarDestino"
                ClientIDMode="Static"
                CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100"
                runat="server">
                <i class="bi bi-plus-circle me-2 icon-green"></i>
                Agregar destino <span class="float-end">></span>
            </asp:LinkButton>
        </div>
    </div>

    <div class="row justify-content-center text-center">
        <div class="col-xl-10 mb-4">
            <asp:LinkButton ID="lnkModificarDestino"
                ClientIDMode="Static" CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100" runat="server">
                <i class="bi bi-exclamation-circle me-2 icon-orange"></i>
                Modificar destino <span class="float-end">></span>
            </asp:LinkButton>
        </div>
    </div>

    <div class="row justify-content-center text-center">
        <div class="col-xl-10 mb-2">
            <asp:LinkButton ID="lnkAgregarSeccion" ClientIDMode="Static" CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100" runat="server">
                <i class="bi bi-plus-circle me-2 icon-green"></i>
                Agregar sección <span class="float-end">></span>
            </asp:LinkButton>
        </div>
    </div>

    <div class="row justify-content-center text-center">
        <div class="col-xl-10 mb-4">
            <asp:LinkButton ID="lnkModificarSeccion" ClientIDMode="Static" CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100" runat="server">
                <i class="bi bi-exclamation-circle me-2 icon-orange"></i>
                Modificar sección <span class="float-end">></span>
            </asp:LinkButton>
        </div>
    </div>

    <div class="row justify-content-center text-center">
        <div class="col-xl-10 mb-2">
            <asp:LinkButton ID="lnkAgregarEstadoInst" ClientIDMode="Static" CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100" runat="server">
                <i class="bi bi-plus-circle me-2 icon-green"></i>
                Agregar estado de instrumento <span class="float-end">></span>
            </asp:LinkButton>
        </div>
    </div>

    <div class="row justify-content-center text-center">
        <div class="col-xl-10 mb-4">
            <asp:LinkButton ID="lnkModificarEstadoInst" ClientIDMode="Static" CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100" runat="server">
                <i class="bi bi-exclamation-circle me-2 icon-orange"></i>
                Modificar estado de instrumento <span class="float-end">></span>
            </asp:LinkButton>
        </div>
    </div>

    <div class="row justify-content-center text-center">
        <div class="col-xl-10 mb-2">
            <asp:LinkButton ID="lnkAgregarTiempoHab" ClientIDMode="Static" CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100" runat="server">
                <i class="bi bi-plus-circle me-2 icon-green"></i>
                Agregar tiempo de habilitación <span class="float-end">></span>
            </asp:LinkButton>
        </div>
    </div>

    <div class="row justify-content-center text-center">
        <div class="col-xl-10 mb-4">
            <asp:LinkButton ID="lnkModificarTiempoHab" ClientIDMode="Static" CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100" runat="server">
                <i class="bi bi-exclamation-circle me-2 icon-orange"></i>
                Modificar tiempo de habilitación <span class="float-end">></span>
            </asp:LinkButton>
        </div>
    </div>
</asp:Content>
