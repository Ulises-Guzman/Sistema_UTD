<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioSeccion.aspx.cs" Inherits="Sistema_UTD.GestionInstrumentos.FormularioSeccion" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!-- Cabecera -->
<div class="row justify-content-center">
    <div class="col-xl-10 mt-3 mb-3">
        <div class="p-4 bg-white shadow-sm rounded" data-bs-theme="ligth">
            <h4>
                <i class="bi bi-ui-radios me-1"></i>
                Modificar Sección
            </h4>
            <small class="mb-1 text-secondary" id="lblDescripcion" runat="server">Modifique datos de secciones y activaciones</small>
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
                    ¡Los datos de la sección han sido modificados!
                </div>

                <div id="alertaNoSatisfactoria" class="alert alert-danger fade show" style="display: none" role="alert" runat="server">
                </div>
            </div>
        </div>

        <!-- Panel Buscador -->
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row justify-content-center">
                    <div class="col-xl-10 mb-2">
                        <div class="p-3 bg-white shadow-sm rounded" data-bs-theme="ligth">
                            <!-- Buscador -->
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="txtBuscar" class="col-form-label">Buscador</label>
                                    <asp:TextBox ID="txtBuscar" ClientIDMode="Static" CssClass="form-control" placeholder="Buscar sección..." AutoPostBack="true" OnTextChanged="txtBuscar_TextChanged" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- Título y botón actualizar -->
        <div class="row justify-content-center">
            <div class="col-xl-10 mt-3 mb-2 d-flex justify-content-between align-items-center">
                <h4 class="m-0">Lista de Secciones</h4>
                <asp:LinkButton ID="lnkBtnActualizar" ClientIDMode="Static" CssClass="btn btn-success align-content-end" OnClick="lnkBtnActualizar_Click" runat="server">
                <i class="bi bi-arrow-clockwise"></i> Actualizar
                </asp:LinkButton>
            </div>
        </div>
        <!-- Grilla -->
        <div class="row justify-content-center">
            <div class="col-xl-10 mt-3 mb-4">
                <asp:GridView
                    ID="gvSecciones"
                    ClientIDMode="Static"
                    CssClass="table table-striped table-hover"
                    AutoGenerateColumns="false"
                    DataKeyNames="Id"
                    OnSelectedIndexChanged="gvSecciones_SelectedIndexChanged"
                    OnPageIndexChanging="gvSecciones_PageIndexChanging"
                    AllowPaging="true" PageSize="5"
                    runat="server">
                    <Columns>
                        <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                        <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
                        <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍️" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Formulario modal modificar sección -->
        <div class="modal fade" id="frmModalSeccion" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-body-tertiary">
                        <div class="d-flex flex-column flex-grow-1 me-2">
                            <h1 class="modal-title fs-5">
                                <i class="bi bi-ui-radios me-1"></i>
                                <asp:Label ID="lblTituloModalSeccion" ClientIDMode="Static" Text="Modificar sección" runat="server"></asp:Label>
                            </h1>
                            <small class="mb-1 text-secondary" id="lblDescripcionModalSeccion">Modifique datos de la sección para clasificar instrumentos</small>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <asp:Panel ID="pnlModalSeccion" DefaultButton="btnAceptarModificarSeccion" runat="server">
                        <div class="modal-body">
                            <!-- Form -->
                            <div class="row mb-2">
                                <label for="txtRango" class="col-form-label">Sección</label>
                                <div class="col">
                                    <asp:TextBox ID="txtSeccion" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                    <div class="invalid-feedback">Por favor, ingrese una sección.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-3">
                                    <div class="form-check form-switch ms-1">
                                        <input class="form-check-input" type="checkbox" clientidmode="Static" id="chkSeccionActivo" onclick="cambiarCampoSeccion(this)" runat="server">
                                        <label class="form-check-label" for="chkSeccionActivo" runat="server">Activo</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer bg-body-tertiary">
                            <asp:Button ID="btnCancelarModificarSeccion" CssClass="btn btn-secondary" Text="Cancelar" data-bs-dismiss="modal" runat="server" />
                            <asp:Button ID="btnAceptarModificarSeccion" ClientIDMode="Static" CssClass="btn btn-primary" Text="Aceptar" OnClientClick="return validarSeccionModificado(chkSeccionActivo);" OnClick="btnAceptarModificarSeccion_Click" runat="server" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <!-- Fin formulario modal modificar un sección -->

    </ContentTemplate>
</asp:UpdatePanel>
<script src="/Scripts/modalSeccion.js?v=<%= DateTime.Now.Ticks %>"></script>
<script src="/Scripts/validarModificarCASeccion.js?v=<%= DateTime.Now.Ticks %>"></script>
<script src="/Scripts/alertGeneral.js?v=<%= DateTime.Now.Ticks %>"></script>
</asp:Content>
