<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Sistema_UTD.Default" %>

<%--Propiedad para acceder a la Master desde el CodeBehind--%>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Cabecera -->
    <div class="row justify-content-center">
        <div class="col-xl-10 mt-3 mb-1">
            <div class="p-3 bg-white shadow-sm rounded" data-bs-theme="ligth">
                <h4>
                    <i class="bi bi-sliders2 me-1"></i>
                    Panel de control
                </h4>
                <small class="mb-1 text-secondary">Gestión y configuraciones generales disponibles para el usuario</small>
            </div>
        </div>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- Alerta de notificación -->
            <div class="row justify-content-center">
                <div class="col-xl-10 mt-1 mb-5">
                    <div id="alertaSatisfactoria" class="alert alert-success fade show" style="display: none" role="alert" runat="server">
                        ¡El usuario ha sido creado!
                    </div>

                    <%--<div id="alertaNoSatisfactoria" class="alert alert-danger alert-dismissible fade show" role="alert" visible="false" runat="server">
                        El usuario No ha sido creado!
                    </div>--%>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-2">
                    <asp:LinkButton ID="lnkBtnGestionComponentes"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100"
                        OnClick="lnkBtnGestionComponentes_Click"
                        runat="server">
                        <i class="bi bi-wrench-adjustable me-2 fs-4"></i>
                        Gestión de componentes asociados a modelo de instrumentos <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-2">
                    <asp:LinkButton ID="lnkGestionComponentesAsoc"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100"
                        OnClick="lnkGestionComponentesAsoc_Click"
                        runat="server">
                        <i class="bi bi-wrench-adjustable-circle me-2 fs-4"></i>
                        Gestión de componentes asociados a instrumentos <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <!-- Agregar más opciones -->

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-2">
                    <asp:LinkButton ID="lnkBtnConfiguracionCuenta"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100"
                        runat="server">
                        <i class="bi bi-person-gear me-2 fs-4"></i>
                        Configuración de cuenta <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
