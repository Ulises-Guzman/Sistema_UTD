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
                <div class="col-xl-5 mb-2">

                    <div class="col-xl text-start mb-1">
                        <div class="p-3 bg-white shadow-sm rounded-top" data-bs-theme="ligth">
                            <h5>Gestión de componentes asociados
                            </h5>
                            <small class="mb-1 text-secondary">Agregue componentes para asociar a los modelos de instrumentos o instrumentos recibidos</small>
                        </div>
                    </div>

                    <div class="row justify-content-center text-center">
                        <div class="col-xl- mb-1">
                            <asp:LinkButton ID="lnkBtnCompAsocModelos"
                                ClientIDMode="Static"
                                CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100 rounded-0"
                                OnClick="lnkBtnCompAsocModelos_Click"
                                runat="server">
                                <i class="bi bi-wrench-adjustable me-2 fs-4"></i>
                                Modelos de instrumentos <span class="float-end">></span>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <div class="row justify-content-center text-center">
                        <div class="col-xl mb-1">
                            <asp:LinkButton ID="lnkBtnCompAsocInstrumentos"
                                ClientIDMode="Static"
                                CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100 rounded-0 rounded-bottom"
                                OnClick="lnkBtnCompAsocInstrumentos_Click"
                                runat="server">
                                <i class="bi bi-wrench-adjustable-circle me-2 fs-4"></i>
                                Instrumentos <span class="float-end">></span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <!-- Agregar más opciones -->
                <%--<div class="row">--%>
                <div class="col-xl-5 mb-2">

                    <div class="col-xl text-start mb-1">
                        <div class="p-3 bg-white shadow-sm rounded-top" data-bs-theme="ligth">
                            <h5>Gestión de instrumentos recibidos
                            </h5>
                            <small class="mb-1 text-secondary">Registre instrumentos recibidos</small>
                        </div>
                    </div>

                    <div class="col-xl mb-4">
                        <asp:LinkButton ID="lnkBtnInstrumentosRecibidos"
                            ClientIDMode="Static"
                            CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100 rounded-0 rounded-bottom"
                            runat="server">
                            <i class="bi bi-tools me-2 fs-4"></i>
                            Instrumentos recibidos <span class="float-end">></span>
                        </asp:LinkButton>
                    </div>

                    <div class="col-xl text-start mb-1">
                        <div class="p-3 bg-white shadow-sm rounded-top" data-bs-theme="ligth">
                            <h5>Gestión de usuarios
                            </h5>
                            <small class="mb-1 text-secondary">Administre usuarios y sus datos</small>
                        </div>
                    </div>
                    <div class="col-xl mb-2">
                        <asp:LinkButton ID="lnkBtnUsuarios"
                            ClientIDMode="Static"
                            CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100 rounded-0 rounded-bottom"
                            OnClick="lnkBtnUsuarios_Click"
                            runat="server">
                            <i class="bi bi-people-fill me-2 fs-4"></i>
                            Usuarios <span class="float-end">></span>
                        </asp:LinkButton>
                    </div>
                </div>
                <%--</div>--%>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
