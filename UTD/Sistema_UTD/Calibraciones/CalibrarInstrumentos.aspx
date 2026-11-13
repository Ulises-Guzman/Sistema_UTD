<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CalibrarInstrumentos.aspx.cs" Inherits="Sistema_UTD.Calibraciones.CalibrarInstrumentos" %>

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
                    Calibrar Instrumentos
                </h4>
                <small class="mb-1 text-secondary" id="lblDescripcion" runat="server">Seleccione el instrumento que quiera calibrar</small>
            </div>
        </div>
    </div>

    <!-- Panel Buscador -->
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

            <div class="row justify-content-center">
                <div class="col-xl-10 mt-1 mb-2">
                    <div class="p-3 bg-white shadow-sm rounded" data-bs-theme="ligth">
                        <!-- Buscador -->
                        <div class="row">
                            <div class="col-md-4">
                                <label for="txtBuscar" class="col-form-label">Buscador</label>
                                <asp:TextBox ID="txtBuscar" ClientIDMode="Static" CssClass="form-control" PlaceHolder="Número de serie..." AutoPostBack="true" OnTextChanged="txtBuscar_TextChanged" runat="server"></asp:TextBox>
                                <%--OnTextChanged="txtBuscar_TextChanged"--%>
                                <small class="mb-1 text-secondary" id="lblInstruccion" runat="server">Precione [Tab] para buscar </small>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Título y botón actualizar -->
            <div class="row justify-content-center">
                <div class="col-xl-10 mt-3 mb-2 d-flex justify-content-between align-items-center">
                    <h4 class="m-0">Lista de instrumentos a calibrar</h4>
                    <asp:LinkButton ID="lnkBtnActualizar" ClientIDMode="Static" CssClass="btn btn-success align-content-end" OnClick="lnkBtnActualizar_Click" runat="server">
                        <i class="bi bi-arrow-clockwise"></i> Actualizar
                    </asp:LinkButton>
                </div>
            </div>
            <!-- Grilla instrumentos a calibrar-->
            <div class="row justify-content-center">
                <div class="col-xl-10 mt-3 mb-4">
                    <asp:GridView
                        ID="gvNoHabilitados"
                        ClientIDMode="Static"
                        CssClass="table table-striped table-hover"
                        AutoGenerateColumns="false"
                        DataKeyNames="Id"
                        AllowPaging="true" PageSize="5"
                        EmptyDataText="No se encontraron coincidencias"
                        OnRowCommand="gvNoHabilitados_RowCommand"
                        OnPageIndexChanging="gvNoHabilitados_PageIndexChanging"
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
                            <%--<asp:TemplateField HeaderText="Activo">
                                <ItemTemplate>
                                    <span class='<%# Convert.ToBoolean(Eval("Activo")) ? "badge rounded-pill bg-success-subtle text-success-emphasis" : "badge rounded-pill bg-danger-subtle text-danger-emphasis" %>'>
                                        <%# Convert.ToBoolean(Eval("Activo")) ? "Activo" : "Inactivo" %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <%--<asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="🛠️" CssClass="text-decoration-none me-2" />--%>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CommandName="seleccion" CommandArgument='<%# Eval("Id") %>' Text="🛠️" ToolTip="Calibrar" CssClass="text-decoration-none me-2" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <hr />
                </div>
            </div>

            <div class="row justify-content-center">
                <div class="col-xl-10 mt-1 mb-2">
                    <div class="p-3 bg-white shadow-sm rounded" data-bs-theme="ligth">
                        <!-- Buscador -->
                        <div class="row">
                            <div class="col-md-4">
                                <label for="txtBuscar1" class="col-form-label">Buscador</label>
                                <asp:TextBox ID="txtBuscar1" ClientIDMode="Static" CssClass="form-control" PlaceHolder="Número de serie..." AutoPostBack="true" OnTextChanged="txtBuscar1_TextChanged" runat="server"></asp:TextBox>
                                <small class="mb-1 text-secondary" id="Small1" runat="server">Precione [Tab] para buscar </small>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Título y botón actualizar -->
            <div class="row justify-content-center">
                <div class="col-xl-10 mt-3 mb-2 d-flex justify-content-between align-items-center">
                    <h4 class="m-0">Lista de instrumentos calibrados</h4>
                    <asp:LinkButton ID="lnkBtnActualizar1" ClientIDMode="Static" CssClass="btn btn-success align-content-end" OnClick="lnkBtnActualizar1_Click" runat="server">
                        <i class="bi bi-arrow-clockwise"></i> Actualizar
                    </asp:LinkButton>
                </div>
            </div>
            <!-- Grilla instrumentos calibrados-->
            <div class="row justify-content-center">
                <div class="col-xl-10 mt-3 mb-4">
                    <asp:GridView
                        ID="gvHabilitados"
                        ClientIDMode="Static"
                        CssClass="table table-striped table-hover"
                        AutoGenerateColumns="false"
                        DataKeyNames="Id"
                        AllowPaging="true" PageSize="5"
                        EmptyDataText="No se encontraron coincidencias"
                        OnRowCommand="gvHabilitados_RowCommand"
                        OnPageIndexChanging="gvHabilitados_PageIndexChanging"
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
                            <asp:BoundField HeaderText="Fecha Habilitación" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" DataField="FechaHabilitacion" />
                            <asp:BoundField HeaderText="Período (meses)" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" DataField="CantidadMeses" />
                            <asp:BoundField HeaderText="Estado" DataField="EstadoDescripcion" />

                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <%--<!-- Botón original de Selección -->
                                    <asp:LinkButton runat="server" CommandName="Select" Text="🔍" ToolTip="Seleccionar" CssClass="text-decoration-none me-2" />--%>

                                    <!-- Nuevo botón para Imprimir Certificado -->
                                    <asp:LinkButton runat="server" CommandName="Imprimir" CommandArgument='<%# Eval("Id") %>' Text="🖨️" ToolTip="Imprimir certificado" CssClass="text-decoration-none" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<script src="/Scripts/validarNuevoInstrumento.js?v=<%= DateTime.Now.Ticks %>"></script>--%>
    <script src="/Scripts/alertGeneralNav.js?v=<%= DateTime.Now.Ticks %>"></script>
</asp:Content>
