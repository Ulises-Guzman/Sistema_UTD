<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Sistema_UTD.Admin.Usuarios" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
        <div class="col-xl-10 mt-3 mb-4">
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
            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-2">
                    <asp:LinkButton ID="lnkBtnCrearUsuario" ClientIDMode="Static" CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100" OnClick="lnkBtnCrearUsuario_Click" runat="server">
                        <i class="bi bi-person-add me-2"></i>
                        Crear usuario <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-2" data-bs-theme="light">
                    <asp:LinkButton ID="lnkBtnModificar" ClientIDMode="Static" CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100" OnClick="lnkBtnModificar_Click" runat="server">
                        <i class="bi bi-person-exclamation me-2"></i>
                        Modificar usuario <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

<%--            <div class="row justify-content-center text-center">
                <div class="col-lg-8 mb-3" data-bs-theme="light">
                    <asp:LinkButton ID="lnkBtnActDesactUsuario" ClientIDMode="Static" CssClass="p-3 btn btn-light bg-white btn-lg shadow text-start w-100" runat="server">
                        <i class="bi bi-person-dash me-2"></i>
                        Desactivar usuario <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>--%>

            <div class="row justify-content-center text-center">
                <div class="col-xl-10 mb-2" data-bs-theme="light">
                    <asp:LinkButton ID="lnkBtnBuscarUsuario" ClientIDMode="Static" CssClass="p-3 btn btn-light bg-white btn-lg shadow-sm text-start w-100" runat="server">
                        <i class="bi bi-search me-2"></i>
                        Buscar usuario <span class="float-end">></span>
                    </asp:LinkButton>
                </div>
            </div>

            


            <!-- Formulario Modal Multi -->
            <div class="modal fade" id="frmModalABM" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="d-flex flex-column flex-grow-1 me-2">
                                <h1 class="modal-title fs-5">
                                    <i class="bi bi-ui-radios me-1"></i>
                                    <asp:Label ID="lblTituloModalABM" ClientIDMode="Static" runat="server"></asp:Label>
                                </h1>
                                <small class="mb-1 text-secondary" id="lblDescripcionModalABM" runat="server"></small>
                            </div>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <!-- Form -->
                            <div class="mb-3">
                                <label for="txtUsuario" class="col-form-label">Usuario</label>
                                <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="txtContrasenia" class="col-form-label">Contraseña</label>
                                <asp:TextBox ID="txtContrasenia" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="ddlRol" class="col-form-label">Rol</label>
                                <asp:DropDownList ID="ddlRol" CssClass="form-select" runat="server"></asp:DropDownList>
                            </div>
                            <div class="mb-3">
                                <label for="txtApellido" class="col-form-label">Apellido</label>
                                <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="txtNombre" class="col-form-label">Nombre</label>
                                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            <button type="button" class="btn btn-primary">Aceptar</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
