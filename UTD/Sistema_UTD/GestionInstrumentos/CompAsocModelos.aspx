<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompAsocModelos.aspx.cs" Inherits="Sistema_UTD.GestionInstrumentos.GestionCompModInst" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
        <div class="col-xl-10 mt-3 mb-4">
            <div class="p-3 bg-white shadow-sm rounded" data-bs-theme="ligth">
                <h4>
                    <i class="bi bi-wrench-adjustable me-1"></i>
                    Gestión componentes asociados a modelos de instrumentos
                </h4>
                <small class="mb-1 text-secondary">Agregue, modifique o desactive nuevos modelos, rangos y unidades de medida</small>
            </div>
        </div>
    </div>

    <div class="row justify-content-center text-center">
        <div class="col-xl-10 mb-2">
            <asp:LinkButton ID="lnkAgregarModelo"
                ClientIDMode="Static"
                CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100"
                runat="server">
                <i class="bi bi-plus-circle me-2 icon-green"></i>
                Agregar modelo <span class="float-end">></span>
            </asp:LinkButton>
        </div>
    </div>

    <div class="row justify-content-center text-center">
        <div class="col-xl-10 mb-4">
            <asp:LinkButton ID="lnkModificarModelo"
                ClientIDMode="Static"
                CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100"
                runat="server">
                <i class="bi bi-exclamation-circle me-2 icon-orange"></i>
                Modificar modelo <span class="float-end">></span>
            </asp:LinkButton>
        </div>
    </div>

    <div class="row justify-content-center text-center">
        <div class="col-xl-10 mb-2">
            <asp:LinkButton ID="lnkAgregarRango"
                ClientIDMode="Static"
                CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100"
                runat="server">
                <i class="bi bi-plus-circle me-2 icon-green"></i>
                Agregar rango <span class="float-end">></span>
            </asp:LinkButton>
        </div>
    </div>

    <div class="row justify-content-center text-center">
        <div class="col-xl-10 mb-4">
            <asp:LinkButton ID="lnkModificarRango" 
                ClientIDMode="Static"
                CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100"
                runat="server">
                <i class="bi bi-exclamation-circle me-2 icon-orange"></i>
                Modificar rango <span class="float-end">></span>
            </asp:LinkButton>
        </div>
    </div>

    <div class="row justify-content-center text-center">
        <div class="col-xl-10 mb-2">
            <asp:LinkButton ID="lnkAgregarUnidadMedida" ClientIDMode="Static" CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100" runat="server">
                <i class="bi bi-plus-circle me-2 icon-green"></i>
                Agregar unidad de medida <span class="float-end">></span>
            </asp:LinkButton>
        </div>
    </div>

    <div class="row justify-content-center text-center">
        <div class="col-xl-10 mb-4">
            <asp:LinkButton ID="lnkModificarUnidadMedida" ClientIDMode="Static" CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100" runat="server">
                <i class="bi bi-exclamation-circle me-2 icon-orange"></i>
                Modificar unidad de medida <span class="float-end">></span>
            </asp:LinkButton>
        </div>
    </div>
</asp:Content>
