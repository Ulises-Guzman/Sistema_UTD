<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Sistema_UTD.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md mx-3 my-3">
            <div class="p-3 mb-2 bg-body-tertiary shadow rounded">
                <h4>
                 <i class="bi bi-sliders2 me-1"></i>
                    Panel de control
                </h4>
                <small class="mb-1 text-secondary">Configuraciones generales disponibles para el usuario</small>
            </div>
        </div>
        <div class="row">
            <div class="col-4-md mx-3">
                <asp:LinkButton ID="LinkButton1" CssClass="p-3 mb-3 btn btn-light btn-lg shadow" runat="server">
                    <i class="bi bi-person-circle me-2"></i>
                    Configuración de cuenta
                </asp:LinkButton>
            </div>
        </div>
        <div class="row">
            <div class="col-4-md mx-3">
                <asp:LinkButton ID="LinkButton2" CssClass="p-3 mb-3 btn btn-light btn-lg shadow" runat="server">
                    <i class="bi bi-person-circle me-2"></i>
                    Opcion 2
                </asp:LinkButton>
            </div>
        </div>
        <div class="row">
            <div class="col-4-md mx-3">
                <asp:LinkButton ID="LinkButton3" CssClass="p-3 mb-3 btn btn-light btn-lg shadow" runat="server">
                    <i class="bi bi-person-circle me-2"></i>
                     Opcion 2
                </asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
