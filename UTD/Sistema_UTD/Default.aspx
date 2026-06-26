<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Sistema_UTD.Default" %>

<%--Propiedad para acceder a la Master desde el CodeBehind--%>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
        <div class="col-xl-10 mt-3 mb-4">
            <div class="p-3 bg-white shadow-sm rounded" data-bs-theme="ligth">
                <h4>
                    <i class="bi bi-sliders2 me-1"></i>
                    Panel de control
                </h4>
                <small class="mb-1 text-secondary">Configuraciones generales disponibles para el usuario</small>
            </div>
        </div>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-2">
                    <asp:LinkButton ID="lnkBtnConfiguracionCuenta" ClientIDMode="Static" CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100" runat="server">
                        <i class="bi bi-person-gear me-2"></i>
                        Configuración de cuenta <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-2">
                    <asp:LinkButton ID="LinkButton1" ClientIDMode="Static" CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100" runat="server">
                        <i class="bi bi-diagram-3 me-2"></i>
                        Administración de componentes del sistema <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
