<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlanillaCalibracion.aspx.cs" Inherits="Sistema_UTD.Calibraciones.PlanillaCalibracion" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Barra superior de acciones -->
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h3 class="m-0"><i class="bi bi-clipboard-data text-primary"></i>Planilla de Calibración</h3>
        <div class="d-flex gap-2">
            <!-- Botones de Exportación PDF -->
            <asp:LinkButton ID="btnExportarHistorial" runat="server" CssClass="btn btn-outline-secondary shadow-sm">
            <i class="bi bi-file-earmark-pdf text-danger"></i> Historial (PDF)
            </asp:LinkButton>
            <asp:LinkButton ID="btnExportarPlanilla" runat="server" CssClass="btn btn-primary shadow-sm">
            <i class="bi bi-file-earmark-pdf"></i> Planilla y Puntos (PDF)
            </asp:LinkButton>
        </div>
    </div>

    <div class="row g-4">
        <!-- Columna Izquierda: Datos del Equipo -->
        <div class="col-lg-4">
            <div class="card shadow-sm border-0 rounded-4">
                <div class="card-header bg-light border-0 pt-4 pb-0">
                    <h6 class="text-uppercase fw-bold text-muted">Datos del Instrumento</h6>
                </div>
                <div class="card-body">
                    <!-- Controles Read-Only con form-control-plaintext -->
                    <label class="form-label text-secondary small">Marca / Modelo</label>
                    <asp:TextBox runat="server" CssClass="form-control-plaintext fw-bold" Text="Fluke 87V" ReadOnly="true"></asp:TextBox>
                    <hr class="text-muted opacity-25">
                    <label class="form-label text-secondary small">Condiciones Ambientales</label>
                    <div class="input-group mb-2">
                        <span class="input-group-text bg-white"><i class="bi bi-thermometer-half"></i></span>
                        <asp:TextBox runat="server" CssClass="form-control" Placeholder="22.5 °C"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <!-- Columna Derecha: Puntos de Calibración -->
        <div class="col-lg-8">
            <div class="card shadow-sm border-0 rounded-4 h-100">
                <div class="card-body p-0">
                    <div class="table-responsive">
                        <table class="table table-hover align-middle mb-0">
                            <thead class="table-light">
                                <tr>
                                    <th>Punto Nominal</th>
                                    <th>Valor Leído</th>
                                    <th>Error</th>
                                    <th>Estado</th>
                                </tr>
                            </thead>
                            <tbody>
                                <!-- Filas generadas dinámicamente -->
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
