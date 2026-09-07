<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Sistema_UTD.Account.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="mb-5 text-center"><i class="bi bi-nut-fill me-3"></i>Sistema UTD</h1>

    <!-- Trajeta de login -->
    <div class="card shadow-lg bg-white rounded border-0" style="width: 400px;">
        <div class="card-body p-4 text-center">

            <!-- Título tarjeta -->
            <h3 class="mb-2">Iniciar Sesión</h3>

            <div class="mb-3 text-start">
                <label for="txtUsuario" class="form-label">Usuario</label>
                <asp:TextBox ID="txtUsuario" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="mb-4 text-start">
                <label for="txtContrasenia" class="form-label">Contraseña</label>
                <asp:TextBox ID="txtContrasenia" ClientIDMode="Static" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
            </div>

            <asp:Button ID="btnIngresar" Text="Ingresar" CssClass="btn btn-primary w-100" OnClick="btnIngresar_Click" runat="server" />
        
        </div>
    </div>
</asp:Content>
