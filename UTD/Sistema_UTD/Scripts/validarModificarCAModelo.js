'use strict';

// 1. Declaración las variables globales
let txtModelo;

// Lógica para cambiar el texto del botón Aceptar (feedback)
document.addEventListener("DOMContentLoaded", function () {
    // Verificamos que el ScriptManager de ASP.NET esté cargado
    if (typeof Sys !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        // Se ejecuta apenas el usuario hace clic y la petición viaja al servidor
        prm.add_beginRequest(function (sender, args) {
            var btnAceptar = document.getElementById('btnAceptarModificarModelo'); // Asegúrate de tener ClientIDMode="Static"
            if (btnAceptar) {
                btnAceptar.disabled = true;
                btnAceptar.value = "Guardando..."; // Feedback visual de espera
            }
        });

        // Se ejecuta cuando el servidor devuelve la respuesta (éxito o error)
        prm.add_endRequest(function (sender, args) {
            var btnAceptar = document.getElementById('btnAceptar');
            if (btnAceptar) {
                btnAceptar.disabled = false;
                btnAceptar.value = "Aceptar";
            }
        });
    }
});

// 2. Función para enganchar eventos (preparada para el UpdatePanel)
function enlazarEventos() {
    txtModelo = document.getElementById("txtModelo");

    let campos = [txtModelo];

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
function cambiarCampoModificar(checkbox) {
    let camposGenerales = [txtModelo];
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
    if (campo.value.trim() === "") {
        campo.classList.remove("is-valid");
        campo.classList.add("is-invalid");
        return false;
    } else {
        campo.classList.remove("is-invalid");
        campo.classList.add("is-valid");
        return true;
    }
}

// 7. Función principal para el botón Aceptar
function validarModeloModificado(checkboxModelo) {
    let estaActivoModelo = checkboxModelo.checked;

    if (!estaActivoModelo) {
        return true; // Todo en orden, para desactivar al usuario
    } else {
        enlazarEventos(); // Forzamos actualización de variables

        const modeloValido = validarCampo(txtModelo);

        // Si ALGUNO es falso (y no estaba deshabilitado), bloqueamos el guardado
        if (!modeloValido) {
            return false;
        }

        return true; // Todo en orden
    }
}