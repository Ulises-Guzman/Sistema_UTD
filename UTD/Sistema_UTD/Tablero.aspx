<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tablero.aspx.cs" Inherits="Sistema_UTD.Scripts.Tablero" %>

<%@ MasterType VirtualPath="~/Site.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
    <div class="col-xl-10 mt-3 mb-3">
        <div class="p-4 bg-white shadow-sm rounded d-flex justify-content-between align-items-center">
            <div>
                <h4><i class="bi bi-grid me-2 text-primary"></i>Panel de Control UTD</h4>
                <small class="text-secondary">Resumen general de trazabilidad e inventario físico.</small>
            </div>
            <!-- Accesos rápidos para el técnico -->
            <div class="d-flex gap-2">
                <asp:LinkButton ID="btnNuevaRecepcion" CssClass="btn btn-outline-primary" runat="server">
                    <i class="bi bi-plus-circle"></i> Recibir Equipo
                </asp:LinkButton>
                <asp:LinkButton ID="btnNuevaCalibracion" CssClass="btn btn-primary" runat="server">
                    <i class="bi bi-file-earmark-check"></i> Nueva Calibración
                </asp:LinkButton>
            </div>
        </div>
    </div>
</div>

<div class="row justify-content-center">
    <div class="col-xl-10">
        <!-- Fila 1: Semáforo de Vencimientos (Requisito Principal) -->
        <div class="row g-3 mb-3">
            <div class="col-md-4">
                <div class="card bg-danger text-white shadow-sm rounded h-100 border-0">
                    <div class="card-body p-4">
                        <div class="d-flex justify-content-between">
                            <div>
                                <h6 class="text-uppercase fw-semibold mb-1">Vencidos</h6>
                                <h2 class="fw-bold mb-0"><asp:Label ID="lblRojo" runat="server" Text="12"></asp:Label></h2>
                            </div>
                            <i class="bi bi-exclamation-triangle fs-1 opacity-50"></i>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card bg-warning text-dark shadow-sm rounded h-100 border-0">
                    <div class="card-body p-4">
                        <div class="d-flex justify-content-between">
                            <div>
                                <h6 class="text-uppercase fw-semibold mb-1">Por Vencer (< 3 Meses)</h6>
                                <h2 class="fw-bold mb-0"><asp:Label ID="lblAmarillo" runat="server" Text="8"></asp:Label></h2>
                            </div>
                            <i class="bi bi-clock-history fs-1 opacity-50"></i>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card bg-success text-white shadow-sm rounded h-100 border-0">
                    <div class="card-body p-4">
                        <div class="d-flex justify-content-between">
                            <div>
                                <h6 class="text-uppercase fw-semibold mb-1">En Tiempo</h6>
                                <h2 class="fw-bold mb-0"><asp:Label ID="lblVerde" runat="server" Text="145"></asp:Label></h2>
                            </div>
                            <i class="bi bi-check2-circle fs-1 opacity-50"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Fila 2: Estados Operativos (Pedido por el Técnico en Entrevista 3) -->
        <div class="row g-3">
            <div class="col-md-4">
                <div class="card bg-white shadow-sm rounded p-3 h-100">
                    <div class="d-flex align-items-center">
                        <div class="bg-primary bg-opacity-10 text-primary rounded p-3 me-3">
                            <i class="bi bi-gear-wide-connected fs-4"></i>
                        </div>
                        <div>
                            <small class="text-secondary fw-semibold text-uppercase">En Proceso</small>
                            <h4 class="mb-0 fw-bold"><asp:Label ID="lblEnProceso" runat="server" Text="24"></asp:Label></h4>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card bg-white shadow-sm rounded p-3 h-100">
                    <div class="d-flex align-items-center">
                        <div class="bg-info bg-opacity-10 text-info rounded p-3 me-3">
                            <i class="bi bi-patch-check fs-4"></i>
                        </div>
                        <div>
                            <small class="text-secondary fw-semibold text-uppercase">Habilitados</small>
                            <h4 class="mb-0 fw-bold"><asp:Label ID="lblHabilitados" runat="server" Text="112"></asp:Label></h4>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card bg-white shadow-sm rounded p-3 h-100">
                    <div class="d-flex align-items-center">
                        <div class="bg-secondary bg-opacity-10 text-secondary rounded p-3 me-3">
                            <i class="bi bi-slash-circle fs-4"></i>
                        </div>
                        <div>
                            <small class="text-secondary fw-semibold text-uppercase">No Habilitados</small>
                            <h4 class="mb-0 fw-bold"><asp:Label ID="lblNoHabilitados" runat="server" Text="5"></asp:Label></h4>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>KD
</asp:Content>
