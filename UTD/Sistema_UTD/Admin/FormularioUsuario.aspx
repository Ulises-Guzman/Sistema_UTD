<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioUsuario.aspx.cs" Inherits="Sistema_UTD.Admin.FormularioUsuario" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
        <div class="col-xl-10 mt-3 mb-4">
            <div class="p-3 mb-2 bg-white shadow-sm rounded" data-bs-theme="ligth">
                <h4>
                    <i class="bi bi-ui-radios me-1"></i>
                    <asp:Literal ID="tituloFormulario" ClientIDMode="Static" Text=" Usuario" runat="server" />
                </h4>
                <small class="mb-1 text-secondary" id="lblDescripcion" runat="server"></small>
            </div>
        </div>
    </div>

    <!-- Panel Buscador -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row justify-content-center">
                <div class="col-xl-10 mb-4">
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
                                    <asp:ListItem Text="> Datos desde DB" />
                                    <asp:ListItem Text="> Datos desde DB" />
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
                                <asp:Button ID="btnBuscar" CssClass="btn btn-primary" runat="server" Text="Buscar" />
                            </div>
                        </div>

                        <div class="d-flex align-items-center mt-2">
                            <asp:CheckBox ID="chkFiltro" CssClass="form-check mb-0 me-2" AutoPostBack="true" runat="server" />
                            <label class="form-label mb-0">Filtro</label>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:Panel ID="pnlGrilla" Visible="false" runat="server">
        <!-- Grilla -->
        <div class="row">
            <div calss="col">
                <h1>GridView_ModificarUsuarios</h1>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView
                            ID="gvUsuarios"
                            Visible="false"
                            CccClass="table table-striped table-hover"
                            AutoGenerateColumns="false"
                            DataKeyNames="Id"
                            AllowPaging="true" PageSize="5"
                            runat="server">
                            <Columns>
                                <asp:BoundField HeaderText="Usuario" DataField="NombUsuario" />
                                <asp:BoundField HeaderText="Contraseña" DataField="Contrasenia" />
                                <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
                                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                                <asp:BoundField HeaderText="Rol" DataField="Rol" />
                                <asp:BoundField HeaderText="Activo" DataField="Activo" />
                                <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍️" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
