<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Sistema_UTD.Admin.Usuarios" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Cabecera -->
    <div class="row justify-content-center">
        <div class="col-xl-10 mt-3 mb-1">
            <div class="p-3 bg-white shadow-sm rounded" data-bs-theme="ligth">
                <h4>
                    <i class="bi bi-person-lines-fill me-1"></i>
                    Gestión de Usuarios
                </h4>
                <small class="mb-1 text-secondary">Cree usuarios, modifique datos y activaciones, realice búsquedas con listados, restablecimiento de contraseñas</small>
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
                        ¡El usuario ha sido creado!
                    </div>

                    <%--<div id="alertaNoSatisfactoria" class="alert alert-danger alert-dismissible fade show" role="alert" visible="false" runat="server">
                        El usuario No ha sido creado!
                    </div>--%>
                </div>
            </div>

            <!-- Panel de tarjetas -->
            <div class="row justify-content-center">
                <div class="col-xl-10">
                    <div class="row">
                        <div class="col-lg-3 mb-5">
                            <div class="card border-light shadow-sm">
                                <div class="card-body d-flex flex-column">
                                    <h6 class="card-title"><i class="bi bi-pen me-2 fs-5"></i>Administradores</h6>
                                    <asp:Label ID="lblNumAdministrador" ClientIDMode="Static" CssClass="card-text ms-auto fs-1" Text="0" runat="server"></asp:Label>
                                    <small class="text-secondary mt-auto">Activo</small>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 mb-5">
                            <div class="card border-light shadow-sm">
                                <div class="card-body d-flex flex-column">
                                    <h6 class="card-title"><i class="bi bi-wrench me-2 fs-5"></i>Técnicos</h6>
                                    <asp:Label ID="lblNumTecnico" ClientIDMode="Static" CssClass="card-text ms-auto fs-1" Text="0" runat="server"></asp:Label>
                                    <small class="text-secondary mt-auto">Activo</small>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 mb-5">
                            <div class="card border-light shadow-sm">
                                <div class="card-body d-flex flex-column">
                                    <h6 class="card-title"><i class="bi bi-person-standing me-2 fs-5"></i>Clientes</h6>
                                    <asp:Label ID="lblNumCliente" ClientIDMode="Static" CssClass="card-text ms-auto fs-1" Text="0" runat="server"></asp:Label>
                                    <small class="text-secondary mt-auto">Activo</small>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Panel de botones -->
            <div class="row justify-content-center text-center">
                <div class="col-xl-10 text-start mb-1">
                    <div class="p-3 bg-white shadow-sm rounded-top" data-bs-theme="ligth">
                        <h5>
                            Usuarios
                        </h5>
                    </div>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-1" data-bs-theme="light">
                    <asp:LinkButton ID="lnkBtnCrearUsuario"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100 rounded-0"
                        OnClick="lnkBtnCrearUsuario_Click"
                        runat="server">
                        <i class="bi bi-person-add me-2 icon-green"></i>
                        Crear usuario <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-4" data-bs-theme="light">
                    <asp:LinkButton ID="lnkBtnModificarUsuario"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100 rounded-0 rounded-bottom"
                        OnClick="lnkBtnModificarUsuario_Click"
                        runat="server">
                        <i class="bi bi-person-exclamation me-2 icon-orange"></i>
                        Modificar usuario <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <%--<div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-2" data-bs-theme="light">
                    <asp:LinkButton ID="lnkBtnBuscarUsuario"
                        ClientIDMode="Static"
                        CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100"
                        runat="server">
                        <i class="bi bi-search me-2 text-primary fs-4"></i>
                        Buscar usuario <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>--%>

            <!-- Formulario Modal Crear Usuario -->
            <div class="modal fade" id="frmModalUsuario" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-body-tertiary">
                            <div class="d-flex flex-column flex-grow-1 me-2">
                                <h1 class="modal-title fs-5">
                                    <i class="bi bi-ui-radios me-1"></i>
                                    <asp:Label ID="lblTituloModalUsuario" ClientIDMode="Static" runat="server"></asp:Label>
                                </h1>
                                <small class="mb-1 text-secondary" id="lblDescripcionModalUsuario" clientidmode="Static" runat="server"></small>
                            </div>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <!-- Form -->
                            <div class="mb-3">
                                <label for="txtUsuario" class="col-form-label">Usuario</label>
                                <asp:TextBox ID="txtUsuario" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                <div class="invalid-feedback">Por favor, ingrese un nombre de usuario.</div>
                                <div class="valid-feedback">Ok</div>
                            </div>
                            <div class="mb-3">
                                <label for="txtContrasenia" class="col-form-label">Contraseña</label>
                                <asp:TextBox ID="txtContrasenia" ClientIDMode="Static" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                                <div class="invalid-feedback">Por favor, ingrese una contraseña.</div>
                                <div class="valid-feedback">Ok</div>
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
                            <asp:Button ID="btnAceptar" CssClass="btn btn-primary" Text="Aceptar" OnClientClick="return validarUsuarioNuevo();" OnClick="btnAceptar_Click" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="/Scripts/modalUsuario.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/validarNuevoUsuario.js?v=<%= DateTime.Now.Ticks %>"></script>
    <script src="/Scripts/alertUsuario.js?v=<%= DateTime.Now.Ticks %>"></script>
</asp:Content>
