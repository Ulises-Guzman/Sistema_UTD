'use strict';

// 1. Declaración las variables globales
let txtNumeroParte, txtNomenclatura, txtMarca, ddlModelo, ddlRango, ddlUnidadMedida;

// Lógica para cambiar el texto del botón Aceptar (feedback)
document.addEventListener("DOMContentLoaded", function () {
    // Verifica que el ScriptManager de ASP.NET esté cargado
    if (typeof Sys !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        // Se ejecuta apenas el usuario hace clic y la petición viaja al servidor
        prm.add_beginRequest(function (sender, args) {
            var btnAceptar = document.getElementById('btnAgregar'); // Asegúrate de tener ClientIDMode="Static"
            if (btnAceptar) {
                btnAceptar.disabled = true;
                btnAceptar.value = "Guardando..."; // Feedback visual de espera
            }
        });

        // Se ejecuta cuando el servidor devuelve la respuesta (éxito o error)
        prm.add_endRequest(function (sender, args) {
            var btnAceptar = document.getElementById('btnAgregar');
            if (btnAceptar) {
                btnAceptar.disabled = false;
                btnAceptar.value = "Aceptar";
            }
        });
    }
});

// 2. Función para enganchar eventos (preparada para el UpdatePanel)
function enlazarEventos() {
    txtNumeroParte = document.getElementById("txtNumeroParte");
    txtNomenclatura = document.getElementById("txtNomenclatura");
    txtMarca = document.getElementById("txtMarca");
    ddlModelo = document.getElementById("ddlModelo");
    ddlRango = document.getElementById("ddlRango");
    ddlUnidadMedida = document.getElementById("ddlUnidadMedida");

    let campos = [txtNumeroParte, txtNomenclatura, txtMarca, ddlModelo, ddlRango, ddlUnidadMedida];

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

// // 4. Función que maneja el switch general (Modelo Activo/Inactivo)
// function cambiarCamposEstado(checkbox) {
//     let camposGenerales = [txtNumeroParte, txtNomenclatura, txtMarca, ddlModelo, ddlRango, ddlUnidadMedida];
//     let estaActivo = checkbox.checked;

//     // Habilitar o deshabilitar campos generales
//     camposGenerales.forEach(campo => {
//         if (campo) {
//             campo.disabled = !estaActivo;
//             // Si se acaba de deshabilitarlo, se le quita los colores de validación
//             if (!estaActivo) {
//                 campo.classList.remove("is-valid", "is-invalid");
//             }
//         }
//     });
// }

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
function validarNuevoModeloInstrumento() {
    enlazarEventos(); // Forzar actualización de variables

    const numeroParteValido = validarCampo(txtNumeroParte);
    const nomenclaturaValido = validarCampo(txtNomenclatura);
    const marcaValido = validarCampo(txtMarca);
    const modeloValido = validarCampo(ddlModelo);
    const rangoValido = validarCampo(ddlRango);
    const unidadMedidaValido = validarCampo(ddlUnidadMedida);

    // Si ALGUNO es falso (y no estaba deshabilitado), bloqueamos el guardado
    if (!numeroParteValido || !nomenclaturaValido || !marcaValido || !modeloValido || !rangoValido || !unidadMedidaValido) {
        return false;
    }

    return true; // Todo en orden
}