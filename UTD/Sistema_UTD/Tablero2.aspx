<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tablero2.aspx.cs" Inherits="Sistema_UTD.Tablero_2" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center mt-3">
        <div class="col-xl-10">
            <div class="row g-3">

                <!-- Tarjeta Vencidos -->
                <div class="col-md-4">
                    <div class="card border-0 border-start border-danger border-4 shadow-sm rounded p-2 h-100">
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center mb-2">
                                <h6 class="text-danger fw-bold text-uppercase mb-0">Vencidos</h6>
                                <div class="bg-danger bg-opacity-10 text-danger rounded-circle p-2">
                                    <i class="bi bi-x-octagon fs-4"></i>
                                </div>
                            </div>
                            <h2 class="mb-1 text-dark">
                                <asp:Label ID="lblVencidosMin" runat="server" Text="12"></asp:Label>
                            </h2>
                            <small class="text-secondary">Acción requerida</small>
                        </div>
                    </div>
                </div>

                <!-- Tarjeta Por Vencer -->
                <div class="col-md-4">
                    <div class="card border-0 border-start border-warning border-4 shadow-sm rounded p-2 h-100">
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center mb-2">
                                <h6 class="text-warning fw-bold text-uppercase mb-0">Por Vencer</h6>
                                <div class="bg-warning bg-opacity-10 text-warning rounded-circle p-2">
                                    <i class="bi bi-exclamation-circle fs-4"></i>
                                </div>
                            </div>
                            <h2 class="mb-1 text-dark">
                                <asp:Label ID="lblPorVencerMin" runat="server" Text="8"></asp:Label>
                            </h2>
                            <small class="text-secondary">< 3 meses</small>
                        </div>
                    </div>
                </div>

                <!-- Tarjeta En Tiempo -->
                <div class="col-md-4">
                    <div class="card border-0 border-start border-success border-4 shadow-sm rounded p-2 h-100">
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center mb-2">
                                <h6 class="text-success fw-bold text-uppercase mb-0">En Tiempo</h6>
                                <div class="bg-success bg-opacity-10 text-success rounded-circle p-2">
                                    <i class="bi bi-shield-check fs-4"></i>
                                </div>
                            </div>
                            <h2 class="mb-1 text-dark">
                                <asp:Label ID="lblEnTiempoMin" runat="server" Text="145"></asp:Label>
                            </h2>
                            <small class="text-secondary">> 3 meses</small>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
