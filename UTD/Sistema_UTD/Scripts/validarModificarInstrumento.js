'use strict';

// 1. Declaración las variables globales
let txtNumSerie, txtFechaRecepcion, ddlEstado, ddlDestino, ddlSeccion, ddlUsuarioCliente;

// Lógica para cambiar el texto del botón Aceptar (feedback)
document.addEventListener("DOMContentLoaded", function () {
    // Verifica que el ScriptManager de ASP.NET esté cargado
    if (typeof Sys !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        // Se ejecuta apenas el usuario hace clic y la petición viaja al servidor
        prm.add_beginRequest(function (sender, args) {
            var btnAceptar = document.getElementById('btnRegistrar'); // Asegúrate de tener ClientIDMode="Static"
            if (btnAceptar) {
                btnAceptar.disabled = true;
                btnAceptar.value = "Guardando..."; // Feedback visual de espera
            }
        });

        // Se ejecuta cuando el servidor devuelve la respuesta (éxito o error)
        prm.add_endRequest(function (sender, args) {
            var btnAceptar = document.getElementById('btnRegistrar');
            if (btnAceptar) {
                btnAceptar.disabled = false;
                btnAceptar.value = "Aceptar";
            }
        });
    }
});

// 2. Función para enganchar eventos (preparada para el UpdatePanel)
function enlazarEventos() {
    // ddlNomenclatura = document.getElementById("ddlNomenclatura");
    txtNumSerie = document.getElementById("txtNumSerie");
    txtFechaRecepcion = document.getElementById("txtFechaRecepcion");
    ddlEstado = document.getElementById("ddlEstado");
    ddlDestino = document.getElementById("ddlDestino");
    ddlSeccion = document.getElementById("ddlSeccion");
    ddlUsuarioCliente = document.getElementById("ddlUsuarioCliente");

    let campos = [txtNumSerie, txtFechaRecepcion, ddlEstado, ddlDestino, ddlSeccion, ddlUsuarioCliente];

    campos.forEach(campo => {
        if (campo) {
            campo.addEventListener('input', () => validarCampo(campo));
            campo.addEventListener('blur', () => validarCampo(campo));
        }
    });
}

// 3. Manejo del ciclo de vida de ASP.NET
if (typeof Sys !== 'undefined') {
    Sys.Application.add_load(enlazarEventos);
} else {
    document.addEventListener("DOMContentLoaded", enlazarEventos);
}

// 4. Función que maneja el switch general (Modelo Activo/Inactivo)
function cambiarCamposEstado(checkbox) {
    let camposGenerales = [txtNumSerie, txtFechaRecepcion, ddlEstado, ddlDestino, ddlSeccion, ddlUsuarioCliente];
    let estaActivo = checkbox.checked;

    // Habilitar o deshabilitar campos generales
    camposGenerales.forEach(campo => {
        if (campo) {
            campo.disabled = !estaActivo;
            // Si se acaba de deshabilitarlo, se le quita los colores de validación
            if (!estaActivo) {
                campo.classList.remove("is-valid", "is-invalid");
            }
        }
    });
}

// 6. Validación individual modificada
function validarCampo(campo) {
    if (!campo) return false;

    // Se valida normal si el campo está habilitado
    if (campo.value.trim() === "" || campo.value === "0") {
        campo.classList.remove("is-valid");
        campo.classList.add("is-invalid");
        return false;
    } else {
        campo.classList.remove("is-invalid");
        campo.classList.add("is-valid");
        return true;
    }
}

// 7. Función principal para el botón Agregar
function validarModificadoInstrumento() {
    enlazarEventos(); // Forzar actualización de variables

    // const nomenclaturaValido = validarCampo(ddlNomenclatura);
    const numSerieValido = validarCampo(txtNumSerie);
    const fechaRecepcionValido = validarCampo(txtFechaRecepcion);
    const estadoValido = validarCampo(ddlEstado);
    const destinoValido = validarCampo(ddlDestino);
    const seccionValido = validarCampo(ddlSeccion);
    const usuarioClienteValido = validarCampo(ddlUsuarioCliente);

    // Si ALGUNO es falso (y no estaba deshabilitado), bloqueamos el guardado
    if (!numSerieValido || !fechaRecepcionValido || !estadoValido || !destinoValido || !seccionValido || !usuarioClienteValido) {
        return false;
    }

    return true; // Todo en orden
}