<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioModeloInstrumento.aspx.cs" Inherits="Sistema_UTD.GestionInstrumentos.FormularioModeloInstrumento" %>

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
                    Modelos de instrumentos
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
                                    Modificar Modelo de Instrumento
                                </h4>
                            </div>
                            <small class="mb-1 text-secondary" id="Small1" runat="server">Modifique un modelo de instrumento y activaciones</small>
                        </div>
                        <asp:Panel ID="pnlModeloInstrumentoModificar" DefaultButton="btnModificar" runat="server">
                            <!-- Form Layout -->
                            <div class="row g-3">
                                <div class="col-md-6">
                                    <label for="lblNumeroParte" class="form-label">Número de parte</label>
                                    <asp:TextBox CssClass="form-control" ClientIDMode="Static" ID="txtNumeroParte" runat="server"></asp:TextBox>
                                    <div class="invalid-feedback">Por favor, ingrese un numero de parte.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtNomenclatura" class="form-label">Nomenclatura</label>
                                    <asp:TextBox CssClass="form-control" ClientIDMode="Static" ID="txtNomenclatura" runat="server"></asp:TextBox>
                                    <div class="invalid-feedback">Por favor, ingrese una nomenclatura.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtMarca" class="form-label">Marca</label>
                                    <asp:TextBox CssClass="form-control" ClientIDMode="Static" ID="txtMarca" runat="server"></asp:TextBox>
                                    <div class="invalid-feedback">Por favor, ingrese una marca.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-md-3">
                                    <label for="ddlModelo" class="form-label">Tipo de modelo</label>
                                    <asp:DropDownList CssClass="form-select" ClientIDMode="Static" ID="ddlModelo" runat="server"></asp:DropDownList>
                                    <div class="invalid-feedback">Por favor, seleccione una opción.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-md-3"></div>
                                <div class="col-md-2">
                                    <label for="ddlRango" class="form-label">Rango</label>
                                    <asp:DropDownList CssClass="form-select" ClientIDMode="Static" ID="ddlRango" runat="server"></asp:DropDownList>
                                    <div class="invalid-feedback">Por favor, seleccione una opción.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-md-2">
                                    <label for="ddlUnidadMedida" class="form-label">Unidad de medida</label>
                                    <asp:DropDownList CssClass="form-select" ClientIDMode="Static" ID="ddlUnidadMedida" runat="server"></asp:DropDownList>
                                    <div class="invalid-feedback">Por favor, seleccione una opción.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-md-8"></div>
                                <div class="col-md-12">
                                    <div class="col-12 mt-2">
                                        <div class="form-check form-switch ms-1">
                                            <input class="form-check-input" type="checkbox" clientidmode="Static" id="chkModeloInstrumentoActivo" onclick="cambiarCamposEstado(this)" runat="server">
                                            <label class="form-check-label" for="chkModeloInstrumentoActivo" runat="server">Activo</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 mt-4">
                                    <asp:Button ID="btnModificar" CssClass="btn btn-primary me-3" ClientIDMode="Static" Text="Modificar" OnClientClick="return validarModeloInstrumentoModificado(chkModeloInstrumentoActivo);" OnClick="btnModificar_Click" runat="server" />
                                    <asp:Button ID="btnVolver" CssClass="btn btn-secondary" ClientIDMode="Static" Text="&larr; Volver" OnClick="btnVolver_Click" runat="server" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="/Scripts/validarModificarModeloInstrumento.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/alertGeneralNav.js?v=<%= DateTime.Now.Ticks %>"></script>
</asp:Content>
