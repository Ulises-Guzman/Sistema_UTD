<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioUsuario.aspx.cs" Inherits="Sistema_UTD.Admin.FormularioUsuario" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Cabecera -->
    <div class="row justify-content-center">
        <div class="col-xl-10 mt-3 mb-1">
            <div class="p-3 bg-white shadow-sm rounded" data-bs-theme="ligth">
                <h4>
                    <i class="bi bi-ui-radios me-1"></i>
                    Modificar Usuario
                </h4>
                <small class="mb-1 text-secondary" id="lblDescripcion" runat="server">Modifique datos del usuario, activaciones y reestablesca contraseñas</small>
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
                        ¡Los datos de usuario han sido modificados!
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
                            <div class="p-3 mb-2 bg-white shadow-sm rounded" data-bs-theme="ligth">
                                <!-- Menu filtro avanzado -->
                                <% if (chkFiltro.Checked) %>
                                <% { %>
                                <div class="row mt-1 mb-2">
                                    <div class="col-md-3">
                                        <label class="form-label">Campo</label>
                                        <asp:DropDownList ID="ddlCampo" CssClass="form-select" runat="server">
                                            <asp:ListItem Text="Apellido" />
                                            <asp:ListItem Text="Nombre" />
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label class="form-label">Rol</label>
                                        <asp:DropDownList ID="ddlCriterioRol" CssClass="form-select" runat="server">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-6">
                                        <div>
                                            <label class="form-label">Estado</label>

                                        </div>
                                        <div class="d-flex align-items-center">
                                            <asp:RadioButton ID="rbTodos" ClientIDMode="Static" CssClass="form-check mb-0 me-2" GroupName="filtroEstado" runat="server" />
                                            <label class="form-check-label" for="rbTodos">Todos</label>
                                            <asp:RadioButton ID="rbActivo" ClientIDMode="Static" CssClass="form-check mb-0 me-2" GroupName="filtroEstado" runat="server" />
                                            <label class="form-check-label" for="rbActivo">Activo</label>
                                            <asp:RadioButton ID="rdInactivo" ClientIDMode="Static" CssClass="form-check mb-0 me-2" GroupName="filtroEstado" runat="server" />
                                            <label class="form-check-label" for="rdInactivo">Inactivo</label>
                                        </div>
                                    </div>
                                </div>
                                <% } %>

                                <!-- Buscador -->
                                <div class="row">
                                    <div class="col-md-4 mt-2">
                                        <asp:TextBox ID="txtBuscar" CssClass="form-control" placeholder="Buscar usuario..." runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 mt-2">
                                        <asp:Button ID="btnBuscar"
                                            CssClass="btn btn-primary"
                                            Text="Buscar"
                                            OnClick="btnBuscar_Click"
                                            runat="server" />
                                    </div>
                                </div>

                                <div class="d-flex align-items-center mt-2">
                                    <asp:CheckBox ID="chkFiltro" CssClass="form-check mb-0 me-2" AutoPostBack="true" OnCheckedChanged="chkFiltro_CheckedChanged" runat="server" />
                                    <label class="form-label mb-0">Filtro</label>
                                </div>

                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <!-- Grilla -->
            <div class="row justify-content-center">
                <div class="col-xl-10 mt-3 mb-4">
                    <h5 class="mb-3">Lista de Usuarios</h5>
                    <asp:GridView
                        ID="gvUsuarios"
                        CssClass="table table-striped table-hover"
                        AutoGenerateColumns="false"
                        DataKeyNames="Id"
                        OnSelectedIndexChanged="gvUsuarios_SelectedIndexChanged"
                        OnPageIndexChanging="gvUsuarios_PageIndexChanging"
                        AllowPaging="true" PageSize="5"
                        runat="server">
                        <Columns>
                            <asp:BoundField HeaderText="Usuario" DataField="NombUsuario" />
                            <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
                            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                            <asp:BoundField HeaderText="Rol" DataField="Rol" />
                            <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
                            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍️" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <!-- Formulario Modal Modificar Usuario -->
            <div class="modal fade" id="frmModalUsuario" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-body-tertiary">
                            <div class="d-flex flex-column flex-grow-1 me-2">
                                <h1 class="modal-title fs-5">
                                    <i class="bi bi-ui-radios me-1"></i>
                                    <asp:Label ID="lblTituloModalUsuario" ClientIDMode="Static" Text="Modificar Usuario" runat="server"></asp:Label>
                                </h1>
                                <small class="mb-1 text-secondary" id="lblDescripcionModalUsuario" clientidmode="Static" runat="server">Modifique datos del usuario, activaciones y reestablesca contraseñas</small>
                            </div>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <!-- Form -->
                            <div class="row mb-2">
                                <label for="txtUsuario" class="col-form-label">Usuario</label>
                                <div class="col">
                                    <asp:TextBox ID="txtUsuario" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                    <div class="invalid-feedback">Por favor, ingrese un nombre de usuario.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-3">
                                    <div class="form-check form-switch ms-1">
                                        <input class="form-check-input" type="checkbox" clientidmode="Static" id="chkUsuarioActivo" onclick="cambiarCampos(this)" runat="server">
                                        <label class="form-check-label" for="chkActivo" runat="server">Activo</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <label for="txtContrasenia" class="col-form-label">Contraseña</label>
                                <div class="col">
                                    <asp:TextBox ID="txtContrasenia" ClientIDMode="Static" CssClass="form-control" TextMode="Password" Enabled="false" runat="server"></asp:TextBox>
                                    <div class="invalid-feedback">Por favor, ingrese una contraseña.</div>
                                    <div class="valid-feedback">Ok</div>
                                </div>
                                <div class="col-3">
                                    <div class="form-check form-switch ms-1">
                                        <input class="form-check-input" type="checkbox" clientidmode="Static" id="chkCambioContrasenia" onclick="cambiarCampoContrasenia(this)" runat="server">
                                        <label class="form-check-label" for="chkActivo" runat="server">Cambio</label>
                                    </div>
                                </div>
                            </div>
                            <div class="mb-3">
                                <label for="ddlRol" class="col-form-label">Rol</label>
                                <asp:DropDownList ID="ddlRol" ClientIDMode="Static" CssClass="form-select" runat="server"></asp:DropDownList>
                                <div class="invalid-feedback">Por favor, seleccione un rol.</div>
                                <div class="valid-feedback">Ok</div>
                            </div>
                            <div class="mb-3">
                                <label for="txtApellido" class="col-form-label">Apellido</label>
                                <asp:TextBox ID="txtApellido" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                <div class="invalid-feedback">Por favor, ingrese un apellido.</div>
                                <div class="valid-feedback">Ok</div>
                            </div>
                            <div class="mb-3">
                                <label for="txtNombre" class="col-form-label">Nombre</label>
                                <asp:TextBox ID="txtNombre" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                <div class="invalid-feedback">Por favor, ingrese un nombre.</div>
                                <div class="valid-feedback">Ok</div>
                            </div>
                        </div>
                        <div class="modal-footer bg-body-tertiary">
                            <asp:Button ID="btnCancelar" CssClass="btn btn-secondary" Text="Cancelar" data-bs-dismiss="modal" runat="server" />
                            <asp:Button ID="btnAceptar" CssClass="btn btn-primary" Text="Aceptar" OnClientClick="return validar();" OnClick="btnAceptar_Click" runat="server" />
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="/Scripts/modalUsuario.js"></script>
    <script src="/Scripts/validarModificarUsuario.js"></script>
    <script src="/Scripts/alertUsuario.js"></script>
</asp:Content>
