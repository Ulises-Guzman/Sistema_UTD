<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioInstrumento.aspx.cs" Inherits="Sistema_UTD.GestionInstrumentos.FormularioInstrumento" %>

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
                    Instrumentos Recibidos
                </h4>
                <small class="mb-1 text-secondary" id="lblDescripcion" runat="server">Agregue, modifique o desactive nuevos modelos de instrumentos</small>
            </div>
        </div>
    </div>


    <!-- Formulario agregar modelo -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- Alerta de notificación -->
            <div class="row justify-content-center">
                <div class="col-xl-10 mb-5">
                    <div id="alertaSatisfactoria" class="alert alert-success fade show" style="display: none" role="alert" runat="server">
                        ¡Los datos del modelo de instrumento han sido modificados!
                    </div>

                    <div id="alertaNoSatisfactoria" class="alert alert-danger fade show" style="display: none" role="alert" runat="server">
                    </div>
                </div>
            </div>
            <div class="row justify-content-center">
                <div class="col-xl-10 mt-3 mb-1">
                    <div class="p-4 bg-white shadow-sm rounded" data-bs-theme="ligth">
                        <div class="row mb-4">
                            <div class="col">
                                <h4>
                                    <i class="bi bi-card-list me-1"></i>
                                    Modificar Instrumento Recibido
                                </h4>
                            </div>
                            <small class="mb-1 text-secondary" id="Small1" runat="server">Modifique un instrumento recibido y activaciones</small>
                        </div>
                        <asp:Panel ID="pnlModeloInstrumentoModificar" DefaultButton="btnModificar" runat="server">
                            <!-- Form Layout -->
                            <div class="row g-3">
                                <div class="col-md-5">
                                    <label for="txtNumSerie" class="form-label">Número de serie</label>
                                    <asp:TextBox CssClass="form-control" ClientIDMode="Static" ID="txtNumSerie" runat="server"></asp:TextBox>
                                    <div class="invalid-feedback">Por favor, seleccione una opción.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-md-3">
                                    <label for="txtFechaRecepcion" class="form-label">Fecha de recepción</label>
                                    <asp:TextBox CssClass="form-control" ClientIDMode="Static" ID="txtFechaRecepcion" TextMode="Date" runat="server"></asp:TextBox>
                                    <div class="invalid-feedback">Por favor, seleccione una opción.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-md-3">
                                    <label for="ddlEstado" class="form-label">Estado</label>
                                    <asp:DropDownList CssClass="form-select" ClientIDMode="Static" ID="ddlEstado" runat="server"></asp:DropDownList>
                                    <div class="invalid-feedback">Por favor, seleccione una opción.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-1"></div>
                                <div class="col-md-6">
                                    <label for="ddlDestino" class="form-label">Destino</label>
                                    <asp:DropDownList CssClass="form-select" ClientIDMode="Static" ID="ddlDestino" runat="server"></asp:DropDownList>
                                    <div class="invalid-feedback">Por favor, seleccione una opción.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-md-5">
                                    <label for="ddlSeccion" class="form-label">Sección</label>
                                    <asp:DropDownList CssClass="form-select" ClientIDMode="Static" ID="ddlSeccion" runat="server"></asp:DropDownList>
                                    <div class="invalid-feedback">Por favor, seleccione una opción.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-1"></div>
                                <div class="col-md-4">
                                    <label for="ddlUsuarioCliente" class="form-label">Usuario cliente</label>
                                    <asp:DropDownList CssClass="form-select" ClientIDMode="Static" ID="ddlUsuarioCliente" runat="server"></asp:DropDownList>
                                    <div class="invalid-feedback">Por favor, seleccione una opción.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-8"></div>
                                <div class="cool-12">
                                    <div class="form-check">
                                        <input class="form-check-input" type="checkbox" value="" clientidmode="Static" id="chkPatronPrimario" runat="server">
                                        <label class="form-check-label" for="chkPatronPrimario">
                                            Patrón primario
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-12 mt-2">
                                        <div class="form-check form-switch ms-1">
                                            <input class="form-check-input" type="checkbox" clientidmode="Static" id="chkInstrumentoActivo" onclick="cambiarCamposEstado(this)" runat="server">
                                            <label class="form-check-label" for="chkInstrumentoActivo" runat="server">Activo</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:Button ID="btnModificar" CssClass="btn btn-primary" ClientIDMode="Static" OnClientClick="return validarModificadoInstrumento()" OnClick="btnModificar_Click" Text="Modificar" runat="server" />
                                    <asp:Button ID="btnVolver" CssClass="btn btn-secondary" ClientIDMode="Static" Text="&larr; Volver" OnClick="btnVolver_Click" runat="server" />
                                    <%--OnClientClick="return validarNuevoInstrumento()"--%>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="/Scripts/validarModificarInstrumento.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/alertGeneralNav.js?v=<%= DateTime.Now.Ticks %>"></script>
</asp:Content>
