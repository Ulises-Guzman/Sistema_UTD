<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompAsocModelos.aspx.cs" Inherits="Sistema_UTD.GestionInstrumentos.GestionCompModInst" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Cabecera -->
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

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- Alerta de notificación -->
            <div class="row justify-content-center">
                <div class="col-xl-10 mt-1 mb-5">
                    <div id="alertaSatisfactoria" class="alert alert-success fade show" style="display: none" role="alert" runat="server">
                        ¡El componente ha sido creado!
                    </div>

                    <%--<div id="alertaNoSatisfactoria" class="alert alert-danger alert-dismissible fade show" role="alert" visible="false" runat="server">
                            El usuario No ha sido creado!
                        </div>--%>
                </div>
            </div>

            <!-- Panel de tarjetas -->
            <div class="row justify-content-center">
                <div class="col-xl-10">
                    <div class="row">
                        <div class="col-lg-3 mb-5">
                            <div class="card border-light shadow-sm">
                                <div class="card-body d-flex flex-column">
                                    <h6 class="card-title"><i class="bi bi-pen me-2 fs-5"></i>Modelos</h6>
                                    <asp:Label ID="lblNumModelo" ClientIDMode="Static" CssClass="card-text ms-auto fs-1" Text="0" runat="server"></asp:Label>
                                    <small class="text-secondary mt-auto">Activo</small>
                                </div>
                            </div>
                        </div>
                        <%--<div class="col-lg-3 mb-5">
                    <div class="card border-light shadow-sm">
                        <div class="card-body d-flex flex-column">
                            <h6 class="card-title"><i class="bi bi-wrench me-2 fs-5"></i>Rangos</h6>
                            <asp:Label ID="lblNumRango" ClientIDMode="Static" CssClass="card-text ms-auto fs-1" Text="0" runat="server"></asp:Label>
                            <small class="text-secondary mt-auto">Activo</small>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 mb-5">
                    <div class="card border-light shadow-sm">
                        <div class="card-body d-flex flex-column">
                            <h6 class="card-title"><i class="bi bi-person-standing me-2 fs-5"></i>Unidades de medida</h6>
                            <asp:Label ID="lblNumUnidadesMedida" ClientIDMode="Static" CssClass="card-text ms-auto fs-1" Text="0" runat="server"></asp:Label>
                            <small class="text-secondary mt-auto">Activo</small>
                        </div>
                    </div>
                </div>--%>
                    </div>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 text-start mb-1">
                    <div class="p-3 bg-white shadow-sm rounded-top" data-bs-theme="ligth">
                        <h5>Modelos
                        </h5>
                    </div>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-1">
                    <asp:LinkButton ID="lnkAgregarModelo"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100 rounded-0"
                        OnClick="lnkAgregarModelo_Click"
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
                        CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100 rounded-0 rounded-bottom"
                        OnClick="lnkModificarModelo_Click"
                        runat="server">
                        <i class="bi bi-exclamation-circle me-2 icon-orange"></i>
                        Modificar modelo <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 text-start mb-1">
                    <div class="p-3 bg-white shadow-sm rounded-top" data-bs-theme="ligth">
                        <h5>Rangos
                        </h5>
                    </div>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-1">
                    <asp:LinkButton ID="lnkAgregarRango"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100 rounded-0"
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
                        CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100 rounded-0 rounded-bottom"
                        runat="server">
                        <i class="bi bi-exclamation-circle me-2 icon-orange"></i>
                        Modificar rango <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 text-start mb-1">
                    <div class="p-3 bg-white shadow-sm rounded-top" data-bs-theme="ligth">
                        <h5>Unidades de medida
                        </h5>
                    </div>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-1">
                    <asp:LinkButton ID="lnkAgregarUnidadMedida"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100 rounded-0"
                        runat="server">
                        <i class="bi bi-plus-circle me-2 icon-green"></i>
                        Agregar unidad de medida <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-4">
                    <asp:LinkButton ID="lnkModificarUnidadMedida"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100 rounded-0 rounded-bottom"
                        runat="server">
                        <i class="bi bi-exclamation-circle me-2 icon-orange"></i>
                        Modificar unidad de medida <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <!-- Formularios modales para componentes -->
            <!-- Formulario Modal Crear Modelo -->
            <div class="modal fade" id="frmModalModelo" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-body-tertiary">
                            <div class="d-flex flex-column flex-grow-1 me-2">
                                <h1 class="modal-title fs-5">
                                    <i class="bi bi-ui-radios me-1"></i>
                                    <asp:Label ID="lblTituloModalModelo" ClientIDMode="Static" runat="server"></asp:Label>
                                </h1>
                                <small class="mb-1 text-secondary" id="lblDescripcionModalModelo" clientidmode="Static" runat="server"></small>
                            </div>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <!-- Form -->
                            <div class="mb-3">
                                <label for="txtModelo" class="col-form-label">Modelo</label>
                                <asp:TextBox ID="txtModelo" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                <div class="invalid-feedback">Por favor, ingrese el modelo.</div>
                                <div class="valid-feedback">Ok</div>
                            </div>
                        </div>
                        <div class="modal-footer bg-body-tertiary">
                            <asp:Button ID="btnCancelar" CssClass="btn btn-secondary" Text="Cancelar" data-bs-dismiss="modal" runat="server" />
                            <asp:Button ID="btnAceptar" CssClass="btn btn-primary" Text="Aceptar" OnClientClick="return validarModeloNuevo();" runat="server" />
                             <%--OnClick="btnAceptar_Click"--%>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Fin Formulario Modal Crear Modelo -->

            <!-- Final de los fomularios modales de Altas -->
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="/Scripts/modalModelo.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/validarNuevoModelo.js?v=<%= DateTime.Now.Ticks %>"></script>
</asp:Content>
