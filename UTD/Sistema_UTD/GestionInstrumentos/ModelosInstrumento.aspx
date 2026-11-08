<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModelosInstrumento.aspx.cs" Inherits="Sistema_UTD.GestionInstrumentos.ModeloInstrumento" %>

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
                        ¡El nuevo modelo de instruemnto ha sido creado!
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
                                    Agregar Modelo de Instrumento
                                </h4>
                            </div>
                            <small class="mb-1 text-secondary" id="Small1" runat="server">Agregar nuevo modelo de instrumento</small>
                        </div>
                        <!-- Form Layout -->
                        <asp:Panel ID="pnlModeloInstrumentoAgregar" DefaultButton="btnAgregar" runat="server">
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
                                <div class="col-12">
                                    <asp:Button ID="btnAgregar" CssClass="btn btn-primary" ClientIDMode="Static" Text="Agregar"
                                        OnClientClick="return validarNuevoModeloInstrumento()" OnClick="btnAgregar_Click" runat="server" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<div class="row justify-content-center">
        <div class="col-xl-10 mt-3 mb-3">
        </div>
    </div>--%>
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
                                <asp:TextBox ID="txtBuscar" ClientIDMode="Static" CssClass="form-control" PlaceHolder="Nomenclatura..." AutoPostBack="true" OnTextChanged="txtBuscar_TextChanged" runat="server"></asp:TextBox>
                                <small class="mb-1 text-secondary" id="lblInstruccion" runat="server">Precione [Tab] para buscar </small>
                            </div>
                        </div>

                        <%-- <div class="d-flex align-items-center mt-4 mb-2">
                            <asp:CheckBox ID="chkFiltro" ClientIDMode="Static" CssClass="form-check mb-0 me-2" AutoPostBack="true" runat="server" />
                            %--OnCheckedChanged="chkFiltro_CheckedChanged"--%
                            <label class="form-label mb-0">Filtro Avazado</label>
                        </div>--%>

                        <%--                        <!-- Menu filtro avanzado -->
                        <asp:Panel ID="pnlFiltroAvanzado" Visible="false" runat="server">
                            <div class="row mt-3 mb-4">
                                <div class="col-md-4">
                                    <label for="ddlCriterioCampo" class="form-label">Campo</label>
                                    <asp:DropDownList ID="ddlCriterioCampo" ClientIDMode="Static" CssClass="form-select" runat="server"></asp:DropDownList>
                                </div>

                                <div class="col-md-4">
                                    <label for="ddlCriterioRol" class="form-label">Rol</label>
                                    <asp:DropDownList ID="ddlCriterioRol" ClientIDMode="Static" CssClass="form-select" runat="server">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-4">
                                    <label for="ddlCriterioEstado" class="form-label">Estado</label>
                                    <asp:DropDownList ID="ddlCriterioEstado" ClientIDMode="Static" CssClass="form-select" runat="server"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="row mt-2 mb-3">
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtBuscarAvanzado" ClientIDMode="Static" CssClass="form-control" placeholder="Ingrese palabra clave..." runat="server"></asp:TextBox>
                                    <div class="invalid-feedback">Por favor, ingrese una palabra clave.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="btnBuscarAvanzado"
                                        ClientIDMode="Static"
                                        CssClass="btn btn-primary"
                                        Text="Buscar"
                                        runat="server" />
                                        %--OnClientClick="return validarUsuarioBuscado();"--%
                                        %--OnClick="btnBuscarAvanzado_Click"--%
                                </div>
                            </div>
                        </asp:Panel> --%>
                    </div>
                </div>
            </div>

            <!-- Título y botón actualizar -->
            <div class="row justify-content-center">
                <div class="col-xl-10 mt-3 mb-2 d-flex justify-content-between align-items-center">
                    <h4 class="m-0">Lista Modelos de Instrumento</h4>
                    <asp:LinkButton ID="lnkBtnActualizar" ClientIDMode="Static" CssClass="btn btn-success align-content-end" OnClick="lnkBtnActualizar_Click" runat="server">
            <i class="bi bi-arrow-clockwise"></i> Actualizar
                    </asp:LinkButton>
                </div>
            </div>
            <!-- Grilla -->
            <div class="row justify-content-center">
                <div class="col-xl-10 mt-3 mb-4">
                    <asp:GridView
                        ID="gvModelosInstrumento"
                        ClientIDMode="Static"
                        CssClass="table table-striped table-hover"
                        AutoGenerateColumns="false"
                        DataKeyNames="Id"
                        AllowPaging="true" PageSize="5"
                        EmptyDataText="No se encontraron coincidencias"
                        OnSelectedIndexChanged="gvModelosInstrumento_SelectedIndexChanged"
                        OnPageIndexChanging="gvModelosInstrumento_PageIndexChanging"
                        runat="server">
                        <Columns>
                            <asp:BoundField HeaderText="Número de Parte" DataField="NumParte" />
                            <asp:BoundField HeaderText="Nomenclatura" DataField="Nomenclatura" />
                            <asp:BoundField HeaderText="Marca" DataField="Marca" />
                            <asp:BoundField HeaderText="Modelo" DataField="ModeloDescripcion" />
                            <asp:BoundField HeaderText="Rango" DataField="RangoDescripcion" />
                            <asp:BoundField HeaderText="Unidad" DataField="UnidadMedidaDescripcion" />
                            <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
                            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍️" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="/Scripts/validarNuevoModeloInstrumento.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/alertGeneralNav.js?v=<%= DateTime.Now.Ticks %>"></script>
</asp:Content>
