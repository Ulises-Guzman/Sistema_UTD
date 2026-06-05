<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Sistema_UTD.Admin.Usuarios" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
        <div class="col col-xl my-3">
            <div class="p-3 mb-2 bg-body-tertiary shadow rounded">
                <h4>
                    <i class="bi bi-person-lines-fill me-1"></i>
                    Gestión de Usuarios
                </h4>
                <small class="mb-1 text-secondary">Realice altas, modificaciones o bajas, búsquedas con listados, restablecimiento de contraseñas</small>
            </div>
        </div>
        <div class="row justify-content-around">
            <div class="d-grid col-6">
                <asp:LinkButton ID="lnkBtnCrearUsuario" CssClass="p-3 mb-3 btn btn-light btn-lg shadow text-start" runat="server">
                    <i class="bi bi-person-add me-2"></i>
                    Crear usuario <span class="float-end">></span>
                </asp:LinkButton>
            </div>
            <div class="d-grid col-6">
                <asp:LinkButton ID="lnkBtnModificar" CssClass="p-3 mb-3 btn btn-light btn-lg shadow text-start" runat="server">
                    <i class="bi bi-person-exclamation me-2"></i>
                    Modificar usuario <span class="float-end">></span>
                </asp:LinkButton>
            </div>
        </div>
        <div class="row justify-content-around">
            <div class="d-grid col-6">
                <asp:LinkButton ID="lnkBtnEliminarUsuario" CssClass="p-3 mb-3 btn btn-light btn-lg shadow text-start" runat="server">
                    <i class="bi bi-person-dash me-2"></i>
                    Eliminar usuario <span class="float-end">></span>
                </asp:LinkButton> 
            </div>
            <div class="d-grid col-6">
                <asp:LinkButton ID="lnkBtnBuscarUsuario" CssClass="p-3 mb-3 btn btn-light btn-lg shadow text-start" runat="server">
                    <i class="bi bi-search me-2"></i>
                    Buscar usuario <span class="float-end">></span>
                </asp:LinkButton> 
            </div>
        </div>
    </div>
</asp:Content>
