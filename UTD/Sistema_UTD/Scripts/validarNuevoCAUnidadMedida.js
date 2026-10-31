'use strict';

// 1. Declaramos las variables globales
let txtUnidadMedida;

// Lógica para cambiar el texto del botón Aceptar (feedback)
document.addEventListener("DOMContentLoaded", function () {
    // Verificamos que el ScriptManager de ASP.NET esté cargado
    if (typeof Sys !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        // Se ejecuta apenas el usuario hace clic y la petición viaja al servidor
        prm.add_beginRequest(function (sender, args) {
            var btnAceptar = document.getElementById('btnAceptarNuevaUnidadMedida'); // Asegúrate de tener ClientIDMode="Static"
            if (btnAceptar) {
                btnAceptar.disabled = true;
                btnAceptar.value = "Guardando..."; // Feedback visual de espera
            }
        });

        // Se ejecuta cuando el servidor devuelve la respuesta (éxito o error)
        prm.add_endRequest(function (sender, args) {
            var btnAceptar = document.getElementById('btnAceptarNuevaUnidadMedida');
            if (btnAceptar) {
                btnAceptar.disabled = false;
                btnAceptar.value = "Aceptar";
            }
        });
    }
});

// 2. Creamos una función que "engancha" los eventos.
// Esta función se llamará al inicio y DESPUÉS de cada recarga del UpdatePanel.
function enlazarEventos() {
    // Volvemos a buscar los controles en la pantalla (porque pudieron ser recreados por C#)
    txtUnidadMedida = document.getElementById("txtUnidadMedida");

    // Valida si exite el campo
    if (txtUnidadMedida) {
        // Evento 'input': Valida mientras el usuario teclea
        txtUnidadMedida.addEventListener('input', () => validarCampo(txtUnidadMedida));

        // Evento 'blur': Valida cuando el usuario presiona TAB o hace clic fuera del campo
        txtUnidadMedida.addEventListener('blur', () => validarCampo(txtUnidadMedida));
    }
}

// 3. Si existe el ScriptManager de ASP.NET, usamos su método de carga
if (typeof Sys !== 'undefined') {
    Sys.Application.add_load(enlazarEventos);
} else {
    // Si no hay UpdatePanel, usamos el método tradicional de HTML
    document.addEventListener("DOMContentLoaded", enlazarEventos);
}

// 4. Función genérica que valida un solo campo
function validarCampo(campo) {
    // Si el campo no existe por alguna razón, no hacemos nada
    if (!campo) return false;

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

// 5. Función principal para el botón Guardar
function validarUnidadMedida() {
    // Forzamos la actualización de variables por seguridad antes de validar
    enlazarEventos();

    const modeloValido = validarCampo(txtUnidadMedida);

    // Si alguno es falso, retorna false (detiene el guardado)
    if (!modeloValido) {
        return false;
    }

    return true; // Todo ok, permite que viaje a C#
}