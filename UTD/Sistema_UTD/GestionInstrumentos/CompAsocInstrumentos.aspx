<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompAsocInstrumentos.aspx.cs" Inherits="Sistema_UTD.GestionInstrumentos.GestionCompAsocInst" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Cabecera -->
    <div class="row justify-content-center">
        <div class="col-xl-10 mt-3 mb-3">
            <div class="p-4 bg-white shadow-sm rounded" data-bs-theme="ligth">
                <h4>
                    <i class="bi bi-wrench-adjustable me-1"></i>
                    Gestión de componentes asociados a instrumentos
                </h4>
                <small class="mb-1 text-secondary">Agregue, modifique o desactive nuevos destinos, secciones, estados de instrumentos, tiempos de habilitación</small>
            </div>
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- Alerta de notificación -->
            <div class="row justify-content-center">
                <div class="col-xl-10 mb-5">
                    <div id="alertaSatisfactoria" class="alert alert-success fade show" style="display: none" role="alert" runat="server">
                        ¡El componente ha sido creado!
                    </div>

                    <div id="alertaNoSatisfactoria" class="alert alert-danger fade show" style="display: none" role="alert" runat="server">
                    </div>
                </div>
            </div>

            <!-- Panel de botones -->
            <div class="row justify-content-center">
                <div class="col-xl-10">
                    <div class="p-3 bg-white text-start rounded-top border-bottom" data-bs-theme="ligth">
                        <h5>Destinos
                        </h5>
                    </div>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10">
                    <asp:LinkButton ID="lnkAgregarDestino"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg text-start w-100 rounded-0 border-bottom"
                        OnClick="lnkAgregarDestino_Click"
                        runat="server">
                    <i class="bi bi-plus-circle me-2 icon-green"></i>
                    Agregar destino <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-4">
                    <asp:LinkButton ID="lnkModificarDestino"
                        ClientIDMode="Static" CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100 rounded-0 rounded-bottom"
                        OnClick="lnkModificarDestino_Click"
                        runat="server">
                    <i class="bi bi-exclamation-circle me-2 icon-orange"></i>
                    Modificar destino <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <div class="row justify-content-center">
                <div class="col-xl-10">
                    <div class="p-3 bg-white text-start rounded-top border-bottom" data-bs-theme="ligth">
                        <h5>Secciones
                        </h5>
                    </div>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10">
                    <asp:LinkButton ID="lnkAgregarSeccion"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg text-start w-100 rounded-0 border-bottom"
                        runat="server">
                    <i class="bi bi-plus-circle me-2 icon-green"></i>
                    Agregar sección <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-4">
                    <asp:LinkButton ID="lnkModificarSeccion"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100 rounded-0 rounded-bottom"
                        runat="server">
                    <i class="bi bi-exclamation-circle me-2 icon-orange"></i>
                    Modificar sección <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <div class="row justify-content-center">
                <div class="col-xl-10">
                    <div class="p-3 bg-white text-start rounded-top border-bottom" data-bs-theme="ligth">
                        <h5>Estados de instrumento
                        </h5>
                    </div>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10">
                    <asp:LinkButton ID="lnkAgregarEstadoInst"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg text-start w-100 rounded-0 border-bottom"
                        runat="server">
                    <i class="bi bi-plus-circle me-2 icon-green"></i>
                    Agregar estado de instrumento <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-4">
                    <asp:LinkButton ID="lnkModificarEstadoInst"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100 rounded-0 rounded-bottom"
                        runat="server">
                    <i class="bi bi-exclamation-circle me-2 icon-orange"></i>
                    Modificar estado de instrumento <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10">
                    <div class="p-3 bg-white text-start rounded-top border-bottom" data-bs-theme="ligth">
                        <h5>Tiempos de habilitación
                        </h5>
                    </div>
                </div>
            </div>

            <div class="row justify-content-center">
                <div class="col-xl-10">
                    <asp:LinkButton ID="lnkAgregarTiempoHab"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg text-start w-100 rounded-0 border-bottom"
                        runat="server">
                    <i class="bi bi-plus-circle me-2 icon-green"></i>
                    Agregar tiempo de habilitación <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-4">
                    <asp:LinkButton ID="lnkModificarTiempoHab"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100 rounded-0 rounded-bottom"
                        runat="server">
                    <i class="bi bi-exclamation-circle me-2 icon-orange"></i>
                    Modificar tiempo de habilitación <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <!-- Formularios modales para componentes asociados a instrumentos -->
            <!-- Formulario modal crear nuevo destino -->
            <div class="modal fade" id="frmModalDestino" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-body-tertiary">
                            <div class="d-flex flex-column flex-grow-1 me-2">
                                <h1 class="modal-title fs-5">
                                    <i class="bi bi-ui-radios me-1"></i>
                                    <asp:Label ID="lblTituloModalDestino" ClientIDMode="Static" Text="Crear destino" runat="server"></asp:Label>
                                </h1>
                                <small class="mb-1 text-secondary" id="lblDescripcionModalDestino">Cree un nuevo destino para clasificar instrumentos</small>
                            </div>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <asp:Panel ID="pnlModalDestino" DefaultButton="btnAceptarNuevoDestino" runat="server">
                            <div class="modal-body">
                                <!-- Form -->
                                <div class="mb-3">
                                    <label for="txtCuatrigrama" class="col-form-label">Cuatrigrama</label>
                                    <asp:TextBox ID="txtCuatrigrama" ClientIDMode="Static" CssClass="form-control text-uppercase" MaxLength="4" runat="server"></asp:TextBox>
                                    <div class="invalid-feedback">Por favor, ingrese el nuevo cuatrigrama del destino.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="mb-3">
                                    <label for="txtDestino" class="col-form-label">Destino</label>
                                    <asp:TextBox ID="txtDestino" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                    <div class="invalid-feedback">Por favor, ingrese el nuevo destino.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                            </div>
                            <div class="modal-footer bg-body-tertiary">
                                <asp:Button ID="btnCancelarNuevDestino" CssClass="btn btn-secondary" Text="Cancelar" data-bs-dismiss="modal" runat="server" />
                                <asp:Button ID="btnAceptarNuevoDestino" CssClass="btn btn-primary" ClientIDMode="Static" Text="Aceptar" OnClientClick="return validarDestino();" OnClick="btnAceptarNuevoDestino_Click" runat="server" />
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <!-- Fin formulario modal crear nuevo destino -->

            <!-- Fin formularios modales para componentes asociados a instrumentos -->
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="/Scripts/modalDestino.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/validarNuevoCADestino.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/alertGeneral.js?v=<%= DateTime.Now.Ticks %>"></script>
</asp:Content>
