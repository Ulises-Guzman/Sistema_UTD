<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Instrumento.aspx.cs" Inherits="Sistema_UTD.GestionInstrumentos.InstrumentoRecibido" %>

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
                <small class="mb-1 text-secondary" id="lblDescripcion" runat="server">Agregue, modifique o desactive nuevos instrumentos</small>
            </div>
        </div>
    </div>


    <!-- Formulario agregar instrumento -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- Alerta de notificación -->
            <div class="row justify-content-center">
                <div class="col-xl-10 mb-5">
                    <div id="alertaSatisfactoria" class="alert alert-success fade show" style="display: none" role="alert" runat="server">
                        ¡El nuevo instrumento ha sido creado!
                    </div>

                    <div id="alertaNoSatisfactoria" class="alert alert-danger fade show" style="display: none" role="alert" runat="server">
                    </div>
                </div>
            </div>

            <asp:Panel ID="pnlInstrumentoRegistrar" DefaultButton="btnRegistrar" runat="server">
                <div class="row justify-content-center">
                    <div class="col-xl-10 mt-3 mb-1">
                        <div class="p-4 bg-white shadow-sm rounded" data-bs-theme="ligth">
                            <%--<div class="row mb-4">
                                <div class="col">
                                    <h4>
                                        <i class="bi bi-card-list me-1"></i>
                                        Agregar Instrumento
                                    </h4>
                                </div>
                                <small class="mb-1 text-secondary" id="Small1" runat="server">Agregar nuevo instrumento</small>
                            </div>--%>
                            <!-- Form Layout -->

                            <div class="row g-3">
                                <div class="col-12">
                                    <h4>
                                        <i class="bi bi-1-circle"></i>
                                        Seleccionar Modelo de Instrumento
                                    </h4>
                                </div>
                                <!-- Sección modelo de instrumento-->
                                <div class="col-md-6">
                                    <label for="ddlNomenclatura" class="form-label">Nomenclatura modelo</label>
                                    <asp:DropDownList CssClass="form-select"
                                        ClientIDMode="Static" ID="ddlNomenclatura"
                                        OnSelectedIndexChanged="ddlNomenclatura_SelectedIndexChanged"
                                        AutoPostBack="true"
                                        runat="server">
                                    </asp:DropDownList>
                                    <div class="invalid-feedback">Por favor, ingrese una nomenclatura.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtMarca" class="form-label">Marca</label>
                                    <asp:TextBox CssClass="form-control" ClientIDMode="Static" ID="txtMarca" runat="server"></asp:TextBox>
                                    <div class="invalid-feedback">Por favor, seleccione una opción.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-md-3">
                                    <label for="txtModelo" class="form-label">Tipo de modelo</label>
                                    <asp:TextBox CssClass="form-control" ClientIDMode="Static" ID="txtModelo" runat="server"></asp:TextBox>
                                    <div class="invalid-feedback">Por favor, seleccione una opción.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-md-3"></div>
                                <div class="col-md-2">
                                    <label for="txtRango" class="form-label">Rango</label>
                                    <asp:TextBox CssClass="form-control" ClientIDMode="Static" ID="txtRango" runat="server"></asp:TextBox>
                                    <div class="invalid-feedback">Por favor, seleccione una opción.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-md-2">
                                    <label for="txtUnidadMedida" class="form-label">Unidad de medida</label>
                                    <asp:TextBox CssClass="form-control" ClientIDMode="Static" ID="txtUnidadMedida" runat="server"></asp:TextBox>
                                    <div class="invalid-feedback">Por favor, seleccione una opción.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-md-8"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Fin sección modelo de instrumento-->

                <!-- Sección instrumento-->
                <div class="row justify-content-center">
                    <div class="col-xl-10 mt-1 mb-1">
                        <div class="p-4 bg-white shadow-sm rounded" data-bs-theme="ligth">
                            <div class="row g-3">
                                <div class="col-12">
                                    <h4 class="mt-3">
                                        <i class="bi bi-2-circle"></i>
                                        Registrar Instrumento Recibido
                                    </h4>
                                </div>
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
                                <div class="col-12">
                                    <asp:Button ID="btnRegistrar" CssClass="btn btn-primary" ClientIDMode="Static" OnClientClick="return validarNuevoInstrumento()" OnClick="btnRegistrar_Click" Text="Registrar"
                                        runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </asp:Panel>

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
                <div class="col-xl-10 mt-1 mb-2">
                    <div class="p-3 bg-white shadow-sm rounded" data-bs-theme="ligth">
                        <!-- Buscador -->
                        <div class="row">
                            <div class="col-md-4">
                                <label for="txtBuscar" class="col-form-label">Buscador</label>
                                <asp:TextBox ID="txtBuscar" ClientIDMode="Static" CssClass="form-control" PlaceHolder="Número de serie..." AutoPostBack="true" OnTextChanged="txtBuscar_TextChanged" runat="server"></asp:TextBox>
                                <small class="mb-1 text-secondary" id="lblInstruccion" runat="server">Precione [Tab] para buscar </small>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Título y botón actualizar -->
            <div class="row justify-content-center">
                <div class="col-xl-10 mt-3 mb-2 d-flex justify-content-between align-items-center">
                    <h4 class="m-0">Lista de Instrumentos Recibidos</h4>
                    <asp:LinkButton ID="lnkBtnActualizar" ClientIDMode="Static" CssClass="btn btn-success align-content-end" OnClick="lnkBtnActualizar_Click" runat="server">
         <i class="bi bi-arrow-clockwise"></i> Actualizar
                    </asp:LinkButton>
                </div>
            </div>
            <!-- Grilla -->
            <div class="row justify-content-center">
                <div class="col-xl-10 mt-3 mb-4">
                    <asp:GridView
                        ID="gvInstrumentos"
                        ClientIDMode="Static"
                        CssClass="table table-striped table-hover"
                        AutoGenerateColumns="false"
                        DataKeyNames="Id"
                        AllowPaging="true" PageSize="5"
                        EmptyDataText="No se encontraron coincidencias"
                        OnSelectedIndexChanged="gvInstrumentos_SelectedIndexChanged"
                        OnPageIndexChanging="gvInstrumentos_PageIndexChanging"
                        runat="server">
                        <Columns>
                            <asp:BoundField HeaderText="Nomenclatura" DataField="Nomenclatura" />
                            <asp:BoundField HeaderText="Marca" DataField="Marca" />
                            <asp:BoundField HeaderText="Nº de serie" DataField="NumSerie" />
                            <asp:TemplateField HeaderText="Patrón">
                                <ItemTemplate>
                                    <span class='<%# Convert.ToBoolean(Eval("PatronPrimario")) ? "badge rounded-pill bg-success-subtle text-success-emphasis" : "badge rounded-pill bg-danger-subtle text-danger-emphasis" %>'>
                                        <%# Convert.ToBoolean(Eval("PatronPrimario")) ? "Si" : "No" %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Recepción" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" DataField="FechaRecepcion" />
                            <asp:BoundField HeaderText="Destino" DataField="Cuatrigrama" />
                            <asp:TemplateField HeaderText="Habilitado">
                                <ItemTemplate>
                                    <span class='<%# Convert.ToBoolean(Eval("Habilitacion")) ? "badge rounded-pill bg-success-subtle text-success-emphasis" : "badge rounded-pill bg-danger-subtle text-danger-emphasis" %>'>
                                        <%# Convert.ToBoolean(Eval("Habilitacion")) ? "Si" : "No" %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activo">
                                <ItemTemplate>
                                    <span class='<%# Convert.ToBoolean(Eval("Activo")) ? "badge rounded-pill bg-success-subtle text-success-emphasis" : "badge rounded-pill bg-danger-subtle text-danger-emphasis" %>'>
                                        <%# Convert.ToBoolean(Eval("Activo")) ? "Activo" : "Inactivo" %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍️"  />
                        </Columns>
                    </asp:GridView>
                    <%--OnSelectedIndexChanged="gvModelosInstrumento_SelectedIndexChanged"
                    OnPageIndexChanging="gvModelosInstrumento_PageIndexChanging"--%>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="/Scripts/validarNuevoInstrumento.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/alertGeneralNav.js?v=<%= DateTime.Now.Ticks %>"></script>
</asp:Content>
