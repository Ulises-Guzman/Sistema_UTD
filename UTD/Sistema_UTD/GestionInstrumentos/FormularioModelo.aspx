<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioModelo.aspx.cs" Inherits="Sistema_UTD.GestionInstrumentos.Modelos" %>

<%@ MasterType VirtualPath="~/Site.Master"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Cabecera -->
    <div class="row justify-content-center">
        <div class="col-xl-10 mt-3 mb-1">
            <div class="p-3 bg-white shadow-sm rounded" data-bs-theme="ligth">
                <h4>
                    <i class="bi bi-ui-radios me-1"></i>
                    Modificar Modelo
                </h4>
                <small class="mb-1 text-secondary" id="lblDescripcion" runat="server">Modifique datos del modelo y activaciones</small>
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
                        ¡Los datos del modelo han sido modificados!
                    </div>

                    <%--<div id="alertaNoSatisfactoria" class="alert alert-danger alert-dismissible fade show" role="alert" visible="false" runat="server">
                     El usuario No ha sido creado!
                 </div>--%>
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
                                        <asp:TextBox ID="txtBuscar" ClientIDMode="Static" CssClass="form-control" placeholder="Buscar usuario..." AutoPostBack="true" runat="server"></asp:TextBox>
                                        <%--OnTextChanged="txtBuscar_TextChanged"--%>
                                    </div>
                                </div>

                                <%--<div class="d-flex align-items-center mt-4 mb-2">
                                    <asp:CheckBox ID="chkFiltro" ClientIDMode="Static" CssClass="form-check mb-0 me-2" AutoPostBack="true" OnCheckedChanged="chkFiltro_CheckedChanged" runat="server" />
                                    <label class="form-label mb-0">Filtro Avazado</label>
                                </div>--%>

                                <%--<!-- Menu filtro avanzado -->
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
                                                OnClientClick="return validarUsuarioBuscado();"
                                                OnClick="btnBuscarAvanzado_Click"
                                                runat="server" />
                                        </div>
                                    </div>
                                </asp:Panel>--%>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <!-- Título y botón actualizar -->
            <div class="row justify-content-center">
                <div class="col-xl-10 mt-3 mb-2 d-flex justify-content-between align-items-center">
                    <h4 class="m-0">Lista de Modelos</h4>
                    <asp:LinkButton ID="lnkBtnActualizar" ClientIDMode="Static" CssClass="btn btn-success align-content-end" runat="server">
                    <%--OnClick="lnkBtnActualizar_Click"--%>
                     <i class="bi bi-arrow-clockwise"></i> Actualizar
                    </asp:LinkButton>
                </div>
            </div>
            <!-- Grilla -->
            <div class="row justify-content-center">
                <div class="col-xl-10 mt-3 mb-4">
                    <asp:GridView
                        ID="gvModelos"
                        ClientIDMode="Static"
                        CssClass="table table-striped table-hover"
                        AutoGenerateColumns="false"
                        DataKeyNames="Id"
                        
                        AllowPaging="true" PageSize="5"
                        runat="server">
                        <Columns>
                            <asp:BoundField HeaderText="Modelo" DataField="Descripcion" />
                            <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
                            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍️" />
                        </Columns>
                    </asp:GridView>
                    <%--OnSelectedIndexChanged="gvUsuarios_SelectedIndexChanged"
                        OnPageIndexChanging="gvUsuarios_PageIndexChanging"--%>
                </div>
            </div>

            <!-- Formulario Modal Modificar Modelo -->
            <div class="modal fade" id="frmModalModelo" tabindex="-1" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-body-tertiary">
                            <div class="d-flex flex-column flex-grow-1 me-2">
                                <h1 class="modal-title fs-5">
                                    <i class="bi bi-ui-radios me-1"></i>
                                    <asp:Label ID="lblTituloModalModelo" ClientIDMode="Static" Text="Modificar Modelo" runat="server"></asp:Label>
                                </h1>
                                <small class="mb-1 text-secondary" id="lblDescripcionModalModelo" clientidmode="Static" runat="server">Modifique datos del modelo y activaciones</small>
                            </div>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <!-- Form -->
                            <div class="row mb-2">
                                <label for="txtModelo" class="col-form-label">Modelo</label>
                                <div class="col">
                                    <asp:TextBox ID="txtModelo" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                    <div class="invalid-feedback">Por favor, ingrese el modelo.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-3">
                                    <div class="form-check form-switch ms-1">
                                        <input class="form-check-input" type="checkbox" clientidmode="Static" id="chkModeloActivo" onclick="cambiarCamposModificar(this)" runat="server">
                                        <label class="form-check-label" for="chkUsuarioActivo" runat="server">Activo</label>
                                    </div>
                                </div>
                            </div>
                            
                        <div class="modal-footer bg-body-tertiary">
                            <asp:Button ID="btnCancelar" CssClass="btn btn-secondary" Text="Cancelar" data-bs-dismiss="modal" runat="server" />
                            <asp:Button ID="btnAceptar" CssClass="btn btn-primary" Text="Aceptar" OnClientClick="return validarUsuarioModificado(chkModeloActivo);" runat="server" />
                            <%--OnClick="btnAceptar_Click"--%>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<script src="/Scripts/modalUsuario.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/validarModificarUsuario.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/validarBuscarAvanzado.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/alertUsuario.js?v=<%= DateTime.Now.Ticks %>"></script>--%>
</asp:Content>
