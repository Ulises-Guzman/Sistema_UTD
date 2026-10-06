<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompAsocModelos.aspx.cs" Inherits="Sistema_UTD.GestionInstrumentos.GestionCompModInst" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Cabecera -->
    <div class="row justify-content-center">
        <div class="col-xl-10 mt-3 mb-4">
            <div class="p-4 bg-white shadow-sm rounded" data-bs-theme="ligth">
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

                    <div id="alertaNoSatisfactoria" class="alert alert-danger fade show" style="display: none" role="alert" runat="server">
                    </div>
                </div>
            </div>

            <div class="row justify-content-center">
                <div class="col-xl-10 text-start">
                    <div class="p-3 bg-white text-start rounded-top border-bottom" data-bs-theme="ligth">
                        <h5>Modelos
                        </h5>
                    </div>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10">
                    <asp:LinkButton ID="lnkAgregarModelo"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg text-start w-100 rounded-0 border-bottom"
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

            <div class="row justify-content-center">
                <div class="col-xl-10 ">
                    <div class="p-3 bg-white text-start rounded-top border-bottom" data-bs-theme="ligth">
                        <h5>Rangos
                        </h5>
                    </div>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10">
                    <asp:LinkButton ID="lnkAgregarRango"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg text-start w-100 rounded-0 border-bottom"
                        OnClick="lnkAgregarRango_Click"
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
                <div class="col-xl-10 text-start">
                    <div class="p-3 bg-white rounded-top border-bottom" data-bs-theme="ligth">
                        <h5>Unidades de medida
                        </h5>
                    </div>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10">
                    <asp:LinkButton ID="lnkAgregarUnidadMedida"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg text-start w-100 rounded-0 border-bottom"
                        OnClick="lnkAgregarUnidadMedida_Click"
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

            <!-- Formularios modales para componentes asociados a modelos -->
            <!-- Formulario modal crear nuevo modelo -->
            <div class="modal fade" id="frmModalModelo" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-body-tertiary">
                            <div class="d-flex flex-column flex-grow-1 me-2">
                                <h1 class="modal-title fs-5">
                                    <i class="bi bi-ui-radios me-1"></i>
                                    <asp:Label ID="lblTituloModalModelo" ClientIDMode="Static" Text="Crear modelo" runat="server"></asp:Label>
                                </h1>
                                <small class="mb-1 text-secondary" id="lblDescripcionModalModelo">Cree un nuevo modelo para clasificar instrumentos</small>
                            </div>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <!-- Form -->
                            <div class="mb-3">
                                <label for="txtModelo" class="col-form-label">Modelo</label>
                                <asp:TextBox ID="txtModelo" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                <div class="invalid-feedback">Por favor, ingrese el nuevo modelo.</div>
                                <div class="valid-feedback">Ok</div>
                            </div>
                        </div>
                        <div class="modal-footer bg-body-tertiary">
                            <asp:Button ID="btnCancelarNuevoModelo" CssClass="btn btn-secondary" Text="Cancelar" data-bs-dismiss="modal" runat="server" />
                            <asp:Button ID="btnAceptarNuevoModelo" CssClass="btn btn-primary" Text="Aceptar" OnClientClick="return validarModelo();" OnClick="btnAceptarNuevoModelo_Click" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- Fin formulario modal crear nuevo modelo -->

            <!-- Formulario modal crear nuevo rango -->
            <div class="modal fade" id="frmModalRango" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-body-tertiary">
                            <div class="d-flex flex-column flex-grow-1 me-2">
                                <h1 class="modal-title fs-5">
                                    <i class="bi bi-ui-radios me-1"></i>
                                    <asp:Label ID="lblTituloModalRango" ClientIDMode="Static" Text="Crear rango" runat="server"></asp:Label>
                                </h1>
                                <small class="mb-1 text-secondary" id="lblDescripcionModalRango">Cree un nuevo rango para clasificar instrumentos</small>
                            </div>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <!-- Form -->
                            <div class="mb-3">
                                <label for="txtRango" class="col-form-label">Rango</label>
                                <asp:TextBox ID="txtRango" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                <div class="invalid-feedback">Por favor, ingrese el nuevo rango.</div>
                                <div class="valid-feedback">Ok</div>
                            </div>
                        </div>
                        <div class="modal-footer bg-body-tertiary">
                            <asp:Button ID="btnCancelarNuevoRango" CssClass="btn btn-secondary" Text="Cancelar" data-bs-dismiss="modal" runat="server" />
                            <asp:Button ID="btnAceptarNuevoRango" CssClass="btn btn-primary" Text="Aceptar" OnClientClick="return validarRango();" OnClick="btnAceptarNuevoRango_Click" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- Fin formulario modal crear nuevo rango -->

            <!-- Formulario modal crear nueva unidad de medida -->
            <div class="modal fade" id="frmModalUnidad" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-body-tertiary">
                            <div class="d-flex flex-column flex-grow-1 me-2">
                                <h1 class="modal-title fs-5">
                                    <i class="bi bi-ui-radios me-1"></i>
                                    <asp:Label ID="lblTituloModalUnidad" ClientIDMode="Static" Text="Crear Unidad de medida" runat="server"></asp:Label>
                                </h1>
                                <small class="mb-1 text-secondary" id="lblDescripcionModalUnidad">Cree una nueva unidad de medida para clasificar instrumentos</small>
                            </div>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <!-- Form -->
                            <div class="mb-3">
                                <label for="txtUnidad" class="col-form-label">Unidad de medida</label>
                                <asp:TextBox ID="txtUnidad" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                <div class="invalid-feedback">Por favor, ingrese la nueva unidad de medida.</div>
                                <div class="valid-feedback">Ok</div>
                            </div>
                        </div>
                        <div class="modal-footer bg-body-tertiary">
                            <asp:Button ID="btnCancelarNuevaUnidad" CssClass="btn btn-secondary" Text="Cancelar" data-bs-dismiss="modal" runat="server" />
                            <asp:Button ID="btnAceptarNuevaUnidad" CssClass="btn btn-primary" Text="Aceptar" OnClientClick="return validarUnidad()" OnClick="btnAceptarNuevaUnidad_Click" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- Fin formulario modal crear nueva unidad de medida -->

            <!-- Fin formularios modales para componentes asociados a modelos -->
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="/Scripts/modalModelo.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/modalRango.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/modalUnidad.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/validarCompAsocModelo.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/validarCompAsocRango.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/validarCompAsocUnidad.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/alertModelo.js?v=<%= DateTime.Now.Ticks %>"></script>
</asp:Content>
